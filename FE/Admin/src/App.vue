<template>
	<div>
		<router-view></router-view>
	</div>
</template>

<script>
	export default {
		name: "App",

		created() {
			this.$store.dispatch("user/tryLogInUser");
			this.$store.dispatch("category/getCategoriesList");
		},
	};
</script>

<style></style>
