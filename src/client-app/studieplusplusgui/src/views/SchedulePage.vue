<template>
  <ion-page>
    <ion-content :fullscreen="true" class="schedule-content">
      <div class="schedule-shell">
        <section class="calendar-panel">
          <vue-cal
            ref="calendarRef"
            class="schedule-cal"
            :views="['day']"
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
@import url('https://fonts.googleapis.com/css2?family=Space+Grotesk:wght@400;500;600;700&family=Plus+Jakarta+Sans:wght@400;500;600;700&display=swap');

.schedule-content {
  --background: transparent;
  --offset-bottom: 0px !important;
  --padding-bottom: 0px;
  --overflow: hidden;
  overflow: hidden;
}

.schedule-shell {
  max-width: 900px;
  height: 100%;
  margin: 0 auto;
  padding: 4px 10px 6px;
  box-sizing: border-box;
  display: flex;
  flex-direction: column;
  gap: 0;
}

.calendar-panel {
  flex: 1 1 auto;
  min-height: 0;
  border-radius: 20px;
  border: 1px solid rgba(144, 168, 200, 0.24);
  background: linear-gradient(180deg, rgba(17, 27, 43, 0.56) 0%, rgba(22, 34, 53, 0.5) 100%);
  box-shadow: 0 12px 24px rgba(9, 16, 28, 0.18);
  padding: 0;
  overflow: hidden;
}

.schedule-cal {
  --sc-navy: #112645;
  --sc-navy-soft: #1b365f;
  --sc-ink: #1f3556;
  --sc-mist: #dbe6fb;
  --sc-line: #8ca8cf;
  --sc-surface: #d5e0f3;
  --sc-cell: #ecf2ff;

  height: 100%;
  min-height: 100%;
  --vuecal-primary-color: var(--sc-navy-soft);
  --vuecal-secondary-color: var(--sc-surface);
  --vuecal-base-color: var(--sc-ink);
  --vuecal-contrast-color: var(--sc-mist);
  --vuecal-border-color: color-mix(in srgb, var(--sc-line) 58%, transparent);
  --vuecal-header-color: var(--sc-mist);
  --vuecal-event-color: var(--sc-mist);
  --vuecal-event-border-color: color-mix(in srgb, var(--sc-navy) 45%, transparent);
  --vuecal-border-radius: 16px;
  --vuecal-min-schedule-size: 0px;
  --vuecal-min-cell-size: 0px;
  --vuecal-transition-duration: 0.25s;

  border: 0;
  border-radius: 20px;
  box-shadow: none;
  overflow: hidden;
  background: linear-gradient(180deg, #dfe8fa 0%, #d0dcf1 100%);
  backdrop-filter: saturate(112%);
  font-family: "Plus Jakarta Sans", "Avenir Next", "Helvetica Neue", sans-serif;
}

/* Keep the rounded bottom corners while visually letting the grid continue behind the bar. */
:deep(.schedule-cal .vuecal__header) {
  background: transparent !important;
  border-top-left-radius: 20px;
  border-top-right-radius: 20px;
  overflow: hidden;
  position: relative;
  z-index: 8;
}

:deep(.schedule-cal .vuecal__title-bar) {
  position: relative;
  z-index: 9;
  margin-top: 0;
  margin-bottom: -10px;
  padding-top: 0;
  padding-bottom: 12px;
  background: linear-gradient(135deg, #12294a 0%, #1e4172 100%) !important;
  border-bottom-left-radius: 14px;
  border-bottom-right-radius: 14px;
  overflow: hidden;
  box-shadow: inset 0 1px 0 rgba(30, 65, 114, 0.45);
}

:deep(.schedule-cal .vuecal__time-column) {
  z-index: 2 !important;
}

:deep(.schedule-cal .vuecal__title),
:deep(.schedule-cal .vuecal__nav--today) {
  letter-spacing: 0.01em;
  font-weight: 600;
}

:deep(.schedule-cal .vuecal__title),
:deep(.schedule-cal .vuecal__nav),
:deep(.schedule-cal .vuecal__nav--today),
:deep(.schedule-cal button.vuecal__title) {
  color: var(--sc-mist) !important;
}

:deep(.schedule-cal .vuecal__nav),
:deep(.schedule-cal .vuecal__view-button),
:deep(.schedule-cal .vuecal__nav--today) {
  border-radius: 999px;
}

:deep(.schedule-cal .vuecal__nav--today) {
  text-transform: uppercase;
  font-size: 0.75rem;
  font-weight: 700;
}

:deep(.schedule-cal .vuecal__weekdays-headings),
:deep(.schedule-cal .vuecal__weekday),
:deep(.schedule-cal .vuecal__time-column),
:deep(.schedule-cal .vuecal__all-day-label) {
  background-color: var(--sc-surface) !important;
  color: var(--sc-ink) !important;
  border-color: color-mix(in srgb, var(--sc-line) 78%, transparent) !important;
}

:deep(.schedule-cal .vuecal__time-cell label),
:deep(.schedule-cal .vuecal__time-cell-label) {
  color: #244062 !important;
  font-weight: 500;
}

:deep(.schedule-cal .vuecal__cell) {
  background-color: var(--sc-cell);
}

:deep(.schedule-cal .vuecal__event) {
  border-radius: 8px;
  box-shadow: 0 3px 8px rgba(16, 40, 71, 0.12);
}

:deep(.schedule-cal .vuecal__body),
:deep(.schedule-cal .vuecal__scrollable),
:deep(.schedule-cal .vuecal__body-wrap) {
  height: 100% !important;
  min-height: 0 !important;
  overflow: hidden !important;
}

@media (max-width: 640px) {
  .schedule-shell {
    padding: 2px 8px 8px;
  }

  .calendar-panel {
    padding: 0;
  }
}
</style>

<script setup lang="ts">
import { IonPage, IonContent } from '@ionic/vue';
import { nextTick, onBeforeUnmount, onMounted, ref } from 'vue';
import { VueCal } from 'vue-cal'
import 'vue-cal/style'

const timeFrom = 7 * 60;
const timeTo = 18 * 60;
const timeStep = 60;

const calendarRef = ref<any>(null);
const timeCellHeight = ref(40);

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

onMounted(() => {
  void recalculateTimeCellHeight();
  window.addEventListener('resize', recalculateTimeCellHeight);
});

onBeforeUnmount(() => {
  window.removeEventListener('resize', recalculateTimeCellHeight);
});
</script>
