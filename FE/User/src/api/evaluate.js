import axios from "@/request";

export default {
	getList(id) {
		return axios.get(`/api/Evaluates/${id}`);
	},

	createItem(form) {
		return axios.post("/api/Evaluates", form);
	},
};
