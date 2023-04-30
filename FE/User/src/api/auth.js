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

	updatePassword(params) {
		return axios.put("/api/Customers/reset-pass", undefined, { params });
	},

	getResetPasswordToken(params) {
		return axios.post("/api/Customers/forgot-password", undefined, { params });
	},

	updatePassword2(params, id) {
		return axios.put(`/api/customers/update-password/${id}`, undefined, { params });
	},

	resetForgotPassword(params) {
		return axios.post("/api/Customers/confirm-token-password", undefined, { params });
	},
};
