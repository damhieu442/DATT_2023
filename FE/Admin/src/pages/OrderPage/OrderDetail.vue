<template>
	<div>
		<h1 class="text-2xl">
			<button class="cursor-pointer mr-4 bg-transparent border-0" @click="goBack">
				<i class="fas fa-arrow-left" /></button
			>Chi tiết đơn hàng
		</h1>
		<p>
			Trạng thái:
			<span :style="{ color: stateColors[order.state] }">{{ orderStates[order.state] }}</span>
		</p>
		<a-divider />
		<a-row :gutter="[24, 24]">
			<a-col :span="6">
				<h3 class="uppercase text-lg">Mã đơn hàng</h3>
				<p>{{ order.id || "-" }}</p>
			</a-col>
			<a-col :span="6">
				<h3 class="uppercase text-lg">Ngày tạo</h3>
				<p>{{ order.createTime || "-" }}</p>
			</a-col>
			<a-col :span="6">
				<h3 class="uppercase text-lg">Địa chỉ nhận</h3>
				<p>{{ order.address || "-" }}</p>
			</a-col>
			<a-col :span="6">
				<h3 class="uppercase text-lg">Người nhận</h3>
				<p>{{ order.name || "-" }}</p>
			</a-col>
			<a-col :span="8">
				<h3 class="uppercase text-lg">Số điện thoại người nhận</h3>
				<p>{{ order.phone || "-" }}</p>
			</a-col>
			<a-col :span="16">
				<h3 class="uppercase text-lg">Ghi chú</h3>
				<p>{{ order.note || "-" }}</p>
			</a-col>
			<a-col :span="24">
				<OrderItemsTable :data="order.orders" :loading="isGettingData" />
			</a-col>
			<a-col :span="6">
				<h3 class="uppercase text-lg">Phương thức thanh toán</h3>
				<p>{{ PaymentMethodLabels[order.paymentMethod] }}</p>
			</a-col>
			<a-col :span="6">
				<h3 class="uppercase text-lg">Tình trạng thanh toán</h3>
				<p
					class="font-bold text-lg"
					:class="{
						'text-zinc-800': !order.isPayment,
						'text-cyan-500': order.isPayment,
					}"
				>
					{{ order.isPayment ? "Đã thanh toán" : "Chưa thanh toán" }}
				</p>
			</a-col>
			<a-col :span="6">
				<h3 class="uppercase text-lg">Tổng hóa đơn</h3>
				<p class="text-red-500 font-bold text-lg">{{ order.total || "-" }} VNĐ</p>
			</a-col>

			<a-col :span="6" v-if="!!order.reason">
				<h3 class="uppercase text-lg">Lý do hủy</h3>
				<p>{{ order.reason }}</p>
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
	import orderStates, { COLORS as stateColors, keys as EOrderState } from "@/constants/order";
	import OrderItemsTable from "@/components/template/OrderPage/DetailOrderItemList.vue";
	import { computed, onMounted, reactive, ref } from "vue";
	import { notification } from "ant-design-vue";
	import { useRoute, useRouter } from "vue-router";
	import CancelOrderModal from "@/components/template/OrderPage/CancelOrderModal.vue";
	import { order as orderAPI, mics as micsAPI, product as productAPI } from "@/api";
	import { orderAdapter } from "@/utils/orderAdapter";
	import { PaymentMethodLabels } from "@/constants/order";

	const route = useRoute();
	const router = useRouter();
	const rfCancelModal = ref(null);

	const isSubmitting = ref(false);
	const isGettingData = ref(false);
	const order = reactive({});
	const productSizes = ref(new Map());
	const orderId = route.params.id;

	const goBack = () => {
		router.back();
	};

	const getDetailCartProducts = async () => {
		try {
			const productResponses = await Promise.all(
				order.orders.map((product) => productAPI.getDetail(product.id)),
			);

			for (const response of productResponses) {
				productSizes.value.set(response.data.ShoeId, response.data.Size);
			}
		} catch (error) {}
	};

	const changeState = async (status) => {
		if (status === EOrderState.rejected) {
			rfCancelModal.value.open();
			return;
		}

		try {
			isSubmitting.value = true;

			const response = await orderAPI.updateDetailOrder(orderId, {
				BillId: orderId,
				Status: status,
			});

			order.state = status;

			notification.success({ message: "Cập nhật thành công" });
		} catch (error) {
			console.log(error);
			notification.error({ message: "Có lỗi xảy ra, thử lại sau" });
		} finally {
			isSubmitting.value = false;
		}
	};

	const cancelOrder = async (form) => {
		console.log("Form: ", form);

		try {
			isSubmitting.value = true;

			const response = await orderAPI.updateDetailOrder(orderId, {
				BillId: orderId,
				Status: EOrderState.rejected,
				Reason: form.reason,
			});

			const updateProducts = order.orders.map((product) => {
				const productSize = JSON.parse(productSizes.value.get(product.id));
				const selectedSize = productSize.find((size) => size.SizeName === product.size);
				selectedSize.Amount += product.amount;
				selectedSize.SoldNumber -= product.amount;
				return {
					ShoeId: product.id,
					Size: JSON.stringify(productSize),
				};
			});

			const productUpdateResponse = Promise.all(
				updateProducts.map((item) => productAPI.updateProductv2(item.ShoeId, item)),
			).then(console.log);

			micsAPI.sendEmail(
				orderAdapter.generateOrderCancelEmail({
					id: orderId,
					name: order.name,
					email: order.email,
					reason: form.reason,
				}),
			);

			order.state = EOrderState.rejected;
			notification.success({ message: "Hủy đơn hàng thành công" });
		} catch (error) {
			console.log(error);
			notification.error({ message: "Có lỗi xảy ra, thử lại sau" });
		} finally {
			isSubmitting.value = false;
		}
	};

	const stateButtons = computed(() => {
		return [
			{
				key: EOrderState.deliverying,
				label: "Xác nhận",
				color: stateColors[EOrderState.deliverying],
				show: order.state === EOrderState.processing,
				handler: () => changeState(EOrderState.deliverying),
			},
			{
				key: EOrderState.complete,
				label: "Hoàn thành",
				color: stateColors[EOrderState.complete],
				show: order.state === EOrderState.deliverying,
				handler: () => changeState(EOrderState.complete),
			},
			{
				key: EOrderState.rejected,
				label: "Hủy đơn",
				color: stateColors[EOrderState.rejected],
				show:
					order.state === EOrderState.deliverying ||
					order.state === EOrderState.processing,
				handler: () => changeState(EOrderState.rejected),
			},
		];
	});

	const getOrderDetail = async () => {
		isGettingData.value = true;
		try {
			const [products, orderResponse] = await Promise.all([
				orderAPI.getOrderProductList(orderId),
				orderAPI.getDetailOrder(orderId),
			]);

			if (products.status === 200 && orderResponse.status === 200) {
				const orderData = orderResponse.data;
				const orderList = products.data;

				const orderDetail = orderAdapter.transformAPIResponseToOrderDetailData(
					orderData,
					orderList,
				);
				console.log("Detailt: ", orderDetail);
				Object.assign(order, orderDetail);
			}
		} catch (error) {
			console.log("Error: ", error);
		}

		isGettingData.value = false;
	};

	onMounted(() => {
		getOrderDetail().then(getDetailCartProducts);
	});
</script>
