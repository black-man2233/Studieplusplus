<template>
  <ion-page v-bind="$attrs">
    <ion-header>
      <ion-toolbar>
        <ion-title>Indstillinger</ion-title>
      </ion-toolbar>
    </ion-header>

    <ion-content :fullscreen="false">
      <SettingsModalComponent label="Profile" />
      <SettingsModalComponent label="Sound" />
      <SettingsModalComponent label="Theme" />
      <SettingsModalComponent label="Login" />
    
    </ion-content>
  </ion-page>
</template>

<style scoped>
ion-header {
  background-color: #f0f0f0;
}

.settings-header {
  font-size: 24px;
  font-weight: bold;
  margin: 16px;
  background-color: #f0f0f0;
}
</style>

<script setup lang="ts">
import {
  IonHeader,
  IonContent,
  IonToolbar,
  IonTitle,
  IonPage,
} from '@ionic/vue';
import { ref } from 'vue';

import SettingsModalComponent from '@/components/SettingsModalComponent.vue';


</script>
