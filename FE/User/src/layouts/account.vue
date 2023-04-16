<template>
	<main class="page-layout">
		<UserSidebar class="page-layout__sidebar" />

		<div class="page-layout__main">
			<router-view />
		</div>
	</main>
</template>

<script setup>
	import UserSidebar from "@/components/layout/UserSidebar.vue";
	import { EKeys } from "@/constants/config";
	import { computed } from "@vue/reactivity";
	import Cookie from "js-cookie";
	import { watch } from "vue";
	import { useRouter } from "vue-router";
	import { useStore } from "vuex";

	const store = useStore();
	const router = useRouter();
	const isLoggedIn = computed(() => !!store.state.user.uid);

	watch(isLoggedIn, (isLoggedIn) => {
		if (!isLoggedIn) {
			router.replace("/");
		}
	});

	const token = Cookie.get(EKeys.accessToken);

	if (!token) {
		router.replace("/");
	}
</script>

<style lang="scss" scoped>
	.page-layout {
		max-width: 1230px;
		width: 100%;
		margin: 0 auto;
		font-family: "Roboto", sans-serif;
		font-size: 1rem;
		display: flex;
		padding: 1rem 0;

		&__sidebar {
			width: fit-content;
		}

		&__main {
			flex: 1 1 auto;
			margin-left: 1rem;
		}
	}
</style>
