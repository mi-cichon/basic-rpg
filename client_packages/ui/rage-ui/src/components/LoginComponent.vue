<template>
  <div class="body" :class="{ disappear: disappear }">
    <div class="flags">
      <div class="flag pl" @click="changeLocale('pl')"></div>
      <div class="flag en" @click="changeLocale('en')"></div>
    </div>
    <div class="header">
      {{ $t("login.loginTitle") }}
    </div>
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
    <button class="button" @click="login">{{ $t("login.loginButton") }}</button>
    <div class="register">
      {{ $t("login.registerText") }}
      <div @click="disappearClick">{{ $t("login.registerClick") }}</div>
    </div>
    <div class="simple-logo"></div>
  </div>
</template>

<script lang="ts">
//import rpc from "rage-rpc";

export default {
  data() {
    return {
      username: "",
      password: "",
      usernameError: false,
      passwordError: false,
      disappear: false,
    };
  },
  methods: {
    login() {
      if (this.username == "") {
        this.usernameError = true;
      }

      if (this.password == "") {
        this.passwordError = true;
      }

      if (this.passwordError || this.usernameError) {
        return;
      }
      //rpc.callClient("client_tryLogin", [this.username, this.password]);
    },
    disappearClick() {
      this.disappear = true;
      setTimeout(() => {
        this.$router.push("/register");
      }, 400);
    },
    changeLocale(locale: string) {
      if (!this.$i18n.availableLocales.includes(locale)) {
        return;
      }
      this.$i18n.locale = locale;
    },
  },
};
</script>

<style scoped>
.flags {
  position: absolute;
  left: 50%;
  bottom: -2rem;
  display: flex;
  justify-content: space-around;
  transform: translateX(-50%);
}

.flag {
  height: 2rem;
  width: 3.9rem;
  background-position: center;
  background-size: cover;
  background-repeat: no-repeat;
  position: absolute;
  top: 50%;
  transform: translateY(-50%);
}

.pl {
  background-image: url("./../assets/language/flags/pl.png");
  left: -5rem;
}
.en {
  background-image: url("./../assets/language/flags/en.png");
  right: -5rem;
}

.flag:hover {
  filter: contrast(150%);
  width: 5rem;
  height: 2.5rem;
}
.simple-logo {
  width: 10rem;
  height: 5rem;
  background-image: url("./../assets/images/logo-simple.png");
  background-position: center;
  background-repeat: no-repeat;
  background-size: 100%;
}
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
  text-align: center;
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
