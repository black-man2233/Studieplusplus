<template>
  <ion-page v-bind="$attrs">


    <ion-content :fullscreen="true" class="settings-content">
      <div class="settings-shell">
        <section class="settings-hero">
          <p class="hero-kicker">Studie+Plus</p>
          <h1 class="hero-title">Indstillinger</h1>
          <p class="hero-subtitle">Tilpas profil, lyd, tema og sikkerhed for en mere personlig oplevelse.</p>
        </section>

        <section class="settings-panel">
          <p class="panel-label">Generelt</p>
          <div class="settings-row">
            <SettingsModalComponent label="Profil" :options="profileOptions" />
          </div>
          <div class="settings-row">
            <SettingsModalComponent label="Lyd" :options="soundOptions" />
          </div>
          <div class="settings-row">
            <SettingsModalComponent label="Tema" :options="themeOptions" />
          </div>

          <p class="panel-label security-label">Sikkerhed</p>
          <div class="settings-row">
            <SettingsModalComponent
              label="Login"
              :options="loginOptions"
              @option-select="handleLoginOption"
            />
          </div>
        </section>
      </div>
    </ion-content>
  </ion-page>
</template>

<style scoped>
@import "../styles/views/settings-page.css";
</style>

<script setup lang="ts">
import { useRouter } from 'vue-router';
import {
  IonContent,
  IonPage,
} from '@ionic/vue';
import { defineAsyncComponent } from 'vue';
import { useAuth } from '@/composables/useAuth';

const router = useRouter();
const { logout } = useAuth();

const SettingsModalComponent = defineAsyncComponent(
  () => import('@/components/SettingsModalComponent.vue') as Promise<any>
);

const profileOptions = ['Rediger profil', 'Skift profilbillede', 'Privatlivsindstillinger'];
const soundOptions = ['Notifikationslyde', 'Ringetone', 'Vibration'];
const themeOptions = ['Lyst tema', 'Mørkt tema', 'Følg system'];
const loginOptions = ['Skift adgangskode', 'To-faktor autentificering', 'Log ud'];

function handleLoginOption(option: string) {
  if (option === 'Log ud') {
    logout();
    router.replace('/login');
  }
}

</script>
