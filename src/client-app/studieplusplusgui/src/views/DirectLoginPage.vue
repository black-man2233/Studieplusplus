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
  const ok = login(username.value, password.value, 'direct');
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
@import "../styles/views/direct-login-page.css";
</style>
