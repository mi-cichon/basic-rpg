<template>
  <div class="chat_messageWrapper">
    <div class="chat_bar" :style="{ backgroundColor: typeColor }"></div>
    <div class="chat_messageContent">
      <div class="chat_meta">
        <div class="chat_type">
          <i :class="typeIcon" :style="{ color: typeColor }"></i>
        </div>

        <div class="chat_name">
          {{ $t("chat.penaltyTitle") }}
        </div>
      </div>
      <div class="chat_text">
        {{
          $t(getPenaltyText(message.type), {
            player: message.name,
            by: message.by,
            time: message.time,
            reason: message.message,
          })
        }}
      </div>
    </div>
  </div>
</template>

<script lang="ts">
export default {
  props: ["message", "typeColor", "typeIcon", "permissionColor"],
  methods: {
    getPenaltyText(type: any) {
      switch (type) {
        case 5:
          return "chat.penaltyMuteText";
        case 6:
          return "chat.penaltyLicenceText";
        case 7:
          return "chat.penaltyKickText";
        case 8:
          return "chat.penaltyBanText";
      }
      return "";
    },
  },
};
</script>

<style scoped></style>
