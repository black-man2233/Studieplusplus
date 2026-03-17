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
          <p class="hero-subtitle">Få hurtigt overblik over dine samtaler med klasse, lærere og gruppe.</p>

          <div class="hero-stats">
            <div class="stat-card">
              <span class="stat-label">Ulæste</span>
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
            <ion-item
              v-for="chat in chats"
              :key="chat.name"
              class="message-item"
              button
              :detail="true"
              @click="openChat(chat)"
            >
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

          <ion-modal :is-open="isChatOpen" @didDismiss="closeChat">
            <ion-header class="chat-modal-header">
              <ion-toolbar class="chat-toolbar">
                <div v-if="selectedChat" class="chat-header-main">
                  <ion-avatar class="chat-header-avatar">
                    <ion-img :src="selectedChat.avatar" :alt="selectedChat.name" />
                  </ion-avatar>
                  <div class="chat-header-copy">
                    <p class="chat-header-name">{{ selectedChat.name }}</p>
                    <p class="chat-header-status"><span class="status-dot"></span>Aktiv nu</p>
                  </div>
                </div>
                <ion-button fill="clear" class="chat-header-close" @click="closeChat" aria-label="Luk chat">
                  <ion-icon :icon="closeOutline" />
                </ion-button>
              </ion-toolbar>
            </ion-header>
            <ion-content class="chat-modal-content ion-padding">
              <div class="chat-thread">
                <div
                  v-for="message in activeMessages"
                  :key="message.id"
                  class="bubble-row"
                  :class="message.sender === 'me' ? 'is-me' : 'is-them'"
                >
                  <div class="bubble">{{ message.text }}</div>
                  <span class="bubble-time">{{ message.time }}</span>
                </div>
              </div>
            </ion-content>

            <ion-footer class="chat-footer">
              <ion-toolbar>
                <div class="chat-composer">
                  <input
                    v-model="messageDraft"
                    type="text"
                    class="chat-input"
                    placeholder="Skriv en besked..."
                    @keydown.enter.prevent="sendMessage"
                  />
                  <ion-button class="send-btn" @click="sendMessage">
                    <ion-icon :icon="sendOutline" />
                  </ion-button>
                </div>
              </ion-toolbar>
            </ion-footer>
          </ion-modal>
        </section>

        <section class="quick-actions">
          <ion-button expand="block" class="action-btn primary-action">
            <ion-icon :icon="createOutline" />
            Ny besked
          </ion-button>
          <ion-button expand="block" fill="outline" class="action-btn secondary-action">
            <ion-icon :icon="mailOpenOutline" />
            Marker alle som læst
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
  IonModal,
  IonFooter,
} from '@ionic/vue';
import { computed, ref } from 'vue';
import { closeOutline, createOutline, funnelOutline, mailOpenOutline, sendOutline } from 'ionicons/icons';

type ChatItem = {
  name: string;
  preview: string;
  time: string;
  unread: number;
  avatar: string;
};

type ChatMessage = {
  id: number;
  sender: 'me' | 'them';
  text: string;
  time: string;
};

const chats = ref<ChatItem[]>([
  {
    name: 'Projektgruppe H5',
    preview: 'Kan vi mødes 13:15 i lokale B-204 for at fordele opgaver?',
    time: '09:42',
    unread: 3,
    avatar: 'https://images.unsplash.com/photo-1522071820081-009f0129c71c?q=80&w=300&auto=format&fit=crop',
  },
  {
    name: 'Mette - Systemudvikling',
    preview: 'Husk at læse API-opgaven inden timen i morgen.',
    time: '08:17',
    unread: 1,
    avatar: 'https://images.unsplash.com/photo-1487412720507-e7ab37603c6f?q=80&w=300&auto=format&fit=crop',
  },
  {
    name: 'Studievejledning',
    preview: 'Din samtale er booket onsdag kl. 10:00.',
    time: 'I går',
    unread: 0,
    avatar: 'https://images.unsplash.com/photo-1535713875002-d1d0cf377fde?q=80&w=300&auto=format&fit=crop',
  },
  {
    name: 'Signe - ERP Team',
    preview: 'Jeg har opdateret slides. Vil du tage intro-delen?',
    time: 'I går',
    unread: 2,
    avatar: 'https://images.unsplash.com/photo-1494790108377-be9c29b29330?q=80&w=300&auto=format&fit=crop',
  },
  {
    name: 'Anders - Praktik',
    preview: 'Tak for status. Kan du sende commit-link inden kl. 15?',
    time: 'I går',
    unread: 1,
    avatar: 'https://images.unsplash.com/photo-1500648767791-00dcc994a43e?q=80&w=300&auto=format&fit=crop',
  },
  {
    name: 'Lene - Studieadmin',
    preview: 'Vi mangler din underskrift på praktikaftalen.',
    time: 'Man',
    unread: 0,
    avatar: 'https://images.unsplash.com/photo-1580489944761-15a19d654956?q=80&w=300&auto=format&fit=crop',
  },
  {
    name: 'API Workshop',
    preview: 'Nye noter er lagt op i Teams under "Materialer".',
    time: 'Man',
    unread: 4,
    avatar: 'https://images.unsplash.com/photo-1519389950473-47ba0277781c?q=80&w=300&auto=format&fit=crop',
  },
  {
    name: 'Sofie - Design',
    preview: 'Jeg har opdateret komponentfarverne til staging.',
    time: 'Søn',
    unread: 0,
    avatar: 'https://images.unsplash.com/photo-1544005313-94ddf0286df2?q=80&w=300&auto=format&fit=crop',
  },
  {
    name: 'Mikkel - Mentor',
    preview: 'Flot fremgang. Hold fokus på testdokumentation.',
    time: 'Søn',
    unread: 2,
    avatar: 'https://images.unsplash.com/photo-1541534401786-2077eed87a72?q=80&w=300&auto=format&fit=crop',
  },
  {
    name: 'Klasseforum H5',
    preview: 'Nogen der vil bytte torsdagens fremlæggelses-slot?',
    time: 'Lør',
    unread: 5,
    avatar: 'https://images.unsplash.com/photo-1529156069898-49953e39b3ac?q=80&w=300&auto=format&fit=crop',
  },
]);

