<template>
	<main class="main">
		<h2>Quên mật khẩu</h2>
		<ForgotPasswordForm
			v-if="!isResetForgotPassword"
			@reset-password="navigateToResetPasswordHander"
		/>
		<ResetForgotPasswordForm v-else :email="resetEmail" />
	</main>
</template>

<script setup>
	import { computed, ref } from "vue";

	import ForgotPasswordForm from "@/components/template/ForgotPasswordPage/ForgotPasswordForm.vue";
	import ResetForgotPasswordForm from "@/components/template/ForgotPasswordPage/ResetPasswordForm.vue";

	const isResetForgotPassword = computed(() => {
		return !!resetEmail.value;
	});

	const navigateToResetPasswordHander = (form) => {
		resetEmail.value = form.email;
	};

	const resetEmail = ref("");
</script>

<style lang="scss" scoped>
	.main {
		width: 100%;
		padding: 1rem 0;
		> h2 {
			font-size: 1.5rem; /* 24px */
			line-height: 2rem; /* 32px */
		}
	}
</style>
