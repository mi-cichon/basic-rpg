import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import LoginComponent from "./components/LoginComponent.vue";
import { createI18n } from "vue-i18n";
import messages from "./assets/language/messages.json";

const app = createApp(App);

app.use(router);

const i18n = createI18n({
  locale: "pl",
  fallbackLocale: "pl",
  messages,
});

app.use(i18n);

app.component("LoginComponent", LoginComponent);

app.mount("#app");
