<template>
	<div>
		<router-view></router-view>
	</div>
</template>

<script>
	import { onBeforeMount } from "vue";
	import { useStore } from "vuex";

	export default {
		name: "App",
		components: {},
		setup() {
			const store = useStore();

			onBeforeMount(() => {
				store.dispatch("category/getCategoriesList");
			});
		},
	};
</script>

<style>
	@import url(@/css/app.css);
</style>
