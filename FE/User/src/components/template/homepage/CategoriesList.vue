<template>
	<section class="categories">
		<ul>
			<li v-for="category in categories" :key="category.id">
				<CategoryItem :category="category" />
			</li>
		</ul>
	</section>
</template>

<script>
	import CategoryItem from "./components/CategoryItem.vue";

	export default {
		components: { CategoryItem },
		name: "CategoriesList",
		props: ["categories"],
		functional: true,
	};
</script>

<style lang="scss" scoped>
	@use "sass:math";

	.categories {
		margin: 1rem 0;
		> ul {
			display: flex;
			justify-content: space-between;

			> li {
				width: math.div(1, 3) * 100%;
				position: relative;
				margin: 0;
				padding: 0 15px 30px;
			}
		}
	}
</style>
