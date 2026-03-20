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

          <h1 class="profile-name">{{ profileName }}</h1>
          <p class="profile-education">{{ profileEducation }}</p>

          <ion-chip class="profile-chip">
            <ion-icon :icon="schoolOutline" class="chip-icon" />
            <ion-label>{{ profileCode }}</ion-label>
          </ion-chip>

          <p v-if="profileMessage" class="profile-message">{{ profileMessage }}</p>

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
          <ion-accordion-group value="personlig">
            <ion-accordion value="personlig">
              <ion-item v-bind="{ slot: 'header' }" class="accordion-header" lines="none">
                <ion-icon :icon="personOutline" class="header-icon" />
                <ion-label>Personlig Information</ion-label>
              </ion-item>

              <div v-bind="{ slot: 'content' }" class="accordion-content ion-padding">
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
              <ion-item v-bind="{ slot: 'header' }" class="accordion-header" lines="none">
                <ion-icon :icon="bookOutline" class="header-icon" />
                <ion-label>Skole Forløb</ion-label>
              </ion-item>

              <div v-bind="{ slot: 'content' }" class="accordion-content ion-padding">
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
              <ion-item v-bind="{ slot: 'header' }" class="accordion-header" lines="none">
                <ion-icon :icon="documentTextOutline" class="header-icon" />
                <ion-label>Noter</ion-label>
              </ion-item>

              <div v-bind="{ slot: 'content' }" class="accordion-content ion-padding">
                <ion-list lines="none" class="info-list">
                  <ion-item v-for="note in notes" :key="note" class="info-item" lines="none">
                    <ion-label class="single-line-label">{{ note }}</ion-label>
                  </ion-item>
                </ion-list>
              </div>
            </ion-accordion>

            <ion-accordion value="arbejdsgiver">
              <ion-item v-bind="{ slot: 'header' }" class="accordion-header" lines="none">
                <ion-icon :icon="briefcaseOutline" class="header-icon" />
                <ion-label>Arbejdsgiver</ion-label>
              </ion-item>

              <div v-bind="{ slot: 'content' }" class="accordion-content ion-padding">
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
  onIonViewDidEnter,
} from "@ionic/vue";
import { computed, ref } from "vue";
import { getStoredLoginIdentifier, getStoredLoginMethod } from "@/services/authStorage";
import { findStudentByLoginIdentifier, getStudents } from "@/services/studentsService";

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

const defaultEducation = "Datateknikker med speciale i programmering";
const defaultProfileCode = "H5PD010126";

const profileName = ref("Bruger");
const profileEducation = ref(defaultEducation);
const profileCode = ref(defaultProfileCode);
const profileMessage = ref("");
const profileEmail = ref("-");

const personalInfo = computed(() => [
  {
    icon: callOutline,
    label: "Telefon",
    value: "+45 22 33 44 55",
  },
  {
    icon: mailOutline,
    label: "Mail",
    value: profileEmail.value,
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
]);

const schoolPath = [
  {
    period: "2025 - nu",
    title: "Datateknikker med speciale i programmering",
  },
  {
    period: "2024 - 2025",
    title: "Grundforløb 2 - IT & programmering",
  },
  {
    period: "2023 - 2024",
    title: "Introforløb i softwareudvikling",
  },
];

const notes = [
  "Projektstyring: afleveringskrav gennemgået",
  "Systemudvikling: repeter API design til torsdag",
  "Gruppearbejde: møde kl. 13:15 i lokale B-204",
];

const employer = {
  name: "Nordic Dev Solutions",
  role: "Praktikplads - Junior Developer",
  details:
    "Arbejder med frontend-opgaver i Vue og deltager i sprint planlægning. Fokus på kvalitet, test og samarbejde i teamet.",
};

async function loadDirectLoginProfile() {
  const loginMethod = getStoredLoginMethod();
  const loginIdentifier = getStoredLoginIdentifier();

  if (loginMethod !== "direct") {
    profileMessage.value = "Profil-data er kun koblet på direkte login lige nu.";
    profileEmail.value = "-";
    return;
  }

  if (!loginIdentifier) {
    profileMessage.value = "Ingen direkte login-bruger fundet i sessionen.";
    profileEmail.value = "-";
    return;
  }

  profileEmail.value = loginIdentifier;

  try {
    const students = await getStudents();
    const student = findStudentByLoginIdentifier(students, loginIdentifier);

    if (!student) {
      profileName.value = loginIdentifier;
      profileCode.value = defaultProfileCode;
      profileMessage.value = "Kunne ikke matche brugeren i elevlisten, viser login-oplysninger i stedet.";
      return;
    }

    profileName.value = `${student.firstName} ${student.lastName}`.trim();
    profileEmail.value = student.email;
    profileCode.value = `ELEV-${student.id.slice(0, 8).toUpperCase()}`;
    profileMessage.value = "";
  } catch (error) {
    console.error("Failed to load profile data", error);
    profileName.value = loginIdentifier;
    profileMessage.value = "Kunne ikke hente profil-data fra API.";
  }
}

onIonViewDidEnter(() => {
  void loadDirectLoginProfile();
});
</script>

<style scoped>
@import "../styles/views/profile-page.css";
</style>
