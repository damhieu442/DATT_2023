import axios from "@/request";

export default {
	sendFeedBack(form) {
		return axios.post("/api/FeedBacks", form);
	},
};
