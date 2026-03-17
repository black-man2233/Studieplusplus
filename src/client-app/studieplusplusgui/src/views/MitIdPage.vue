<template>
  <ion-page class="provider-page">
    <ion-content :fullscreen="true" class="provider-content">
      <div class="provider-shell">
        <header class="provider-header">
          <p class="provider-kicker">Studie+Plus</p>
          <h1 class="provider-title">MitID</h1>
          <p class="provider-subtitle">MitID support er midlertidigt utilgaengelig</p>
        </header>

        <section class="provider-card">
          <p class="provider-copy">
            MitID login er ikke tilgaengelig i appen lige nu.
          </p>

          <ion-button expand="block" class="provider-btn" @click="showUnavailable = true">
            Vis besked
          </ion-button>

          <ion-button fill="clear" class="back-btn" @click="goBack">
            Tilbage
          </ion-button>
        </section>

        <ion-alert
          :is-open="showUnavailable"
          header="MitID"
          message="MitID is not available at this time."
          :buttons="['OK']"
          @didDismiss="showUnavailable = false"
        />
      </div>
    </ion-content>
  </ion-page>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import { IonPage, IonContent, IonButton, IonAlert } from '@ionic/vue';

const router = useRouter();
const showUnavailable = ref(false);

onMounted(() => {
  showUnavailable.value = true;
});

function goBack() {
  router.replace('/login');
}
</script>

<style scoped>
@import "../styles/views/mitid-page.css";
</style>
