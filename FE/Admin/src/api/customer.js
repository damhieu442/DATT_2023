import axios from "@/request";

export default {
	getList(query, data = []) {
		return axios.post("/api/Customers/PagingAndFilter", data, { params: query });
	},

	getDetail(id) {
		return axios.get(`/api/Customers/${id}`);
	},

	updateInfo(id, form) {
		return axios.put(`/api/Customers/${id}`, form);
	},

	deleteUser(id) {
		return axios.delete(`/api/Customers/${id}`);
	},

	createUser(form) {
		return axios.post("/api/Customers", form);
	},
};
