import axios from "@/request";

export default {
	signUp(form) {
		return axios.post("/api/Customers", form);
	},

	signIn(form) {
		return axios.post("/api/Customers/authenticate", form);
	},

	updateUser(form, id) {
		return axios.put(`/api/Customers/${id}`, form);
	},

	updatePassword(form) {
		return axios.put("/api/Customers/reset-pass", form);
	},

	getResetPasswordToken(params) {
		return axios.post("/api/Customers/forgot-password", undefined, { params });
	},

	resetForgotPassword(params) {
		return axios.post("/api/Customers/confirm-token-password", undefined, { params });
	},
};
