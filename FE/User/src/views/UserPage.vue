<template>
	<main class="main">
		<h2 className="text-2xl">Thông tin tài khoản</h2>
		<UserForm
			ref="rfUserForm"
			:user="userInfo"
			:loading="isUpdating"
			@submit="updateUserInfoHandler"
		/>
	</main>
</template>

<script setup>
	import UserForm from "@/components/template/UserPage/UserForm.vue";
	import { notification } from "ant-design-vue";
	import { ref } from "vue";
	import { useStore } from "vuex";

	const store = useStore();
	const rfUserForm = ref(null);
	const isUpdating = ref(false);

	const userInfo = store.state.user;

	const updateUserInfoHandler = async (form) => {
		isUpdating.value = true;
		const isSuccess = await store.dispatch("user/updateUserInfo", form);
		if (isSuccess) {
			notification.success({ message: "Cập nhật thông tin thành công" });
		} else {
			notification.error({ message: "Có lỗi xảy ra, vui lòng thử lại" });
		}
		isUpdating.value = false;
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
