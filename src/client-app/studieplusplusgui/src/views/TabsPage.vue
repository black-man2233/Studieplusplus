  <template>
    <ion-page>
      <nav class="desktop-sidebar" :class="{ expanded: isSidebarExpanded }">
        <div class="sidebar-header">
          <button class="sidebar-toggle" @click="toggleSidebar" aria-label="Toggle sidebar">
            <ion-icon :icon="menuOutline" />
          </button>
        </div>

        <ul class="sidebar-nav">
          <li>
            <router-link to="/tabs/home" class="nav-item" :title="!isSidebarExpanded ? 'Hjem' : ''">
              <ion-icon :icon="homeOutline" class="nav-icon" />
              <span class="nav-label">Hjem</span>
            </router-link>
          </li>
          <li>
            <router-link to="/tabs/schedule" class="nav-item" :title="!isSidebarExpanded ? 'Skema' : ''">
              <ion-icon :icon="calendarOutline" class="nav-icon" />
              <span class="nav-label">Skema</span>
            </router-link>
          </li>
          <li>
            <router-link to="/tabs/messages" class="nav-item" :title="!isSidebarExpanded ? 'Beskeder' : ''">
              <ion-icon :icon="chatbubbleEllipsesOutline" class="nav-icon" />
              <span class="nav-label">Beskeder</span>
            </router-link>
          </li>
          <li>
            <router-link to="/tabs/profile" class="nav-item" :title="!isSidebarExpanded ? 'Profil' : ''">
              <ion-icon :icon="personOutline" class="nav-icon" />
              <span class="nav-label">Profil</span>
            </router-link>
          </li>
          <li>
            <router-link to="/tabs/settings" class="nav-item" :title="!isSidebarExpanded ? 'Indstillinger' : ''">
              <ion-icon :icon="settingsOutline" class="nav-icon" />
              <span class="nav-label">Indstillinger</span>
            </router-link>
          </li>
        </ul>
      </nav>

      <ion-tabs id="main-content" class="desktop-tabs">
        <ion-router-outlet :animated="false"></ion-router-outlet>

        <ion-tab-bar v-bind="{ slot: 'bottom' }" class="mobile-tabs">
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
  IonTabs,
  IonRouterOutlet,
  IonTabBar,
  IonTabButton,
  IonLabel,
  IonIcon,
} from '@ionic/vue';
import { ref } from 'vue';

import {
  calendarOutline,
  chatbubbleEllipsesOutline,
  homeOutline,
  personOutline,
  settingsOutline,
  menuOutline,
} from 'ionicons/icons';

const isSidebarExpanded = ref(true);

const toggleSidebar = () => {
  isSidebarExpanded.value = !isSidebarExpanded.value;
};
</script>

<style scoped>
@import "../styles/views/tabs-page.css";
</style>