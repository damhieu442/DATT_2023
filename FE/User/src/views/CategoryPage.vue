<template>
	<main class="main">
		<PageTitle
			:total="pagination.total"
			:orderBy="filter.orderBy"
			@filter="sortProductHandler"
		/>
		<div class="main__content">
			<Sidebar
				class="main__content__aside"
				:widgetProducts="widgetProducts"
				:loading="isLoadingPromotedProducts"
				@filter="filterProductInRange"
			/>
			<ProductList
				class="main__content__main"
				:products="products"
				:pagination="pagination"
				:loading="isLoadingData"
				@update:page="pageChangeHandler"
			/>
		</div>
	</main>
</template>

<script setup>
	import { useStore } from "vuex";
	import { useRoute } from "vue-router";
	import { computed, onMounted, reactive, ref, watch } from "vue";
	import PageTitle from "@/components/template/Category/PageTitle.vue";
	import Sidebar from "@/components/template/Category/SidebarFilter.vue";
	import ProductList from "@/components/template/Category/ProductList.vue";
	import { product as productAPI } from "@/api";
	import productFactory from "@/utils/productFactory.js";

	const store = useStore();
	const route = useRoute();

	const widgetProducts = ref([]);
	const products = ref([]);
	const isLoadingData = ref(false);
	const isLoadingPromotedProducts = ref(false);

	const filter = reactive({
		orderBy: "",
		minPrice: 0,
		maxPrice: 10_000_000,
	});

	const pagination = reactive({
		current: 1,
		total: 0,
		pageSize: 12,
	});

	const category = computed(() => {
		if ("category" in route.params) {
			const rootCategory = store.state.category.rootCategories[route.params.slug];

			const category = rootCategory.children?.find?.(
				(category) => category.id === route.params.category,
			);

			return category || "";
		} else {
			const category = store.state.category.rootCategories[route.params.slug];
			return category || "";
		}
	});

	const getProductListHandler = async () => {
		try {
			isLoadingData.value = true;
			const query = {
				keyWord: "",
				minPrice: filter.minPrice,
				maxPrice: filter.maxPrice,
				CategoryCode: category.value.categoryCode,
				pageSize: pagination.pageSize,
				pageNumber: pagination.current,
			};

			const body = {
				Field: "Id",
				Order: "ASC",
				operation: "",
				DataType: "",
				Type: "sort",
			};

			switch (filter.orderBy) {
				case "":
					body.Field = "CreatedDate";
					body.Order = "DESC";
					break;
				// TODO
				case "popularity":
					body.Field = "Price";
					body.Order = "ASC";
					break;
				// TODO
				case "rating":
					body.Field = "Price";
					body.Order = "DESC";
					break;
				case "date":
					body.Field = "CreatedDate";
					body.Order = "DESC";
					break;
				case "price":
					body.Field = "Price";
					body.Order = "ASC";
					break;
				case "price-desc":
					body.Field = "Price";
					body.Order = "DESC";
					break;

				default:
					break;
			}

			const response = await productAPI.getFilteredList(query, [body]);

			pagination.total = response.data.TotalRecord;

			products.value = response.data.Data.map(
				productFactory.transformProductAPIResponseToCategoryProduct,
			);
		} catch (error) {}

		isLoadingData.value = false;
	};

	const getPromotedProducts = async () => {
		isLoadingPromotedProducts.value = true;

		try {
			const query = {
				keyWord: "",
				minPrice: 0,
				maxPrice: 10_000_000,
				CategoryCode: category.categoryCode,
				pageSize: 5,
				pageNumber: 1,
			};

			const body = {
				Field: "CreatedDate",
				Order: "DESC",
				operation: "",
				DataType: "",
				Type: "sort",
			};

			const response = await productAPI.getFilteredList(query, [body]);

			widgetProducts.value = response.data.Data.map(
				productFactory.transformProductAPIResponseToPromotedProduct,
			);
		} catch (error) {}
		isLoadingPromotedProducts.value = false;
	};

	const pageChangeHandler = (page) => {
		pagination.current = page;
		getProductListHandler();
	};

	const sortProductHandler = (orderBy) => {
		filter.orderBy = orderBy;
		getProductListHandler();
	};

	const filterProductInRange = ([minPrice, maxPrice]) => {
		filter.minPrice = minPrice;
		filter.maxPrice = maxPrice;
		getProductListHandler();
	};

	watch(
		() => route.params,
		() => {
			getProductListHandler();
			getPromotedProducts();
		},
		{ deep: true },
	);

	onMounted(() => {
		getProductListHandler();
		getPromotedProducts();
	});
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

				> :deep(div):first-child {
					min-height: 1020px;
					align-content: flex-start;
				}
			}
		}
	}
</style>
