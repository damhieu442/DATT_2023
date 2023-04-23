<template>
	<div class="homepage">
		<a-row :gutter="[24, 24]" type="flex" justify="space-between">
			<a-col :span="6">
				<Card>
					<template #title>
						<p class="font-bold">Tổng số đơn hàng tháng này</p>
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
	import CompleteOrderGrowth from "@/components/template/Homepage/CompleteOrderGrowthChart.vue";
	import Card from "@/components/template/Homepage/Card.vue";
	import { reactive, ref } from "vue";

	const order = reactive({
		all: 100,
		processing: 100,
		deliverying: 100,
		cancel: 100,
	});

	const statistic = ref({
		1: 15,
		2: 50,
		3: 70,
		4: 90,
		5: 150,
		6: 170,
	});
</script>

<style>
	.homepage {
	}
</style>
