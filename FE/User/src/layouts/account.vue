<template>
	<main class="main">
		<UserSidebar class="main__sidebar" />

		<div class="main__main">
			<router-view />
		</div>
	</main>
</template>

<script setup>
	import UserSidebar from "@/components/layout/UserSidebar.vue";
</script>

<style lang="scss" scoped>
	.main {
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