const chatThreads = ref<Record<string, ChatMessage[]>>({
  'Projektgruppe H5': [
    { id: 1, sender: 'them', text: 'Kan vi mødes 13:15 i lokale B-204 for at fordele opgaver?', time: '09:42' },
    { id: 2, sender: 'me', text: 'Ja, det passer. Jeg tager laptop med.', time: '09:45' },
  ],
  'Mette - Systemudvikling': [
    { id: 3, sender: 'them', text: 'Husk at læse API-opgaven inden timen i morgen.', time: '08:17' },
    { id: 4, sender: 'me', text: 'Noteret, jeg gennemgår den i eftermiddag.', time: '08:19' },
  ],
  'Signe - ERP Team': [
    { id: 5, sender: 'them', text: 'Jeg har opdateret slides. Vil du tage intro-delen?', time: 'I går' },
    { id: 6, sender: 'me', text: 'Ja, jeg tager intro + demo flow.', time: 'I går' },
  ],
});

let messageIdCounter = 100;
const nextMessageId = () => {
  messageIdCounter += 1;
  return messageIdCounter;
};

const selectedChat = ref<ChatItem | null>(null);
const isChatOpen = ref(false);
const activeMessages = ref<ChatMessage[]>([]);
const messageDraft = ref('');

const formatNow = () =>
  new Date().toLocaleTimeString('da-DK', {
    hour: '2-digit',
    minute: '2-digit',
  });

const fallbackThread = (chat: ChatItem): ChatMessage[] => [
  { id: nextMessageId(), sender: 'them', text: chat.preview, time: chat.time },
];

const persistActiveThread = () => {
  if (!selectedChat.value) return;
  chatThreads.value[selectedChat.value.name] = [...activeMessages.value];
};

const openChat = (chat: ChatItem) => {
  selectedChat.value = chat;
  activeMessages.value = [...(chatThreads.value[chat.name] ?? fallbackThread(chat))];
  chat.unread = 0;
  messageDraft.value = '';
  isChatOpen.value = true;
};

const closeChat = () => {
  persistActiveThread();
  isChatOpen.value = false;
  selectedChat.value = null;
};

const sendMessage = () => {
  const text = messageDraft.value.trim();
  if (!text || !selectedChat.value) return;

  const now = formatNow();
  activeMessages.value.push({
    id: nextMessageId(),
    sender: 'me',
    text,
    time: now,
  });

  selectedChat.value.preview = text;
  selectedChat.value.time = now;
  messageDraft.value = '';
  persistActiveThread();

  const openChatName = selectedChat.value.name;
  window.setTimeout(() => {
    if (!selectedChat.value || selectedChat.value.name !== openChatName) return;

    activeMessages.value.push({
      id: nextMessageId(),
      sender: 'them',
      text: 'Perfekt, tak. Vi skriver videre her.',
      time: formatNow(),
    });
    persistActiveThread();
  }, 650);
};

const unreadCount = computed(() => chats.value.reduce((sum, chat) => sum + chat.unread, 0));
const activeToday = computed(() => chats.value.filter((chat) => chat.time !== 'I går').length);
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
  display: flex;
  flex-direction: column;
  min-height: 0;
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
  max-height: clamp(290px, 44vh, 500px);
  overflow-y: auto;
  overscroll-behavior: contain;
  scrollbar-width: thin;
  scrollbar-color: rgba(162, 187, 222, 0.55) transparent;
}

.message-list::-webkit-scrollbar {
  width: 6px;
}

.message-list::-webkit-scrollbar-thumb {
  background: rgba(162, 187, 222, 0.55);
  border-radius: 999px;
}

