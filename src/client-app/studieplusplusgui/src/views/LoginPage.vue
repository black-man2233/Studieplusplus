<template>
  <ion-page class="login-page">
    <ion-content :fullscreen="true" class="login-content">
      <div class="login-shell">
        <header class="login-header">
          <p class="login-kicker">Studie+Plus</p>
          <h1 class="login-title">Log ind</h1>
          <p class="login-subtitle">Vaelg loginmetode for at fortsaette</p>
        </header>

        <section class="method-section" aria-label="Login metoder">
          <button
            class="method-card unilogin"
            type="button"
            @click="goTo('/login/unilogin')"
          >
            <span class="method-badge">UNI</span>
            <span class="method-text-wrap">
              <span class="method-title">UNILogin</span>
              <span class="method-subtitle">For elever og studerende</span>
            </span>
          </button>

          <button
            class="method-card direkte"
            type="button"
            @click="goTo('/login/direkte')"
          >
            <span class="method-badge">DIR</span>
            <span class="method-text-wrap">
              <span class="method-title">Direkte login</span>
              <span class="method-subtitle">Brugernavn og adgangskode</span>
            </span>
          </button>

          <button
            class="method-card mitid"
            type="button"
            @click="goTo('/login/mitid')"
          >
            <span class="method-badge">ID</span>
            <span class="method-text-wrap">
              <span class="method-title">MitID</span>
              <span class="method-subtitle">Offentlig digital identitet</span>
            </span>
          </button>
        </section>

        <div class="dev-login-wrap">
          <button class="dev-login-btn" type="button" @click="handleDevLogin">
            Dev login
          </button>
        </div>
      </div>
    </ion-content>
  </ion-page>
</template>

<script setup lang="ts">
import { useRouter } from 'vue-router';
import { IonPage, IonContent } from '@ionic/vue';
import { useAuth } from '@/composables/useAuth';

const router = useRouter();
const { login } = useAuth();

function goTo(path: '/login/unilogin' | '/login/mitid' | '/login/direkte') {
  router.push(path);
}

function handleDevLogin() {
  const ok = login('dev@studieplus.local', 'dev-login');
  if (ok) {
    router.replace('/tabs/home');
  }
}
</script>

<style scoped>
@import "../styles/views/login-page.css";
</style>
