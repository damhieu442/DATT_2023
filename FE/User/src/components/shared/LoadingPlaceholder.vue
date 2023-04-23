<template>
	<div class="loading-container">
		<div v-if="isLoading" class="loader">
			<slot name="loader">
				<PageLoader />
			</slot>
		</div>
		<slot v-else></slot>
	</div>
</template>

<script>
	import PageLoader from "./PageLoader.vue";

	export default {
		props: { isLoading: { type: Boolean, default: true } },
		components: { PageLoader },
	};
</script>

<style lang="scss" scoped>
	.loading-container {
		min-height: 500px;
	}

	.loader {
		display: flex;
		justify-content: center;
		align-items: center;
		padding-top: 2rem;
	}
</style>
