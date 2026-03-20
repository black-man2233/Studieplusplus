<template>
  <ion-page>
    <ion-content :fullscreen="true" class="messages-content">
      <ion-header collapse="condense">
        <ion-toolbar>
          <ion-title size="large">Beskeder</ion-title>
        </ion-toolbar>
      </ion-header>

      <div class="messages-shell">
        <section class="messages-hero">
          <p class="hero-kicker">Studie+Plus</p>
          <h1 class="hero-title">Beskeder</h1>
          <p class="hero-subtitle">Få hurtigt overblik over dine samtaler med klasse, lærere og gruppe.</p>

          <div class="hero-stats">
            <div class="stat-card">
              <span class="stat-label">Ulæste</span>
              <strong class="stat-value">{{ unreadCount }}</strong>
            </div>
            <div class="stat-card">
              <span class="stat-label">Samtaler</span>
              <strong class="stat-value">{{ chats.length }}</strong>
            </div>
            <div class="stat-card">
              <span class="stat-label">Aktive i dag</span>
              <strong class="stat-value">{{ activeToday }}</strong>
            </div>
          </div>
        </section>

        <section class="messages-panel">
          <div class="panel-head">
            <p class="panel-title">Seneste samtaler</p>
            <ion-button fill="clear" class="filter-btn">
              <ion-icon :icon="funnelOutline" />
              Filtrer
            </ion-button>
          </div>

          <p v-if="messagesLoading" class="panel-state">Henter beskeder...</p>
          <p v-else-if="messagesError" class="panel-state panel-state--error">{{ messagesError }}</p>

          <ion-list lines="none" class="message-list">
            <ion-item
              v-for="chat in chats"
              :key="chat.key"
              class="message-item"
              button
              :detail="true"
              @click="openChat(chat)"
            >
              <template #start>
                <ion-avatar class="message-avatar">
                  <ion-img :src="chat.avatar" :alt="chat.name" />
                </ion-avatar>
              </template>

              <ion-label>
                <div class="message-head">
                  <h3>{{ chat.name }}</h3>
                  <span>{{ chat.time }}</span>
                </div>
                <p>{{ chat.preview }}</p>
              </ion-label>

              <ion-badge v-if="chat.unread > 0" class="unread-badge">{{ chat.unread }}</ion-badge>
            </ion-item>
          </ion-list>

          <ion-modal :is-open="isChatOpen" @didDismiss="closeChat">
            <ion-header class="chat-modal-header">
              <ion-toolbar class="chat-toolbar">
                <div v-if="selectedChat" class="chat-header-main">
                  <ion-avatar class="chat-header-avatar">
                    <ion-img :src="selectedChat.avatar" :alt="selectedChat.name" />
                  </ion-avatar>
                  <div class="chat-header-copy">
                    <p class="chat-header-name">{{ selectedChat.name }}</p>
                    <p class="chat-header-status"><span class="status-dot"></span>Aktiv nu</p>
                  </div>
                </div>
                <ion-button fill="clear" class="chat-header-close" @click="closeChat" aria-label="Luk chat">
                  <ion-icon :icon="closeOutline" />
                </ion-button>
              </ion-toolbar>
            </ion-header>
            <ion-content class="chat-modal-content ion-padding">
              <div class="chat-thread">
                <div
                  v-for="message in activeMessages"
                  :key="message.id"
                  class="bubble-row"
                  :class="message.sender === 'me' ? 'is-me' : 'is-them'"
                >
                  <div class="bubble">{{ message.text }}</div>
                  <span class="bubble-time">{{ message.time }}</span>
                </div>
              </div>
            </ion-content>

            <ion-footer class="chat-footer">
              <ion-toolbar>
                <div class="chat-composer">
                  <input
                    v-model="messageDraft"
                    type="text"
                    class="chat-input"
                    placeholder="Skriv en besked..."
                    @keydown.enter.prevent="sendMessage"
                  />
                  <ion-button class="send-btn" @click="sendMessage">
                    <ion-icon :icon="sendOutline" />
                  </ion-button>
                </div>
              </ion-toolbar>
            </ion-footer>
          </ion-modal>
        </section>

        <section class="quick-actions">
          <ion-button expand="block" class="action-btn primary-action">
            <ion-icon :icon="createOutline" />
            Ny besked
          </ion-button>
          <ion-button expand="block" fill="outline" class="action-btn secondary-action">
            <ion-icon :icon="mailOpenOutline" />
            Marker alle som læst
          </ion-button>
        </section>
      </div>
    </ion-content>
  </ion-page>
