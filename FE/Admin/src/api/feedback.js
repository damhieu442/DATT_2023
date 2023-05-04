import axios from "@/request";

export default {
	sendFeedBack(form) {
		return axios.post("/api/FeedBacks", form);
	},

	getFilterList(params, body) {
		return axios.post("/api/FeedBacks/PagingAndFilter", body, { params });
	},

	updateFeedBack(id, form) {
		return axios.put(`/api/FeedBacks/v2/${id}`, form);
	},

	deleteFeedBack(id) {
		return axios.delete(`/api/FeedBacks/${id}`);
	},

	exportExcel() {
		return axios.post("/api/feedbacks/exportv2", [], {
			responseType: "blob",
		});
	},
};
