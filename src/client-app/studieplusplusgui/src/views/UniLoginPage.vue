<template>
  <ion-page class="provider-page">
    <ion-content :fullscreen="true" class="provider-content">
      <div class="provider-shell">
        <header class="provider-header">
          <p class="provider-kicker">Studie+Plus</p>
          <h1 class="provider-title">UNILogin</h1>
          <p class="provider-subtitle">Log ind med din skolekonto</p>
        </header>

        <section class="provider-card">
          <p class="provider-copy">Indtast dit UNILogin brugernavn og kodeord.</p>

          <ion-item class="login-item" lines="none">
            <ion-label position="stacked">Brugernavn</ion-label>
            <ion-input
              v-model="username"
              placeholder="abc123"
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

          <ion-button expand="block" class="provider-btn" :disabled="loading" @click="startUniLogin">
            <ion-spinner v-if="loading" name="crescent" />
            <span v-else>Log ind med UNILogin</span>
          </ion-button>

          <ion-button fill="clear" class="back-btn" @click="goBack">
            Tilbage
          </ion-button>
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
  IonButton,
  IonSpinner,
  IonItem,
  IonLabel,
  IonInput,
} from '@ionic/vue';
import { useAuth } from '@/composables/useAuth';

const router = useRouter();
const { login } = useAuth();
const loading = ref(false);
const username = ref('');
const password = ref('');
const errorMsg = ref('');

async function startUniLogin() {
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
@import "../styles/views/uni-login-page.css";
</style>
