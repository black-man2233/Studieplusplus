  <template>
    <ion-page>
      <ion-menu
        content-id="main-content"
        class="desktop-menu"
        type="overlay"
        side="start"
        @ionWillOpen="isDesktopMenuOpen = true"
        @ionDidClose="isDesktopMenuOpen = false"
      >
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

      <div class="floating-menu-button" v-show="!isDesktopMenuOpen">
        <ion-menu-button :auto-hide="false" aria-label="Åbn menu"></ion-menu-button>
      </div>

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
  IonMenuButton,
} from '@ionic/vue';
import { ref } from 'vue';

import {
  calendarOutline,
  chatbubbleEllipsesOutline,
  homeOutline,
  personOutline,
  settingsOutline,
} from 'ionicons/icons';

const isDesktopMenuOpen = ref(false);
</script>

<style scoped>
@media (min-width: 992px) {
  .floating-menu-button {
    position: fixed;
    top: 20px;
    left: 20px;
    z-index: 900;
  }

  .floating-menu-button ion-menu-button {
    --background: linear-gradient(135deg, rgba(47, 78, 168, 0.85) 0%, rgba(63, 95, 139, 0.8) 100%);
    --color: #f0f6ff;
    --border-radius: 14px;
    --padding-start: 0;
    --padding-end: 0;
    --padding-top: 0;
    --padding-bottom: 0;
    backdrop-filter: blur(8px);
    box-shadow: 0 8px 20px rgba(11, 19, 35, 0.28);
    transition: all 0.2s ease;
    width: 42px;
    height: 42px;
    display: flex;
    align-items: center;
    justify-content: center;
  }

  .floating-menu-button ion-menu-button:hover {
    --background: linear-gradient(135deg, rgba(57, 98, 188, 0.95) 0%, rgba(73, 115, 159, 0.9) 100%);
    border-color: rgba(177, 201, 234, 0.55);
    box-shadow: 0 12px 28px rgba(11, 19, 35, 0.38);
    transform: translateY(-2px);
  }

  .floating-menu-button ion-menu-button:active {
    transform: translateY(0);
  }
}

@media (max-width: 991px) {
  .floating-menu-button {
    display: none;
  }
}
</style>