</template>

<script setup lang="ts">
import {
  IonPage,
  IonHeader,
  IonToolbar,
  IonTitle,
  IonContent,
  IonList,
  IonItem,
  IonAvatar,
  IonImg,
  IonLabel,
  IonButton,
  IonIcon,
  IonBadge,
  IonModal,
  IonFooter,
  onIonViewDidEnter,
} from '@ionic/vue';
import { computed, ref } from 'vue';
import { closeOutline, createOutline, funnelOutline, mailOpenOutline, sendOutline } from 'ionicons/icons';
import { getNormalizedMessageChats } from '@/services/messagesService';

type ChatItem = {
  key: string;
  name: string;
  preview: string;
  time: string;
  unread: number;
  avatar: string;
};

type ChatMessage = {
  id: number;
  sender: 'me' | 'them';
  text: string;
  time: string;
};

const chats = ref<ChatItem[]>([
  {
    key: 'Projektgruppe H5',
    name: 'Projektgruppe H5',
    preview: 'Kan vi mødes 13:15 i lokale B-204 for at fordele opgaver?',
    time: '09:42',
    unread: 3,
    avatar: 'https://images.unsplash.com/photo-1522071820081-009f0129c71c?q=80&w=300&auto=format&fit=crop',
  },
  {
    key: 'Mette - Systemudvikling',
    name: 'Mette - Systemudvikling',
    preview: 'Husk at læse API-opgaven inden timen i morgen.',
    time: '08:17',
    unread: 1,
    avatar: 'https://images.unsplash.com/photo-1487412720507-e7ab37603c6f?q=80&w=300&auto=format&fit=crop',
  },
  {
    key: 'Studievejledning',
    name: 'Studievejledning',
    preview: 'Din samtale er booket onsdag kl. 10:00.',
    time: 'I går',
    unread: 0,
    avatar: 'https://images.unsplash.com/photo-1535713875002-d1d0cf377fde?q=80&w=300&auto=format&fit=crop',
  },
  {
    key: 'Signe - ERP Team',
    name: 'Signe - ERP Team',
    preview: 'Jeg har opdateret slides. Vil du tage intro-delen?',
    time: 'I går',
    unread: 2,
    avatar: 'https://images.unsplash.com/photo-1494790108377-be9c29b29330?q=80&w=300&auto=format&fit=crop',
  },
  {
    key: 'Anders - Praktik',
    name: 'Anders - Praktik',
    preview: 'Tak for status. Kan du sende commit-link inden kl. 15?',
    time: 'I går',
    unread: 1,
    avatar: 'https://images.unsplash.com/photo-1500648767791-00dcc994a43e?q=80&w=300&auto=format&fit=crop',
  },
  {
    key: 'Lene - Studieadmin',
    name: 'Lene - Studieadmin',
    preview: 'Vi mangler din underskrift på praktikaftalen.',
    time: 'Man',
    unread: 0,
    avatar: 'https://images.unsplash.com/photo-1580489944761-15a19d654956?q=80&w=300&auto=format&fit=crop',
  },
  {
    key: 'API Workshop',
    name: 'API Workshop',
    preview: 'Nye noter er lagt op i Teams under "Materialer".',
    time: 'Man',
    unread: 4,
    avatar: 'https://images.unsplash.com/photo-1519389950473-47ba0277781c?q=80&w=300&auto=format&fit=crop',
  },
  {
    key: 'Sofie - Design',
    name: 'Sofie - Design',
    preview: 'Jeg har opdateret komponentfarverne til staging.',
    time: 'Søn',
    unread: 0,
    avatar: 'https://images.unsplash.com/photo-1544005313-94ddf0286df2?q=80&w=300&auto=format&fit=crop',
  },
  {
    key: 'Mikkel - Mentor',
    name: 'Mikkel - Mentor',
    preview: 'Flot fremgang. Hold fokus på testdokumentation.',
    time: 'Søn',
    unread: 2,
    avatar: 'https://images.unsplash.com/photo-1541534401786-2077eed87a72?q=80&w=300&auto=format&fit=crop',
  },
  {
    key: 'Klasseforum H5',
    name: 'Klasseforum H5',
    preview: 'Nogen der vil bytte torsdagens fremlæggelses-slot?',
    time: 'Lør',
    unread: 5,
    avatar: 'https://images.unsplash.com/photo-1529156069898-49953e39b3ac?q=80&w=300&auto=format&fit=crop',
  },
]);

