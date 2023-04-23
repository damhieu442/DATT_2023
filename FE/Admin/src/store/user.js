import { auth } from "@/api";
import { EKeys, ERole } from "@/constants/config";
import Cookies from "js-cookie";

const ROOT_URL = process.env.VUE_APP_API_URL;

const cart = {
	namespaced: true,
	state: () => ({
		username: "",
		userEmail: "",
		fullName: "",
		uid: "",
		phone: "",
		address: "",
		image: "",
	}),
	getters: {},
	mutations: {},

	actions: {
		async signIn({ state }, form) {
			try {
				console.log("Form: ", form);

				const { username, password } = form;
				const formData = { Username: username, Password: password, Role: ERole.ADMIN };

				const token = await auth.signIn(formData);

				console.log("token,", token);

				const userInfo = token.data;
				const accessToken = userInfo.Token;

				state.username = username;
				state.userEmail = userInfo.Email;
				state.fullName = userInfo.FullName;
				state.uid = userInfo.CustomerId;
				state.phone = userInfo.PhoneNumber;
				state.address = userInfo.Address;
				state.image = userInfo.ImgName
					? ROOT_URL.concat("/api/Shoes/imgName/", userInfo.ImgName)
					: "";

				if (form.isRemeberPassword) {
					localStorage.setItem(EKeys.username, username);
					localStorage.setItem(EKeys.userEmail, userInfo.Email);
					localStorage.setItem(EKeys.fullName, userInfo.FullName);
					localStorage.setItem(EKeys.uid, userInfo.CustomerId);
					localStorage.setItem(EKeys.image, userInfo.ImgName || "");
					localStorage.setItem(EKeys.address, userInfo.Address);
					localStorage.setItem(EKeys.phone, userInfo.PhoneNumber);
					Cookies.set(EKeys.accessToken, accessToken, { expires: 400 });
				} else {
					sessionStorage.setItem(EKeys.username, state.username);
					sessionStorage.setItem(EKeys.userEmail, state.userEmail);
					sessionStorage.setItem(EKeys.fullName, userInfo.FullName);
					sessionStorage.setItem(EKeys.uid, userInfo.CustomerId);
					sessionStorage.setItem(EKeys.image, userInfo.ImgName || "");
					sessionStorage.setItem(EKeys.address, userInfo.Address);
					sessionStorage.setItem(EKeys.phone, userInfo.PhoneNumber);
					Cookies.set(EKeys.accessToken, accessToken);
				}

				return true;
			} catch (error) {
				if (error.name === "AxiosError") {
					return error.response.data;
				}

				return false;
			}
		},

		async signUp({ state }, form) {
			try {
				const token = await new Promise((resolve) => setTimeout(resolve, 1500, 123456));

				state.username = form.name;
				state.userEmail = form.email;

				sessionStorage.setItem(EKeys.username, state.username);
				sessionStorage.setItem(EKeys.userEmail, state.userEmail);
				Cookies.set(EKeys.accessToken, token);

				return true;
			} catch (error) {
				return false;
			}
		},

		signOut({ state }) {
			localStorage.clear();
			sessionStorage.clear();

			for (const key in Cookies.get()) {
				Cookies.remove(key);
			}

			Object.assign(state, {
				username: "",
				userEmail: "",
				fullName: "",
				uid: "",
				phone: "",
				address: "",
				image: "",
			});
		},

		tryLogInUser({ state }) {
			const username =
				localStorage.getItem(EKeys.username) ||
				sessionStorage.getItem(EKeys.username) ||
				"";
			const email =
				localStorage.getItem(EKeys.userEmail) ||
				sessionStorage.getItem(EKeys.userEmail) ||
				"";
			const fullName =
				localStorage.getItem(EKeys.fullName) ||
				sessionStorage.getItem(EKeys.fullName) ||
				"";
			const uid = localStorage.getItem(EKeys.uid) || sessionStorage.getItem(EKeys.uid) || "";
			const image =
				localStorage.getItem(EKeys.image) || sessionStorage.getItem(EKeys.image) || "";
			const address =
				localStorage.getItem(EKeys.address) || sessionStorage.getItem(EKeys.address) || "";
			const phone =
				localStorage.getItem(EKeys.phone) || sessionStorage.getItem(EKeys.phone) || "";

			Object.assign(state, {
				username,
				userEmail: email,
				fullName,
				uid,
				phone,
				address,
				image,
			});
		},

		async updateUserInfo(context, form) {
			const formData = new FormData();
			formData.append("CustomerId", form.uid);
			formData.append("FullName", form.fullName);
			formData.append("Email", form.userEmail);
			formData.append("PhoneNumber", form.phone);
			formData.append("Address", form.address);
			formData.append("Img", form.image);

			try {
				const response = await auth.updateUser(formData, form.uid);

				console.log("Response: ", response);
				if ((response.status > 199) & (response.status < 300)) {
					return true;
				}
				return false;
			} catch (error) {
				return false;
			}
		},

		async updateUserPassword(context, form) {
			const formData = new FormData();
			formData.append("CustomerId", form.customerId);
			formData.append("Password", form.newPassword);

			try {
				const response = await auth.updateUser(formData, form.customerId);

				console.log("Response: ", response);
				if ((response.status > 199) & (response.status < 300)) {
					return true;
				}
				return false;
			} catch (error) {
				return false;
			}
		},
	},
};

export default cart;
