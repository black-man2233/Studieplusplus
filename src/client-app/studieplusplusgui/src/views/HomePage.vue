<template>
  <ion-page class="home-page">
    <ion-header>

    </ion-header>

    <ion-content :fullscreen="true" class="home-content">
      <div class="page-shell">
        <section class="hero">
          <p class="hero-kicker">Studie+Plus</p>
          <h1 class="hero-title">Hjem</h1>
          <p class="hero-subtitle">Alt det vigtige samlet et sted.</p>
        </section>

        <div class="slider-wrap">
          <Swiper
            :modules="[Pagination]"
            :slides-per-view="'auto'"
            :space-between="14"
            :grab-cursor="true"
            :pagination="{ clickable: true }"
            :breakpoints="{ 768: { spaceBetween: 18 } }"
          >
            <SwiperSlide v-for="card in cards" :key="card.title" class="card-slide">
              <article class="card-box" :style="{ '--card-bg': card.bg }">
                <ion-label class="card-kicker">{{ card.kicker }}</ion-label>
                <ion-label class="card-title">{{ card.title }}</ion-label>
                <ion-img class="card-img" :src="card.image" :alt="card.title" />
              </article>
            </SwiperSlide>
          </Swiper>
        </div>

        <ion-grid class="overview-grid">
          <ion-row class="equal-row">
            <ion-col size="12" size-md="6">
              <HomePageCards title="Lektier" :items="homeworkItems" />
            </ion-col>
            <ion-col size="12" size-md="6">
              <HomePageCards title="Dagens Skema" :items="scheduleItems" />
            </ion-col>
          </ion-row>
          <ion-row class="equal-row">
            <ion-col size="12">
              <HomePageCards
                title="Dagens Ret"
                :items="mealItems"
                thumbnail="https://images.unsplash.com/photo-1546069901-ba9599a7e63c?q=80&w=1200&auto=format&fit=crop"
              />
            </ion-col>
          </ion-row>
        </ion-grid>
      </div>
    </ion-content>
  </ion-page>
</template>

<script setup lang="ts">
import {
  IonPage,
  IonHeader,
  IonContent,
  IonCol,
  IonGrid,
  IonRow,
  IonLabel,
  IonImg,
} from "@ionic/vue";

import { defineAsyncComponent, ref } from "vue";
import { Swiper, SwiperSlide } from "swiper/vue";
import { Pagination } from "swiper/modules";

const HomePageCards = defineAsyncComponent(
  () => import("@/components/HomePageCard.vue") as Promise<any>
);

import "swiper/css";
import "swiper/css/pagination";

const cards = ref([
  {
    kicker: "Pinned",
    title: "Aflevering i morgen",
    bg: "linear-gradient(145deg, #3f2b24 0%, #5a3d2f 42%, #6f4d3e 100%)",
    image: "https://images.unsplash.com/photo-1454165804606-c3d57bc86b40?q=80&w=1200&auto=format&fit=crop",
  },
  {
    kicker: "Påmindelse",
    title: "Møde med projektgruppen",
    bg: "linear-gradient(145deg, #1e3243 0%, #284d68 50%, #2f5f7f 100%)",
    image: "https://images.unsplash.com/photo-1522071820081-009f0129c71c?q=80&w=1200&auto=format&fit=crop",
  },
  {
    kicker: "Nyt",
    title: "Nyt materiale uploadet",
    bg: "linear-gradient(145deg, #2b2c4a 0%, #3a3f67 50%, #4a5680 100%)",
    image: "https://images.unsplash.com/photo-1513258496099-48168024aec0?q=80&w=1200&auto=format&fit=crop",
  },
]);

const homeworkItems = [
  "ERP case-opgave",
  "Læs kapitel 4 i Projektstyring",
  "Forbered præsentation",
  "Upload refleksionsnotat",
];

const scheduleItems = [
  "08:15 Projektstyring",
  "10:00 Systemudvikling",
  "12:30 Gruppearbejde",
];

const mealItems = ["Rød grød med fløde", "Frisk salat", "Fuldkornsbrød"];
</script>

<style scoped>
@import url('https://fonts.googleapis.com/css2?family=Manrope:wght@400;500;600;700;800&display=swap');

.home-content {
  --background: transparent;
}

.page-shell {
  padding: 24px 12px 18px;
}

.hero {
  padding: 6px 12px 10px;
}

.hero-kicker {
  margin: 0;
  font-family: "Manrope", "Segoe UI", sans-serif;
  font-size: 0.74rem;
  font-weight: 800;
  letter-spacing: 0.13em;
  text-transform: uppercase;
  color: #d9c9ae;
}

.hero-title {
  margin: 4px 0 2px;
  font-family: "Manrope", "Segoe UI", sans-serif;
  font-size: clamp(1.8rem, 4.6vw, 2.4rem);
  line-height: 1.1;
  font-weight: 800;
  color: #edf3ff;
}

