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
			<ProductTabs
				ref="rfProductInfoTabs"
				:infos="productInfo?.additionInfos"
				:comments="productEvalutes"
				:is-logged-in="isLoggedIn"
				@create-comment="createNewEvaluateHandler"
			/>
			<RelatedProducts :products="relatedProducts" />
		</div>
	</main>
</template>

<script setup>
	import { computed, onMounted, ref } from "vue";

	import ProductGallery from "@/components/template/ProductDetail/ProductGallery.vue";
	import ProductInfo from "@/components/template/ProductDetail/ProductInfo.vue";
	import ProductTabs from "@/components/template/ProductDetail/ProductTabs.vue";
	import RelatedProducts from "@/components/template/ProductDetail/RelatedProducts.vue";
	import PageLoader from "@/components/shared/PageLoader.vue";
	import { useRoute } from "vue-router";
	import { evaluate, product as productAPI } from "@/api";
	import productFactory from "@/utils/productFactory";
	import { useStore } from "vuex";
	import { notification } from "ant-design-vue";

	const route = useRoute();
	const store = useStore();

	const isLoadingData = ref(false);

	const productImages = ref([]);

	const productEvalutes = ref([]);

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

	const rfProductInfoTabs = ref(null);

	const isLoggedIn = computed(() => !!store.state.user.uid);

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
				productInfo.value.additionInfos.target = categories[0].name;
			}
		} catch (error) {}

		isLoadingData.value = false;
	};

	const getProductEvaluate = async () => {
		try {
			const response = await evaluate.getList(route.params.id);
			if (response.status > 199 && response.status < 300) {
				productEvalutes.value = response.data.map(
					productFactory.transformEvaluateResponseToEvaluate,
				);
			}
		} catch (error) {}
	};

	const createNewEvaluateHandler = async (form) => {
		const formData = new FormData();

		formData.append("Star", form.rating);
		formData.append("FullName", form.username);
		formData.append("Email", form.email);
		formData.append("Comment", form.comment);
		formData.append("ShoeId", route.params.id || "");
		formData.append("CreatedBy", store.state.user.username);

		try {
			const response = await evaluate.createItem(formData);

			if (response.status > 199 && response.status < 300) {
				notification.success({ message: "Đánh giá thành công" });
				rfProductInfoTabs.value.resetCommentForm();
				getProductEvaluate();
			} else {
				throw new Error();
			}
		} catch (error) {
			notification.error({ message: "Có lỗi xảy ra, vui lòng thử lại" });
		}
	};

	onMounted(() => {
		getProductDetailData();
		getProductEvaluate();
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
