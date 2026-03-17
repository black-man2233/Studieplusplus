<template>
  <ion-page>


    <ion-content :fullscreen="true" class="profile-content">
      <ion-header collapse="condense">
        <ion-toolbar>
          <ion-title size="large">Profil</ion-title>
        </ion-toolbar>
      </ion-header>

      <div class="profile-shell">
        <section class="hero-card">
          <div class="hero-motion" aria-hidden="true">
            <span class="motion-orb orb-a"></span>
            <span class="motion-orb orb-b"></span>
            <span class="motion-orb orb-c"></span>
          </div>

          <p class="hero-kicker">Studie+Plus</p>

          <ion-avatar class="profile-avatar">
            <ion-img
              src="https://fastly.picsum.photos/id/866/1920/1080.jpg?hmac=dNBuPEp10RySTqlc5EpGw7QyrFIpBd2X88r1Ixla7pw"
              alt="Profile Picture"
            />
          </ion-avatar>

          <h1 class="profile-name">Kevin</h1>
          <p class="profile-education">Datateknikker med speciale i programmering</p>

          <ion-chip class="profile-chip">
            <ion-icon :icon="schoolOutline" class="chip-icon" />
            <ion-label>H5PD010126</ion-label>
          </ion-chip>

          <div class="quick-stats">
            <div class="stat-card">
              <span class="stat-title">Gennemført</span>
              <strong class="stat-value">78%</strong>
            </div>
            <div class="stat-card">
              <span class="stat-title">Fravær</span>
              <strong class="stat-value">2.1%</strong>
            </div>
            <div class="stat-card">
              <span class="stat-title">Noter</span>
              <strong class="stat-value">14</strong>
            </div>
          </div>
        </section>

        <section class="accordion-shell">
          <!-- eslint-disable vue/no-deprecated-slot-attribute -->
          <ion-accordion-group value="personlig">
            <ion-accordion value="personlig">
              <ion-item slot="header" class="accordion-header" lines="none">
                <ion-icon :icon="personOutline" class="header-icon" />
                <ion-label>Personlig Information</ion-label>
              </ion-item>

              <div class="accordion-content ion-padding" slot="content">
                <ion-list lines="none" class="info-list">
                  <ion-item v-for="item in personalInfo" :key="item.label" class="info-item" lines="none">
                    <ion-icon :icon="item.icon" class="item-icon" />
                    <ion-label>
                      <p class="item-label">{{ item.label }}</p>
                      <h3 class="item-value">{{ item.value }}</h3>
                    </ion-label>
                  </ion-item>
                </ion-list>
              </div>
            </ion-accordion>

            <ion-accordion value="skole">
              <ion-item slot="header" class="accordion-header" lines="none">
                <ion-icon :icon="bookOutline" class="header-icon" />
                <ion-label>Skole Forloeb</ion-label>
              </ion-item>

              <div class="accordion-content ion-padding" slot="content">
                <ion-list lines="none" class="info-list">
                  <ion-item v-for="item in schoolPath" :key="item.title" class="info-item" lines="none">
                    <ion-label>
                      <p class="item-label">{{ item.period }}</p>
                      <h3 class="item-value">{{ item.title }}</h3>
                    </ion-label>
                  </ion-item>
                </ion-list>
              </div>
            </ion-accordion>

            <ion-accordion value="noter">
              <ion-item slot="header" class="accordion-header" lines="none">
                <ion-icon :icon="documentTextOutline" class="header-icon" />
                <ion-label>Noter</ion-label>
              </ion-item>

              <div class="accordion-content ion-padding" slot="content">
                <ion-list lines="none" class="info-list">
                  <ion-item v-for="note in notes" :key="note" class="info-item" lines="none">
                    <ion-label class="single-line-label">{{ note }}</ion-label>
                  </ion-item>
                </ion-list>
              </div>
            </ion-accordion>

            <ion-accordion value="arbejdsgiver">
              <ion-item slot="header" class="accordion-header" lines="none">
                <ion-icon :icon="briefcaseOutline" class="header-icon" />
                <ion-label>Arbejdsgiver</ion-label>
              </ion-item>

              <div class="accordion-content ion-padding" slot="content">
                <ion-card class="employer-card">
                  <ion-card-header>
                    <ion-card-title>{{ employer.name }}</ion-card-title>
                    <ion-card-subtitle>{{ employer.role }}</ion-card-subtitle>
                  </ion-card-header>
                  <ion-card-content>
                    {{ employer.details }}
                  </ion-card-content>
                </ion-card>
              </div>
            </ion-accordion>
          </ion-accordion-group>
          <!-- eslint-enable vue/no-deprecated-slot-attribute -->
        </section>
      </div>
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
  IonImg,
  IonAvatar,
  IonLabel,
  IonItem,
  IonAccordion,
  IonCard,
  IonCardTitle,
  IonCardSubtitle,
  IonCardContent,
  IonCardHeader,
  IonAccordionGroup,
  IonList,
  IonChip,
  IonIcon,
} from "@ionic/vue";

