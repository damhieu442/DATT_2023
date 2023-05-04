import { auth } from "@/api";
import { EKeys, ERole } from "@/constants/config";
import Cookies from "js-cookie";

const IMAGE_BASE_URL = process.env.VUE_APP_API_URL;

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
	getters: {
		authInfo(state) {
			const username =
				state.username ||
				localStorage.getItem(EKeys.username) ||
				sessionStorage.getItem(EKeys.username);
			const email =
				state.userEmail ||
				localStorage.getItem(EKeys.userEmail) ||
				sessionStorage.getItem(EKeys.userEmail);
			const uid =
				state.uid || localStorage.getItem(EKeys.uid) || sessionStorage.getItem(EKeys.uid);
			const fullName =
				state.fullName ||
				localStorage.getItem(EKeys.fullName) ||
				sessionStorage.getItem(EKeys.fullName);

			return {
				username,
				email,
				uid,
				fullName,
			};
		},
	},
	mutations: {},

	actions: {
		async signIn({ state, dispatch }, form) {
			try {
				const { email: username, password } = form;
				const formData = { Username: username, Password: password, Role: ERole.USER };

				const token = await auth.signIn(formData);

				const userInfo = token.data;
				const accessToken = userInfo.Token;

				state.username = username;
				state.userEmail = userInfo.Email;
				state.fullName = userInfo.FullName;
				state.uid = userInfo.CustomerId;
				state.phone = userInfo.PhoneNumber;
				state.address = userInfo.Address;
				state.image = userInfo.ImgName
					? IMAGE_BASE_URL.concat("/api/Shoes/imgName/", userInfo.ImgName)
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
				dispatch("cart/getUserCart");
				return true;
			} catch (error) {
				return false;
			}
		},

		async signUp({ state }, signUpForm) {
			try {
				const form = new FormData();
				form.append("Email", signUpForm.email);
				form.append("UserName", signUpForm.username);
				form.append("FullName", signUpForm.name);
				form.append("Password", signUpForm.password);
				form.append("Role", 0);

				const token = await auth.signUp(form);

				state.username = signUpForm.name;
				state.userEmail = signUpForm.email;

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
