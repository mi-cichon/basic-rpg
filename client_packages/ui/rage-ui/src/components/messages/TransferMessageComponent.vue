<template>
  <div class="chat_messageWrapper">
    <div class="chat_bar" :style="{ backgroundColor: typeColor }"></div>
    <div class="chat_messageContent">
      <div class="chat_meta">
        <div class="chat_type">
          <i :class="typeIcon" :style="{ color: typeColor }"></i>
        </div>

        <div class="chat_name">
          <div>
            {{ $t("chat.transferReceivedTitle") }}
          </div>
        </div>
      </div>
      <div class="chat_text">
        <p :style="{ color: permissionColor }">{{ message.name }}</p>
        [{{ message.id }}]
        {{ ` ${$t("chat.transferReceivedText")} $${message.message}!` }}
      </div>
    </div>
  </div>
</template>

<script lang="ts">
export default {
  props: ["message", "typeColor", "typeIcon", "permissionColor"],
};
</script>

<style scoped></style>
