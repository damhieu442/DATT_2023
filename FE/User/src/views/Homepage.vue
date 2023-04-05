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
	import { ref } from "vue";
	import Carousel from "@/components/template/homepage/Carousel.vue";
	import CategoriesList from "@/components/template/homepage/CategoriesList.vue";
	import FeatureProducts from "@/components/template/homepage/FeatureProducts.vue";
	import OtherAccessory from "@/components/template/homepage/OtherAccessory.vue";
	import Promotion from "@/components/template/homepage/Promotion.vue";
	import { UpCircleOutlined } from "@ant-design/icons-vue";

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
			products: [
				{
					id: "1",
					name: "Chuck Taylor Classic",
					price: 1_250_000,
					image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-1.jpg",
					extraLinks: [
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-1.jpg",
							size: "1920w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-1-300x192.jpg",
							size: "300w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-1-768x492.jpg",
							size: "768w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-1-1024x655.jpg",
							size: "1024w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-1-600x384.jpg",
							size: "600w",
						},
					],
				},
				{
					id: "2",
					name: "Chuck Taylor Classic",
					price: 1_250_000,
					image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-2.jpg",
					extraLinks: [
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-2.jpg",
							size: "1920w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-2-300x192.jpg",
							size: "300w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-2-768x492.jpg",
							size: "768w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-2-1024x655.jpg",
							size: "1024w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-2-600x384.jpg",
							size: "600w",
						},
					],
				},
				{
					id: "3",
					name: "Chuck Taylor Classic",
					price: 1_250_000,
					image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-3.jpg",
					extraLinks: [
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-3.jpg",
							size: "1920w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-3-300x192.jpg",
							size: "300w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-3-768x492.jpg",
							size: "768w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-3-1024x655.jpg",
							size: "1024w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-3-600x384.jpg",
							size: "600w",
						},
					],
				},
				{
					id: "4",
					name: "Chuck Taylor Classic",
					price: 1_250_000,
					image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-4.jpg",
					extraLinks: [
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-4.jpg",
							size: "1920w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-4-300x192.jpg",
							size: "300w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-4-768x492.jpg",
							size: "768w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-4-1024x655.jpg",
							size: "1024w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-4-600x384.jpg",
							size: "600w",
						},
					],
				},
				{
					id: "5",
					name: "Chuck Taylor Classic",
					price: 1_250_000,
					image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-5.jpg",
					extraLinks: [
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-5.jpg",
							size: "1920w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-5-300x192.jpg",
							size: "300w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-5-768x492.jpg",
							size: "768w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-5-1024x655.jpg",
							size: "1024w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-5-600x384.jpg",
							size: "600w",
						},
					],
				},
				{
					id: "6",
					name: "Chuck Taylor Classic",
					price: 1_250_000,
					image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-6.jpg",
					extraLinks: [
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-6.jpg",
							size: "1920w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-6-300x192.jpg",
							size: "300w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-6-768x492.jpg",
							size: "768w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-6-1024x655.jpg",
							size: "1024w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-6-600x384.jpg",
							size: "600w",
						},
					],
				},
				{
					id: "7",
					name: "Chuck Taylor Classic",
					price: 1_250_000,
					image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-7.jpg",
					extraLinks: [
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-7.jpg",
							size: "1920w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-7-300x192.jpg",
							size: "300w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-7-768x492.jpg",
							size: "768w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-7-1024x655.jpg",
							size: "1024w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-7-600x384.jpg",
							size: "600w",
						},
					],
				},
				{
					id: "8",
					name: "Chuck Taylor Classic",
					price: 1_250_000,
					image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-8.jpg",
					extraLinks: [
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-8.jpg",
							size: "1920w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-8-300x192.jpg",
							size: "300w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-8-768x492.jpg",
							size: "768w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-8-1024x655.jpg",
							size: "1024w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-8-600x384.jpg",
							size: "600w",
						},
					],
				},
			],
		},
		{
			id: "2",
			tabName: "Sản phẩm bán chạy",
			tabCode: "best-sold-product",
			products: [
				{
					id: "1",
					name: "Chuck Taylor Classic",
					price: 1_250_000,
					image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-1.jpg",
					extraLinks: [
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-1.jpg",
							size: "1920w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-1-300x192.jpg",
							size: "300w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-1-768x492.jpg",
							size: "768w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-1-1024x655.jpg",
							size: "1024w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-1-600x384.jpg",
							size: "600w",
						},
					],
				},
				{
					id: "2",
					name: "Chuck Taylor Classic",
					price: 1_250_000,
					image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-2.jpg",
					extraLinks: [
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-2.jpg",
							size: "1920w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-2-300x192.jpg",
							size: "300w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-2-768x492.jpg",
							size: "768w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-2-1024x655.jpg",
							size: "1024w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-2-600x384.jpg",
							size: "600w",
						},
					],
				},
				{
					id: "3",
					name: "Chuck Taylor Classic",
					price: 1_250_000,
					image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-3.jpg",
					extraLinks: [
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-3.jpg",
							size: "1920w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-3-300x192.jpg",
							size: "300w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-3-768x492.jpg",
							size: "768w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-3-1024x655.jpg",
							size: "1024w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-3-600x384.jpg",
							size: "600w",
						},
					],
				},
				{
					id: "4",
					name: "Chuck Taylor Classic",
					price: 1_250_000,
					image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-4.jpg",
					extraLinks: [
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-4.jpg",
							size: "1920w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-4-300x192.jpg",
							size: "300w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-4-768x492.jpg",
							size: "768w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-4-1024x655.jpg",
							size: "1024w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-4-600x384.jpg",
							size: "600w",
						},
					],
				},
				{
					id: "5",
					name: "Chuck Taylor Classic",
					price: 1_250_000,
					image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-5.jpg",
					extraLinks: [
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-5.jpg",
							size: "1920w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-5-300x192.jpg",
							size: "300w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-5-768x492.jpg",
							size: "768w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-5-1024x655.jpg",
							size: "1024w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-5-600x384.jpg",
							size: "600w",
						},
					],
				},
				{
					id: "6",
					name: "Chuck Taylor Classic",
					price: 1_250_000,
					image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-6.jpg",
					extraLinks: [
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-6.jpg",
							size: "1920w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-6-300x192.jpg",
							size: "300w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-6-768x492.jpg",
							size: "768w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-6-1024x655.jpg",
							size: "1024w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-6-600x384.jpg",
							size: "600w",
						},
					],
				},
				{
					id: "7",
					name: "Chuck Taylor Classic",
					price: 1_250_000,
					image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-7.jpg",
					extraLinks: [
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-7.jpg",
							size: "1920w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-7-300x192.jpg",
							size: "300w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-7-768x492.jpg",
							size: "768w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-7-1024x655.jpg",
							size: "1024w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-7-600x384.jpg",
							size: "600w",
						},
					],
				},
				{
					id: "8",
					name: "Chuck Taylor Classic",
					price: 1_250_000,
					image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-8.jpg",
					extraLinks: [
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-8.jpg",
							size: "1920w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-8-300x192.jpg",
							size: "300w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-8-768x492.jpg",
							size: "768w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-8-1024x655.jpg",
							size: "1024w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-8-600x384.jpg",
							size: "600w",
						},
					],
				},
			],
		},
		{
			id: "3",
			tabName: "Sản phẩm phổ biến",
			tabCode: "trending-product",
			products: [
				{
					id: "1",
					name: "Chuck Taylor Classic",
					price: 1_250_000,
					image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-1.jpg",
					extraLinks: [
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-1.jpg",
							size: "1920w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-1-300x192.jpg",
							size: "300w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-1-768x492.jpg",
							size: "768w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-1-1024x655.jpg",
							size: "1024w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-1-600x384.jpg",
							size: "600w",
						},
					],
				},
				{
					id: "2",
					name: "Chuck Taylor Classic",
					price: 1_250_000,
					image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-2.jpg",
					extraLinks: [
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-2.jpg",
							size: "1920w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-2-300x192.jpg",
							size: "300w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-2-768x492.jpg",
							size: "768w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-2-1024x655.jpg",
							size: "1024w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-2-600x384.jpg",
							size: "600w",
						},
					],
				},
				{
					id: "3",
					name: "Chuck Taylor Classic",
					price: 1_250_000,
					image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-3.jpg",
					extraLinks: [
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-3.jpg",
							size: "1920w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-3-300x192.jpg",
							size: "300w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-3-768x492.jpg",
							size: "768w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-3-1024x655.jpg",
							size: "1024w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-3-600x384.jpg",
							size: "600w",
						},
					],
				},
				{
					id: "4",
					name: "Chuck Taylor Classic",
					price: 1_250_000,
					image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-4.jpg",
					extraLinks: [
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-4.jpg",
							size: "1920w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-4-300x192.jpg",
							size: "300w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-4-768x492.jpg",
							size: "768w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-4-1024x655.jpg",
							size: "1024w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-4-600x384.jpg",
							size: "600w",
						},
					],
				},
				{
					id: "5",
					name: "Chuck Taylor Classic",
					price: 1_250_000,
					image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-5.jpg",
					extraLinks: [
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-5.jpg",
							size: "1920w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-5-300x192.jpg",
							size: "300w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-5-768x492.jpg",
							size: "768w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-5-1024x655.jpg",
							size: "1024w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-5-600x384.jpg",
							size: "600w",
						},
					],
				},
				{
					id: "6",
					name: "Chuck Taylor Classic",
					price: 1_250_000,
					image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-6.jpg",
					extraLinks: [
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-6.jpg",
							size: "1920w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-6-300x192.jpg",
							size: "300w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-6-768x492.jpg",
							size: "768w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-6-1024x655.jpg",
							size: "1024w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-6-600x384.jpg",
							size: "600w",
						},
					],
				},
				{
					id: "7",
					name: "Chuck Taylor Classic",
					price: 1_250_000,
					image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-7.jpg",
					extraLinks: [
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-7.jpg",
							size: "1920w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-7-300x192.jpg",
							size: "300w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-7-768x492.jpg",
							size: "768w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-7-1024x655.jpg",
							size: "1024w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-7-600x384.jpg",
							size: "600w",
						},
					],
				},
				{
					id: "8",
					name: "Chuck Taylor Classic",
					price: 1_250_000,
					image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-8.jpg",
					extraLinks: [
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-8.jpg",
							size: "1920w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-8-300x192.jpg",
							size: "300w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-8-768x492.jpg",
							size: "768w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-8-1024x655.jpg",
							size: "1024w",
						},
						{
							link: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-8-600x384.jpg",
							size: "600w",
						},
					],
				},
			],
		},
	]);

	const otherAccessories = ref([
		{
			id: 1,
			name: "Converse Star Chevron Jogger",
			price: 950_000,
			image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/jogger.jpg",
		},
		{
			id: 2,
			name: "Converse “Metal CONS” Pull Over Hoodie",
			price: 1_400_000,
			image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/T-shirt-2.jpg",
		},
		{
			id: 3,
			name: "Converse X Suicidal Tendencies Long Sleeve Tee",
			price: 700_000,
			image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/T-shirt-1.jpg",
		},
		{
			id: 4,
			name: "Converse Collegiate Text SS Tee",
			price: 600_000,
			image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/T-shirt.jpg",
		},
		{
			id: 5,
			name: "Poly Chuck Plus 1.0",
			price: 770_000,
			image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/balo-4-1-300x225.jpg",
		},
		{
			id: 6,
			name: "Speed 2 Backpack",
			price: 700_000,
			image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/balo-3.jpg",
		},
		{
			id: 7,
			name: "Lil Duffel",
			price: 700_000,
			image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/balo-2.jpg",
		},
		{
			id: 8,
			name: "Sport Duffel",
			price: 1_000_000,
			image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/balo-1.jpg",
		},
	]);

	const discountProducts = ref([
		{
			id: 1,
			promotionAmount: 30,
			image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/sale-off-8.jpg",
			name: "Jack Purcell Pro",
			price: 1500000,
		},
		{
			id: 2,
			promotionAmount: 30,
			image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/sale-off-7.jpg",
			name: "One Star Pro OG Update",
			price: 1500000,
		},
		{
			id: 3,
			promotionAmount: 30,
			image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/sale-off-6.jpg",
			name: "Chuck Taylor All Star 70 Poplin Shirt",
			price: 1600000,
		},
		{
			id: 4,
			promotionAmount: 30,
			image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/sale-off-5.jpg",
			name: "84 Thunderbolt Breathable Mesh",
			price: 1600000,
		},
		{
			id: 5,
			promotionAmount: 30,
			image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/sale-off-4.jpg",
			name: "Chuck Taylor All Star Ox x Nike Flyknit Multi",
			price: 2000000,
		},
		{
			id: 6,
			promotionAmount: 30,
			image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/sale-off-3.jpg",
			name: "Break Point Foundational Leather",
			price: 1600000,
		},
		{
			id: 7,
			promotionAmount: 30,
			image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/sale-off-2.jpg",
			name: "Cons Auckland Ultra",
			price: 1300000,
		},
		{
			id: 8,
			promotionAmount: 30,
			image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/sale-off-1.jpg",
			name: "Cons PL 76 Foundational Leather",
			price: 1500000,
		},
	]);
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
		}
	}
</style>
