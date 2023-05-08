<template>
	<div class="homepage">
		<a-row :gutter="[24, 24]" type="flex" justify="space-between">
			<a-col :span="6">
				<Card>
					<template #title>
						<p class="font-bold">Tổng số đơn hàng trong năm</p>
					</template>
					<p class="text-xl m-0">{{ order.all }} đơn</p>
				</Card>
			</a-col>
			<a-col :span="6">
				<Card>
					<template #title>
						<p class="font-bold">Tổng số đơn hàng đang chờ xác nhận</p>
					</template>
					<p class="text-xl m-0">{{ order.processing }} đơn</p>
				</Card>
			</a-col>
			<a-col :span="6">
				<Card>
					<template #title>
						<p class="font-bold">Tổng số đơn hàng đang được giao</p>
					</template>
					<p class="text-xl m-0">{{ order.deliverying }} đơn</p>
				</Card>
			</a-col>
			<a-col :span="6">
				<Card>
					<template #title>
						<p class="font-bold">Tổng số đơn hàng đã hủy</p>
					</template>
					<p class="text-xl m-0">{{ order.cancel }} đơn</p>
				</Card>
			</a-col>
		</a-row>
		<div class="my-4">
			<a-row :gutter="[24, 24]">
				<a-col :span="12">
					<h3 class="text-xl font-normal">Thống kê doanh thu theo tháng</h3>
					<RevenueStatistic :growth-statistic="revenueStatistic" />
				</a-col>
				<a-col :span="12">
					<h3 class="text-xl font-normal">Thống kê đơn hàng đã hoàn thành theo tháng</h3>
					<CompleteOrderGrowth :growth-statistic="statistic" />
				</a-col>

				<a-col :span="12">
					<ProductStatisticList title="Sản phẩm bán chạy nhất" :products="saleProducts">
						<template #product-detail="{ product }">
							<p class="m-0 w-full mt-1">
								<span class="mr-2">
									<strong> Giá bán : </strong> {{ product.price }} (-{{
										product.discount
									}})
								</span>
								<span class="mr-2">
									<strong>Số lượng bán:</strong> {{ product.sold }}
								</span>
								<span class="mr-2">
									<strong>Còn lại:</strong> {{ product.remain }}
								</span>
							</p>
						</template>
					</ProductStatisticList>
				</a-col>
				<a-col :span="12">
					<ProductStatisticList
						title="Sản phẩm có tỉ lệ tồn hàng cao nhất"
						:products="remainProducts"
					>
						<template #product-detail="{ product }">
							<p class="m-0 w-full mt-1">
								<span class="mr-2">
									<strong> Giá bán : </strong> {{ product.price }} (-{{
										product.discount
									}})
								</span>
								<span class="mr-2">
									<strong>Số lượng:</strong> {{ product.total }}
								</span>
								<span class="mr-2">
									<strong>Hàng tồn:</strong> {{ product.remain }}
								</span>
								<span class="mr-2">
									<strong>Tỷ lệ tồn:</strong>
									{{ Math.trunc((product.remain / product.total) * 100) }} %
								</span>
								<span class="mr-2">
									<strong>Đánh giá:</strong>
									{{ product.rating }}
								</span>
							</p>
						</template>
					</ProductStatisticList>
				</a-col>
				<a-col :span="24">
					<h3 class="text-xl font-normal">
						Thống kê sản phẩm đã bán trong tháng này theo danh mục
					</h3>
					<CategoryStatistic :growth-statistic="categories" />
				</a-col>
			</a-row>
		</div>
	</div>
</template>

