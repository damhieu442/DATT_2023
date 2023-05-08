<template>
	<div>
		<div class="flex justify-between items-center my-4">
			<h1 class="text-xl font-bold">Danh sách sản phẩm</h1>
			<div>
				<a-button
					class="rounded border-0 inline-block mx-2 py-1 bg-sky-400 text-base text-white h-auto px-6 ml-4 align-middle hover:text-white hover:bg-sky-500"
					:loading="isExporting"
					@click="exportExcelHandler"
					>Xuất excel</a-button
				>
				<a-button
					class="rounded border-0 inline-block mx-2 py-1 bg-sky-400 text-base text-white h-auto px-6 ml-4 align-middle hover:text-white hover:bg-sky-500"
					@click="getProductList"
					>Làm mới</a-button
				>

				<router-link
					class="py-1 px-6 mx-2 rounded inline-block border border-transparent bg-blue-400 text-white hover:text-white text-base"
					to="/san-pham/tao-moi"
				>
					Tạo sản phẩm
				</router-link>
			</div>
		</div>
		<div class="my-5 px-7 py-6 border bg-cyan-200 rounded-md">
			<ProductFilter :loading="isSearchingProduct" @submit="filterProduct" />
		</div>
		<ProductTableList
			:loading="isGettingData"
			:data="products"
			@delete="openConfirmDeleteDialog"
		/>
		<ThePagination
			:data="pagination"
			@update:page="getProductList"
			@update:size="getProductList"
		/>
		<DeleteProductModal ref="rfDeleteModal" @delete="deleteProductHandler" />
	</div>
</template>

<script setup>
	import { useRoute } from "vue-router";
	import { notification } from "ant-design-vue";
	import { onMounted, reactive, ref } from "vue";

	import { product as productAPI } from "@/api";
	import ProductFilter from "@/components/template/ProductPage/ProductFilter.vue";
	import { DEFAULT_VALUES } from "@/constants/config";
	import { productFactory } from "@/utils/products";
	import ProductTableList from "@/components/template/ProductPage/ProductList.vue";
	import ThePagination from "@/components/shared/ThePagination.vue";
	import DeleteProductModal from "@/components/template/ProductPage/ConfirmDeleteProductModal.vue";

	const route = useRoute();
	const isExporting = ref(false);
	const isGettingData = ref(false);
	const isSearchingProduct = ref(false);
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
				minPrice: 0,
				maxPrice: 100_000_000,
				keyWord: undefined,
			};

			const body = {
				Field: "ShoeId",
				Order: "ASC",
				operation: "",
				DataType: "",
				Type: "sort",
			};

			if (filter) {
				query.keyWord = filter.search || undefined;
				query.minPrice = filter.minPrice;
				query.maxPrice = filter.maxPrice;
				query.CategoryCode = filter.category;

				switch (filter.sort) {
					case "":
						body.Field = "CreatedDate";
						body.Order = "DESC";

						break;
					case "popularity":
						body.Field = "AvgStar";
						body.Order = "ASC";

						break;
					case "rating":
						body.Field = "AvgStar";
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

			console.log("Body: ", body);

			const response = await productAPI.getFilteredList(query, [body]);

			if (response.status > 199 && response.status < 300) {
				if ("Data" in response.data) {
					const data = response.data;

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
		console.log("Filter: ", filter);
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

	const exportExcelHandler = async () => {
		isExporting.value = true;

		try {
			const result = await productAPI.exportExcel();

			if (result.status === 200) {
				const blob = new Blob([result.data], {
					type:
						result.data.type ||
						"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
				});

				const downloadURL = URL.createObjectURL(blob);
				const aEl = document.createElement("a");

				aEl.href = downloadURL;
				aEl.download = "bao_cao";
				aEl.style.display = "none";
				document.body.appendChild(aEl);
				aEl.click();
				aEl.remove();
			} else if (result.status > 499) {
				throw new Error("Server error");
			}
		} catch (error) {
			notification.error({ message: "Có lỗi xảy ra, vui lòng thử lại" });
		} finally {
			isExporting.value = false;
		}
	};

	onMounted(() => getProductList());
</script>
