<template>
  <div class="body">
    <div class="scroll" ref="chatScroll">
      <div class="message" v-for="message in messages" :key="message.id">
        <div class="avatar"></div>
        <div class="content">
          <RegularMessageComponent
            v-if="message.type == 0 || message.type == 1 || message.type == 2"
            :message="message"
            :type-color="getTypeColor(message.type)"
            :type-icon="getTypeIcon(message.type)"
            :permission-color="getPermissionColor(message.type)"
          ></RegularMessageComponent>

          <TransferMessageComponent
            v-if="message.type == 3"
            :message="message"
            :type-color="getTypeColor(message.type)"
            :type-icon="getTypeIcon(message.type)"
            :permission-color="getPermissionColor(message.type)"
          ></TransferMessageComponent>

          <InfoMessageComponent
            v-if="message.type == 9"
            :message="message"
            :type-color="getTypeColor(message.type)"
            :type-icon="getTypeIcon(message.type)"
          ></InfoMessageComponent>

          <PrivateMessageComponent
            v-if="message.type == 4"
            :message="message"
            :type-color="getTypeColor(message.type)"
            :type-icon="getTypeIcon(message.type)"
            :permission-color="getPermissionColor(message.type)"
          ></PrivateMessageComponent>

          <PenaltyMessageComponent
            v-if="
              message.type == 5 ||
              message.type == 6 ||
              message.type == 7 ||
              message.type == 8
            "
            :message="message"
            :type-color="getTypeColor(message.type)"
            :type-icon="getTypeIcon(message.type)"
            :permission-color="getPermissionColor(message.type)"
          ></PenaltyMessageComponent>
        </div>
      </div>
    </div>
    <ChatInputComponent
      v-if="showInput"
      @message-sent="$emit('chatMessageSent')"
    ></ChatInputComponent>
  </div>
</template>

<script lang="ts">
import RegularMessageComponent from "./messages/RegularMessageComponent.vue";
import TransferMessageComponent from "./messages/TransferMessageComponent.vue";
import InfoMessageComponent from "./messages/InfoMessageComponent.vue";
import PrivateMessageComponent from "./messages/PrivateMessageComponent.vue";
import PenaltyMessageComponent from "./messages/PenaltyMessageComponent.vue";
import ChatInputComponent from "./messages/ChatInputComponent.vue";

declare interface Message {
  id: number;
  name: string;
  message: string;
  type: number;
  permission: number;
  from: boolean;
  time: string;
}
export default {
  data() {
    return {
      messages: [] as Message[],
    };
  },
  methods: {
    getTypeIcon(type: number) {
      switch (type) {
        case 0:
          return "fa-solid fa-street-view";
        case 1:
          return "fa-solid fa-globe";
        case 2:
          return "fa-solid fa-user-group";
        case 3:
          return "fa-solid fa-hand-holding-dollar";
        case 4:
          return "fa-solid fa-comments";
        case 5:
          return "fa-solid fa-comment-slash";
        case 6:
          return "fa-solid fa-id-card";
        case 7:
          return "fa-solid fa-user-slash";
        case 8:
          return "fa-solid fa-gavel";
        case 9:
          return "fa-solid fa-info";
      }
    },
    getTypeColor(type: number) {
      switch (type) {
        case 0:
          return "#bfbfbf";
        case 1:
          return "#00d9ff";
        case 2:
          return "#8ce038";
        case 3:
          return "#007a12";
        case 4:
          return "#7000d9";
        case 5:
          return "#e80505";
        case 6:
          return "#e80505";
        case 7:
          return "#e80505";
        case 8:
          return "#e80505";
        case 9:
          return "#f8ff36";
      }
    },
    getPermissionColor(permission: number) {
      switch (permission) {
        case 0:
          return "#fff";
        case 1:
          return "#d90000";
        case 2:
          return "#ff2e2e";
        case 3:
          return "#ff8f2e";
        case 4:
          return "#3cff2e";
      }
    },
    addMessage(message: any) {
      this.messages.push(message);
      var scroll = this.$refs.chatScroll as any;
      this.$nextTick(() => {
        scroll.scrollTop = scroll.scrollHeight;
      });
    },
  },
  components: {
    RegularMessageComponent,
    TransferMessageComponent,
    InfoMessageComponent,
    PrivateMessageComponent,
    PenaltyMessageComponent,
    ChatInputComponent,
  },
  props: ["showInput"],
};
</script>

<style scoped>
.body {
  width: 55rem;
  height: 30rem;
  position: relative;
  left: 1rem;
  top: 1rem;
  font-family: "Roboto";
}

.scroll {
  height: 100%;
  width: 100%;
  box-sizing: border-box;
  overflow-y: scroll;
}
.scroll::-webkit-scrollbar {
  display: none;
}

/* Hide scrollbar for IE, Edge and Firefox */
.scroll {
  -ms-overflow-style: none; /* IE and Edge */
  scrollbar-width: none; /* Firefox */
}

.message {
  height: 5rem;
  width: auto;
  max-width: 50rem;
  padding: 0.1rem;
  box-sizing: border-box;
  display: flex;
  justify-content: flex-start;
  align-items: center;
}

.avatar {
  background-image: url("./../assets/images/avatar.png");
  background-position: center;
  background-size: cover;
  background-repeat: no-repeat;
  width: 4rem;
  height: 4rem;
  border-radius: 50%;
  border: 4px solid #12192c;
  box-sizing: border-box;
}

.content {
  max-height: 5rem;
  height: auto;
  width: auto;
  display: flex;
  flex-direction: column;
  justify-content: center;
  padding-left: 0.8rem;
  box-sizing: border-box;
  max-width: 40rem;
}
</style>

<style>
.chat_messageWrapper {
  position: relative;
}

.chat_messageContent {
  background-color: rgb(18, 25, 44, 0.9);
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: flex-start;
  border-top-right-radius: 1rem;
  border-bottom-right-radius: 1rem;
  padding-right: 1rem;
  padding-left: 0.5rem;
  padding-bottom: 0.2rem;
}
.chat_meta {
  height: 1.5rem;
  display: flex;
  justify-content: flex-start;
  align-items: center;
  padding-left: 0.2rem;
}

.chat_text {
  height: auto;
  font-size: 0.9rem;
  color: white;
  padding: 0.1rem;
  padding-left: 0.5rem;
  padding-right: 1rem;
  border-top-right-radius: 2rem;
  border-bottom-right-radius: 2rem;
  box-sizing: border-box;
  min-height: 1.2rem;
}

.chat_bar {
  height: 100%;
  width: 0.4rem;
  border-top-left-radius: 1rem;
  border-bottom-left-radius: 1rem;
  box-sizing: border-box;
  position: absolute;
  left: -0.4rem;
}

.chat_type {
  font-size: 0.9rem;
  height: 1.5rem;
  width: 1.5rem;
  line-height: 1.5rem;
  text-align: center;
}

.chat_name {
  font-size: 0.9rem;
  height: 1.5rem;
  width: auto;
  line-height: 1.5rem;
  text-align: center;
  color: white;
  padding: 0.25rem;
  padding-left: 0.2rem;
  padding-right: 0.3rem;
  box-sizing: border-box;
  line-height: 1rem;
}

.chat_name > div {
  display: inline;
}

.chat_messageWrapper p {
  margin: 0;
  display: inline;
}
</style>
