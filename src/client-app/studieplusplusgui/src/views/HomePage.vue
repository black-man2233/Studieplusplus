<template>
  <ion-page>
    <ion-header>

    </ion-header>

    <ion-content :fullscreen="true">
      <ion-header collapse="condense">
        <ion-toolbar>
          <ion-title size="large">Hjem</ion-title>
        </ion-toolbar>
      </ion-header>

      <div class="slider-wrap">
        <Swiper :modules="[Pagination]" :slides-per-view="'auto'" :space-between="12" :grab-cursor="true" :pagination="{ clickable: true }">
          <SwiperSlide v-for="card in cards" :key="card" class="card-slide">
            <div class="card-box">
              <ion-label>Card #{{ card }}</ion-label>
              <ion-img
                class="card-img"
                src="https://fastly.picsum.photos/id/866/1920/1080.jpg?hmac=dNBuPEp10RySTqlc5EpGw7QyrFIpBd2X88r1Ixla7pw"
                alt="placeholder"
              />
            </div>
          </SwiperSlide>
        </Swiper>
      </div>

      <ion-grid :fixed="true">
        <ion-row>
          <ion-col>1</ion-col>
          <ion-col>2</ion-col>
        </ion-row>
        <ion-row>
          <ion-col>3</ion-col>
          <ion-col>4</ion-col>
        </ion-row>
      </ion-grid>

      <ion-grid :fixed="true">
      </ion-grid>
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
  IonCol,
  IonGrid,
  IonRow,
  IonLabel,
  IonImg,
} from "@ionic/vue";

import { ref } from "vue";
import { Swiper, SwiperSlide } from "swiper/vue";
import { Pagination } from "swiper/modules";

import "swiper/css";
import "swiper/css/pagination";

const cards = ref([0, 1, 2, 3, 4, 5, 6]);
</script>

<style scoped>
ion-grid {
  padding: 10%;
  margin-top: 10%;
}
ion-col {
  border: 1px solid #ccc;
  border-radius: 10px;
  padding: 12%;
  margin: 2%;
  text-align: center;
}

.slider-wrap {
  padding: 0 10%;
  margin-top: 10%;
}

.card-slide {
  width: 60vw;
  max-width: 300px;
}

.card-box {
  height: 200px;
  border: 2px solid #f8f8f8;
  border-radius: 10px;
  /*padding: 10px;*/
  box-sizing: border-box;
  display: flex;
  flex-direction: column;
  gap: 8px;
  background: #020024;
  background: linear-gradient(90deg,rgba(2, 0, 36, 1) 0%, rgba(9, 9, 121, 1) 35%, rgba(0, 212, 255, 1) 100%);
}

.card-img {
  pointer-events: none;
  height: 100%;
  width: 100%;
  object-fit: contain;
}
</style>
