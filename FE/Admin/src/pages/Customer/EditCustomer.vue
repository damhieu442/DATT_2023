<template>
	<div>
		<div v-if="!notFound">
			<div class="flex justify-between items-center">
				<h1 class="text-2xl">Thông tin khách hàng</h1>
			</div>
			<CustomerDetail
				class="my-6"
				:user="userInfo"
				:isAdmin="userInfo?.role === ERole.SUPER_ADMIN"
				@submit="updateUserInfoHandler"
			/>
		</div>
		<div v-else>
			<h1 class="text-3xl text-center mt-52">
				Không tìm thấy thông tin người dùng trong CSDL
			</h1>
			<router-link
				class="py-2 px-10 rounded-md bg-blue-500 hover:bg-blue-400 text-white hover:text-white block mx-auto my-5 w-fit"
				to="/khach-hang"
			>
				Quay lại trang danh sách khách hàng
			</router-link>
		</div>
	</div>
</template>

<script setup>
	import { customer } from "@/api";
	import { notification } from "ant-design-vue";
	import { onMounted, ref } from "vue";
	import { useRoute, useRouter } from "vue-router";
	import { ERole } from "@/constants/config";

	import CustomerDetail from "@/components/template/CustomerPage/CustomerDetail.vue";

	const notFound = ref(false);
	const userInfo = ref(null);

	const route = useRoute();
	const router = useRouter();
	const userId = route.params.id;

	const getUserDetailInfo = async () => {
		const ROOT_URL = process.env.VUE_APP_API_URL;

		try {
			const response = await customer.getDetail(userId);
			console.log("Response: ", response);
			if (response.status > 199 && response.status < 300) {
				const data = response.data;
				userInfo.value = {
					username: data.UserName,
					fullName: data.FullName,
					phoneNumer: data.PhoneNumber,
					email: data.Email,
					avatarURL: data.ImgName
						? ROOT_URL.concat("/api/Customers/imgName/", data.ImgName.split(".")[0])
						: "",
					address: data.Address,
					role: data.Role,
				};
			} else {
				throw new Error();
			}
		} catch (error) {
			console.log("Error: ", error);
			notFound.value = true;
			notification.error({ message: "Có lỗi xảy ra, vui lòng thử lại" });
		}
	};

	/**
	 * @typedef TUpdateForm
	 * @property {string} username
	 * @property {string?} password
	 * @property {string} fullName
	 * @property {string} phoneNumer
	 * @property {string} email
	 * @property {string} avatarURL
	 * @property {string} address
	 * @property {number} role
	 * @property {File?} avatarFile
	 *
	 *
	 * @param {TUpdateForm} form
	 */
	const updateUserInfoHandler = async (form) => {
		const { address, avatarFile, email, fullName, password, phoneNumer, role, username } = form;

		const formData = new FormData();
		formData.append("CustomerId", userId);
		formData.append("UserName", username);
		formData.append("FullName", fullName);
		formData.append("Email", email);
		formData.append("PhoneNumber", phoneNumer);
		formData.append("Address", address);
		formData.append("Role", role);

		if (password) {
			formData.append("Password", password);
		}

		if (avatarFile instanceof File) {
			formData.append("Img", avatarFile);
		}

		try {
			const response = await customer.updateInfo(userId, formData);

			if (response.status > 199 && response.status < 300) {
				notification.success({ message: "Cập nhật thông tin khách hàng thành công" });
				router.push("/khach-hang");
			} else {
				throw new Error();
			}
		} catch (error) {
			notification.error({ message: "Có lỗi xảy ra, vui lòng thử lại" });
		}
	};

	onMounted(() => {
		getUserDetailInfo();
	});
</script>
