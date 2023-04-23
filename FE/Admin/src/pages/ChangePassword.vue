<template>
	<div class="main">
		<h2 className="text-2xl">Thiết lập mật khẩu</h2>
		<ChangePasswordForm :loading="isSendingData" @submit="updatePasswordHandler" />
	</div>
</template>

<script setup>
	import { auth } from "@/api";
	import ChangePasswordForm from "@/components/template/ChangePasswordPage/ChangePasswordForm.vue";
	import { notification } from "ant-design-vue";
	import { ref } from "vue";

	const isSendingData = ref(false);

	const updatePasswordHandler = async (form) => {
		const { customerId, newPassword: passWordNew, oldPassword: passWordOld } = form;
		isSendingData.value = true;

		try {
			const response = await auth.updatePassword(customerId, { passWordNew, passWordOld });
			if (response.status > 199 && response.status < 300) {
				notification.success({ message: "Cập nhật mật khẩu thành công" });
			} else {
				throw new Error();
			}
		} catch (error) {
			notification.error({ message: "Có lỗi xảy ra, vui lòng thử lại" });
		} finally {
			isSendingData.value = false;
		}
	};
</script>

<style lang="scss" scoped>
	.main {
		> h2 {
			font-size: 1.5rem; /* 24px */
			line-height: 2rem; /* 32px */
		}
	}
</style>
