<template>
	<div>
		<h1 class="text-2xl">Chi tiết đơn hàng</h1>
		<p>
			Trạng thái:
			<span :style="{ color: stateColors[order.state] }">{{ orderStates[order.state] }}</span>
		</p>
		<a-divider />
		<a-row :gutter="[24, 24]">
			<a-col :span="8">
				<h3 class="uppercase text-lg">Mã đơn hàng</h3>
				<p>{{ order.id || "-" }}</p>
			</a-col>
			<a-col :span="8">
				<h3 class="uppercase text-lg">Ngày tạo</h3>
				<p>{{ order.createTime || "-" }}</p>
			</a-col>
			<a-col :span="8">
				<h3 class="uppercase text-lg">Địa chỉ nhận</h3>
				<p>{{ order.address || "-" }}</p>
			</a-col>
			<a-col :span="24">
				<OrderItemsTable :data="order.orders" :loading="isGettingData" />
			</a-col>
			<a-col :span="7">
				<h3 class="uppercase text-lg">Phương thức thanh toán</h3>
				<p>Trực tiếp</p>
			</a-col>
			<a-col :span="5">
				<h3 class="uppercase text-lg">Phí giao hàng</h3>
				<p>{{ order.shipping || "0" }} VNĐ</p>
			</a-col>
			<a-col :span="6">
				<h3 class="uppercase text-lg">Tổng tiền sản phẩm</h3>
				<p>{{ order.amount || "0" }} VNĐ</p>
			</a-col>
			<a-col :span="6">
				<h3 class="uppercase text-lg">Tổng hóa đơn</h3>
				<p class="text-red-500 font-bold text-lg">{{ order.total || "-" }} VNĐ</p>
			</a-col>
			<a-col :span="24">
				<a-space class="flex justify-end" :size="24">
					<template v-for="button in stateButtons">
						<button
							v-if="button.show"
							:key="button.key"
							:style="{
								background: button.color,
							}"
							class="text-white px-8 py-2 rounded disabled:cursor-not-allowed"
							:loading="isSubmitting"
							:disabled="isSubmitting"
							@click="button.handler"
						>
							{{ button.label }}
						</button>
					</template>
				</a-space>
			</a-col>
		</a-row>
		<CancelOrderModal ref="rfCancelModal" @submit="cancelOrder" />
	</div>
</template>

<script setup>
	import orderStates, { COLORS as stateColors } from "@/constants/order";
	import OrderItemsTable from "@/components/template/OrderPage/DetailOrderItemList.vue";
	import { computed, onMounted, reactive, ref } from "vue";
	import { notification } from "ant-design-vue";
	import { useRoute } from "vue-router";
	import CancelOrderModal from "@/components/template/OrderPage/CancelOrderModal.vue";

	const route = useRoute();
	const rfCancelModal = ref(null);

	const [deliverying, processing, complete, rejected] = Object.entries(orderStates);

	const isSubmitting = ref(false);
	const isGettingData = ref(false);
	const order = reactive({});

	const changeState = async (status) => {
		if (status === rejected[0]) {
			rfCancelModal.value.open();
			return;
		}

		try {
			isSubmitting.value = true;

			// await this.$api.order.update(this.$route.params.id, { status });
			order.state = status;
			// this.$set(this.order, "state", status);

			notification.success({ message: "Cập nhật thành công" });
		} catch (error) {
			console.log(error);
			notification.error({ message: "Có lỗi xảy ra, thử lại sau" });
		} finally {
			isSubmitting.value = false;
		}
	};

	const cancelOrder = (form) => {
		console.log("Form: ", form);
		notification.success({ message: "Hủy đơn hàng thành công" });
		order.state = rejected[0];
	};

	const stateButtons = computed(() => {
		return [
			{
				key: deliverying[0],
				label: "Xác nhận",
				color: stateColors[deliverying[0]],
				show: order.state === processing[0],
				handler: () => changeState(deliverying[0]),
			},
			{
				key: complete[0],
				label: "Hoàn thành",
				color: stateColors[complete[0]],
				show: order.state === deliverying[0],
				handler: () => changeState(complete[0]),
			},
			{
				key: rejected[0],
				label: "Hủy đơn",
				color: stateColors[rejected[0]],
				show: order.state === deliverying[0] || order.state === processing[0],
				handler: () => changeState(rejected[0]),
			},
		];
	});

	const getOrderDetail = async () => {
		isGettingData.value = true;

		// const id = route.params.id
		const orderDetail = await new Promise((res) =>
			setTimeout(res, 1500, {
				id: "6278bca38bd5470009e5ded6",
				orders: [
					{
						id: 1,
						name: "Tên sản phẩm 1",
						image: "https://images.unsplash.com/photo-1644982653424-1bfed7f972a2?ixlib=rb-1.2.1&ixid=MnwxMjA3fDF8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=789&q=80",
						category: "Nước ép hạt",
						createTime: 1652149368173,
						price: 100,
						amount: 10,
					},
					{
						id: 2,
						name: "Tên sản phẩm 1",
						image: "https://images.unsplash.com/photo-1644982653424-1bfed7f972a2?ixlib=rb-1.2.1&ixid=MnwxMjA3fDF8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=789&q=80",
						category: "Nước ép hạt",
						createTime: 1652149368173,
						price: 100,
						amount: 10,
					},
					{
						id: 3,
						name: "Tên sản phẩm 1",
						image: "https://images.unsplash.com/photo-1644982653424-1bfed7f972a2?ixlib=rb-1.2.1&ixid=MnwxMjA3fDF8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=789&q=80",
						category: "Nước ép hạt",
						createTime: 1652149368173,
						price: 100,
						amount: 10,
					},
				],
				discount: 0,
				name: "we wre",
				phone: "01718890326",
				address: "wer, werwe",
				note: "abc",
				state: "processing",
				amount: 1581,
				shipping: 20,
				total: 1601,
				user: "619f77b553cc5a1858ef3670",
				createTime: "2022-05-09T06:04:52.338Z",
			}),
		);

		Object.assign(order, orderDetail);

		isGettingData.value = false;
	};

	onMounted(() => {
		getOrderDetail();
	});
</script>
