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
ion-card {
  border: 1px solid rgba(35, 47, 72, 0.4);
  border-radius: 18px;
  background: linear-gradient(180deg, rgba(104, 117, 145, 0.95) 0%, rgba(88, 102, 129, 0.96) 100%);
  box-shadow: 0 10px 20px rgba(18, 26, 42, 0.22);
  width: 100%;
  height: auto;
  overflow: hidden;
}

ion-card-header {
  padding: 14px 14px 10px;
  border-bottom: 1px solid rgba(226, 235, 255, 0.14);
  background: rgba(62, 76, 103, 0.78);
}

ion-card-title {
  font-size: 1rem;
  font-weight: 700;
  color: #f5f8ff;
  letter-spacing: 0.01em;
}

ion-card-content {
  padding-top: 8px;
}

ion-list {
  background: transparent;
}

ion-item {
  --background: transparent;
  --color: #e6edff;
  --inner-padding-end: 8px;
  --min-height: 38px;
  font-size: 0.92rem;
  font-weight: 500;
}

ion-item ion-label {
  color: #e6edff;
}

.card-thumbnail {
  width: 100%;
  max-height: 140px;
  object-fit: cover;
  border-radius: 10px;
  margin-top: 8px;
  margin-bottom: 8px;
  pointer-events: none;
}
</style>
