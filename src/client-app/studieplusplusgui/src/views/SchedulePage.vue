<template>
  <ion-page>
    <ion-content :fullscreen="true" class="schedule-content">
      <div class="schedule-shell">
        <section class="calendar-panel">
          <vue-cal
            :key="calendarModeKey"
            ref="calendarRef"
            class="schedule-cal"
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
import { computed, nextTick, onBeforeUnmount, onMounted, ref } from 'vue';
import { VueCal } from 'vue-cal'
import 'vue-cal/style'

const timeFrom = 7 * 60;
const timeTo = 18 * 60;
const timeStep = 60;
const desktopBreakpoint = 992;
type CalendarView = 'day' | 'week';

const calendarRef = ref<any>(null);
const timeCellHeight = ref(40);
const isDesktop = ref(false);

const calendarView = computed<CalendarView>(() => (isDesktop.value ? 'week' : 'day'));
const calendarViews = computed<CalendarView[]>(() => [calendarView.value]);
const calendarModeKey = computed(() => `schedule-${calendarView.value}`);

const getIsDesktop = () => window.matchMedia(`(min-width: ${desktopBreakpoint}px)`).matches;

const recalculateTimeCellHeight = async () => {
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
  void recalculateTimeCellHeight();
  window.setTimeout(() => {
    void recalculateTimeCellHeight();
  }, 100);
  window.addEventListener('resize', handleResize);
});

onIonViewDidEnter(() => {
  isDesktop.value = getIsDesktop();
  void recalculateTimeCellHeight();
  window.requestAnimationFrame(() => {
    void recalculateTimeCellHeight();
  });
});

onBeforeUnmount(() => {
  window.removeEventListener('resize', handleResize);
});
</script>
