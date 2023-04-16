<template>
	<main class="main">
		<h2 className="text-2xl">Thiết lập mật khẩu</h2>
		<ChangePasswordForm />
	</main>
</template>

<script setup>
	import ChangePasswordForm from "@/components/template/ChangePasswordPage/ChangePasswordForm.vue";
</script>

<style lang="scss" scoped>
	.main {
		> h2 {
			font-size: 1.5rem; /* 24px */
			line-height: 2rem; /* 32px */
		}
	}
</style>
