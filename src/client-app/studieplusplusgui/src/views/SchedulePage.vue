<template>
  <ion-page>
    <ion-content :fullscreen="true" class="schedule-content">
      <div class="schedule-shell">
        <section class="calendar-panel">
          <div v-if="isLoading" class="schedule-state">Loading schedule...</div>
          <div v-else-if="errorMessage" class="schedule-state schedule-state--error">{{ errorMessage }}</div>

          <vue-cal
            v-if="!useFallbackView"
            :key="calendarModeKey"
            ref="calendarRef"
            class="schedule-cal"
            :events="calendarEvents"
            :views="calendarViews"
            :active-view="calendarView"
            :start-week-on-sunday="false"
            :time-from="timeFrom"
            :time-to="timeTo"
            :time-step="timeStep"
            :time-cell-height="timeCellHeight"
            :style="{ '--vuecal-height': '100%' }"
            :today-button="true"
            :views-bar="false"
            time-at-cursor
            hide-weekends
          >
          </vue-cal>

          <div v-else class="schedule-fallback">
            <h2 class="schedule-fallback-title">Weekly Schedule</h2>
            <p v-if="calendarEvents.length === 0 && !isLoading" class="schedule-fallback-empty">No events this week.</p>

            <div v-else class="schedule-days">
              <article v-for="day in weekDays" :key="day.value" class="schedule-day-card">
                <h3 class="schedule-day-title">{{ day.label }}</h3>
                <ul v-if="eventsByDay[day.value]?.length" class="schedule-fallback-list">
                  <li
                    v-for="event in eventsByDay[day.value]"
                    :key="`${event.start.toISOString()}-${event.title}`"
                    class="schedule-fallback-item"
                  >
                    <span class="schedule-fallback-time">{{ formatEventTime(event.start, event.end) }}</span>
                    <span class="schedule-fallback-text">{{ event.title }} · {{ event.content }}</span>
                  </li>
                </ul>
                <p v-else class="schedule-day-empty">No events</p>
              </article>
            </div>
          </div>
        </section>
      </div>
    </ion-content>
  </ion-page>
</template>

<style scoped>
@import "../styles/views/schedule-page.css";
</style>

<script setup lang="ts">
import { IonPage, IonContent, onIonViewDidEnter } from '@ionic/vue';
import { computed, nextTick, onBeforeUnmount, onErrorCaptured, onMounted, ref } from 'vue';
import { VueCal } from 'vue-cal'
import 'vue-cal/style'
import type { EnrichedScheduleEntry } from '@/services/enrichedScheduleService';
import { getEnrichedWeeklySchedule } from '@/services/enrichedScheduleService';

const timeFrom = 7 * 60;
const timeTo = 18 * 60;
const timeStep = 60;
const desktopBreakpoint = 992;
type CalendarView = 'day' | 'week';

const isLoading = ref(true);
const errorMessage = ref('');
const useFallbackView = ref(false);
const calendarRef = ref<any>(null);
const timeCellHeight = ref(40);
const isDesktop = ref(false);

type CalendarEvent = {
  start: Date;
  end: Date;
  title: string;
  content: string;
  class: string;
};

const calendarEvents = ref<CalendarEvent[]>([]);

const calendarView = computed<CalendarView>(() => (isDesktop.value ? 'week' : 'day'));
const calendarViews = computed<CalendarView[]>(() => [calendarView.value]);
const calendarModeKey = computed(() => `schedule-${calendarView.value}`);

const getIsDesktop = () => window.matchMedia(`(min-width: ${desktopBreakpoint}px)`).matches;

const weekDays = [
  { value: 1, label: 'Monday' },
  { value: 2, label: 'Tuesday' },
  { value: 3, label: 'Wednesday' },
  { value: 4, label: 'Thursday' },
  { value: 5, label: 'Friday' },
] as const;

const eventsByDay = computed<Record<number, CalendarEvent[]>>(() => {
  const dayMap: Record<number, CalendarEvent[]> = { 1: [], 2: [], 3: [], 4: [], 5: [] };

  for (const event of calendarEvents.value) {
    const jsDay = event.start.getDay();
    const weekday = jsDay === 0 ? 7 : jsDay;
    if (dayMap[weekday]) {
      dayMap[weekday].push(event);
    }
  }

  for (const day of Object.keys(dayMap)) {
    dayMap[Number(day)].sort((a, b) => a.start.getTime() - b.start.getTime());
  }

  return dayMap;
});

