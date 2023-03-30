<template>
	<main class="main">
		<Carousel :caroselList="caroselList" />
		<CategoriesList :categories="categoryList" />
	</main>
</template>

<script setup>
	import { ref } from "vue";
	import Carousel from "@/components/template/homepage/Carousel.vue";
	import CategoriesList from "@/components/template/homepage/CategoriesList.vue";

	const caroselList = ref([
		{
			id: "1",
			description:
				"Chào mừng bạn đến với ngôi nhà Converse! Tại đây, mỗi một dòng chữ, mỗi chi tiết và hình ảnh đều là những bằng chứng mang dấu ấn lịch sử Converse 100 năm, và đang không ngừng phát triển lớn mạnh.",
			imgURL: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/slide-1.jpg",
		},
		{
			id: "2",
			description:
				"Chào mừng bạn đến với ngôi nhà Converse! Tại đây, mỗi một dòng chữ, mỗi chi tiết và hình ảnh đều là những bằng chứng mang dấu ấn lịch sử Converse 100 năm, và đang không ngừng phát triển lớn mạnh.",
			imgURL: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/slide-2.jpg",
		},
		{
			id: "3",
			description:
				"Chào mừng bạn đến với ngôi nhà Converse! Tại đây, mỗi một dòng chữ, mỗi chi tiết và hình ảnh đều là những bằng chứng mang dấu ấn lịch sử Converse 100 năm, và đang không ngừng phát triển lớn mạnh.",
			imgURL: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/slide-5.jpg",
		},
	]);

	const categoryList = ref([
		{
			id: "1",
			titleImg:
				"http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/title_block_03.png",
			backgroundImg:
				"http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/product_block_03.jpg",
			url: "/danh-muc/nam",
		},
		{
			id: "2",
			titleImg:
				"http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/title_block_05.png",
			backgroundImg:
				"http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/product_block_05.jpg",
			url: "/danh-muc/nu",
		},
		{
			id: "3",
			titleImg:
				"http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/title_block_07.png",
			backgroundImg:
				"http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/product_block_07.jpg",
			url: "/danh-muc/phu-kien-khac",
		},
	]);
</script>

<style lang="scss">
	.main {
		max-width: 1230px;
		margin: 0 auto;
	}
</style>