const chatThreads = ref<Record<string, ChatMessage[]>>({
  'Projektgruppe H5': [
    { id: 1, sender: 'them', text: 'Kan vi mødes 13:15 i lokale B-204 for at fordele opgaver?', time: '09:42' },
    { id: 2, sender: 'me', text: 'Ja, det passer. Jeg tager laptop med.', time: '09:45' },
  ],
  'Mette - Systemudvikling': [
    { id: 3, sender: 'them', text: 'Husk at læse API-opgaven inden timen i morgen.', time: '08:17' },
    { id: 4, sender: 'me', text: 'Noteret, jeg gennemgår den i eftermiddag.', time: '08:19' },
  ],
  'Signe - ERP Team': [
    { id: 5, sender: 'them', text: 'Jeg har opdateret slides. Vil du tage intro-delen?', time: 'I går' },
    { id: 6, sender: 'me', text: 'Ja, jeg tager intro + demo flow.', time: 'I går' },
  ],
});

const fallbackChats = JSON.parse(JSON.stringify(chats.value)) as ChatItem[];
const fallbackThreads = JSON.parse(JSON.stringify(chatThreads.value)) as Record<string, ChatMessage[]>;

let messageIdCounter = 100;
const nextMessageId = () => {
  messageIdCounter += 1;
  return messageIdCounter;
};

const selectedChat = ref<ChatItem | null>(null);
const isChatOpen = ref(false);
const activeMessages = ref<ChatMessage[]>([]);
const messageDraft = ref('');
const messagesLoading = ref(false);
const messagesError = ref('');

const formatNow = () =>
  new Date().toLocaleTimeString('da-DK', {
    hour: '2-digit',
    minute: '2-digit',
  });

const fallbackThread = (chat: ChatItem): ChatMessage[] => [
  { id: nextMessageId(), sender: 'them', text: chat.preview, time: chat.time },
];

const persistActiveThread = () => {
  if (!selectedChat.value) return;
  chatThreads.value[selectedChat.value.key] = [...activeMessages.value];
};

const openChat = (chat: ChatItem) => {
  selectedChat.value = chat;
  activeMessages.value = [...(chatThreads.value[chat.key] ?? fallbackThread(chat))];
  chat.unread = 0;
  messageDraft.value = '';
  isChatOpen.value = true;
};

const closeChat = () => {
  persistActiveThread();
  isChatOpen.value = false;
  selectedChat.value = null;
};

const sendMessage = () => {
  const text = messageDraft.value.trim();
  if (!text || !selectedChat.value) return;

  const now = formatNow();
  activeMessages.value.push({
    id: nextMessageId(),
    sender: 'me',
    text,
    time: now,
  });

  selectedChat.value.preview = text;
  selectedChat.value.time = now;
  messageDraft.value = '';
  persistActiveThread();

  const openChatKey = selectedChat.value.key;
  window.setTimeout(() => {
    if (!selectedChat.value || selectedChat.value.key !== openChatKey) return;

    activeMessages.value.push({
      id: nextMessageId(),
      sender: 'them',
      text: 'Perfekt, tak. Vi skriver videre her.',
      time: formatNow(),
    });
    persistActiveThread();
  }, 650);
};

const applyFallbackData = () => {
  chats.value = JSON.parse(JSON.stringify(fallbackChats)) as ChatItem[];
  chatThreads.value = JSON.parse(JSON.stringify(fallbackThreads)) as Record<string, ChatMessage[]>;
};

const loadMessagesFromApi = async () => {
  messagesLoading.value = true;
  messagesError.value = '';

  try {
    const normalizedChats = await getNormalizedMessageChats();

    if (normalizedChats.length === 0) {
      applyFallbackData();
      messagesError.value = '';
      return;
    }

    chats.value = normalizedChats.map((chat) => ({
      key: chat.key,
      name: chat.name,
      preview: chat.preview,
      time: chat.time,
      unread: chat.unread,
      avatar: chat.avatar,
    }));

    chatThreads.value = Object.fromEntries(
      normalizedChats.map((chat) => [chat.key, chat.messages])
    );
  } catch (error) {
    console.error('Failed to load messages from API', error);
    applyFallbackData();
    messagesError.value = '';
  } finally {
    messagesLoading.value = false;
  }
};

onIonViewDidEnter(() => {
  void loadMessagesFromApi();
});

const unreadCount = computed(() => chats.value.reduce((sum, chat) => sum + chat.unread, 0));
const activeToday = computed(() => chats.value.filter((chat) => chat.time !== 'I går').length);
</script>

<style scoped>
@import "../styles/views/message-page.css";
</style>
