<template>
  <div>
    <ion-button :id="triggerId" expand="block" fill="clear" class="setting-trigger">
      <span>{{ label }}</span>
      <ion-icon :icon="chevronForwardOutline" />
    </ion-button>

    <ion-modal ref="modal" :trigger="triggerId">
      <ion-header>
        <ion-toolbar>
          <ion-title>{{ label }}</ion-title>
        </ion-toolbar>
      </ion-header>
      <ion-content class="ion-padding">
        <ion-list inset>
          <ion-item v-for="option in options" :key="option" button detail @click="selectOption(option)">
            <ion-label>{{ option }}</ion-label>
          </ion-item>
        </ion-list>

        <ion-button expand="block" fill="outline" @click="dismiss">Close</ion-button>
      </ion-content>
    </ion-modal>
  </div>
</template>

<script lang="ts">
import { defineComponent, PropType, ref } from 'vue';
import {
  IonButton,
  IonIcon,
  IonModal,
  IonHeader,
  IonToolbar,
  IonTitle,
  IonContent,
  IonList,
  IonItem,
  IonLabel,
} from '@ionic/vue';
import { chevronForwardOutline } from 'ionicons/icons';

type ModalRef = {
  $el?: {
    dismiss: (data?: unknown, role?: string) => void;
  };
};

export default defineComponent({
  name: 'SettingsModalComponent',
  emits: ['option-select'],
  components: {
    IonButton,
    IonIcon,
    IonModal,
    IonHeader,
    IonToolbar,
    IonTitle,
    IonContent,
    IonList,
    IonItem,
    IonLabel,
  },
  props: {
    label: {
      type: String,
      required: true,
    },
    options: {
      type: Array as PropType<string[]>,
      required: true,
    },
  },
  setup(_, { emit }) {
    const triggerId = `settings-modal-${Math.random().toString(36).slice(2)}`;
    const modal = ref<ModalRef | null>(null);

    const dismiss = () => modal.value?.$el?.dismiss(null, 'cancel');
    const selectOption = (option: string) => {
      emit('option-select', option);
      dismiss();
    };

    return {
      chevronForwardOutline,
      dismiss,
      modal,
      selectOption,
      triggerId,
    };
  },
});
</script>

<style scoped>
ion-button {
  color: #f0f0f0;
}

.setting-trigger {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

ion-icon {
  color: #f0f0f0;
}
</style>