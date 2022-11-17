<template>
  <div class="body" :class="{ disappear: disappear }">
    <div class="backButton" @click="disappearClick">
      <i class="fa-solid fa-arrow-left-long"></i>
    </div>
    <div class="header">{{ $t("register.registerTitle") }}</div>
    <div class="input" :class="{ error: usernameError }">
      <i class="fa-solid fa-user"></i>
      <input
        v-model="username"
        type="text"
        :placeholder="$t('login.usernamePlaceholder')"
        v-on:change="usernameError = false"
      />
    </div>
    <div class="input" :class="{ error: passwordError }">
      <i class="fa-solid fa-lock"></i>
      <input
        v-model="password"
        type="password"
        :placeholder="$t('login.passwordPlaceholder')"
        v-on:change="passwordError = false"
      />
    </div>
    <div class="input" :class="{ error: repeatedPasswordError }">
      <i class="fa-solid fa-lock"></i>
      <input
        v-model="repeatedPassword"
        type="password"
        :placeholder="$t('register.repeatPasswordPlaceholder')"
        v-on:change="repeatedPasswordError = false"
      />
    </div>
    <button class="button" @click="register">
      {{ $t("register.registerClick") }}
    </button>
  </div>
</template>

<script lang="ts">
import rpc from "rage-rpc";

export default {
  data() {
    return {
      username: "",
      password: "",
      repeatedPassword: "",
      usernameError: false,
      passwordError: false,
      repeatedPasswordError: false,
      disappear: false,
    };
  },
  methods: {
    register() {
      if (
        this.username == "" ||
        !this.isASCII(this.username) ||
        this.username.length < 3 ||
        this.username.length > 24
      ) {
        this.usernameError = true;
      }

      if (this.password == "") {
        this.passwordError = true;
      }

      if (this.repeatedPassword == "") {
        this.repeatedPasswordError = true;
      }

      if (this.repeatedPassword != this.password) {
        this.passwordError = true;
        this.repeatedPasswordError = true;
      }

      if (
        this.passwordError ||
        this.usernameError ||
        this.repeatedPasswordError
      ) {
        return;
      }

      rpc.callClient("client_tryRegister", [this.username, this.password]);
    },
    disappearClick() {
      this.disappear = true;
      setTimeout(() => {
        this.$router.push("/login");
      }, 400);
    },
    isASCII(str: string) {
      // eslint-disable-next-line no-control-regex
      return /^[\x00-\x7F]+$/.test(str);
    },
  },
  created() {
    rpc.register("ui_registerSuccessful", () => {
      this.disappearClick();
    });
  },
};
</script>

<style scoped>
.body {
  width: 30rem;
  height: 35rem;
  background-color: #12192c;
  left: 50%;
  top: 50%;
  position: absolute;
  transform: translate(-50%, -50%);
  border-radius: 5px;
  box-shadow: 0 0 8px black;
  display: flex;
  flex-direction: column;
  align-items: center;
}

.backButton {
  position: absolute;
  left: 1.5rem;
  top: 1rem;
  width: 1rem;
  height: 1rem;
  font-size: 2.2rem;
}

.backButton:hover > i {
  color: #0c9;
  text-shadow: #0c9 1px 0 10px;
}

.header {
  font-size: 3rem;
  color: white;
  font-family: "Sen";
  padding: 1em;
}

i {
  color: gray;
}

.input {
  width: 70%;
  display: flex;
  justify-content: space-between;
  height: 4rem;
  background-color: transparent;
  border: 2px solid gray;
  margin-bottom: 2rem;
  border-radius: 10px;
}

.input:focus-within {
  border: 2px solid #0c9;
}

.input > i {
  width: 15%;
  font-size: 1.4rem;
  text-align: center;
  line-height: 4rem;
}

.input > input {
  width: 85%;
  background-color: transparent;
  border: none;
  font-size: 1.3rem;
  font-family: "Sono";
  color: white;
}

input:focus {
  outline: none;
}

.button {
  background-color: #0c9;
  width: 70%;
  height: 4rem;
  border-radius: 10px;
  color: white;
  font-family: "Sen";
  font-size: 1.5rem;
  border: none;
  margin-bottom: 2rem;
}

.button:hover {
  box-shadow: 0 0 8px #0c9;
}

.register {
  width: 70%;
  font-family: "Sono";
  color: white;
  font-size: 1.3rem;
}

.register > div {
  display: inline-block;
  color: #0c9;
}

.register > div:hover {
  text-shadow: #0c9 1px 0 10px;
}

.error {
  border: 2px solid red !important;
}
</style>
