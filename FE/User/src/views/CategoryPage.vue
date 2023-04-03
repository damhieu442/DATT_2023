<template>
	<main class="main">
		<PageTitle />
		<div class="main__content">
			<Sidebar class="main__content__aside" :widgetProducts="widgetProducts" />
			<ProductList
				class="main__content__main"
				:pagination="pagination"
				@update:page="pageChangeHandler"
			/>
		</div>
	</main>
</template>

<script setup>
	import PageTitle from "@/components/template/Category/PageTitle.vue";
	import Sidebar from "@/components/template/Category/SidebarFilter.vue";
	import ProductList from "@/components/template/Category/ProductList.vue";
	import { reactive, ref } from "vue";

	const widgetProducts = ref([
		{
			id: 1,
			link: "/san-pham/1",
			image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/men-psy-1-100x100.jpg",
			name: "Chuck 70 Psy-Kicks Ox",
			price: 2_800_000,
		},
		{
			id: 2,
			link: "/san-pham/2",
			image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-psy-2-100x100.jpg",
			name: "Chuck 70 Psy-Kicks Ox",
			price: 1_800_000,
		},
		{
			id: 3,
			link: "/san-pham/3",
			image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-sunbaked-1-100x100.jpg",
			name: "One Star Sunbaked",
			price: 1_600_000,
		},
		{
			id: 4,
			link: "/san-pham/4",
			image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-sunbaked-2-100x100.jpg",
			name: "One Star Sunbaked",
			price: 1_600_000,
		},
		{
			id: 5,
			link: "/san-pham/5",
			image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-sunbaked-3-100x100.jpg",
			name: "One Star Sunbaked",
			price: 1_600_000,
		},
	]);

	const pagination = reactive({
		current: 1,
		total: 200,
		pageSize: 10,
	});

	const pageChangeHandler = (page) => {
		pagination.current = page;
	};
</script>

<style lang="scss" scoped>
	.main {
		max-width: 1230px;
		margin: 0 auto;
		font-family: "Roboto", sans-serif;

		&__content {
			display: flex;
			align-items: flex-start;
			gap: 1rem;
			margin: 2rem 0;

			&__aside {
				min-width: 300px;
			}

			&__main {
				flex: 1 1 auto;
			}
		}
	}
</style>
