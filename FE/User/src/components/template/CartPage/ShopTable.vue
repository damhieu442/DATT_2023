<template>
	<div class="cart-shop-table">
		<a-table
			:columns="columns"
			:dataSource="products"
			rowKey="id"
			:pagination="false"
			class="cart-shop-table__table"
		>
			<template #bodyCell="{ column, record }">
				<template v-if="column.dataIndex === 'remove-product'">
					<button
						class="cart-shop-table__table__remove"
						@click="() => removeProduct(record)"
					>
						<CloseOutlined />
					</button>
				</template>

				<template v-else-if="column.dataIndex === 'image'">
					<img
						:src="record.image"
						alt=""
						width="300"
						height="225"
						class="cart-shop-table__table__img"
					/>
				</template>

				<template v-else-if="column.dataIndex === 'quantity'">
					<a-input-number
						:controls="false"
						:value="record.quantity"
						class="cart-shop-table__table__amount"
						@blur="(event) => amountInputBlurHandler(event, record)"
					>
						<template #addonBefore>
							<button
								class="cart-shop-table__table__amount__btn cart-shop-table__table__amount--increase"
								@click="() => increaseProductQuality(record)"
							>
								+
							</button>
						</template>
						<template #addonAfter>
							<button
								class="cart-shop-table__table__amount__btn cart-shop-table__table__amount--decrease"
								:disabled="record.quantity <= 1"
								@click="() => decreaseProductQuality(record)"
							>
								-
							</button>
						</template>
					</a-input-number>
				</template>
			</template>
		</a-table>
		<div class="cart-shop-table__actions">
			<router-link to="/san-pham" class="cart-shop-table__actions__link">
				<ArrowLeftOutlined />
				tiếp tục xem sản phẩm
			</router-link>
			<a-button
				class="cart-shop-table__actions__update"
				:loading="isSynchronizing"
				:disabled="isSynchronizing"
				@click="synchronizeCartProducts"
			>
				Cập nhật giỏ hàng
			</a-button>
		</div>
	</div>
</template>

<script setup>
	import { computed, h } from "vue";
	import { useStore } from "vuex";
	import { CloseOutlined, ArrowLeftOutlined } from "@ant-design/icons-vue";
	import { EMutationTypes } from "@/store/cart";
	import { Modal } from "ant-design-vue";

	const emits = defineEmits(["synchronize"]);

	const props = defineProps({
		products: {
			type: Array,
			default() {
				return [];
			},
		},

		isSynchronizing: Boolean,
	});

	const store = useStore();

	const products = computed(() => {
		const numberFormater = new Intl.NumberFormat();
		return props.products.map((product) => ({
			id: product.id,
			image: product.image,
			name: product.name,
			size: product.size,
			discount: `-${product.discount || 0}%`,
			price: numberFormater.format(product.price) + "đ",
			quantity: product.quantity,
			total:
				numberFormater.format(
					Math.trunc(product.price * product.quantity * (1 - product.discount / 100)),
				) + "đ",
		}));
	});

	const columns = [
		{ title: "Sản phẩm", dataIndex: "remove-product", colSpan: 3 },
		{ dataIndex: "image", colSpan: 0 },
		{
			dataIndex: "name",
			colSpan: 0,
		},
		{
			dataIndex: "size",
			title: "Kích cỡ",
		},
		{
			title: "Giá",
			dataIndex: "price",
		},
		{
			title: "Số lượng",
			dataIndex: "quantity",
		},
		{
			title: "Giảm giá",
			dataIndex: "discount",
		},
		{
			title: "Thành tiền",
			dataIndex: "total",
		},
	];

	const synchronizeCartProducts = () => {
		emits("synchronize");
	};

	const amountInputBlurHandler = (event, product) => {
		const productAmount = Number(event.target.value.replace(/\D/g, "")) || 0;

		if (productAmount < 0) {
			return;
		}

		store.commit("cart/" + EMutationTypes.UPDATE_PRODUCT_AMOUNT, {
			productId: product.id,
			amount: productAmount,
			size: product.size,
		});
	};

	const increaseProductQuality = (product) => {
		store.commit("cart/" + EMutationTypes.UPDATE_PRODUCT_AMOUNT, {
			productId: product.id,
			amount: product.quantity + 1,
			size: product.size,
		});
	};

	const decreaseProductQuality = (product) => {
		store.commit("cart/" + EMutationTypes.UPDATE_PRODUCT_AMOUNT, {
			productId: product.id,
			amount: product.quantity - 1,
			size: product.size,
		});
	};

	const removeProduct = (product) => {
		Modal.confirm({
			content: h("h5", {
				innerHTML: `Bạn có chắc bỏ sản phẩm này? <strong>${product.name} - ${product.size}</strong>`,
			}),
			onOk() {
				store.dispatch("cart/removeProductFromCart", product.id);
				// store.commit("cart/" + EMutationTypes.REMOVE_PRODUCT, product.id);
			},

			onCancel() {
				Modal.destroyAll();
			},

			cancelText: "Hủy",
			okText: "Xóa",
		});
	};
