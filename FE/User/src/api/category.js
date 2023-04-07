import axios from "@/request";

export default {
	getList() {
		return axios.get("/api/Categorys");
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
	 * @param {string} body[].Field
	 * @param {string} body[].Order
	 * @param {string} body[].operation
	 * @param {string} body[].DataType
	 * @param {string} body[].Type
	 */
	getCategoryProducts(params, body) {
		return axios.post("/api/Categorys/PagingAndFilter", body, { params });
	},
};
