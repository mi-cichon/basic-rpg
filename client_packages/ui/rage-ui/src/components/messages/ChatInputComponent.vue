<template>
  <div class="input_body">
    <input
      maxlength="150"
      v-model="text"
      v-on:keyup.enter="sendMessage()"
      ref="chatInput"
      spellcheck="false"
    />
    <div class="limit">
      <i
        :style="{ color: getWarningColor() }"
        class="fa-solid fa-pen-fancy"
      ></i>
    </div>
  </div>
</template>

<script lang="ts">
import rpc from "rage-rpc";

export default {
  data() {
    return {
      text: "",
    };
  },
  methods: {
    getWarningColor() {
      if (this.text.length < 75) {
        return "#07f71f";
      }
      if (this.text.length < 125) {
        return "#f7ef07";
      }
      if (this.text.length < 150) {
        return "#f77b07";
      }
      if (this.text.length == 150) {
        return "#f71707";
      }
    },
    sendMessage() {
      const txt = this.stripHtml(this.text);
      if (txt.length == 0 || txt.length > 150) {
        return;
      }
      this.text = "";

      rpc.callClient("client_sendMessage", txt);
      this.$emit("messageSent");
    },
    stripHtml(text: string) {
      let tmp = document.createElement("DIV");
      tmp.innerHTML = text;
      return tmp.textContent || tmp.innerText || "";
    },
  },
  mounted() {
    this.$nextTick(() => {
      (this.$refs.chatInput as any).focus();
    });
  },
};
</script>

<style scoped>
.input_body {
  width: 100%;
  height: 2.2rem;
  position: relative;
  box-sizing: border-box;
}

input {
  color: white;
  font-family: "Roboto";
  outline: none;
  padding-left: 1rem;
  padding-right: 3rem;
  border-radius: 2rem;
  width: 100%;
  height: 2.2rem;
  background-color: #12192c;
  box-sizing: border-box;
}

input:focus {
  outline: none;
}

.limit {
  position: absolute;
  right: 0.7rem;
  top: 50%;
  color: white;
  transform: translateY(-50%);
}
</style>
