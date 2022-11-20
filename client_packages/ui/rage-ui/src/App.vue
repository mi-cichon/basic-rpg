<template>
  <router-view></router-view>

  <NotificationComponent
    v-if="displayNotification"
    :message="notificationMessage"
    :type="notificationType"
  ></NotificationComponent>

  <HudComponent v-if="displayHud"></HudComponent>

  <ChatComponent
    v-if="displayChat"
    ref="chat"
    :show-input="showChatInput"
    @chat-message-sent="showChatInput = false"
  ></ChatComponent>

  <DefaultSpeedometerComponent
    ref="speedometerComponent"
    v-if="displaySpeedometer"
    :max-speed="speedometerMaxSpeed"
    :veh-name="speedometerVehName"
  ></DefaultSpeedometerComponent>
</template>

<script lang="ts">
import rpc from "rage-rpc";
import NotificationComponent from "./components/NotificationComponent.vue";
import HudComponent from "./components/HudComponent.vue";
import ChatComponent from "./components/ChatComponent.vue";
import DefaultSpeedometerComponent from "./components/speedometer/DefaultSpeedometerComponent.vue";

export default {
  data() {
    return {
      showLogin: true,
      displayNotification: false,
      notificationMessage: "",
      notificationType: 0,
      displayHud: false,
      displayChat: false,
      displaySpeedometer: false,
      speedometerMaxSpeed: 0,
      speedometerVehName: "",
      showChatInput: false,
    };
  },
  created() {
    rpc.register("ui_showLoginComponent", (state: boolean) => {
      state ? this.$router.push("/login") : this.$router.push("/");
    });
    rpc.register("ui_showNotification", (args) => {
      const message: string = args.message;
      const type: number = args.type;
      this.showNotification(message, type);
    });
    rpc.register("ui_loginCompleted", (lastPos: boolean) => {
      this.$router.push("/spawnselection/" + lastPos.toString());
    });
    rpc.register("ui_spawnSelectionCompleted", () => {
      this.$router.push("/");
      this.displayChat = true;
      this.displayHud = true;
    });
    rpc.register("ui_showChatInput", (state) => {
      if (this.displayChat) {
        this.showChatInput = state;
      }
    });
    rpc.register("ui_displayMessage", (message) => {
      if (this.displayChat) {
        (this.$refs.chat as any).addMessage(message);
      }
    });
    rpc.register("ui_displaySpeedometer", (values) => {
      if (values.state) {
        this.displaySpeedometer = true;
        this.speedometerMaxSpeed = values.speed;
        this.speedometerVehName = values.name;
        return;
      }
      this.displaySpeedometer = false;
    });
    rpc.register("ui_updateSpeedometer", (values) => {
      if (!this.displaySpeedometer) {
        return;
      }
      (this.$refs.speedometerComponent as any).update(
        values.speed,
        values.street
      );
    });
  },
  methods: {
    showNotification(message: string, type: number) {
      if (this.displayNotification) {
        return;
      }
      this.notificationMessage = this.$t(message);
      this.notificationType = type;
      this.displayNotification = true;
      setTimeout(() => {
        this.displayNotification = false;
      }, 5000);
    },
  },
  components: {
    NotificationComponent,
    HudComponent,
    ChatComponent,
    DefaultSpeedometerComponent,
  },
};
</script>

<style>
@import "./assets/components.css";
</style>

<style>
html,
body {
  user-select: none;
  background-repeat: no-repeat;
  background-position: center;
  background-size: cover;
  box-sizing: border-box;
  overflow: hidden;
}
</style>
