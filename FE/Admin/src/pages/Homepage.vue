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
			<h2 class="text-2xl font-normal">Thống kê đơn hàng đã hoàn thành theo tháng</h2>
			<CompleteOrderGrowth :growth-statistic="statistic" />
		</div>
	</div>
</template>

<script setup>
	import { onMounted, reactive, ref } from "vue";

	import { dashboard as dashboardAPI } from "@/api";

	import CompleteOrderGrowth from "@/components/template/Homepage/CompleteOrderGrowthChart.vue";
	import Card from "@/components/template/Homepage/Card.vue";
	import { notification } from "ant-design-vue";

	const order = reactive({
		all: 0,
		processing: 0,
		deliverying: 0,
		cancel: 0,
	});

	const statistic = ref({});

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

	onMounted(() => {
		getOrderStatistics();
		getMonthlyOrderData();
	});
</script>

<style>
	.homepage {
	}
</style>
