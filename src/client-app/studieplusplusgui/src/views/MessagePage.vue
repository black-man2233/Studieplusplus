<template>
  <ion-page>
    <ion-content :fullscreen="true" class="messages-content">
      <ion-header collapse="condense">
        <ion-toolbar>
          <ion-title size="large">Beskeder</ion-title>
        </ion-toolbar>
      </ion-header>

      <div class="messages-shell">
        <section class="messages-hero">
          <p class="hero-kicker">Studie+Plus</p>
          <h1 class="hero-title">Beskeder</h1>
          <p class="hero-subtitle">Faa hurtigt overblik over dine samtaler med klasse, laerere og gruppe.</p>

          <div class="hero-stats">
            <div class="stat-card">
              <span class="stat-label">Ulaeste</span>
              <strong class="stat-value">{{ unreadCount }}</strong>
            </div>
            <div class="stat-card">
              <span class="stat-label">Samtaler</span>
              <strong class="stat-value">{{ chats.length }}</strong>
            </div>
            <div class="stat-card">
              <span class="stat-label">Aktive i dag</span>
              <strong class="stat-value">{{ activeToday }}</strong>
            </div>
          </div>
        </section>

        <section class="messages-panel">
          <div class="panel-head">
            <p class="panel-title">Seneste samtaler</p>
            <ion-button fill="clear" class="filter-btn">
              <ion-icon :icon="funnelOutline" />
              Filtrer
            </ion-button>
          </div>

          <!-- eslint-disable vue/no-deprecated-slot-attribute -->
          <ion-list lines="none" class="message-list">
            <ion-item v-for="chat in chats" :key="chat.name" class="message-item" button :detail="false">
              <ion-avatar slot="start" class="message-avatar">
                <ion-img :src="chat.avatar" :alt="chat.name" />
              </ion-avatar>

              <ion-label>
                <div class="message-head">
                  <h3>{{ chat.name }}</h3>
                  <span>{{ chat.time }}</span>
                </div>
                <p>{{ chat.preview }}</p>
              </ion-label>

              <ion-badge v-if="chat.unread > 0" class="unread-badge">{{ chat.unread }}</ion-badge>
            </ion-item>
          </ion-list>
          <!-- eslint-enable vue/no-deprecated-slot-attribute -->
        </section>

        <section class="quick-actions">
          <ion-button expand="block" class="action-btn primary-action">
            <ion-icon :icon="createOutline" />
            Ny besked
          </ion-button>
          <ion-button expand="block" fill="outline" class="action-btn secondary-action">
            <ion-icon :icon="mailOpenOutline" />
            Marker alle som laest
          </ion-button>
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
  IonList,
  IonItem,
  IonAvatar,
  IonImg,
  IonLabel,
  IonButton,
  IonIcon,
  IonBadge,
} from '@ionic/vue';
import { computed } from 'vue';
import { createOutline, funnelOutline, mailOpenOutline } from 'ionicons/icons';

const chats = [
  {
    name: 'Projektgruppe H5',
    preview: 'Kan vi moedes 13:15 i lokale B-204 for at fordele opgaver?',
    time: '09:42',
    unread: 3,
    avatar: 'https://images.unsplash.com/photo-1522071820081-009f0129c71c?q=80&w=300&auto=format&fit=crop',
  },
  {
    name: 'Mette - Systemudvikling',
    preview: 'Husk at laese API-opgaven inden timen i morgen.',
    time: '08:17',
    unread: 1,
    avatar: 'https://images.unsplash.com/photo-1487412720507-e7ab37603c6f?q=80&w=300&auto=format&fit=crop',
  },
  {
    name: 'Studievejledning',
    preview: 'Din samtale er booket onsdag kl. 10:00.',
    time: 'I gaar',
    unread: 0,
    avatar: 'https://images.unsplash.com/photo-1535713875002-d1d0cf377fde?q=80&w=300&auto=format&fit=crop',
  },
  {
    name: 'Signe - ERP Team',
    preview: 'Jeg har opdateret slides. Vil du tage intro-delen?',
    time: 'I gaar',
    unread: 2,
    avatar: 'https://images.unsplash.com/photo-1494790108377-be9c29b29330?q=80&w=300&auto=format&fit=crop',
  },
];

const unreadCount = computed(() => chats.reduce((sum, chat) => sum + chat.unread, 0));
const activeToday = computed(() => chats.filter((chat) => chat.time !== 'I gaar').length);
</script>

<style scoped>
.messages-content {
  --background: transparent;
}