const getMondayOfCurrentWeek = (): Date => {
  const now = new Date();
  const monday = new Date(now);
  const currentDay = monday.getDay();
  const deltaToMonday = currentDay === 0 ? -6 : 1 - currentDay;
  monday.setDate(monday.getDate() + deltaToMonday);
  monday.setHours(0, 0, 0, 0);
  return monday;
};

const buildDateFromTemplate = (weekStart: Date, targetDayOfWeek: number, templateIso: string): Date => {
  const templateDate = new Date(templateIso);
  const eventDate = new Date(weekStart);
  // Begræns dag-værdien, så visning ikke bryder ved ugyldige backend-data.
  const normalizedDay = Math.min(7, Math.max(1, targetDayOfWeek));
  eventDate.setDate(eventDate.getDate() + (normalizedDay - 1));
  eventDate.setHours(templateDate.getHours(), templateDate.getMinutes(), 0, 0);
  return eventDate;
};

const mapToCalendarEvents = (entries: EnrichedScheduleEntry[]): CalendarEvent[] => {
  const weekStart = getMondayOfCurrentWeek();
  const events: CalendarEvent[] = [];

  for (const entry of entries) {
    const start = buildDateFromTemplate(weekStart, entry.dayOfTheWeek, entry.startTime);
    const end = buildDateFromTemplate(weekStart, entry.dayOfTheWeek, entry.endTime);

    if (Number.isNaN(start.getTime()) || Number.isNaN(end.getTime()) || end <= start) {
      continue;
    }

    events.push({
      start,
      end,
      title: entry.lessonName,
      content: entry.teacherName,
      class: 'schedule-event',
    });
  }

  return events;
};

const formatEventTime = (start: Date, end: Date): string => {
  const timeFormatter = new Intl.DateTimeFormat('da-DK', { hour: '2-digit', minute: '2-digit' });
  const dayFormatter = new Intl.DateTimeFormat('da-DK', { weekday: 'short' });
  return `${dayFormatter.format(start)} ${timeFormatter.format(start)}-${timeFormatter.format(end)}`;
};

const loadWeeklyEvents = async () => {
  isLoading.value = true;
  errorMessage.value = '';

  try {
    const scheduleEntries = await getEnrichedWeeklySchedule();
    calendarEvents.value = mapToCalendarEvents(scheduleEntries);
  } catch (error) {
    console.error('Failed to load weekly schedule', error);
    calendarEvents.value = [];
    errorMessage.value = 'Could not load schedule right now.';
  } finally {
    isLoading.value = false;
    await recalculateTimeCellHeight();
  }
};

const recalculateTimeCellHeight = async () => {
  if (useFallbackView.value) {
    return;
  }

  await nextTick();

  const calendarEl = calendarRef.value?.$el as HTMLElement | undefined;
  const scrollable = calendarEl?.querySelector('.vuecal__scrollable') as HTMLElement | null;
  const renderedCells = calendarEl?.querySelectorAll('.vuecal__time-column .vuecal__time-cell').length ?? 0;
  const fallbackSlotCount = Math.max(1, Math.ceil((timeTo - timeFrom) / timeStep));
  const slotCount = renderedCells > 0 ? renderedCells : fallbackSlotCount;

  if (!scrollable || slotCount <= 0) {
    return;
  }

  const availableHeight = scrollable.clientHeight;
  if (availableHeight > 0) {
    timeCellHeight.value = availableHeight / slotCount;
  }
};

const handleResize = () => {
  isDesktop.value = getIsDesktop();
  void recalculateTimeCellHeight();
};

onMounted(() => {
  isDesktop.value = getIsDesktop();
  void loadWeeklyEvents();
  void recalculateTimeCellHeight();
  window.setTimeout(() => {
    void recalculateTimeCellHeight();
  }, 100);
  window.addEventListener('resize', handleResize);
});

onIonViewDidEnter(() => {
  isDesktop.value = getIsDesktop();
  void loadWeeklyEvents();
  void recalculateTimeCellHeight();
  window.requestAnimationFrame(() => {
    void recalculateTimeCellHeight();
  });
});

onBeforeUnmount(() => {
  window.removeEventListener('resize', handleResize);
});

onErrorCaptured((error) => {
  // Hvis kalender-komponenten fejler, vis fallback-kort i stedet.
  console.error('Schedule page render error, switching to fallback view', error);
  useFallbackView.value = true;
  return false;
});
</script>
