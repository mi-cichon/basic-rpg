<template>
  <div class="body">
    <div
      class="circle"
      :style="{ 'border-color': getColor() }"
      :class="{ hideCircle: disappear }"
    >
      <i
        class="fa-solid fa-check"
        v-if="type == 0"
        :style="{ color: getColor() }"
      ></i>
      <i
        class="fa-solid fa-info"
        v-if="type == 1"
        style="font-size: 2.1rem"
        :style="{ color: getColor() }"
      ></i>
      <i
        class="fa-solid fa-exclamation"
        v-if="type == 2"
        :style="{ color: getColor() }"
      ></i>
      <i
        class="fa-solid fa-xmark"
        v-if="type == 3"
        :style="{ color: getColor() }"
      ></i>
      <i
        class="fa-solid fa-bug"
        v-if="type == 4"
        :style="{ color: getColor() }"
      ></i>
    </div>
    <div class="notificationWrapper">
      <div class="notificationBody" :class="{ hide: hideNotification }">
        <div class="notification" :style="{ 'border-color': getColor() }">
          <div class="title">{{ getTitle() }}</div>
          <div class="message">
            {{ message }}
          </div>
        </div>
        <div
          class="notificationRound"
          :style="{ 'border-color': getColor() }"
        ></div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
export default {
  props: {
    message: String,
    type: Number,
  },
  data() {
    return {
      hideNotification: false,
      disappear: false,
    };
  },
  methods: {
    getTitle() {
      switch (this.type) {
        case 0:
          return this.$t("notifications.success");
        case 1:
          return this.$t("notifications.infomation");
        case 2:
          return this.$t("notifications.warning");
        case 3:
          return this.$t("notifications.fail");
        case 4:
          return this.$t("notifications.error");
        default:
          return "";
      }
    },
    getColor() {
      switch (this.type) {
        case 0:
          return "#59a310";
        case 1:
          return "#0c9";
        case 2:
          return "#bc7407";
        case 3:
          return "#cc2f2c";
        case 4:
          return "#b00020";
        default:
          return "";
      }
    },
  },
  mounted() {
    setTimeout(() => {
      this.hideNotification = true;
      setTimeout(() => {
        this.disappear = true;
      }, 500);
    }, 4000);
  },
};
</script>

<style scoped>
.body {
  position: absolute;
  left: 50%;
  bottom: 15%;
  transform: translateX(-50%);
  width: 30rem;
  height: 6rem;
  overflow: hidden;
}

.circle {
  width: 6rem;
  height: 6rem;
  background-color: #12192c;
  border: 2px solid gray;
  border-radius: 50%;
  position: absolute;
  font-size: 2.6rem;
  color: white;
  box-sizing: border-box;
}

.circle > i {
  position: absolute;
  left: 50%;
  top: 50%;
  transform: translate(-50%, -50%);
}
.notificationBody {
  width: 27rem;
  height: 6rem;
  z-index: -100;
  position: absolute;
  left: -27rem;
  animation-name: show;
  animation-duration: 700ms;
  animation-fill-mode: forwards;
  animation-timing-function: ease-in-out;
}

.hide {
  animation-name: hide;
  animation-duration: 500ms;
  animation-fill-mode: forwards;
  animation-timing-function: ease-in-out;
}

.hideCircle {
  animation-name: hideCircle;
  animation-duration: 500ms;
  animation-fill-mode: forwards;
  animation-timing-function: ease-in-out;
}

.notificationWrapper {
  overflow: hidden;
  width: 27rem;
  height: 6rem;
  z-index: -100;
  position: absolute;
  left: 3rem;
}

@keyframes show {
  0% {
    left: -27rem;
  }
  100% {
    left: 0rem;
  }
}

@keyframes hide {
  0% {
    left: 0rem;
  }
  100% {
    left: -27rem;
  }
}

@keyframes hideCircle {
  0% {
    opacity: 1;
  }
  100% {
    opacity: 0;
  }
}

.notification {
  background-color: #12192c;
  width: 24rem;
  height: 6rem;
  position: relative;
  border: 1px solid gray;
  border-right: none;
  color: rgb(203, 203, 203);
  font-size: 1.2rem;
  padding: 1rem;
  padding-left: 4rem;
  font-family: "Dosis";
  box-sizing: border-box;
  display: flex;
  flex-direction: column;
  justify-content: space-around;
}

.title {
  color: white;
  font-weight: 800;
  font-size: 1.4rem;
}

.message {
  width: 110%;
}

.notificationRound {
  width: 6rem;
  height: 6rem;
  background-color: #12192c;
  position: absolute;
  right: 0;
  top: 0;
  border-radius: 50%;
  border: 1px solid gray;
  box-sizing: border-box;
  z-index: -1;
}
</style>