.messages-shell {
  max-width: 880px;
  margin: 0 auto;
  padding: 20px 12px 24px;
}

.messages-hero {
  border-radius: 22px;
  border: 1px solid rgba(157, 184, 219, 0.26);
  background: linear-gradient(155deg, rgba(30, 45, 70, 0.84) 0%, rgba(43, 68, 105, 0.76) 60%, rgba(63, 95, 139, 0.7) 100%);
  box-shadow: 0 16px 30px rgba(11, 19, 35, 0.28);
  padding: 18px 16px;
}

.hero-kicker {
  margin: 0;
  font-size: 0.72rem;
  font-weight: 800;
  letter-spacing: 0.11em;
  text-transform: uppercase;
  color: #d9c9ae;
}

.hero-title {
  margin: 6px 0 3px;
  font-size: clamp(1.48rem, 4.2vw, 1.96rem);
  line-height: 1.1;
  font-weight: 800;
  color: #f6faff;
}

.hero-subtitle {
  margin: 0;
  max-width: 54ch;
  font-size: 0.92rem;
  color: rgba(229, 238, 252, 0.88);
}

.hero-stats {
  margin-top: 14px;
  display: grid;
  grid-template-columns: repeat(3, minmax(0, 1fr));
  gap: 8px;
}

.stat-card {
  border-radius: 12px;
  border: 1px solid rgba(180, 203, 237, 0.25);
  background: rgba(10, 20, 34, 0.24);
  padding: 9px 8px;
}

.stat-label {
  display: block;
  font-size: 0.7rem;
  font-weight: 700;
  letter-spacing: 0.08em;
  text-transform: uppercase;
  color: rgba(213, 227, 247, 0.84);
}

.stat-value {
  display: block;
  margin-top: 2px;
  font-size: 1.02rem;
  font-weight: 700;
  color: #f6faff;
}

.messages-panel {
  margin-top: 14px;
  border-radius: 20px;
  border: 1px solid rgba(144, 168, 200, 0.24);
  background: linear-gradient(180deg, rgba(17, 27, 42, 0.56) 0%, rgba(21, 33, 51, 0.48) 100%);
  padding: 12px;
}

.panel-head {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin: 2px 2px 10px;
}

.panel-title {
  margin: 0;
  font-size: 0.76rem;
  font-weight: 700;
  letter-spacing: 0.1em;
  text-transform: uppercase;
  color: rgba(203, 220, 244, 0.88);
}

.filter-btn {
  --color: #dbe9ff;
  font-size: 0.84rem;
  text-transform: none;
}

.filter-btn ion-icon {
  margin-right: 6px;
}

.message-list {
  background: transparent;
  padding: 0;
}

.message-item {
  --background: rgba(100, 127, 163, 0.14);
  --min-height: 72px;
  --inner-padding-end: 10px;
  border: 1px solid rgba(160, 186, 220, 0.22);
  border-radius: 14px;
}

.message-item + .message-item {
  margin-top: 8px;
}

.message-avatar {
  width: 42px;
  height: 42px;
  border: 1px solid rgba(198, 219, 246, 0.38);
}

.message-head {
  display: flex;
  justify-content: space-between;
  align-items: baseline;
  gap: 10px;
}

.message-head h3 {
  margin: 0;
  font-size: 0.95rem;
  font-weight: 700;
  color: #f1f7ff;
}

.message-head span {
  font-size: 0.72rem;
  color: rgba(205, 220, 243, 0.8);
}

.message-item p {
  margin: 3px 0 0;
  font-size: 0.87rem;
  color: rgba(221, 233, 250, 0.9);
}

.unread-badge {
  --background: #3f79d4;
  --color: #f4f8ff;
  border-radius: 999px;
}

.quick-actions {
  margin-top: 14px;
  display: grid;
  grid-template-columns: 1fr;
  gap: 8px;
}

.action-btn {
  --border-radius: 14px;
  min-height: 48px;
  text-transform: none;
  font-weight: 600;
}

.action-btn ion-icon {
  margin-right: 8px;
}

.primary-action {
  --background: linear-gradient(140deg, #3660a8 0%, #4d82d6 100%);
  --color: #f6faff;
}

.secondary-action {
  --border-color: rgba(164, 190, 224, 0.5);
  --color: #e4efff;
}

@media (max-width: 640px) {
  .messages-shell {
    padding: 16px 10px 20px;
  }

  .hero-stats {
    grid-template-columns: 1fr;
  }
}
</style>
