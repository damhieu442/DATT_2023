import { createApp } from "vue";
import App from "./App.vue";

import VTooltip from "v-tooltip";
import { BEnum } from "@/js/BEnum";
import { BResource } from "@/js/BResource";
import { store } from "@/store";
import { router } from "@/router";

const app = createApp(App);

app.config.globalProperties.$BEnum = BEnum;
app.config.globalProperties.$BResource = BResource;

app.use(VTooltip);
app.use(store);
app.use(router);

app.mount("#app");
