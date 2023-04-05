<template>
	<main class="main">
		<a-row class="main__overview">
			<a-col :span="12">
				<ProductGallery :images="productImages" />
			</a-col>
			<a-col :span="12">
				<ProductInfo v-bind="productInfo" />
			</a-col>
		</a-row>
		<ProductTabs />
		<RelatedProducts :products="relatedProducts" />
	</main>
</template>

<script setup>
	import { ref } from "vue";

	import ProductGallery from "@/components/template/ProductDetail/ProductGallery.vue";
	import ProductInfo from "@/components/template/ProductDetail/ProductInfo.vue";
	import ProductTabs from "@/components/template/ProductDetail/ProductTabs.vue";
	import RelatedProducts from "@/components/template/ProductDetail/RelatedProducts.vue";

	const productImages = ref([
		"http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-1-300x225.jpg",
		"http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-1-1-300x225.jpg",
		"http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-1-2-300x225.jpg",
		"http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-1-3-300x225.jpg",
		"http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-1-4-300x225.jpg",
	]);

	const productInfo = ref({
		categories: [
			{ name: "Nam", link: "/danh-muc/nam" },
			{ name: "Classic", link: "/danh-muc/classic" },
		],
		id: "1",
		name: "Chuck Taylor Classic",
		price: 1_250_000,
		sizes: [
			{ code: 34, amount: 12 },
			{ code: 36, amount: 12 },
			{ code: 38, amount: 0 },
		],
	});

	const relatedProducts = ref(
		new Array(12).fill("").map((_, i) => ({
			id: i + 1,
			name: "Chuck 70 Archive Prints Hi",
			price: (1_250_000 * i) / 2,
			image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-2-300x225.jpg",
			hoverImage:
				"http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-2-1-300x225.jpg",
		})),
	);
</script>

<style lang="scss" scoped>
	.main {
		max-width: 1230px;
		margin: 1.5rem auto;
		font-family: "Roboto", sans-serif;

		&__overview {
			padding-bottom: 1.5rem;
		}
	}
</style>
