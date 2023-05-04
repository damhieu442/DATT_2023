import axios from "@/request";

export default {
	getList(query, params) {
		return axios.post("/api/bills/pagingandfilter", { params });
	},

	getDetailOrder(id) {
		return axios.get(`/api/bills/${id}`);
	},

	updateDetailOrder(id, form) {
		return axios.put(`/api/bills/${id}`, form);
	},

	getOrderProductList(id) {
		return axios.get(`/api/billdetails/${id}`);
	},

	exportExcel() {
		return axios.post("/api/bills/exportv2", [], {
			responseType: "blob",
		});
	},
};
