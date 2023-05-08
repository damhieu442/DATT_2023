import { createRouter, createWebHistory } from "vue-router";
import { store } from "@/store";

import DefaultLayout from "@/layouts/default.vue";
import AuthenLayout from "@/layouts/auth";

import SignInPage from "@/pages/SignInPage.vue";
import Homepage from "@/pages/Homepage.vue";
import CustomerPage from "@/pages/Customer/CustomerPage.vue";
import CustomerDetail from "@/pages/Customer/CustomerDetail.vue";
import EditCustomerPage from "@/pages/Customer/EditCustomer.vue";
import AddCustomer from "@/pages/Customer/AddCustomer.vue";
import CategoryPage from "@/pages/CategoryPage.vue";
import EvaluatePage from "@/pages/EvaluatePage.vue";
import ProductList from "@/pages/Product/ProductList.vue";
import ProductDetail from "@/pages/Product/ProductDetail.vue";
import AddProduct from "@/pages/Product/AddProduct.vue";
import EditProduct from "@/pages/Product/EditProduct.vue";
import OrderList from "@/pages/OrderPage/OrderList.vue";
import OrderDetail from "@/pages/OrderPage/OrderDetail.vue";
import UserInfoPage from "@/pages/UserInfo.vue";
import ChangePasswordPage from "@/pages/ChangePassword.vue";
import ProductReviewPage from "@/pages/ProductReviewPage.vue";

export const router = createRouter({
	history: createWebHistory(),
	routes: [
		{
			path: "/auth",
			component: AuthenLayout,
			meta: { requiresAuth: false },
			children: [{ path: "", component: SignInPage }],
			beforeEnter: () => {
				const userId = store.state.user.uid;
				if (userId) {
					return "/";
				}
			},
		},
		{
			path: "/",
			component: DefaultLayout,
			meta: { requiresAuth: true },
			children: [
				{ path: "", component: Homepage },
				{ path: "/san-pham", component: ProductList },
				{ path: "/thong-tin-ca-nhan", component: UserInfoPage },
				{ path: "/doi-mat-khau", component: ChangePasswordPage },
				{ path: "/san-pham/tao-moi", component: AddProduct },
				{ path: "/san-pham/:id", component: ProductDetail },
				{ path: "/san-pham/:id/chinh-sua", component: EditProduct },
				{ path: "/danh-gia", component: ProductReviewPage },
				{ path: "/don-hang", component: OrderList },
				{ path: "/don-hang/:id", component: OrderDetail },
				{ path: "/khach-hang", component: CustomerPage },
				{ path: "/khach-hang/them-moi", component: AddCustomer },
				{ path: "/khach-hang/:id", component: CustomerDetail },
				{ path: "/khach-hang/:id/chinh-sua", component: EditCustomerPage },
				{ path: "/danh-muc", component: CategoryPage },
				{ path: "/gop-y", component: EvaluatePage },
			],
		},
	],
});

router.beforeEach((toPath) => {
	const userId = store.state.user.uid;

	if (toPath.meta.requiresAuth && !userId) {
		return "auth";
	}

	return true;
});
