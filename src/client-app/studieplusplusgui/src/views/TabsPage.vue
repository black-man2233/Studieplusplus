  <template>
    <ion-page>
      <ion-menu content-id="main-content" class="desktop-menu" type="overlay" side="start">
        <ion-header>
          <ion-toolbar>
            <ion-title>Menu</ion-title>
          </ion-toolbar>
        </ion-header>

        <ion-content>
          <ion-list>
            <ion-item router-link="/tabs/home" router-direction="root">Home</ion-item>
            <ion-item router-link="/tabs/schedule" router-direction="root">Schedule</ion-item>
            <ion-item router-link="/tabs/messages" router-direction="root">Messages</ion-item>
            <ion-item router-link="/tabs/profile" router-direction="root">Profile</ion-item>
            <ion-item router-link="/tabs/settings" router-direction="root">Settings</ion-item>
          </ion-list>
        </ion-content>
      </ion-menu>

      <ion-header class="menu-header">
        <ion-toolbar>
          <!-- eslint-disable-next-line vue/no-deprecated-slot-attribute -->
          <ion-buttons slot="start">
            <ion-menu-button></ion-menu-button>
          </ion-buttons>
        </ion-toolbar>
      </ion-header>

      <ion-tabs id="main-content">
        <ion-router-outlet></ion-router-outlet>

        <!-- eslint-disable-next-line vue/no-deprecated-slot-attribute -->
        <ion-tab-bar :slot="'bottom'" class="mobile-tabs">
          <ion-tab-button tab="messages" href="/tabs/messages">
            <ion-icon aria-hidden="true" :icon="chatbubbleEllipsesOutline" />
            <ion-label>Beskeder</ion-label>
          </ion-tab-button>

          <ion-tab-button tab="schedule" href="/tabs/schedule">
            <ion-icon aria-hidden="true" :icon="calendarOutline" />
            <ion-label>Skema</ion-label>
          </ion-tab-button>

          <ion-tab-button tab="home" href="/tabs/home">
            <ion-icon aria-hidden="true" :icon="homeOutline" />
            <ion-label>Hjem</ion-label>
          </ion-tab-button>

          <ion-tab-button tab="profile" href="/tabs/profile">
            <ion-icon aria-hidden="true" :icon="personOutline" />
            <ion-label>Profil</ion-label>
          </ion-tab-button>

          <ion-tab-button tab="settings" href="/tabs/settings">
            <ion-icon aria-hidden="true" :icon="settingsOutline" />
            <ion-label>Indstillinger</ion-label>
          </ion-tab-button>
        </ion-tab-bar>
      </ion-tabs>
    </ion-page>
  </template>

<script setup lang="ts">
import {
  IonPage,
  IonMenu,
  IonHeader,
  IonToolbar,
  IonTitle,
  IonContent,
  IonList,
  IonItem,
  IonTabs,
  IonRouterOutlet,
  IonTabBar,
  IonTabButton,
  IonLabel,
  IonIcon,
  IonButtons,
  IonMenuButton,
} from '@ionic/vue';

import {
  calendarOutline,
  chatbubbleEllipsesOutline,
  homeOutline,
  personOutline,
  settingsOutline,
} from 'ionicons/icons';
</script>