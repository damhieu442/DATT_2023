import axios from "@/request";

export default {
	addProductToCart(cart, id) {
		return axios.post(`/api/CartDetails/${id}`, cart);
	},

	getUserCart(id) {
		return axios.get(`/api/CartDetails/${id}`);
	},

	deleteCart(id) {
		return axios.delete(`/api/cartdetails/${id}`);
	},

	updateCart(id, form) {
		return axios.put(`/api/CartDetails/${id}`, form);
	},

	getPaymentIntent(amount) {
		return axios.post("/api/payments", { amount });
	},

	createBill(form, id) {
		return axios.post(`/api/bills/${id}`, form);
	},

	addProductToOrder(uid, orders) {
		return axios.post(`/api/billdetails/${uid}`, orders);
	},

	getDetailOrder(id) {
		return axios.get(`/api/bills/${id}`);
	},

	getOrderList(id) {
		return axios.get(`/api/billdetails/${id}`);
	},

	getUserOrdersList(params) {
		return axios.post("/api/bills/customerid", undefined, { params });
	},

	cancelOrder(id, form) {
		return axios.put(`/api/bills/${id}`, form);
	},
};
