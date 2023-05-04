<template>
	<div>
		<div class="flex justify-between items-center my-4">
			<h1 class="text-2xl">Danh sách đơn hàng</h1>
			<div>
				<a-button
					class="rounded inline-block mx-2 py-1 bg-sky-400 text-base text-white h-auto px-6 ml-4 align-middle hover:text-white hover:bg-sky-500"
					:loading="isExporting"
					@click="exportExcelHandler"
					>Xuất excel</a-button
				>
				<a-button
					class="rounded inline-block mx-2 py-1 bg-sky-400 text-base text-white h-auto px-6 ml-4 align-middle hover:text-white hover:bg-sky-500"
					@click="getData"
					>Làm mới</a-button
				>
			</div>
		</div>

		<div class="my-5 px-7 py-6 border rounded-md bg-cyan-200">
			<OrderFilter />
		</div>
		<OrderListPage :loading="isGettingData" :data="orders" class="my-5" />
		<ThePagination :data="pagination" />
	</div>
</template>

<script setup>
	import { useRoute } from "vue-router";
	import { onMounted, reactive, ref, watch } from "vue";
	import { notification } from "ant-design-vue";

	import { order as orderAPI } from "@/api";
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
	const isExporting = ref(false);
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

	const exportExcelHandler = async () => {
		isExporting.value = true;

		try {
			const result = await orderAPI.exportExcel();

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
