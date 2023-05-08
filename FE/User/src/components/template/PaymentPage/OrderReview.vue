<template>
	<div class="order-overview">
		<h3>ĐƠN HÀNG CỦA BẠN</h3>
		<table class="order-overview__table">
			<thead>
				<tr>
					<th>Sản phẩm</th>
					<th>Tổng</th>
				</tr>
			</thead>
			<tbody>
				<tr
					v-for="product in productList"
					:key="product.id"
					:class="{ error: runningOutCarts.includes(product.cartId) }"
				>
					<td>
						{{ product.name }} - {{ product.size }}
						<strong> x {{ product.quantity }}</strong>
					</td>
					<td>
						<strong> {{ product.total }}đ </strong>
					</td>
				</tr>
			</tbody>
			<tfoot>
				<tr>
					<td>Tổng</td>
					<td>{{ totalPrice }}đ</td>
				</tr>
			</tfoot>
		</table>
		<a-radio-group v-model:value="paymentMethod" class="order-overview__payment">
			<a-collapse v-model:activeKey="activeCollapsePanel" accordion :bordered="false">
				<a-collapse-panel :key="EPaymentMethod.OFFLINE" :show-arrow="false">
					<template #header>
						<a-radio :value="EPaymentMethod.OFFLINE"> </a-radio>
						<span>Trả tiền mặt khi nhận hàng</span>
					</template>
					<p>Trả tiền mặt khi giao hàng.</p>
				</a-collapse-panel>
				<a-collapse-panel :key="EPaymentMethod.ONLINE" :show-arrow="false">
					<template #header>
						<a-radio :value="EPaymentMethod.ONLINE"> </a-radio>
						<span>Chuyển khoản ngân hàng</span>
					</template>
					<p>
						Thực hiện thanh toán vào ngay tài khoản ngân hàng của chúng tôi. Vui lòng sử
						dụng Mã đơn hàng của bạn trong phần Nội dung thanh toán. Đơn hàng sẽ đươc
						giao sau khi tiền đã chuyển.
					</p>
				</a-collapse-panel>
			</a-collapse>
		</a-radio-group>

		<a-button
			type="primary"
			class="order-overview__order"
			:disabled="!isLoggedIn || disabled"
			:loading="loading"
			@click="submitOrderHandler"
			>Đặt hàng</a-button
		>
		<p v-if="!isLoggedIn">Bạn cần đăng nhập để thanh toán</p>
	</div>
</template>

<script setup>
	import { computed } from "@vue/reactivity";
	import { ref } from "vue";
	import { EPaymentMethod } from "@/constants/payment";

	const props = defineProps({
		products: {
			type: Array,
			default() {
				return {
					id: "",
					name: "",
					size: "",
					quantity: 0,
					price: 0,
				};
			},
		},

		runningOutCarts: { type: [Array, String] },

		loading: Boolean,

		isLoggedIn: Boolean,

		disabled: Boolean,
	});

	const emit = defineEmits(["submit"]);

	const paymentMethod = ref(EPaymentMethod.OFFLINE);

	const activeCollapsePanel = computed({
		get() {
			return paymentMethod.value;
		},

		set(value) {
			if (value) {
				paymentMethod.value = value;
			}
		},
	});

	const productList = computed(() => {
		const numberFormater = new Intl.NumberFormat();
		return props.products.map((product) => ({
			id: product.id,
			name: product.name,
			size: product.size,
			quantity: product.quantity,
			cartId: product.cartId,
			price: product.price,
			total: numberFormater.format(
				Math.trunc(product.price * product.quantity * (1 - product.discount / 100)),
			),
		}));
	});

	const totalPrice = computed(() => {
		const numberFormat = new Intl.NumberFormat();

		return numberFormat.format(
			props.products.reduce(
				(total, product) =>
					total +
					Math.trunc(product.price * product.quantity * (1 - product.discount / 100)),
				0,
			),
		);
	});

	const submitOrderHandler = () => {
		emit("submit", paymentMethod.value);
	};
</script>

<style lang="scss">
	.order-overview {
		border: 2px solid #c30005;
		padding: 15px 30px 30px;

		> h3 {
			font-size: 1.1rem;
			overflow: hidden;
			padding-top: 10px;
			font-weight: bolder;
			text-transform: uppercase;
			color: #1c1c1c;
		}

		&__table {
			width: 100%;

			.error {
				color: red;
			}

			> thead {
				> tr {
					border-bottom: 3px solid #ececec;

					> th {
						text-align: left;
						padding: 0.5em 0;
						text-align: left;
						font-size: 0.9rem;
						line-height: 1.05;
						letter-spacing: 0.05em;
						text-transform: uppercase;

						&:last-child {
							text-align: right;
						}
					}
				}
			}
			> tbody,
			> tfoot {
				> tr {
					border-bottom: 1px solid #ececec;
					> td {
						text-align: left;
						padding-top: 15px;
						padding-bottom: 15px;
						padding: 0.5em;
						text-align: left;
						line-height: 1.3;
						font-size: 0.9rem;

						&:last-child {
							text-align: right;
						}
					}
				}
			}

			> tfoot {
				font-weight: 700;
				> tr {
					border-bottom-width: 3px;
				}
			}
		}

		&__payment {
			width: 100%;

			&.ant-radio-group {
				display: block;
				margin-bottom: 1.3rem;
				margin-top: 1rem;
			}

			.ant-collapse-item {
				> .ant-collapse-header {
					font-weight: 700;
				}

				&:last-child {
					border-bottom: 0;
				}
			}
		}

		&__order {
			line-height: 2.4em;
			min-height: 2.5em;
			padding: 0 1.2em;
			font-size: 0.97rem;
			letter-spacing: 0.03em;
			&.ant-btn {
				background-color: #d26e4b;
				color: white;
				border: 1px solid #d26e4b;

				&:focus {
					background-color: #d26e4b;
					border: 1px solid #d26e4b;
				}
			}

			> span {
				text-transform: uppercase;
				font-weight: 700;
			}

			+ p {
				color: #c30005;
				margin: 0.5rem 0;
				font-size: 0.9rem;

				&::before {
					display: inline-block;
					margin-right: 4px;
					color: #c30005;
					font-size: 14px;
					font-family: SimSun, sans-serif;
					line-height: 1;
					content: "*";
				}
			}
		}
	}
</style>
