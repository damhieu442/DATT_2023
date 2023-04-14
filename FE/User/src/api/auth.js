import axios from "@/request";

export default {
	signUp(form) {
		return axios.post("/api/Customers", form);
	},
	signIn(form) {
		return axios.post("/api/Customers/authenticate", form);
	},
};
