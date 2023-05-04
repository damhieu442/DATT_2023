<template>
	<div>
		<div class="flex justify-between items-center">
			<h1 class="text-2xl">
				<button class="cursor-pointer mr-4 bg-transparent border-0" @click="goBack">
					<i class="fas fa-arrow-left" /></button
				>Thêm mới khách hàng
			</h1>
		</div>
		<CustomerDetail class="my-6" @submit="addUser" />
	</div>
</template>

<script setup>
	import { customer } from "@/api";
	import { notification } from "ant-design-vue";
	import { useRouter } from "vue-router";
	import CustomerDetail from "@/components/template/CustomerPage/CustomerDetail.vue";

	const router = useRouter();

	const goBack = () => {
		router.back();
	};

	/**
	 * @typedef TUpdateForm
	 * @property {string} username
	 * @property {string} fullName
	 * @property {string} phoneNumer
	 * @property {string} password
	 * @property {string} email
	 * @property {string} avatarURL
	 * @property {string} address
	 * @property {number} role
	 * @property {File?} avatarFile
	 *
	 *
	 * @param {TUpdateForm} form
	 */
	const addUser = async (form) => {
		const { address, avatarFile, email, fullName, password, phoneNumer, role, username } = form;

		const formData = new FormData();
		formData.append("UserName", username);
		formData.append("FullName", fullName);
		formData.append("Password", password);
		formData.append("Email", email);
		formData.append("PhoneNumber", phoneNumer);
		formData.append("Address", address);
		formData.append("Role", role);

		if (avatarFile instanceof File) {
			formData.append("Img", avatarFile);
		}

		try {
			const response = await customer.createUser(formData);

			if (response.status > 199 && response.status < 300) {
				notification.success({ message: "Tạo tài khoản thành công" });
				router.push("/khach-hang");
			} else {
				throw new Error();
			}
		} catch (error) {
			notification.error({ message: "Có lỗi xảy ra, vui lòng thử lại" });
		}
	};
</script>
