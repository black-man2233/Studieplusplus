<template>
  <ion-card>
    <ion-card-header>
      <ion-card-title>{{ title }}</ion-card-title>
    </ion-card-header>
    <img v-if="thumbnail" :src="thumbnail" class="card-thumbnail" alt="thumbnail" />
    <ion-card-content>
      <ion-list>
        <ion-item v-for="(item, index) in items" :key="index" :lines="index === items.length - 1 ? 'none' : 'inset'">
          <ion-label>{{ item }}</ion-label>
        </ion-item>
      </ion-list>
    </ion-card-content>
  </ion-card>
</template>

<script setup lang="ts">
import { IonCard, IonCardHeader, IonCardTitle, IonCardContent, IonList, IonItem, IonLabel } from "@ionic/vue";

defineProps({
  title: {
    type: String,
    required: true,
  },
  items: {
    type: Array as () => string[],
    required: true,
  },
  thumbnail: {
    type: String,
    required: false,
  },
});
</script>

<style scoped>
  ion-card{
    border: 1px solid #f8f8f8;
    width: 100%;
    height: auto;
  }

  /* keep default card header layout */

  .card-thumbnail {
    width: 100%;
    max-height: 140px;
    object-fit: cover;
    border-radius: 6px;
    margin-top: 8px;
    margin-bottom: 8px;
    pointer-events: none;
  }
</style>