</script>

<style lang="scss">
	.cart-shop-table {
		font-size: 1rem;
		&__table {
			.ant-table-thead > tr > th {
				background-color: #fff;
				font-size: 0.9rem;
				font-weight: 700;
				text-align: left;
				border-bottom: 3px solid #ececec;
				line-height: 1.05;
				letter-spacing: 0.05em;
				text-transform: uppercase;

				&::before {
					display: none;
				}

				&:first-child:not([colspan="1"]) {
					text-align: left;
				}

				&:last-child {
					text-align: right;
				}
			}

			&__remove {
				display: block;
				width: 1.5rem;
				height: 1.5rem;
				font-size: 15px !important;
				line-height: 19px !important;
				border-radius: 100%;
				color: #ccc;
				font-weight: bold;
				text-align: center;
				border: 2px solid currentColor;
				background: transparent;
				cursor: pointer;

				&:hover {
					color: #000;
				}
			}

			&__img {
				width: 4rem;
				height: 3rem;
				object-fit: cover;
				object-position: center;
			}

			.ant-table-content {
				> table {
					> tbody {
						> tr {
							> td {
								&:nth-child(4) {
									text-align: center;
								}

								&:nth-child(5),
								&:last-child {
									font-weight: 700;
								}
							}
						}
					}
				}
			}

			&__amount {
				.ant-input-number-input {
					width: 3rem;
					max-width: 3em;
				}

				.ant-input-number-group-addon {
					padding: 0;
				}

				&__btn {
					cursor: pointer;
					background: transparent;
					border: 0;
					padding: 0 11px;
					font-size: 1.25em;

					&:disabled {
						cursor: not-allowed;
					}
				}
			}
		}

		&__actions {
			margin: 1rem 0;

			&__link,
			&__update.ant-btn {
				text-transform: uppercase;
				font-weight: 700;
			}

			&__link {
				color: #c30005;
				border: 2px solid currentColor;
				padding: 0 1.2em;
				line-height: 2.19em;
				display: inline-block;
				transition: transform 0.3s, border 0.3s, background 0.3s, box-shadow 0.3s,
					opacity 0.3s, color 0.3s;
				margin-right: 1em;

				&:hover {
					color: white;
					background-color: #c30005;
					border-color: #c30005;
				}
			}

			&__update.ant-btn {
				color: white;
				border: 2px solid #c30005;
				padding: 0 1.2em;
				cursor: pointer;
				line-height: 2.19em;
				display: inline-block;
				background-color: #c30005;
				height: auto;
				font-size: 1rem;

				&:hover {
					color: #c30005;
					border-color: #c30005;
				}

				> span {
					text-transform: inherit;
				}
			}
		}
	}
</style>