import {
  personOutline,
  schoolOutline,
  documentTextOutline,
  briefcaseOutline,
  callOutline,
  mailOutline,
  locationOutline,
  timeOutline,
  bookOutline,
} from "ionicons/icons";

const personalInfo = [
  {
    icon: callOutline,
    label: "Telefon",
    value: "+45 22 33 44 55",
  },
  {
    icon: mailOutline,
    label: "Mail",
    value: "kevin.hansen@studieplus.dk",
  },
  {
    icon: locationOutline,
    label: "Lokation",
    value: "Odense, Danmark",
  },
  {
    icon: timeOutline,
    label: "Studietid",
    value: "2. semester",
  },
];

const schoolPath = [
  {
    period: "2025 - nu",
    title: "Datateknikker med speciale i programmering",
  },
  {
    period: "2024 - 2025",
    title: "Grundforloeb 2 - IT & programmering",
  },
  {
    period: "2023 - 2024",
    title: "Introforloeb i softwareudvikling",
  },
];

const notes = [
  "Projektstyring: afleveringskrav gennemgaet",
  "Systemudvikling: repeter API design til torsdag",
  "Gruppearbejde: moede kl. 13:15 i lokale B-204",
];

const employer = {
  name: "Nordic Dev Solutions",
  role: "Praktikplads - Junior Developer",
  details:
    "Arbejder med frontend-opgaver i Vue og deltager i sprint planlaegning. Fokus paa kvalitet, test og samarbejde i teamet.",
};
</script>

<style scoped>
@import url("https://fonts.googleapis.com/css2?family=Space+Grotesk:wght@400;500;600;700&display=swap");

.profile-content {
  --background: transparent;
}

.profile-shell {
  max-width: 880px;
  margin: 0 auto;
  padding: 20px 14px 26px;
  display: flex;
  flex-direction: column;
  gap: 16px;
}

.hero-card {
  position: relative;
  overflow: hidden;
  border-radius: 24px;
  border: 1px solid rgba(170, 198, 236, 0.28);
  background: linear-gradient(155deg, rgba(28, 48, 82, 0.86) 0%, rgba(35, 64, 108, 0.8) 54%, rgba(60, 95, 137, 0.74) 100%);
  box-shadow: 0 18px 30px rgba(14, 24, 44, 0.28);
  padding: 20px 16px 16px;
  text-align: center;
}

.hero-card::before,
.hero-card::after {
  content: "";
  position: absolute;
  border-radius: 999px;
  pointer-events: none;
  z-index: 0;
}

.hero-card::before {
  width: 180px;
  height: 180px;
  top: -86px;
  left: -74px;
  background: radial-gradient(circle, rgba(112, 138, 176, 0.22) 0%, rgba(112, 138, 176, 0) 72%);
  animation: heroGlowDriftA 16s ease-in-out infinite alternate;
}

.hero-card::after {
  width: 140px;
  height: 140px;
  bottom: -64px;
  right: -44px;
  background: radial-gradient(circle, rgba(125, 106, 90, 0.2) 0%, rgba(125, 106, 90, 0) 72%);
  animation: heroGlowDriftB 15s ease-in-out infinite alternate;
}

.hero-motion {
  position: absolute;
  inset: 0;
  pointer-events: none;
  overflow: hidden;
  z-index: 1;
}

