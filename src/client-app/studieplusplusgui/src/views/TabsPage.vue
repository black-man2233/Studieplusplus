  <template>
    <ion-page>
      <!-- Desktop Sidebar -->
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
@media (min-width: 992px) {
  ion-page {
    display: flex;
  }

  .desktop-sidebar {
    position: fixed;
    left: 0;
    top: 0;
    height: 100%;
    width: 70px;
    background: linear-gradient(180deg, rgba(20, 34, 56, 0.95) 0%, rgba(25, 42, 68, 0.92) 100%);
    border-right: 1px solid rgba(152, 181, 220, 0.18);
    display: flex;
    flex-direction: column;
    z-index: 800;
    transition: width 0.3s ease;
    overflow: hidden;
  }

  .desktop-sidebar.expanded {
    width: 220px;
  }

  .sidebar-header {
    padding: 16px;
    display: flex;
    justify-content: center;
    border-bottom: 1px solid rgba(152, 181, 220, 0.12);
  }

  .sidebar-toggle {
    background: transparent;
    border: none;
    color: #e7f1ff;
    cursor: pointer;
    display: flex;
    align-items: center;
    justify-content: center;
    width: 38px;
    height: 38px;
    border-radius: 10px;
    transition: all 0.2s ease;
  }

  .sidebar-toggle:hover {
    background: rgba(100, 140, 190, 0.2);
    color: #f0f6ff;
  }

  .sidebar-toggle ion-icon {
    font-size: 24px;
  }

  .sidebar-nav {
    list-style: none;
    padding: 12px 8px;
    margin: 0;
    flex: 1;
    display: flex;
    flex-direction: column;
    gap: 8px;
  }

  .nav-item {
    display: flex;
    align-items: center;
    gap: 14px;
    padding: 12px 12px;
    border-radius: 12px;
    color: rgba(213, 227, 247, 0.8);
    text-decoration: none;
    font-size: 0.95rem;
    font-weight: 500;
    transition: all 0.2s ease;
    white-space: nowrap;
  }

  .nav-item:hover {
    background: rgba(100, 140, 190, 0.22);
    color: #f0f6ff;
  }

  .nav-item.router-link-active {
    background: linear-gradient(135deg, rgba(63, 121, 212, 0.28) 0%, rgba(79, 141, 223, 0.22) 100%);
    color: #e7f1ff;
    border-left: 3px solid rgba(79, 141, 223, 0.6);
    padding-left: 9px;
  }

  .nav-icon {
    font-size: 22px;
    flex-shrink: 0;
  }

  .nav-label {
    opacity: 0;
    transition: opacity 0.3s ease;
    overflow: hidden;
    text-overflow: ellipsis;
  }

  .desktop-sidebar.expanded .nav-label {
    opacity: 1;
  }

  .desktop-tabs {
    flex: 1;
  }

  .mobile-tabs {
    display: none;
  }
}

@media (max-width: 991px) {
  .desktop-sidebar {
    display: none;
  }

  .mobile-tabs {
    display: flex;
  }
}
</style>