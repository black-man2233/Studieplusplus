import { apiClient } from '@/services/apiClient';
import { getStoredLoginIdentifier } from '@/services/authStorage';
import { findStudentByLoginIdentifier, getStudents } from '@/services/studentsService';
import { getTeachers } from '@/services/teachersService';
import { getWeeklySchedule } from '@/services/weeklyScheduleService';

export type NormalizedChatMessage = {
  id: number;
  sender: 'me' | 'them';
  text: string;
  time: string;
};

export type NormalizedChat = {
  key: string;
  name: string;
  preview: string;
  time: string;
  unread: number;
  avatar: string;
  messages: NormalizedChatMessage[];
};

const DEFAULT_NAME = 'Samtale';
const DEFAULT_PREVIEW = 'Ingen beskeder endnu';

type InternalChatMessage = NormalizedChatMessage & { timestamp: number };

type ConversationPartner = {
  id: string;
  name: string;
};

function asRecord(value: unknown): Record<string, unknown> {
  return typeof value === 'object' && value !== null ? (value as Record<string, unknown>) : {};
}

function pickString(record: Record<string, unknown>, keys: string[]): string {
  for (const key of keys) {
    const value = record[key];
    if (typeof value === 'string' && value.trim().length > 0) {
      return value.trim();
    }
  }

  return '';
}

function pickNumber(record: Record<string, unknown>, keys: string[]): number {
  for (const key of keys) {
    const value = record[key];
    if (typeof value === 'number' && Number.isFinite(value)) {
      return value;
    }
    if (typeof value === 'string') {
      const parsed = Number(value);
      if (!Number.isNaN(parsed)) {
        return parsed;
      }
    }
  }

  return 0;
}

function pickBoolean(record: Record<string, unknown>, keys: string[]): boolean {
  for (const key of keys) {
    const value = record[key];
    if (typeof value === 'boolean') {
      return value;
    }
    if (typeof value === 'number') {
      return value !== 0;
    }
    if (typeof value === 'string') {
      const normalized = value.toLowerCase();
      if (normalized === 'true' || normalized === '1') {
        return true;
      }
      if (normalized === 'false' || normalized === '0') {
        return false;
      }
    }
  }

  return false;
}

function parseTimestamp(value: string): number {
  if (!value) {
    return Date.now();
  }

  const parsed = Date.parse(value);
  return Number.isNaN(parsed) ? Date.now() : parsed;
}

function formatDaTime(timestamp: number): string {
  return new Date(timestamp).toLocaleTimeString('da-DK', {
    hour: '2-digit',
    minute: '2-digit',
  });
}

function buildAvatar(name: string): string {
  const encoded = encodeURIComponent(name || DEFAULT_NAME);
  return `https://ui-avatars.com/api/?name=${encoded}&background=2f4ea8&color=ffffff&size=128`;
}

function unwrapPayload(payload: unknown): unknown[] {
  if (Array.isArray(payload)) {
    return payload;
  }

  const record = asRecord(payload);
  const value = record.value;
  return Array.isArray(value) ? value : [];
}

async function resolveCurrentStudentId(): Promise<string | null> {
  const loginIdentifier = getStoredLoginIdentifier();
  if (!loginIdentifier) {
    return null;
  }

  const students = await getStudents();
  const currentStudent = findStudentByLoginIdentifier(students, loginIdentifier);
  return currentStudent?.id ?? null;
}

async function resolveConversationPartners(currentStudentId: string): Promise<ConversationPartner[]> {
  const [teachers, weeklySchedule] = await Promise.all([
    getTeachers(),
    getWeeklySchedule(),
  ]);

  const teachersById = new Map(teachers.map((teacher) => [teacher.id, teacher]));
  const teacherIdsFromSchedule = new Set(
    weeklySchedule
      .filter((entry) => entry.studentId === currentStudentId)
      .map((entry) => entry.teacherId)
  );

  const partnerIds = teacherIdsFromSchedule.size > 0
    ? [...teacherIdsFromSchedule]
    : teachers.map((teacher) => teacher.id);

  return partnerIds
    .map((teacherId) => {
      const teacher = teachersById.get(teacherId);
      if (!teacher) {
        return null;
      }

      return {
        id: teacherId,
        name: `${teacher.firstName} ${teacher.lastName}`.trim() || DEFAULT_NAME,
      };
    })
    .filter((partner): partner is ConversationPartner => partner !== null);
}

