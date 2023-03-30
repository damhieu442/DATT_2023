import { createApp } from "vue";
import App from "./App.vue";

import VTooltip from "v-tooltip";
import Antd from "ant-design-vue";

import { BEnum } from "@/js/BEnum";
import { BResource } from "@/js/BResource";
import { store } from "@/store";
import { router } from "@/router";

import "ant-design-vue/dist/antd.css";

const app = createApp(App);
app.use(Antd);

app.config.globalProperties.$BEnum = BEnum;
app.config.globalProperties.$BResource = BResource;

app.use(VTooltip);
app.use(store);
app.use(router);

app.mount("#app");
