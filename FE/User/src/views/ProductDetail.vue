<template>
	<main class="main">
		<div class="main__loader" v-if="isLoadingData"><PageLoader /></div>
		<div v-else>
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
		</div>
	</main>
</template>

<script setup>
	import { onMounted, ref } from "vue";

	import ProductGallery from "@/components/template/ProductDetail/ProductGallery.vue";
	import ProductInfo from "@/components/template/ProductDetail/ProductInfo.vue";
	import ProductTabs from "@/components/template/ProductDetail/ProductTabs.vue";
	import RelatedProducts from "@/components/template/ProductDetail/RelatedProducts.vue";
	import PageLoader from "@/components/shared/PageLoader.vue";
	import { useRoute } from "vue-router";
	import { product as productAPI } from "@/api";
	import productFactory from "@/utils/productFactory";
	import { useStore } from "vuex";

	const route = useRoute();
	const store = useStore();

	const isLoadingData = ref(false);

	const productImages = ref([]);

	const productInfo = ref(undefined);

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

	const getProductDetailData = async () => {
		isLoadingData.value = true;

		try {
			const response = await productAPI.getDetail(route.params.id);
			if (response.status < 300 && response.status > 199) {
				const { info, images } = productFactory.transformResponseToProductDetail(
					response.data,
				);
				productImages.value = images;
				const [parentCategory, childCategory] = info.categories;
				const categories = [];
				const categoriesList = store.state.category.rootCategories;

				for (const categoryCode in categoriesList) {
					if (Object.hasOwnProperty.call(categoriesList, categoryCode)) {
						const category = categoriesList[categoryCode];

						if (category.categoryCode === parentCategory) {
							categories.push({
								name: category.name,
								link: "/danh-muc/" + categoryCode,
							});

							for (const cate of category.children) {
								if (cate.code === childCategory) {
									categories.push({
										name: cate.name,
										link: `/danh-muc/${categoryCode}/${cate.id}`,
									});
									break;
								}
							}

							break;
						}
					}
				}

				info.categories = categories;

				productInfo.value = info;
			}
		} catch (error) {}

		isLoadingData.value = false;
	};

	onMounted(() => {
		getProductDetailData();
	});
</script>

<style lang="scss" scoped>
	.main {
		max-width: 1230px;
		margin: 1.5rem auto;
		font-family: "Roboto", sans-serif;
		position: relative;

		&__loader {
			position: absolute;
			top: 50%;
			transform: translateY(-50%);
		}

		&__overview {
			padding-bottom: 1.5rem;
		}
	}
</style>
