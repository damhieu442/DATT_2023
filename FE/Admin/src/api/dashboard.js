import axios from "@/request";

export default {
	getStatistics() {
		return axios.get("/api/dashboards");
	},

	getChartData() {
		return axios.get("/api/dashboards/chart");
	},

	getRevenueData() {
		return axios.get("/api/dashboards/price");
	},

	getCategoryData() {
		return axios.get("/api/dashboards/category");
	},
};
