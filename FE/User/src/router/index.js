import { createRouter, createWebHistory } from "vue-router";
import DefaultLayout from "@/layouts/default.vue";
import UserAccountLayout from "@/layouts/account.vue";

import ContactPage from "@/views/BContact.vue";
import IntroducePage from "@/views/BIntroduce.vue";
import Homepage from "@/views/Homepage.vue";
import CategoryPage from "@/views/CategoryPage.vue";
import ProductDetailPage from "@/views/ProductDetail.vue";
import ProductListPage from "@/views/ProductListPage.vue";
import CartPage from "@/views/CartPage.vue";
import PaymentPage from "@/views/PaymentPage.vue";
import UserPage from "@/views/UserPage.vue";
import ChangePasswordPage from "@/views/ChangePasswordPage.vue";
import OrderManagementPage from "@/views/OrderManagementPage.vue";

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
				{
					path: "danh-muc",
					name: "Category",
					redirect: "/san-pham",
					children: [
						{
							path: ":slug",
							component: CategoryPage,
							children: [
								{
									path: ":category",
									component: CategoryPage,
								},
							],
						},
					],
				},

				{
					path: "san-pham/:id",
					name: "ProductDetail",
					component: ProductDetailPage,
				},
				{
					path: "san-pham",
					name: "Product",
					component: ProductListPage,
				},
				{
					path: "gio-hang",
					name: "Cart",
					component: CartPage,
				},
				{
					path: "thanh-toan",
					name: "Payment",
					component: PaymentPage,
				},
				{
					path: "tai-khoan",
					component: UserAccountLayout,
					children: [
						{ path: "", name: "Account", component: UserPage },
						{
							path: "doi-mat-khau",
							name: "ChangePassword",
							component: ChangePasswordPage,
						},
						{
							path: "don-hang",
							name: "OrderManagement",
							component: OrderManagementPage,
						},
					],
				},
			],
		},
	],
});
