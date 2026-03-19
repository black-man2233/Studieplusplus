import { getTeachers } from '@/services/teachersService';
import type { Teacher } from '@/services/teachersService';
import { getWeeklySchedule } from '@/services/weeklyScheduleService';
import type { WeeklyScheduleEntry } from '@/services/weeklyScheduleService';

export interface EnrichedScheduleEntry extends WeeklyScheduleEntry {
  teacherName: string;
  lessonName: string;
}

// Stabil hash giver samme lektionsnavn for samme entry uden ekstra backend-felt.
function hashToIndex(seed: string, modulo: number): number {
  if (modulo <= 0) {
    return 0;
  }

  let hash = 0;
  for (let i = 0; i < seed.length; i += 1) {
    hash = (hash * 31 + seed.charCodeAt(i)) >>> 0;
  }

  return hash % modulo;
}

function getTeacherName(teacher?: Teacher): string {
  if (!teacher) {
    return 'Ukendt underviser';
  }

  return `${teacher.firstName} ${teacher.lastName}`.trim();
}

function getLessonName(entry: WeeklyScheduleEntry, teacher?: Teacher): string {
  const specializations = teacher?.specializations ?? [];
  if (specializations.length === 0) {
    return 'Lektion';
  }

  const lessonIndex = hashToIndex(entry.id, specializations.length);
  return specializations[lessonIndex] || specializations[0] || 'Lektion';
}

export async function getEnrichedWeeklySchedule(): Promise<EnrichedScheduleEntry[]> {
  // Hent begge datakilder samtidig, og fortsæt selv hvis laerer-opslag fejler.
  const [weeklySchedule, teachers] = await Promise.all([
    getWeeklySchedule(),
    getTeachers().catch(() => [] as Teacher[]),
  ]);

  const teacherById = new Map(teachers.map((teacher) => [teacher.id, teacher]));

  return weeklySchedule.map((entry) => {
    const teacher = teacherById.get(entry.teacherId);

    return {
      ...entry,
      teacherName: getTeacherName(teacher),
      lessonName: getLessonName(entry, teacher),
    };
  });
}
