<template>
  <ion-page>
    <ion-header>
      <ion-toolbar>
        <ion-title>Hjem</ion-title>
      </ion-toolbar>
    </ion-header>
    <ion-content :fullscreen="true">
      <ion-header collapse="condense">
        <ion-toolbar>
          <ion-title size="large">Hjem</ion-title>
        </ion-toolbar>
      </ion-header>
            
      <ion-grid>
        <ion-row>
          <ion-col>1</ion-col>
          <ion-col>2</ion-col>
        </ion-row>
        <ion-row>
          <ion-col>3</ion-col>
          <ion-col>4</ion-col>
        </ion-row>
      </ion-grid>

      <ion-grid>

      </ion-grid>

    </ion-content>
  </ion-page>
</template>

<script setup lang="ts">
import { IonPage, IonHeader, IonToolbar, IonTitle, IonContent } from '@ionic/vue';
import { IonCol, IonGrid, IonRow } from '@ionic/vue';

</script>

<style scoped>
ion-grid {
  padding: 10%;
  margin-top: 10%;
}
ion-col {
  border: 1px solid #ccc;
  padding: 20px;
  text-align: center;
}
</style>