.motion-orb {
  position: absolute;
  border-radius: 999px;
  filter: blur(0.5px);
  opacity: 0.56;
  box-shadow: inset 0 0 0 1px rgba(150, 176, 212, 0.14);
  will-change: transform;
}

.orb-a {
  width: 170px;
  height: 170px;
  top: 8%;
  right: -54px;
  background: radial-gradient(circle at 30% 30%, rgba(121, 146, 184, 0.27) 0%, rgba(121, 146, 184, 0.07) 46%, rgba(121, 146, 184, 0) 72%);
  animation: heroOrbFloatA 10.5s ease-in-out infinite alternate;
}

.orb-b {
  width: 128px;
  height: 128px;
  bottom: -40px;
  left: 28%;
  background: radial-gradient(circle at 50% 50%, rgba(108, 131, 168, 0.24) 0%, rgba(108, 131, 168, 0.06) 52%, rgba(108, 131, 168, 0) 74%);
  animation: heroOrbFloatB 9.2s ease-in-out infinite alternate;
}

.orb-c {
  width: 104px;
  height: 104px;
  top: 34%;
  left: -28px;
  background: radial-gradient(circle at 40% 40%, rgba(128, 107, 91, 0.24) 0%, rgba(128, 107, 91, 0.06) 50%, rgba(128, 107, 91, 0) 72%);
  animation: heroOrbFloatC 8.8s ease-in-out infinite alternate;
}

.hero-card > :not(.hero-motion) {
  position: relative;
  z-index: 2;
}

@keyframes heroGlowDriftA {
  0% {
    transform: translate3d(0, 0, 0) scale(1);
  }
  100% {
    transform: translate3d(16px, 12px, 0) scale(1.14);
  }
}

@keyframes heroGlowDriftB {
  0% {
    transform: translate3d(0, 0, 0) scale(1);
  }
  100% {
    transform: translate3d(-14px, -10px, 0) scale(1.18);
  }
}

@keyframes heroOrbFloatA {
  0% {
    transform: translate3d(0, 0, 0) scale(1);
  }
  100% {
    transform: translate3d(-52px, 32px, 0) scale(1.2);
  }
}

@keyframes heroOrbFloatB {
  0% {
    transform: translate3d(0, 0, 0) scale(1);
  }
  100% {
    transform: translate3d(42px, -30px, 0) scale(1.18);
  }
}

@keyframes heroOrbFloatC {
  0% {
    transform: translate3d(0, 0, 0) scale(1);
  }
  100% {
    transform: translate3d(34px, 22px, 0) scale(1.15);
  }
}

.profile-avatar {
  width: 108px;
  height: 108px;
  margin: 0 auto 10px;
  border: 3px solid rgba(255, 255, 255, 0.7);
  box-shadow: 0 8px 18px rgba(7, 16, 34, 0.36);
}

.hero-kicker {
  margin: 0 0 8px;
  width: 100%;
  text-align: left;
  font-family: "Space Grotesk", "Segoe UI", sans-serif;
  font-size: 0.74rem;
  font-weight: 800;
  letter-spacing: 0.11em;
  text-transform: uppercase;
  color: #d9c9ae;
}

.profile-name {
  margin: 2px 0 4px;
  font-family: "Space Grotesk", "Segoe UI", sans-serif;
  font-size: clamp(1.52rem, 4.2vw, 1.86rem);
  line-height: 1.15;
  font-weight: 700;
  color: #f8fbff;
}

.profile-education {
  margin: 0 auto 10px;
  max-width: 44ch;
  font-family: "Space Grotesk", "Segoe UI", sans-serif;
  font-size: 0.92rem;
  font-weight: 500;
  color: rgba(238, 245, 255, 0.88);
}

.profile-chip {
  margin: 0 auto;
  width: fit-content;
  --background: rgba(18, 30, 51, 0.48);
  --color: #eaf3ff;
  border: 1px solid rgba(187, 213, 248, 0.33);
}

.chip-icon {
  margin-right: 6px;
  font-size: 0.96rem;
}

