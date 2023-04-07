import axios from "axios";
import Cookies from "js-cookie";

import { EKeys } from "@/constants/config.js";

class Request {
	constructor() {
		const instance = axios.create({
			baseURL: process.env.VUE_APP_API_URL,
		});

		instance.interceptors.request.use(
			async (config) => {
				const accessToken = Cookies.get(EKeys.accessToken);
				if (accessToken) {
					config.headers.Authorization = `Bearer ${accessToken}`;
				}

				return config;
			},
			(error) => {
				Promise.reject(error);
			},
		);

		instance.interceptors.response.use(
			function (response) {
				return response;
			},
			function (error) {
				// if (error.response) {
				// 	return handleErrorUtil(error.response, instance);
				// }
				return Promise.reject(error);
			},
		);

		this.instance = instance;
	}

	get = (url, params) => {
		return this.instance.get(url, { params });
	};

	/**
	 * @param {string} url - API URL
	 * @param {Object} data - API body
	 * @param {import("axios").AxiosRequestConfig=} config - additional config
	 */
	post = (url, data, config) => {
		return this.instance.post(url, data, config);
	};

	put = (url, data) => {
		return this.instance.put(url, data);
	};

	patch = (url, data) => {
		return this.instance.patch(url, data);
	};

	delete = (url, data) => {
		return this.instance.delete(url, { data });
	};
}

export default new Request();
