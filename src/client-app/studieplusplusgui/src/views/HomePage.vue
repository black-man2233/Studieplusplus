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
        <Swiper :modules="[Pagination]" :slides-per-view="'auto'" :space-between="10" :grab-cursor="true"
          :pagination="{ clickable: true }">
          <SwiperSlide v-for="card in cards" :key="card" class="card-slide">
            <div class="card-box">
              <ion-label>{{ card }}</ion-label>
              <ion-img class="card-img"
                src="https://fastly.picsum.photos/id/866/1920/1080.jpg?hmac=dNBuPEp10RySTqlc5EpGw7QyrFIpBd2X88r1Ixla7pw"
                alt="placeholder" />
            </div>
          </SwiperSlide>
        </Swiper>
      </div>

      <ion-grid>
        <ion-row class="equal-row">
          <ion-col size="12" size-md="6">
            <HomePageCards title="Lektier" :items="['ERP', 'Kevins pics', 'Send mathias nu-', 'idk']" />
          </ion-col>
          <ion-col size="12" size-md="6">
            <HomePageCards title="Dagens Skema" :items="['Projekt Styring', 'Hvad end lars lære']" />
          </ion-col>
        </ion-row>
        <ion-row class="equal-row">
          <ion-col size="12">
            <HomePageCards title="Dagens Ret" :items="['Rød grød med fløde']" thumbnail="https://ionicframework.com/docs/img/demos/thumbnail.svg"/>
          </ion-col>
        </ion-row>
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
import HomePageCards from "@/components/HomePageCard.vue";

import "swiper/css";
import "swiper/css/pagination";

const cards = ref(["N - PINNED", "I", "G", "G", "E", "R"]);
</script>

<style scoped>
/* ion-grid {
  padding: 10%; 
  margin-top: 10%; 
} */

ion-row {
  /* padding: 5%; */
  margin: 2%;
  text-align: center;
  /* padding: 0% 10%; */
}

ion-row.equal-row {
  align-items: stretch;
}

ion-row.equal-row ion-col {
  display: flex;
}

ion-row.equal-row ion-col > * {
  flex: 1 1 auto;
  display: flex;
  flex-direction: column;
}

@media (min-width: 992px) {
  ion-menu.desktop-menu {
    --width: 260px;
  }

  .swiper {
    overflow: visible;
  }
}

.slider-wrap {
  padding: 0 10%;
  padding-bottom: 5%;
  margin-top: 10%;
  margin-bottom: 30px;
}

:deep(.swiper-pagination) {
  position: static;
  margin-top: 10px;
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
  background: linear-gradient(90deg, rgba(2, 0, 36, 1) 0%, rgba(9, 9, 121, 1) 35%, rgba(0, 212, 255, 1) 100%);
}

.card-img {
  pointer-events: none;
  height: 100%;
  width: 100%;
  object-fit: contain;
}
</style>