.quick-stats {
  margin-top: 14px;
  display: grid;
  grid-template-columns: repeat(3, minmax(0, 1fr));
  gap: 8px;
}

.stat-card {
  border-radius: 14px;
  border: 1px solid rgba(183, 209, 243, 0.24);
  background: rgba(12, 21, 38, 0.22);
  padding: 10px 8px;
}

.stat-title {
  display: block;
  font-family: "Space Grotesk", "Segoe UI", sans-serif;
  font-size: 0.72rem;
  font-weight: 600;
  letter-spacing: 0.08em;
  text-transform: uppercase;
  color: rgba(221, 233, 252, 0.8);
}

.stat-value {
  display: block;
  margin-top: 3px;
  font-family: "Space Grotesk", "Segoe UI", sans-serif;
  font-size: 1.02rem;
  font-weight: 700;
  color: #f8fbff;
}

.accordion-shell {
  border-radius: 22px;
  border: 1px solid rgba(145, 170, 205, 0.24);
  background: linear-gradient(180deg, rgba(16, 25, 40, 0.58) 0%, rgba(21, 33, 52, 0.5) 100%);
  box-shadow: 0 12px 24px rgba(9, 16, 29, 0.18);
  overflow: hidden;
}

.accordion-header {
  --background: rgba(56, 79, 112, 0.82);
  --color: #eef4ff;
  font-family: "Space Grotesk", "Segoe UI", sans-serif;
  font-size: 0.95rem;
  font-weight: 700;
}

:deep(ion-accordion.accordion-expanded .accordion-header) {
  --background: rgba(66, 95, 136, 0.9);
}

.header-icon {
  margin-right: 8px;
  font-size: 1.05rem;
  color: #cbdfff;
}

.accordion-content {
  background: rgba(10, 18, 30, 0.58);
}

.info-list {
  background: transparent;
  padding: 2px 0;
}

.info-item {
  --background: rgba(112, 139, 176, 0.14);
  --inner-padding-end: 12px;
  --min-height: 56px;
  border: 1px solid rgba(156, 182, 216, 0.24);
  border-radius: 14px;
  margin-bottom: 0;
}

.info-item + .info-item {
  margin-top: 6px;
}

.item-icon {
  margin-right: 10px;
  color: #d2e3ff;
  font-size: 1.05rem;
}

.item-label {
  margin: 0;
  font-family: "Space Grotesk", "Segoe UI", sans-serif;
  font-size: 0.75rem;
  font-weight: 600;
  letter-spacing: 0.06em;
  text-transform: uppercase;
  color: rgba(210, 225, 247, 0.78);
}

.item-value {
  margin: 2px 0 0;
  font-family: "Space Grotesk", "Segoe UI", sans-serif;
  font-size: 0.95rem;
  font-weight: 600;
  color: #f2f7ff;
}

.single-line-label {
  font-family: "Space Grotesk", "Segoe UI", sans-serif;
  font-size: 0.92rem;
  font-weight: 500;
  color: #f1f6ff;
}

.employer-card {
  margin: 0;
  border-radius: 16px;
  border: 1px solid rgba(163, 188, 221, 0.28);
  background: linear-gradient(155deg, rgba(35, 56, 87, 0.86) 0%, rgba(47, 77, 118, 0.82) 100%);
}

.employer-card ion-card-title {
  color: #f4f8ff;
  font-family: "Space Grotesk", "Segoe UI", sans-serif;
}

.employer-card ion-card-subtitle {
  color: rgba(223, 236, 255, 0.86);
}

.employer-card ion-card-content {
  color: rgba(235, 243, 255, 0.92);
  line-height: 1.45;
}

@media (max-width: 560px) {
  .quick-stats {
    grid-template-columns: 1fr;
  }

  .profile-shell {
    padding: 16px 10px 22px;
  }
}

@media (prefers-reduced-motion: reduce) {
  .hero-card::before {
    animation-duration: 28s;
  }

  .hero-card::after {
    animation-duration: 26s;
  }

  .orb-a {
    animation-duration: 24s;
  }

  .orb-b {
    animation-duration: 22s;
  }

  .orb-c {
    animation-duration: 20s;
  }
}
</style>
