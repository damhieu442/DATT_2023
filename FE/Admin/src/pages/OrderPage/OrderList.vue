<template>
	<div><h1 class="text-2xl">Danh sách đơn hàng</h1></div>
	<div class="my-5 px-7 py-6 border rounded-md bg-cyan-200">
		<OrderFilter />
	</div>
	<OrderListPage :loading="isGettingData" :data="orders" class="my-5" />
	<ThePagination :data="pagination" />
</template>

<script setup>
	import { useRoute } from "vue-router";
	import { onMounted, reactive, ref, watch } from "vue";
	import { notification } from "ant-design-vue";

	import { order as orderAPI } from "@/api";
	import ORDER_STATE from "@/constants/order";
	import OrderFilter from "@/components/template/OrderPage/OrderFilter.vue";
	import ThePagination from "@/components/shared/ThePagination.vue";
	import OrderListPage from "@/components/template/OrderPage/OrderList.vue";
	import { orderAdapter } from "@/utils/orderAdapter";

	const MAX_RECORD = 12;
	const DEFAULT_PAGE = 1;

	const route = useRoute();

	const pagination = reactive({
		page: Number(route.query.pageNum) || DEFAULT_PAGE,
		total: 0,
		perPage: Number(route.query.pageSize) || MAX_RECORD,
	});
	const isGettingData = ref(false);
	const orders = ref([]);

	const getData = async () => {
		isGettingData.value = true;

		const { pageNum: page, pageSize: limit, search } = route.query;
		const query = {
			pageNumber: page || DEFAULT_PAGE,
			pageSize: limit || MAX_RECORD,
			keyWord: search || undefined,
		};
		const body = {
			Field: "CreatedDate",
			Order: "DESC",
			operation: "",
			DataType: "",
			Type: "sort",
		};

		try {
			const response = await orderAPI.getList([body], query);
			if (response.status > 199 && response.status < 300) {
				const orderData = response.data;
				if ("Data" in orderData) {
					const data = orderData.Data.map(orderAdapter.transformAPIResponseToOrderData);
					orders.value = data;

					Object.assign(pagination, {
						page: Number(orderData.CurrentPage),
						total: Number(orderData.TotalRecord),
						perPage: Number(orderData.CurrentPageRecords),
					});
				} else {
					orders.value = [];
				}
			}
		} catch (error) {
			console.log(error);
			notification.error({ message: "Có lỗi xảy ra, không thế lấy danh sách đơn hàng" });
		} finally {
			isGettingData.value = false;
		}
	};

	onMounted(() => {
		getData();
	});

	watch(
		() => route.query,
		() => {
			getData();
		},
	);
</script>
