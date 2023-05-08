import axios from "@/request";

export default {
	getList() {
		return axios.get("/api/Evaluates");
	},

	getFilterList(params, body) {
		return axios.post("/api/Evaluates/PagingAndFilter", body, { params });
	},

	getDetail(id) {
		return axios.get(`/api/Evaluates/${id}`);
	},

	createItem(form) {
		return axios.post("/api/Evaluates", form);
	},

	updateItem(form, id) {
		return axios.put(`/api/Evaluates/${id}`, form);
	},

	deleteItem(id) {
		return axios.delete(`/api/Evaluates/${id}`);
	},
};
