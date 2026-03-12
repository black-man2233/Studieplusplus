<template>
  <ion-page>
    <ion-header>
      <ion-toolbar>
        <ion-title>Profil</ion-title>
      </ion-toolbar>
    </ion-header>
    <ion-content :fullscreen="true">
      <ion-header collapse="condense">
        <ion-toolbar>
          <ion-title size="large">Profil</ion-title>
        </ion-toolbar>
      </ion-header>

      <div id="profile-header">
        <ion-avatar id="profile-avatar">
          <ion-img
            src="https://fastly.picsum.photos/id/866/1920/1080.jpg?hmac=dNBuPEp10RySTqlc5EpGw7QyrFIpBd2X88r1Ixla7pw"
            alt="Profile Picture" />
        </ion-avatar>
        
      </div>
      <div id="profile-info">
        <ion-label id="profile-name">Kevin</ion-label>
        <br/>
        <ion-label id="profile-education">Datateknikker med speciale i programmering</ion-label>
        <br/>
        <ion-label id="profile-class">H5PD010126</ion-label>
      </div>

      <div class="profile-tabs">
        <ion-accordion-group>
          <ion-accordion value="First">
            <ion-item slot="header" color="light">
              <ion-label>Personlig Information</ion-label>
            </ion-item>
            <div class="ion-padding" slot="content">First</div>
          </ion-accordion>
          <ion-accordion value="second">
            <ion-item slot="header" color="light">
              <ion-label>Skole Forløb</ion-label>
            </ion-item>
            <div class="ion-padding" slot="content">Second</div>
          </ion-accordion>
          <ion-accordion value="third">
            <ion-item slot="header" color="light">
              <ion-label>Noter</ion-label>
            </ion-item>
            <div class="ion-padding" slot="content">Third</div>
          </ion-accordion>
          <ion-accordion value="fourth">
            <ion-item slot="header" color="light">
              <ion-label>Arbejdsgiver</ion-label>
            </ion-item>
            <ion-card slot="content">
              <ion-card-header>
                <ion-card-title>Card Title</ion-card-title>
                <ion-card-subtitle>Card Subtitle</ion-card-subtitle>
              </ion-card-header>
              <ion-card-content>
                Card Content
              </ion-card-content>
            </ion-card>
          </ion-accordion>
        </ion-accordion-group>
      </div>

    </ion-content>
  </ion-page>
</template>

<script setup lang="ts">
import { IonPage, IonHeader, IonToolbar, IonTitle, IonContent, IonImg, IonAvatar, IonLabel, IonItem, IonAccordion, IonCard, IonCardTitle, IonCardSubtitle, IonCardContent, IonCardHeader, IonAccordionGroup,  } from '@ionic/vue';
</script>

<style scoped>
#profile-header {
  max-width: fit-content;
  margin-left: auto;
  margin-right: auto;
  padding: 2vh 0 0.5vh 0;
}
#profile-info {
  max-width: fit-content;
  margin-left: auto;
  margin-right: auto;
  text-align: center;
  display: block;
  margin-bottom: 1vh;
}

#profile-info #profile-name {
  font-size: 1.5em;
  margin-left: auto;
  margin-right: auto;
}
#profile-education {
  font-size: 0.9em;
  color: rgba(255,255,255,0.5);

}
#profile-class {
  text-align: center;
  font-size: 0.9em;
  color: rgba(255,255,255,0.5);
}
</style>
