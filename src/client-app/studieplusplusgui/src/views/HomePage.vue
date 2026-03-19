<template>
  <ion-page class="home-page">
    <ion-content :fullscreen="true" class="home-content">
      <div class="page-shell">
        <section class="hero">
          <p class="hero-kicker">Studie+Plus</p>
          <h1 class="hero-title">Hjem</h1>
          <p class="hero-subtitle">Alt det vigtige samlet et sted.</p>
        </section>

        <div class="slider-wrap">
          <Swiper
            :modules="swiperModules"
            :slides-per-view="'auto'"
            :space-between="14"
            :grab-cursor="true"
            :pagination="swiperPagination"
            :breakpoints="swiperBreakpoints"
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
  IonContent,
  IonCol,
  IonGrid,
  IonRow,
  IonLabel,
  IonImg,
  onIonViewDidEnter,
} from "@ionic/vue";

import { defineAsyncComponent, onMounted, ref } from "vue";
import { Swiper, SwiperSlide } from "swiper/vue";
import { Pagination } from "swiper/modules";
import { getEnrichedWeeklySchedule } from "@/services/enrichedScheduleService";

const HomePageCards = defineAsyncComponent(() => import("@/components/HomePageCard.vue"));

import "swiper/css";
import "swiper/css/pagination";

type HomeCard = {
  kicker: string;
  title: string;
  bg: string;
  image: string;
};

const swiperModules = [Pagination];
const swiperPagination = { clickable: true };
const swiperBreakpoints = { 768: { spaceBetween: 18 } };

const cards: HomeCard[] = [
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
];

const homeworkItems = [
  "ERP case-opgave",
  "Læs kapitel 4 i Projektstyring",
  "Forbered præsentation",
  "Upload refleksionsnotat",
];

const scheduleItems = ref<string[]>(["Henter dagens skema..."]);

function getCurrentWeekday(): number {
  const now = new Date();
  const day = now.getDay();
  return day === 0 ? 7 : day;
}

function toTimeLabel(isoDate: string): string {
  const date = new Date(isoDate);
  if (Number.isNaN(date.getTime())) {
    return "--:--";
  }

  return date.toLocaleTimeString("da-DK", {
    hour: "2-digit",
    minute: "2-digit",
  });
}

async function loadTodaySchedule() {
  try {
    const weeklySchedule = await getEnrichedWeeklySchedule();

    const today = getCurrentWeekday();

    const todaysItems = weeklySchedule
      .filter((entry) => entry.dayOfTheWeek === today)
      .sort((a, b) => new Date(a.startTime).getTime() - new Date(b.startTime).getTime())
      .map((entry) => {
        return `${toTimeLabel(entry.startTime)} ${entry.lessonName} - ${entry.teacherName}`;
      });

    scheduleItems.value = todaysItems.length > 0 ? todaysItems : ["Ingen lektioner i dag"];
  } catch (error) {
    console.error("Failed to load today's schedule", error);
    scheduleItems.value = ["Kunne ikke hente dagens skema"];
  }
}

onMounted(() => {
  void loadTodaySchedule();
});

onIonViewDidEnter(() => {
  void loadTodaySchedule();
});

const mealItems = ["Rød grød med fløde", "Frisk salat", "Fuldkornsbrød"];
</script>

<style scoped>
@import "../styles/views/home-page.css";
</style>
