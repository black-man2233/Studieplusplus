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
.provider-page,
.provider-content {
  --background: linear-gradient(160deg, #f6f9ff 0%, #edf3ff 55%, #f9fcff 100%);
}

.provider-shell {
  min-height: 100%;
  display: flex;
  flex-direction: column;
  justify-content: center;
  gap: 18px;
  padding: 36px 22px;
}

.provider-header {
  text-align: center;
}

.provider-kicker {
  margin: 0 0 8px;
  color: #d9c9ae;
  font-size: 0.8rem;
  font-weight: 800;
  letter-spacing: 0.1em;
  text-transform: uppercase;
}

.provider-title {
  margin: 0;
  color: #dce9ff;
  font-size: 2rem;
  font-weight: 900;
}

.provider-subtitle {
  margin: 8px 0 0;
  color: #b6c8e4;
}

.provider-card {
  width: min(100%, 430px);
  margin: 0 auto;
  border-radius: 16px;
  background: rgba(20, 35, 58, 0.72);
  border: 1px solid rgba(145, 167, 199, 0.35);
  box-shadow: 0 14px 28px rgba(10, 20, 35, 0.32);
  padding: 16px;
}

.provider-copy {
  margin: 0 0 14px;
  color: #cfddf3;
  font-size: 0.95rem;
}

.provider-btn {
  --background: linear-gradient(120deg, #005ea8 0%, #0a76cf 100%);
  --border-radius: 12px;
  height: 50px;
  font-weight: 700;
}

.back-btn {
  margin-top: 8px;
}
</style>
