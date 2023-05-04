<template>
	<main class="main">
		<Carousel :caroselList="caroselList" />
		<CategoriesList :categories="categoryList" />
		<FeatureProducts :tabList="featuresProduct" />
		<OtherAccessory :data="otherAccessories" />
		<Promotion :products="discountProducts" />
		<a-back-top>
			<div class="main__backtop"><UpCircleOutlined /></div>
		</a-back-top>
	</main>
</template>

<script setup>
	import { onMounted, ref } from "vue";
	import Carousel from "@/components/template/homepage/Carousel.vue";
	import CategoriesList from "@/components/template/homepage/CategoriesList.vue";
	import FeatureProducts from "@/components/template/homepage/FeatureProducts.vue";
	import OtherAccessory from "@/components/template/homepage/OtherAccessory.vue";
	import Promotion from "@/components/template/homepage/Promotion.vue";
	import { UpCircleOutlined } from "@ant-design/icons-vue";
	import { product as productAPI } from "@/api";
	import productFactory from "@/utils/productFactory";

	import caroselImg1 from "@/assets/img/slide-1.jpg";
	import caroselImg2 from "@/assets/img/slide-2.jpg";
	import caroselImg3 from "@/assets/img/slide-5.jpg";

	import categoryTitleImg1 from "@/assets/img/title_block_03.png";
	import categoryTitleImg2 from "@/assets/img/title_block_05.png";
	import categoryTitleImg3 from "@/assets/img/title_block_07.png";

	import categoryIllusImg1 from "@/assets/img/product_block_03.jpg";
	import categoryIllusImg2 from "@/assets/img/product_block_05.jpg";
	import categoryIllusImg3 from "@/assets/img/product_block_07.jpg";

	const caroselList = ref([
		{
			id: "1",
			description:
				"Chào mừng bạn đến với ngôi nhà Converse! Tại đây, mỗi một dòng chữ, mỗi chi tiết và hình ảnh đều là những bằng chứng mang dấu ấn lịch sử Converse 100 năm, và đang không ngừng phát triển lớn mạnh.",
			imgURL: caroselImg1,
		},
		{
			id: "2",
			description:
				"Chào mừng bạn đến với ngôi nhà Converse! Tại đây, mỗi một dòng chữ, mỗi chi tiết và hình ảnh đều là những bằng chứng mang dấu ấn lịch sử Converse 100 năm, và đang không ngừng phát triển lớn mạnh.",
			imgURL: caroselImg2,
		},
		{
			id: "3",
			description:
				"Chào mừng bạn đến với ngôi nhà Converse! Tại đây, mỗi một dòng chữ, mỗi chi tiết và hình ảnh đều là những bằng chứng mang dấu ấn lịch sử Converse 100 năm, và đang không ngừng phát triển lớn mạnh.",
			imgURL: caroselImg3,
		},
	]);

	const categoryList = ref([
		{
			id: "1",
			titleImg: categoryTitleImg1,
			backgroundImg: categoryIllusImg1,
			url: "/danh-muc/nam",
		},
		{
			id: "2",
			titleImg: categoryTitleImg2,
			backgroundImg: categoryIllusImg2,
			url: "/danh-muc/nu",
		},
		{
			id: "3",
			titleImg: categoryTitleImg3,
			backgroundImg: categoryIllusImg3,
			url: "/danh-muc/tre-em",
		},
	]);

	const featuresProduct = ref([
		{
			id: "1",
			tabName: "Sản phẩm mới",
			tabCode: "new-product",
			products: [],
		},
		{
			id: "2",
			tabName: "Sản phẩm bán chạy",
			tabCode: "best-sold-product",
			products: [],
		},
		{
			id: "3",
			tabName: "Sản phẩm phổ biến",
			tabCode: "trending-product",
			products: [],
		},
	]);

	const otherAccessories = ref([]);

	const discountProducts = ref([]);

	const getProducts = async (field, order, maxCount = 8) => {
		try {
			const query = {
				keyWord: "",
				minPrice: 0,
				maxPrice: 100_000_000,
				CategoryCode: undefined,
				pageSize: maxCount,
				pageNumber: 1,
			};

			const body = {
				Field: field,
				Order: order,
				operation: "",
				DataType: "",
				Type: "sort",
			};

			const response = await productAPI.getFilteredList(query, [body]);

			return response.data.Data.map(
				productFactory.transformProductAPIResponseToCategoryProduct,
			);
		} catch (error) {
			console.log("Error: ", error);

			return [];
		}
	};

	const getNewestProducts = async () => {
		featuresProduct.value[0].products = await getProducts("CreatedDate", "DESC");
	};

	const getBestSoldProduct = async () => {
		featuresProduct.value[1].products = await getProducts("Price", "ASC");
	};

	const getTrendingProducts = async () => {
		featuresProduct.value[2].products = await getProducts("ShoeId", "ASC");
	};

	const getOtherAccessories = async () => {
		otherAccessories.value = await getProducts("Price", "DESC");
	};

	const getTopDiscountProducts = async () => {
		discountProducts.value = await getProducts("Discount", "DESC");
	};

	const getData = () => {
		getTrendingProducts();
		getBestSoldProduct();
		getNewestProducts();
		getOtherAccessories();
		getTopDiscountProducts();
	};

	onMounted(() => {
		getData();
	});
</script>

<style lang="scss">
	.main {
		max-width: 1230px;
		margin: 0 auto;

		&__backtop {
			height: 40px;
			width: 40px;
			line-height: 40px;
			text-align: center;
			font-size: 40px;
			mix-blend-mode: exclusion;
			color: #00afaf;
		}
	}
</style>
