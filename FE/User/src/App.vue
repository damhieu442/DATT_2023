<template>
	<div>
		<router-view></router-view>
	</div>
</template>

<script>
	import { category } from "@/api";
	import { onBeforeMount } from "vue";
	import { useStore } from "vuex";

	export default {
		name: "App",
		components: {},
		setup() {
			const store = useStore();

			// const getCategoriesList = async () => {
			// 	try {
			// 		const categories = await category.getList();

			// 		console.log(categories);
			// 	} catch (error) {
			// 		console.log("Error: ", error);
			// 	}
			// };

			onBeforeMount(() => {
				store.dispatch("category/getCategoriesList");
				// getCategoriesList();
			});
		},
	};
</script>

<style>
	@import url(@/css/app.css);
</style>