async function getConversationMessages(userId1: string, userId2: string): Promise<unknown[]> {
  const payload = await apiClient.get<unknown>(`/api/Messages/conversation/${userId1}/${userId2}`);
  return unwrapPayload(payload);
}

export async function getNormalizedMessageChats(): Promise<NormalizedChat[]> {
  const currentStudentId = await resolveCurrentStudentId();
  if (!currentStudentId) {
    return [];
  }

  const partners = await resolveConversationPartners(currentStudentId);
  if (partners.length === 0) {
    return [];
  }

  const grouped = new Map<string, NormalizedChat>();
  let nextMessageId = 1;

  for (const partner of partners) {
    let rows: unknown[] = [];
    try {
      rows = await getConversationMessages(currentStudentId, partner.id);
    } catch {
      // Spring en fejlende samtale over, saa resten stadig kan vises.
      continue;
    }

    if (rows.length === 0) {
      continue;
    }

    for (const row of rows) {
      const record = asRecord(row);

      // Stoet flere backend-feltnavne og map til en stabil frontend-model.
      const conversationKey = pickString(record, [
        'conversationId',
        'threadId',
        'chatId',
        'groupId',
        'id',
      ]) || `${currentStudentId}-${partner.id}`;

      const name = pickString(record, [
        'conversationName',
        'threadName',
        'groupName',
        'senderName',
        'fromName',
        'name',
        'title',
        'teacherName',
      ]) || partner.name || DEFAULT_NAME;

      const text = pickString(record, [
        'message',
        'content',
        'body',
        'text',
        'preview',
      ]) || DEFAULT_PREVIEW;

      const timestampIso = pickString(record, [
        'sentAt',
        'createdAt',
        'updatedAt',
        'timestamp',
        'time',
        'date',
      ]);

      const timestamp = parseTimestamp(timestampIso);
      const time = formatDaTime(timestamp);

      const senderId = pickString(record, ['senderId', 'fromId', 'userId']);
      const receiverId = pickString(record, ['receiverId', 'toId']);
      const isMineFromIds = senderId === currentStudentId || receiverId === partner.id;
      const isMine = isMineFromIds || pickBoolean(record, ['isMine', 'fromMe', 'sentByMe']) || pickString(record, ['sender']) === 'me';
      const unreadCount = Math.max(0, pickNumber(record, ['unreadCount', 'unread']));
      const isRead = pickBoolean(record, ['isRead', 'read']);

      if (!grouped.has(conversationKey)) {
        grouped.set(conversationKey, {
          key: conversationKey,
          name,
          preview: text,
          time,
          unread: 0,
          avatar: buildAvatar(name),
          messages: [],
        });
      }

      const chat = grouped.get(conversationKey)!;

      (chat.messages as InternalChatMessage[]).push({
        id: nextMessageId,
        sender: isMine ? 'me' : 'them',
        text,
        time,
        timestamp,
      });

      if (!isMine && !isRead) {
        chat.unread += unreadCount > 0 ? unreadCount : 1;
      }

      nextMessageId += 1;
    }
  }

  const chats = [...grouped.values()].map((chat) => {
    const internalMessages = chat.messages as InternalChatMessage[];
    internalMessages.sort((a, b) => a.timestamp - b.timestamp);
    const latest = internalMessages[internalMessages.length - 1];

    if (latest) {
      chat.preview = latest.text;
      chat.time = latest.time;
    }

    return {
      ...chat,
      messages: internalMessages.map(({ timestamp: _timestamp, ...message }) => message),
    };
  });

  chats.sort((a, b) => {
    const aLatest = a.messages[a.messages.length - 1]?.id ?? 0;
    const bLatest = b.messages[b.messages.length - 1]?.id ?? 0;
    return bLatest - aLatest;
  });

  return chats;
}
