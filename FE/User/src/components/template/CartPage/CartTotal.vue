<template>
	<div class="cart-total">
		<h2>Tổng số lượng</h2>
		<table>
			<tbody>
				<tr>
					<td>Tổng số lượng</td>
					<td>{{ totalAmount }}</td>
				</tr>
				<tr>
					<td>Tổng tiền</td>
					<td>{{ formattedTotalMoney }}đ</td>
				</tr>
			</tbody>
			<tfoot>
				<tr>
					<td colspan="2">
						<button @click="proceedPaymentHandler">Tiến hành thanh toán</button>
					</td>
				</tr>
			</tfoot>
		</table>
	</div>
</template>

<script setup>
	import { computed } from "vue";
	import { useRouter } from "vue-router";

	const router = useRouter();

	const props = defineProps({
		totalMoney: Number,
		totalAmount: Number,
	});

	const formattedTotalMoney = computed(() => {
		return new Intl.NumberFormat().format(props.totalMoney);
	});

	const proceedPaymentHandler = () => {
		router.push("/thanh-toan");
	};
</script>

<style lang="scss" scoped>
	.cart-total {
		> h2 {
			padding: 1rem 0;
			text-align: left;
			border-bottom: 3px solid #ececec;
			font-size: 0.9rem;
			line-height: 1.05;
			letter-spacing: 0.05em;
			text-transform: uppercase;
			font-weight: 700;
			margin-bottom: 1rem;
		}

		> table {
			width: 100%;

			> tbody {
				> tr {
					border-bottom: 1px solid #ececec;

					&:last-child {
						border-bottom: 3px solid #ececec;
					}

					> td {
						font-size: 0.9rem;
						padding: 0.5rem 0;

						&:first-child {
							text-align: left;
						}

						&:last-child {
							text-align: right;
							font-weight: 700;
						}
					}
				}
			}

			> tfoot {
				> tr {
					> td {
						padding: 1.5rem 0;

						> button {
							background-color: #d26e4b;
							color: white;
							min-width: 100%;
							margin-right: 0;
							display: block;
							border-color: rgba(0, 0, 0, 0.05);
							text-transform: uppercase;
							font-size: 0.97rem;
							font-weight: 700;
							letter-spacing: 0.03em;
							line-height: 2.4em;
							min-height: 2.5em;
							padding: 0 1.2em;
							cursor: pointer;
						}
					}
				}
			}
		}
	}
</style>
