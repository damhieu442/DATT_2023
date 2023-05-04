import axios from "@/request";

export default {
	getStatistics() {
		return axios.get("/api/dashboards");
	},
};
