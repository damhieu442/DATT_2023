import { createRouter, createWebHistory } from "vue-router";
import DefaultLayout from "@/layouts/default.vue";

import ContactPage from "@/views/BContact.vue";
import IntroducePage from "@/views/BIntroduce.vue";
import Homepage from "@/views/Homepage.vue";

export const router = createRouter({
	history: createWebHistory(),
	routes: [
		{
			path: "/",
			component: DefaultLayout,

			children: [
				{ path: "", component: Homepage, name: "Homepage" },

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
