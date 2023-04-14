import { auth } from "@/api";
import { EKeys, ERole } from "@/constants/config";
import Cookies from "js-cookie";

const cart = {
	namespaced: true,
	state: () => ({
		username: "",
		userEmail: "",
		fullName: "",
		uid: "",
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
		async signIn({ state }, form) {
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

				if (form.isRemeberPassword) {
					localStorage.setItem(EKeys.username, username);
					localStorage.setItem(EKeys.userEmail, userInfo.Email);
					localStorage.setItem(EKeys.fullName, userInfo.FullName);
					localStorage.setItem(EKeys.uid, userInfo.CustomerId);
					Cookies.set(EKeys.accessToken, accessToken, { expires: 400 });
				} else {
					sessionStorage.setItem(EKeys.username, state.username);
					sessionStorage.setItem(EKeys.userEmail, state.userEmail);
					sessionStorage.setItem(EKeys.fullName, userInfo.FullName);
					sessionStorage.setItem(EKeys.uid, userInfo.CustomerId);
					Cookies.set(EKeys.accessToken, accessToken);
				}

				return true;
			} catch (error) {
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
				uid: "",
				fullName: "",
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

			state.username = username;
			state.userEmail = email;
			state.fullName = fullName;
			state.uid = uid;
		},
	},
};

export default cart;
