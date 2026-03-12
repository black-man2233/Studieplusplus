<template>
  <ion-page>
    <ion-header>
    </ion-header>
    <ion-content :fullscreen="true">
      <vue-cal :today-button="false" :views-bar="false" time-at-cursor hide-weekends>

      </vue-cal>


    </ion-content>
  </ion-page>
</template>

<style scoped>
.vuecal {
  --vuecal-primary-color:
    #316191;
  --vuecal-secondary-color:
    #2e2e2e;
  --vuecal-base-color:
    #ffffff;
  --vuecal-contrast-color:
    #000000;
  --vuecal-border-color:
    color-mix(in srgb, var(--vuecal-base-color) 8%, transparent);
  --vuecal-header-color:
    var(--vuecal-base-color);
  --vuecal-event-color:
    var(--vuecal-base-color);
  --vuecal-event-border-color:
    currentColor;
  --vuecal-border-radius:
    6 px;
  --vuecal-height:
    500 px;
  --vuecal-min-schedule-size:
    0 px;
  --vuecal-min-cell-size:
    0 px;
  --vuecal-transition-duration:
    0, 25 s;
}
</style>

<script setup lang="ts">
import { IonPage, IonHeader, IonToolbar, IonTitle, IonContent } from '@ionic/vue';
import ExploreContainer from '@/components/ExploreContainer.vue';
import { VueCal } from 'vue-cal'
import 'vue-cal/style'
</script>
