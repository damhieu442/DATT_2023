<template>
	<div id="wrapper" class="page-wrapper">
		<Header class="page-header" />
		<router-view class="page-content" />
		<Footer class="page-footer" />
	</div>
</template>

<script>
	import Header from "@/components/layout/BHeader.vue";
	import Footer from "@/components/layout/BFooter.vue";

	export default {
		name: "DefaultLayout",
		components: { Header, Footer },
	};
</script>

<style lang="scss">
	@import url(@/css/app.css);

	.page-wrapper#wrapper {
		min-height: 100vh;
		display: flex;
		flex-direction: column;

		> .page-content {
			flex: 1 1 auto;
		}
	}
</style>
