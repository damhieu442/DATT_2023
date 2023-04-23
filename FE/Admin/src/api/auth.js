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
	updatePassword(id, params) {
		return axios.put(`/api/Customers/update-password/${id}`, undefined, { params });
	},
};