<script setup>
	import { onMounted, reactive, ref } from "vue";
	import { notification } from "ant-design-vue";

	import { dashboard as dashboardAPI, product as productAPI } from "@/api";

	import { productFactory } from "@/utils/products";

	import CompleteOrderGrowth from "@/components/template/Homepage/CompleteOrderGrowthChart.vue";
	import ProductStatisticList from "@/components/template/Homepage/ProductStatisticList.vue";
	import RevenueStatistic from "@/components/template/Homepage/RevenueStatistic.vue";
	import CategoryStatistic from "@/components/template/Homepage/CategoryStatistic.vue";
	import Card from "@/components/template/Homepage/Card.vue";

	const PRODUCT_LIST_CONFIG = {
		COUNT: 8,
		PAGE: 1,
	};

	const order = reactive({
		all: 0,
		processing: 0,
		deliverying: 0,
		cancel: 0,
	});

	const statistic = ref({});
	const saleProducts = ref([]);
	const remainProducts = ref([]);
	const revenueStatistic = ref({});
	const categories = ref({});

	const getOrderStatistics = async () => {
		try {
			const response = await dashboardAPI.getStatistics();

			if (response.status > 199 && response.status < 300) {
				Object.assign(order, {
					all: response.data[0].TotalOrder,
					processing: response.data[0].ProcessingOrder,
					deliverying: response.data[0].DeliveryingOrder,
					cancel: response.data[0].CancelOrder,
				});
			}
		} catch (error) {
			console.log("err", error);
			notification.error({ message: "Có lỗi xảy ra, vui lòng thử lại" });
		}
	};

	const getRevenueStatistics = async () => {
		try {
			const response = await dashboardAPI.getRevenueData();

			if (response.status > 199 && response.status < 300) {
				revenueStatistic.value = response.data[0];
			}
		} catch (error) {
			notification.error({
				message: "Có lỗi xảy ra, không lấy được dữ liệu doanh thu, vui lòng thử lại",
			});
		}
	};

	const getSoldCategoryProducts = async () => {
		try {
			const response = await dashboardAPI.getCategoryData();

			if (response.status > 199 && response.status < 300) {
				categories.value = response.data.reduce((categories, category) => {
					categories[category.CategoryName] = category.TotalSold;
					return categories;
				}, {});
			}
		} catch (error) {
			console.log("err", error);
			notification.error({ message: "Có lỗi xảy ra, vui lòng thử lại" });
		}
	};

	const getMonthlyOrderData = async () => {
		try {
			const response = await dashboardAPI.getChartData();

			if (response.status > 199 && response.status < 300) {
				statistic.value = response.data[0];
			}
		} catch (error) {
			console.log("err", error);
			notification.error({ message: "Có lỗi xảy ra, vui lòng thử lại" });
		}
	};

	const getSalesProducts = async () => {
		try {
			const query = {
				pageSize: PRODUCT_LIST_CONFIG.COUNT,
				pageNumber: PRODUCT_LIST_CONFIG.PAGE,
				minPrice: 0,
				maxPrice: 100_000_000,
				keyWord: undefined,
			};

			const body = {
				Field: "TotalSold",
				Order: "DESC",
				operation: "",
				DataType: "",
				Type: "sort",
			};

			const response = await productAPI.getFilteredList(query, [body]);

			if (response.status > 199 && response.status < 300) {
				if ("Data" in response.data) {
					const data = response.data;

					saleProducts.value = data.Data.map(
						productFactory.transformResponseToProductData,
					);
				} else {
					saleProducts.value = [];
					notification.error({
						message: "Không lấy được danh sách sản phẩm bán chạy",
					});
				}
			} else {
				saleProducts.value = [];
				notification.error({
					message:
						"Có lỗi xảy ra, không lấy được danh sách sản phẩm bán chạy, vui lòng thử lại",
				});
			}
		} catch (error) {
			console.log("Error: ", error);
			notification.error({
				message:
					"Có lỗi xảy ra, không lấy được danh sách sản phẩm bán chạy, vui lòng thử lại",
			});
		}
	};

	const getHighestRemainProducts = async () => {
		try {
			const query = {
				pageSize: PRODUCT_LIST_CONFIG.COUNT,
				pageNumber: PRODUCT_LIST_CONFIG.PAGE,
				minPrice: 0,
				maxPrice: 100_000_000,
				keyWord: undefined,
			};

			const body = {
				Field: "Inventory",
				Order: "DESC",
				operation: "",
				DataType: "",
				Type: "sort",
			};

			const response = await productAPI.getFilteredList(query, [body]);

			if (response.status > 199 && response.status < 300) {
				if ("Data" in response.data) {
					const data = response.data;

					remainProducts.value = data.Data.map(
						productFactory.transformResponseToProductData,
					);
				} else {
					remainProducts.value = [];
					notification.error({
						message: "Không lấy được danh sách sản phẩm tồn",
					});
				}
			} else {
				remainProducts.value = [];
				notification.error({
					message:
						"Có lỗi xảy ra, không lấy được danh sách sản phẩm tồn, vui lòng thử lại",
				});
			}
		} catch (error) {
			console.log("Error: ", error);
			notification.error({
				message: "Có lỗi xảy ra, không lấy được danh sách sản phẩm tồn, vui lòng thử lại",
			});
		}
	};

	onMounted(() => {
		getOrderStatistics();
		getRevenueStatistics();
		getMonthlyOrderData();
		getSalesProducts();
		getHighestRemainProducts();
		getSoldCategoryProducts();
	});
</script>

<style>
	.homepage {
	}
</style>
