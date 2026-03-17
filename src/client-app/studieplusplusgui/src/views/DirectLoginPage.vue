<template>
  <ion-page class="direct-page">
    <ion-content :fullscreen="true" class="direct-content">
      <div class="direct-shell">
        <header class="direct-header">
          <p class="direct-kicker">Studie+Plus</p>
          <h1 class="direct-title">Direkte login</h1>
          <p class="direct-subtitle">Log ind med brugernavn og adgangskode</p>
        </header>

        <section class="direct-card">
          <ion-item class="login-item" lines="none">
            <ion-label position="stacked">Brugernavn</ion-label>
            <ion-input
              v-model="username"
              placeholder="brugernavn"
              autocomplete="username"
              required
            />
          </ion-item>

          <ion-item class="login-item" lines="none">
            <ion-label position="stacked">Adgangskode</ion-label>
            <ion-input
              v-model="password"
              type="password"
              placeholder="••••••••"
              autocomplete="current-password"
              required
            />
          </ion-item>

          <p v-if="errorMsg" class="error-text">{{ errorMsg }}</p>

          <ion-button expand="block" class="direct-btn" :disabled="loading" @click="submitDirectLogin">
            <ion-spinner v-if="loading" name="crescent" />
            <span v-else>Log ind</span>
          </ion-button>

          <ion-button fill="clear" class="back-btn" @click="goBack">Tilbage</ion-button>
        </section>
      </div>
    </ion-content>
  </ion-page>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import {
  IonPage,
  IonContent,
  IonItem,
  IonLabel,
  IonInput,
  IonButton,
  IonSpinner,
} from '@ionic/vue';
import { useAuth } from '@/composables/useAuth';

const router = useRouter();
const { login } = useAuth();

const username = ref('');
const password = ref('');
const loading = ref(false);
const errorMsg = ref('');

async function submitDirectLogin() {
  if (loading.value) {
    return;
  }

  if (!username.value.trim() || !password.value.trim()) {
    errorMsg.value = 'Udfyld brugernavn og adgangskode.';
    return;
  }

  errorMsg.value = '';
  loading.value = true;
  await new Promise((resolve) => setTimeout(resolve, 500));
  const ok = login(username.value, password.value);
  loading.value = false;

  if (ok) {
    router.replace('/tabs/home');
  }
}

function goBack() {
  router.replace('/login');
}
</script>

<style scoped>
.direct-page,
.direct-content {
  --background: linear-gradient(160deg, #f6f9ff 0%, #edf3ff 55%, #f9fcff 100%);
}

.direct-shell {
  min-height: 100%;
  display: flex;
  flex-direction: column;
  justify-content: center;
  gap: 18px;
  padding: 36px 22px;
}

.direct-header {
  text-align: center;
}

.direct-kicker {
  margin: 0 0 8px;
  color: #d9c9ae;
  font-size: 0.8rem;
  font-weight: 800;
  letter-spacing: 0.1em;
  text-transform: uppercase;
}

.direct-title {
  margin: 0;
  color: #dce9ff;
  font-size: 2rem;
  font-weight: 900;
}

.direct-subtitle {
  margin: 8px 0 0;
  color: #b6c8e4;
}

.direct-card {
  width: min(100%, 430px);
  margin: 0 auto;
  border-radius: 16px;
  background: rgba(20, 35, 58, 0.72);
  border: 1px solid rgba(145, 167, 199, 0.35);
  box-shadow: 0 14px 28px rgba(10, 20, 35, 0.32);
  padding: 16px;
}

.login-item {
  --background: rgba(131, 153, 183, 0.22);
  --border-radius: 10px;
  --padding-start: 12px;
  --padding-end: 12px;
  margin-bottom: 10px;
  border: none;
}

:deep(.login-item ion-label) {
  color: #d5e2f5;
}

:deep(.login-item ion-input) {
  --color: #f3f7ff;
  --placeholder-color: #c3d2e8;
}

.error-text {
  margin: 2px 0 10px;
  color: #c53d3d;
  font-size: 0.86rem;
}

.direct-btn {
  --background: linear-gradient(120deg, #0c2f69 0%, #0f4ea8 100%);
  --color: #f2f7ff;
  --border-radius: 12px;
  height: 50px;
  font-weight: 700;
}

.back-btn {
  margin-top: 8px;
}
</style>
