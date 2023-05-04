import axios from "@/request";

export default {
	getList() {
		return axios.get("/api/Shoes");
	},

	/**
	 * @param {Object} params - API Queries
	 * @param {string} params.keyWord - Searched keyword.
	 * @param {number} params.minPrice - min price filter
	 * @param {number} params.maxPrice - max price filter
	 * @param {number} params.CategoryCode - Category code
	 * @param {number} params.pageSize - page size
	 * @param {number} params.pageNumber - page number
	 * @param {Object[]} body - query body
	 * @param {string} body[].Field shoe's properties
	 * @param {"ASC" | "DESC"} body[].Order
	 * @param {string} body[].operation
	 * @param {string} body[].DataType
	 * @param {"sort"} body[].Type
	 */
	getFilteredList(params, body) {
		return axios.post("/api/Shoes/PagingAndFilter", body, { params });
	},

	/**
	 *
	 * @param {string} id Shoe's id
	 * @returns
	 */
	getDetail(id) {
		return axios.get(`/api/Shoes/${id}`);
	},

	deleteProduct(id) {
		return axios.delete(`/api/Shoes/${id}`);
	},

	createProduct(form) {
		return axios.post("/api/Shoes", form);
	},

	updateProduct(id, form) {
		return axios.put(`/api/Shoes/${id}`, form);
	},

	updateProductv2(id, form) {
		return axios.put(`/api/Shoes/v2/${id}`, form);
	},

	exportExcel() {
		return axios.post("/api/shoes/exportv2", [], {
			responseType: "blob",
		});
	},
};
