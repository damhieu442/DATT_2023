import { createRouter, createWebHistory } from "vue-router";
import DefaultLayout from "@/layouts/default.vue";

import ContactPage from "@/views/BContact.vue";
import IntroducePage from "@/views/BIntroduce.vue";

export const router = createRouter({
	history: createWebHistory(),
	routes: [
		{
			path: "/",
			component: DefaultLayout,

			children: [
				{ path: "", redirect: { name: "Introduce" } },

				{
					path: "lien-he",
					component: ContactPage,
					name: "Contact",
				},
				{
					path: "gioi-thieu",
					component: IntroducePage,
					name: "Introduce",
				},
			],
		},
	],
});
