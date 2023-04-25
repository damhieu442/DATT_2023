import axios from "@/request";

export default {
	addProductToCart(cart, id) {
		return axios.post(`/api/CartDetails/${id}`, cart);
	},

	getUserCart(id) {
		return axios.get(`/api/CartDetails/${id}`);
	},

	getPaymentIntent() {
		return axios.get("http://localhost:5000/checkout");
	},
};
