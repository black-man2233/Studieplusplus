<template>
  <div>
    <ion-button :id="triggerId" expand="block" fill="clear">
      <ion-icon :icon="chevronForwardOutline" slot="end" />
      {{ label }}
    </ion-button>

    <ion-modal
      ref="modal"
      :trigger="triggerId"
      :can-dismiss="canDismiss"
      :presenting-element="presentingElement"
    >
      <ion-header>
        <ion-toolbar>
          <ion-title>{{ label }}</ion-title>
          <ion-buttons slot="end">
            <ion-button @click="dismiss()">Close</ion-button>
          </ion-buttons>
        </ion-toolbar>
      </ion-header>
      <ion-content>
        <ion-item>
          <ion-checkbox id="terms" @ionChange="onTermsChanged" :checked="canDismiss">
            <div class="ion-text-wrap">
              Do you accept the terms and conditions?
            </div>
          </ion-checkbox>
        </ion-item>
      </ion-content>
    </ion-modal>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import {
  IonButton,
  IonIcon,
  IonModal,
  IonHeader,
  IonToolbar,
  IonTitle,
  IonButtons,
  IonContent,
  IonItem,
  IonCheckbox,
} from '@ionic/vue';
import { chevronForwardOutline } from 'ionicons/icons';

const props = defineProps({
  label: { type: String, required: true },
});

const triggerId = `settings-modal-${Math.random().toString(36).slice(2)}`;
const modal = ref<any>();
const canDismiss = ref(false);
const presentingElement = ref<HTMLElement | null>(null);

const dismiss = () => modal.value.$el.dismiss(null, 'cancel');

const onTermsChanged = (e: CustomEvent) => {
  canDismiss.value = e.detail.checked;
};
</script>

<style scoped>
ion-button {
  color: #f0f0f0;
}

ion-icon {
  color: #f0f0f0;
  position: absolute;
  right: 0;
}
</style>