.message-item {
  --background: rgba(100, 127, 163, 0.14);
  --min-height: 72px;
  --inner-padding-end: 10px;
  border: 1px solid rgba(160, 186, 220, 0.22);
  border-radius: 14px;
}

.message-item:hover {
  --background: rgba(112, 141, 180, 0.2);
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

.chat-modal-content {
  --background: linear-gradient(180deg, rgba(16, 26, 43, 0.96) 0%, rgba(22, 35, 57, 0.96) 100%);
}

.chat-modal-header {
  --background: linear-gradient(180deg, rgba(20, 34, 56, 0.98) 0%, rgba(18, 31, 52, 0.98) 100%);
  border-bottom: 1px solid rgba(152, 181, 220, 0.18);
  box-shadow: 0 6px 16px rgba(8, 14, 26, 0.3);
}

.chat-toolbar {
  --background: transparent;
  --min-height: 64px;
  padding: 0 4px;
  position: relative;
}

.chat-header-main {
  display: flex;
  align-items: center;
  gap: 10px;
  min-width: 0;
  padding-right: 42px;
}

.chat-header-avatar {
  width: 42px;
  height: 42px;
  border: 1px solid rgba(178, 201, 232, 0.44);
  box-shadow: 0 4px 10px rgba(7, 15, 29, 0.34);
}

.chat-header-copy {
  display: flex;
  flex-direction: column;
  gap: 1px;
  min-width: 0;
}

.chat-header-name {
  margin: 0;
  font-size: 0.96rem;
  font-weight: 700;
  color: #edf4ff;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

.chat-header-status {
  margin: 0;
  display: inline-flex;
  align-items: center;
  gap: 6px;
  font-size: 0.74rem;
  color: rgba(193, 215, 245, 0.82);
}

.status-dot {
  width: 7px;
  height: 7px;
  border-radius: 999px;
  background: #72e89d;
  box-shadow: 0 0 0 4px rgba(114, 232, 157, 0.16);
}

.chat-header-close {
  position: absolute;
  top: 10px;
  right: 8px;
  margin-left: 0;
  --color: #e7f1ff;
  --background: transparent;
  --border-radius: 10px;
  --padding-start: 6px;
  --padding-end: 6px;
  min-height: 32px;
  border: 0;
  box-shadow: none;
  text-transform: none;
}

.chat-header-close ion-icon {
  margin-right: 0;
  font-size: 1.2rem;
}

.chat-thread {
  display: flex;
  flex-direction: column;
  gap: 8px;
  min-height: 100%;
  justify-content: flex-end;
  padding: 4px 2px 12px;
}

.bubble-row {
  display: flex;
  flex-direction: column;
  max-width: 82%;
}

.bubble-row.is-me {
  margin-left: auto;
  align-items: flex-end;
}

.bubble-row.is-them {
  margin-right: auto;
  align-items: flex-start;
}

.bubble {
  padding: 10px 12px;
  border-radius: 14px;
  line-height: 1.34;
  font-size: 0.92rem;
  border: 1px solid rgba(171, 194, 224, 0.25);
}

.bubble-row.is-me .bubble {
  background: linear-gradient(140deg, rgba(54, 96, 168, 0.95) 0%, rgba(77, 130, 214, 0.9) 100%);
  color: #f4f8ff;
  border-bottom-right-radius: 6px;
}

.bubble-row.is-them .bubble {
  background: rgba(88, 113, 149, 0.26);
  color: #eef5ff;
  border-bottom-left-radius: 6px;
}

.bubble-time {
  margin-top: 3px;
  font-size: 0.68rem;
  color: rgba(194, 213, 240, 0.72);
}

.chat-footer {
  --background: rgba(17, 29, 48, 0.98);
}

.chat-composer {
  display: flex;
  align-items: center;
  gap: 8px;
  padding: 4px 4px 6px;
}

.chat-input {
  flex: 1;
  min-height: 42px;
  border: 1px solid rgba(158, 185, 223, 0.35);
  border-radius: 12px;
  background: rgba(71, 96, 130, 0.26);
  color: #f0f6ff;
  padding: 0 12px;
  outline: none;
  font-size: 0.9rem;
}

.chat-input::placeholder {
  color: rgba(198, 216, 241, 0.7);
}

.send-btn {
  --background: linear-gradient(140deg, #3660a8 0%, #4d82d6 100%);
  --color: #f6faff;
  --border-radius: 12px;
  min-height: 42px;
}

@media (max-width: 640px) {
  .messages-shell {
    padding: 16px 10px 20px;
  }

  .hero-stats {
    grid-template-columns: repeat(3, minmax(0, 1fr));
    gap: 6px;
  }

  .message-list {
    max-height: clamp(260px, 40vh, 420px);
  }

  .stat-label {
    font-size: 0.62rem;
  }

  .stat-value {
    font-size: 0.92rem;
  }
}
</style>