.hero-subtitle {
  margin: 0;
  font-family: "Manrope", "Segoe UI", sans-serif;
  font-size: 0.95rem;
  font-weight: 500;
  color: #d7e1f4;
}

ion-row {
  margin: 0;
  text-align: center;
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

ion-col {
  padding: 8px;
}

@media (min-width: 992px) {
  ion-menu.desktop-menu {
    --width: 260px;
  }
}

.slider-wrap {
  padding: 4px 8px 8px;
  margin-bottom: 8px;
  overflow: hidden;
  border: 1px solid rgba(129, 145, 190, 0.2);
  border-radius: 20px;
  background: linear-gradient(180deg, rgba(20, 27, 43, 0.58) 0%, rgba(29, 39, 62, 0.32) 100%);
  backdrop-filter: blur(3px);
}

:deep(.swiper-pagination) {
  position: static;
  margin-top: 12px;
}

:deep(.swiper-pagination-bullet) {
  width: 10px;
  height: 10px;
  background: #a4accf;
  opacity: 0.7;
}

:deep(.swiper-pagination-bullet-active) {
  background: #2f4ea8;
  opacity: 1;
}

.card-slide {
  width: min(72vw, 340px);
  padding: 6px 2px 10px;
}

.card-box {
  position: relative;
  height: 230px;
  border: 1px solid rgba(177, 201, 234, 0.22);
  border-radius: 18px;
  box-sizing: border-box;
  display: flex;
  flex-direction: column;
  gap: 10px;
  padding: 12px;
  overflow: hidden;
  background: var(--card-bg, linear-gradient(145deg, #213247 0%, #2c4a69 55%, #3f5f80 100%));
  box-shadow: 0 12px 22px rgba(17, 24, 42, 0.24);
}

.card-box::before {
  content: "";
  position: absolute;
  width: 190px;
  height: 190px;
  top: -68px;
  left: -46px;
  border-radius: 999px;
  background: radial-gradient(circle at 30% 30%, rgba(255, 255, 255, 0.22) 0%, rgba(255, 255, 255, 0.05) 52%, rgba(255, 255, 255, 0) 75%);
  filter: blur(1px);
  opacity: 0.65;
  pointer-events: none;
  animation: cardOrbFloatA 8.4s ease-in-out infinite alternate;
  z-index: 0;
  will-change: transform;
}

.card-slide:nth-child(3n + 2) .card-box::before {
  top: -28px;
  left: auto;
  right: -62px;
  animation-name: cardOrbFloatB;
  animation-duration: 7.8s;
}

.card-slide:nth-child(3n + 3) .card-box::before {
  top: auto;
  bottom: -82px;
  left: 12%;
  animation-name: cardOrbFloatC;
  animation-duration: 9.1s;
}

.card-box > * {
  position: relative;
  z-index: 1;
}

@keyframes cardOrbFloatA {
  0% {
    transform: translate3d(0, 0, 0) scale(1);
  }
  100% {
    transform: translate3d(74px, 52px, 0) scale(1.22);
  }
}

@keyframes cardOrbFloatB {
  0% {
    transform: translate3d(0, 0, 0) scale(1);
  }
  100% {
    transform: translate3d(-70px, 44px, 0) scale(1.2);
  }
}

@keyframes cardOrbFloatC {
  0% {
    transform: translate3d(0, 0, 0) scale(1);
  }
  100% {
    transform: translate3d(56px, -68px, 0) scale(1.24);
  }
}

@media (prefers-reduced-motion: reduce) {
  .card-box::before {
    animation: none;
  }
}

.card-kicker {
  align-self: flex-start;
  font-family: "Manrope", "Segoe UI", sans-serif;
  font-size: 0.67rem;
  font-weight: 800;
  letter-spacing: 0.13em;
  text-transform: uppercase;
  color: rgba(255, 255, 255, 0.78);
}

.card-title {
  font-family: "Manrope", "Segoe UI", sans-serif;
  font-size: 1.1rem;
  font-weight: 700;
  line-height: 1.2;
  color: #f4f8ff;
  overflow-wrap: anywhere;
}

.card-img {
  pointer-events: none;
  flex: 1 1 auto;
  min-height: 0;
  height: auto;
  width: 100%;
  border-radius: 12px;
  overflow: hidden;
}

:deep(.card-img img) {
  width: 100%;
  height: 100%;
  object-fit: cover;
  display: block;
}

.overview-grid {
  padding: 0;
}

:deep(.overview-grid ion-card) {
  margin: 0;
}

:deep(.overview-grid ion-card-title) {
  color: #f4f8ff !important;
}

:deep(.overview-grid ion-item) {
  --color: #e6edff !important;
}

:deep(.overview-grid ion-item ion-label) {
  color: #e6edff !important;
}
</style>
