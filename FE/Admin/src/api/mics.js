import axios from "@/request";

export default {
	sendEmail(params) {
		axios.post("/api/Emails", undefined, { params });
	},
};
