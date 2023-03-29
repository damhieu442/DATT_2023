<template>
	<div id="wrapper">
		<Header />
		<router-view />
		<Footer />
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

<style>
	@import url(@/css/app.css);
</style>
