<template>
	<div>
		<div class="flex justify-between items-center my-4">
			<h1 class="text-xl font-bold">Danh sách sản phẩm</h1>
			<router-link
				class="py-2 px-14 mr-4 inline-block bg-blue-400 text-white hover:text-white text-base rounded-lg"
				to="/san-pham/tao-moi"
			>
				Tạo sản phẩm
			</router-link>
		</div>
		<div class="my-5 px-7 py-6 border bg-cyan-200 rounded-md">
			<ProductFilter :loading="isSearchingProduct" @submit="filterProduct" />
		</div>
		<ProductTableList
			:loading="isGettingData"
			:data="products"
			@delete="openConfirmDeleteDialog"
		/>
		<ThePagination :data="pagination" />
		<DeleteProductModal ref="rfDeleteModal" @delete="deleteProductHandler" />
	</div>
</template>

<script setup>
	import { product as productAPI } from "@/api";
	import ProductFilter from "@/components/template/ProductPage/ProductFilter.vue";
	import { onMounted, reactive, ref, watch } from "vue";

	import { DEFAULT_VALUES } from "@/constants/config";
	import { useRoute } from "vue-router";
	import { productFactory } from "@/utils/products";
	import ProductTableList from "@/components/template/ProductPage/ProductList.vue";
	import ThePagination from "@/components/shared/ThePagination.vue";
	import DeleteProductModal from "@/components/template/ProductPage/ConfirmDeleteProductModal.vue";
	import { notification } from "ant-design-vue";

	const route = useRoute();
	const isSearchingProduct = ref(false);
	const isGettingData = ref(false);
	const pagination = reactive({
		page: Number(route.query.pageNum) || DEFAULT_VALUES.DEFAULT_PAGE,
		total: DEFAULT_VALUES.DEFAULT_PAGE_COUNT,
		perPage: Number(route.query.pageSize) || DEFAULT_VALUES.DEFAULT_PAGE_SIZE,
	});
	const products = ref([]);
	const rfDeleteModal = ref(null);

	const openConfirmDeleteDialog = (product) => {
		rfDeleteModal.value.open(product);
	};

	const getProductList = async (isSearching, filter) => {
		isSearching && (isSearchingProduct.value = true);
		isGettingData.value = true;
		try {
			const { pageNum: page, pageSize: limit } = route.query;

			const query = {
				pageSize: limit || DEFAULT_VALUES.DEFAULT_PAGE_SIZE,
				pageNumber: page || DEFAULT_VALUES.DEFAULT_PAGE,
			};

			const body = {
				Field: "ShoeId",
				Order: "ASC",
				operation: "",
				DataType: "",
				Type: "sort",
			};

			if (filter) {
				query.keyWord = filter.search;
				query.minPrice = filter.minPrice;
				query.maxPrice = filter.maxPrice;
				query.CategoryCode = filter.category;

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
			}

			const response = await productAPI.getFilteredList(query, [body]);
			console.log("Response: ", response);
			if (response.status > 199 && response.status < 300) {
				if ("Data" in response.data) {
					const data = response.data;
					console.log("data", data);

					products.value = data.Data.map(productFactory.transformResponseToProductData);

					Object.assign(pagination, {
						page: Number(data.CurrentPage),
						total: Number(data.TotalRecord),
						perPage: Number(data.CurrentPageRecords),
					});
				} else {
					products.value = [];
					notification.error({
						message: "Không tìm được kết quả phù hợp",
					});
				}
			} else {
				products.value = [];
				notification.error({
					message: "Có lỗi xảy ra, vui lòng thử lại",
				});
			}
		} catch (error) {
			console.log("Errro: ", error);
			notification.error({
				message: "Có lỗi xảy ra, vui lòng thử lại",
			});
		} finally {
			isSearching && (isSearchingProduct.value = false);
			isGettingData.value = false;
		}
	};

	const filterProduct = (filter) => {
		getProductList(true, filter);
	};

	const deleteProductHandler = async (activeProd) => {
		try {
			const { id } = activeProd;
			const response = await productAPI.deleteProduct(id);

			if (response.status > 199 && response.status < 300) {
				notification.success({ message: "Xóa sản phẩm thành công" });
				getProductList();
			} else {
				throw new Error();
			}
		} catch (error) {
			notification.error({ message: "Có lỗi xảy ra, vui lòng thử lại" });
		}
	};

	onMounted(() => getProductList());

	watch(
		() => route.query,
		() => {
			getProductList();
		},
		{ deep: true },
	);
</script>
