<template>
	<main class="main">
		<h2 class="text-2xl">
			<button class="go-back" @click="goBack">
				<i class="fa-solid fa-chevron-left"></i> Trở lại
			</button>
			Chi tiết đơn hàng
		</h2>
		<LoadingPlaceholder :isLoading="isGettingDetail">
			<div class="title">
				<h4>Mã đơn hàng: {{ orderId }}</h4>
				<a-divider type="vertical" class="divider" />
				<p class="status" :style="{ color: EOrderStateColors[orderInfo.state] }">
					{{ OrderLabels[orderInfo.state] }}
				</p>
			</div>

			<a-row :gutter="[16, 8]" class="order-info">
				<a-col :span="12"> <strong>Họ tên:</strong> {{ orderInfo.customerName }}</a-col>
				<a-col :span="12">
					<strong>Số điện thoại:</strong>{{ orderInfo.customerPhone }}
				</a-col>
				<a-col :span="12">
					<strong>Ngày đặt:</strong>
					{{ orderInfo.createTime }}</a-col
				>
				<a-col :span="24">
					<strong>Địa chỉ:</strong> {{ orderInfo.deliveryAddress }}
				</a-col>
				<a-col :span="24">
					<strong>Ghi chú:</strong> {{ orderInfo.note || "-" }}</a-col
				> </a-row
			><a-divider />
			<ul class="ordered__list">
				<OrderItem
					v-for="item in orderInfo.items"
					:key="item.id + '-' + item.size"
					:item="item"
				/>
			</ul>
			<div class="actions">
				<a-button
					v-if="orderInfo.state === EOrderState.PROCESSING"
					type="primary"
					danger
					@click="cancelOrder"
				>
					Hủy đơn hàng
				</a-button>
				<table>
					<tbody>
						<tr>
							<th>
								<span>Tổng tiền:</span>
							</th>
							<td class="price">{{ orderInfo.totalPrice }} VNĐ</td>
						</tr>
						<tr>
							<th>
								<span> Phương thức thanh toán:</span>
							</th>
							<td>{{ PaymentMethodLabels[orderInfo.paymentMethod] }}</td>
						</tr>
					</tbody>
				</table>
			</div>
		</LoadingPlaceholder>
	</main>
</template>

<script setup>
	import { cart as cartAPI, product as productAPI } from "@/api";
	import { Modal, notification } from "ant-design-vue";
	import { onMounted, ref } from "vue";
	import { useRoute, useRouter } from "vue-router";
	import LoadingPlaceholder from "@/components/shared/LoadingPlaceholder.vue";
	import { OrderLabels, EOrderState, EOrderStateColors } from "@/constants/orders";
	import { orderFactory } from "@/utils/orderFactory";
	import { PaymentMethodLabels } from "@/constants/payment";
	import OrderItem from "@/components/template/OrderDetailPage/OrderItem.vue";

	const route = useRoute();
	const router = useRouter();
	const orderId = route.params.id;
	const isGettingDetail = ref(true);
	const orderInfo = ref(null);
	const productSizes = ref(new Map());

	const getDetailCartProducts = async () => {
		try {
			const productResponses = await Promise.all(
				orderInfo.value.items.map((product) => productAPI.getDetail(product.id)),
			);

			for (const response of productResponses) {
				productSizes.value.set(response.data.ShoeId, response.data.Size);
			}
		} catch (error) {}
	};

	const getData = async () => {
		isGettingDetail.value = true;

		try {
			const [products, order] = await Promise.all([
				cartAPI.getOrderList(orderId),
				cartAPI.getDetailOrder(orderId),
			]);

			if (products.status === 200 && order.status === 200) {
				const orderData = order.data;
				const orderList = products.data;

				orderInfo.value = orderFactory.transformCategoryAPIResponseToCategory(
					orderData,
					orderList,
				);
			}
		} catch (error) {
			console.log("Errror: ", error);
			notification.error({
				message: "Có lỗi xảy ra, vui lòng thử lại",
			});
		} finally {
			isGettingDetail.value = false;
		}
	};

	const goBack = () => {
		router.back();
	};

	const cancelOrder = () => {
		const modal = Modal.confirm({
			title: "Bạn có chắc chắn muốn hủy đơn hàng (Bạn sẽ không thể hoàn tác hành động này)?",
			cancelText: "Không",
			okText: "Hủy",
			onOk: async () => {
				try {
					const response = await cartAPI.cancelOrder(orderId, {
						BillId: orderId,
						IsCancel: 1,
						Status: EOrderState.CANCEL,
						Reason: "Người dùng hủy đơn hàng",
					});

					const updateProducts = orderInfo.value.items.map((product) => {
						const productSize = JSON.parse(productSizes.value.get(product.id));
						const selectedSize = productSize.find(
							(size) => size.SizeName === product.size,
						);
						selectedSize.Amount += product.amount;
						selectedSize.SoldNumber -= product.amount;
						return {
							ShoeId: product.id,
							Size: JSON.stringify(productSize),
						};
					});

					const productUpdateResponse = Promise.all(
						updateProducts.map((item) => productAPI.updateProduct(item.ShoeId, item)),
					).then(console.log);

					if (response.status > 199 && response.status < 300) {
						getData();
						modal.destroy();
					}
				} catch (error) {
					notification.error({ message: "Có lỗi xảy ra, thử lại sau" });
				}
			},
			onCancel: () => {
				modal.destroy();
			},
		});
	};

	onMounted(() => {
		getData().then(getDetailCartProducts);
	});
</script>

<style lang="scss" scoped>
	.main {
		> h2 {
			font-size: 1.5rem; /* 24px */
			line-height: 2rem; /* 32px */
		}

		.divider {
			height: auto;
			border-left: 1px solid gray;
			margin-left: 1rem;
			margin-right: 1rem;
		}
	}

	.go-back {
		color: rgb(96 165 250);
		font-size: 1.125rem; /* 18px */
		line-height: 1.75rem; /* 28px */
		cursor: pointer;
		text-transform: uppercase;
		background-color: transparent;
		border: 0;
		margin-right: 1.25rem;
	}

	.title {
		display: flex;
		font-size: 1rem;
		line-height: 2rem;
		margin-bottom: 1.5rem;

		> h4 {
			margin: 0;
			flex: 1 1 auto;
		}

		.status {
			margin: 0;
			text-transform: uppercase;
			color: rgb(74 222 128);
		}
	}

	.order-info {
		strong {
			margin-right: 0.25rem;
		}
	}

	.actions {
		margin-top: 1.25rem;
		display: flex;
		justify-content: space-between;
		align-items: flex-start;

		> table {
			margin-left: auto;

			th {
				text-align: left;
				padding-right: 1rem;
			}
		}

		.price {
			font-size: 1.125rem; /* 18px */
			line-height: 1.75rem; /* 28px */
			color: rgb(239 68 68);
		}
	}
</style>
