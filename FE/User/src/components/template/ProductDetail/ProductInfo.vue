<template>
	<div class="product-info">
		<a-breadcrumb class="product-info__breadcrumbs">
			<a-breadcrumb-item v-for="(item, index) in breadcrumbs" :key="index">
				<router-link :to="item.link">
					{{ item.name }}
				</router-link>
			</a-breadcrumb-item>
		</a-breadcrumb>

		<h2>{{ name }}</h2>
		<div class="product-info__divider"></div>

		<div class="product-info__price">
			<del v-if="!!discount" class="product-info__price--origin-price">
				<span>{{ formattedOriginPrice }} đ</span> <sup>-{{ discount }}%</sup>
			</del>
			<ins class="product-info__price--new-price">{{ formattedCurrentPrice }} đ</ins>
		</div>

		<div class="product-info__quality">
			<a-input-number
				:value="buyNumber"
				:controls="false"
				:disabled="!selectedSize || selectedSize.amount === 0"
				@change="numberUpdateHandler"
			>
				<template #addonBefore>
					<button
						class="product-info__quality__btn product-info__quality--increase"
						@click="increaseProductQuality"
						:disabled="!selectedSize || selectedSize.amount === 0"
					>
						+
					</button>
				</template>
				<template #addonAfter>
					<button
						class="product-info__quality__btn product-info__quality--decrease"
						@click="decreaseProductQuality"
						:disabled="!selectedSize || selectedSize.amount === 0"
					>
						-
					</button>
				</template>
			</a-input-number>
			<span v-if="!!selectedSize">
				<!-- {{ selectedSize.amount > 0 ? "Còn hàng" : "Hết hàng" }} -->
				Còn lại: {{ selectedSize.amount }}
			</span>
		</div>

		<div class="product-info__size">
			<p>Cỡ giày</p>
			<a-radio-group v-model:value="selectedSize">
				<a-radio-button v-for="size in sizes" :value="size" :key="size.code">{{
					size.code
				}}</a-radio-button>
			</a-radio-group>
		</div>

		<div class="product-info__actions">
			<a-button
				class="product-info__btns product-info__buy-now"
				:loading="isAddingToCart"
				:disabled="!selectedSize || selectedSize.amount === 0 || !uid"
				@click="buyNowHandler"
				>Mua ngay</a-button
			>
			<a-button
				class="product-info__btns product-info__add-to-cart"
				:loading="isAddingToCart"
				:disabled="!selectedSize || selectedSize.amount === 0 || !uid"
				@click="addToCartHandler"
				>Thêm vào giỏ hàng</a-button
			>
		</div>

		<a-row :gutter="16">
			<a-col :span="12">
				<p class="product-info__logos">Tính phí ship tự động</p>
				<a-row :gutter="[8, 8]">
					<a-col :span="8">
						<img width="400" height="200" src="@/assets/img/logo/logo-ghn.jpg" alt="" />
					</a-col>
					<a-col :span="8">
						<img
							width="400"
							height="200"
							src="@/assets/img/logo/logo-ghtk.jpg"
							alt=""
						/>
					</a-col>
					<a-col :span="8">
						<img
							width="400"
							height="200"
							src="@/assets/img/logo/logo-ninja-van.jpg"
							alt=""
						/>
					</a-col>
					<a-col :span="8">
						<img
							width="400"
							height="200"
							src="@/assets/img/logo/logo-shipchung.jpg"
							alt=""
						/>
					</a-col>
					<a-col :span="8">
						<img
							width="400"
							height="200"
							src="@/assets/img/logo/logo-viettle-post.jpg"
							alt=""
					/></a-col>
					<a-col :span="8">
						<img
							width="400"
							height="200"
							src="@/assets/img/logo/logo-vn-post.jpg"
							alt=""
						/>
					</a-col>
				</a-row>
			</a-col>
			<a-col :span="12">
				<p class="product-info__logos">Thanh toán</p>
				<a-row :gutter="[8, 8]">
					<a-col :span="8">
						<img width="400" height="200" src="@/assets/img/logo/logo-acb.jpg" alt="" />
					</a-col>
					<a-col :span="8">
						<img
							width="400"
							height="200"
							src="@/assets/img/logo/logo-techcombank.jpg"
							alt=""
						/>
					</a-col>
					<a-col :span="8">
						<img width="400" height="200" src="@/assets/img/logo/logo-vib.jpg" alt="" />
					</a-col>
					<a-col :span="8">
						<img width="400" height="200" src="@/assets/img/logo/logo-vcb.jpg" alt="" />
					</a-col>
					<a-col :span="8">
						<img
							width="400"
							height="200"
							src="@/assets/img/logo/logo-paypal.jpg"
							alt=""
					/></a-col>
					<a-col :span="8">
						<img
							width="400"
							height="200"
							src="@/assets/img/logo/logo-mastercard.jpg"
							alt=""
						/>
					</a-col>
				</a-row>
			</a-col>
		</a-row>

		<div class="product-info__meta">
			<p>Mã: {{ code }}</p>
			<p>
				Danh mục:

				<span v-for="(category, i) in categories" :key="category.link">
					<router-link :to="category.link">{{ category.name }}</router-link>
					<span v-if="i !== categories.length - 1">, </span>
				</span>
			</p>
		</div>
	</div>
</template>

<script>
	import { notification } from "ant-design-vue";
	import { computed, ref } from "vue";
	import { useRouter } from "vue-router";
	import { useStore } from "vuex";
	export default {
		props: {
			categories: {
				type: Array,
				default() {
					return [{ link: "/danh-muc", name: "Nam" }];
				},
			},
			id: { type: String },
			name: { type: String },
			price: { type: Number },
			sizes: {
				type: Array,
				default() {
					return [{ id: "", code: "34", amount: 12, sold: 0 }];
				},
			},
			code: String,
			discount: Number,
			totalPrice: Number,
			image: String,
		},
		emits: ["reset"],
		setup(props, { emit }) {
			const store = useStore();
			const router = useRouter();

			const buyNumber = ref(1);
			const selectedSize = ref(null);
			const isAddingToCart = ref(false);
			const uid = computed(() => store.state.user.uid);

			const breadcrumbs = computed(() => {
				const breadcrumbs = [
					{
						link: "/",
						name: "Trang chủ",
					},
				];

				for (let i = 0; i < props.categories.length; i++) {
					const category = props.categories[i];

					breadcrumbs.push({
						link: category.link,
						name: category.name,
					});
				}

				return breadcrumbs;
			});

			const activeCartProductItem = computed(() => {
				return store.state.cart.productList.find(
					(product) =>
						product.id === props.id && product.size === selectedSize.value?.code,
				);
			});

			const productPrice = props.discount
				? Math.ceil((props.price * (100 - props.discount)) / 100)
				: props.price;

			const numberFormatter = new Intl.NumberFormat();

			const formattedCurrentPrice = numberFormatter.format(productPrice);
			const formattedOriginPrice = numberFormatter.format(props.price);

			const numberUpdateHandler = (value) => {
				const enteredAmount = Number(value);

				const availabelAmount = activeCartProductItem.value
					? selectedSize.value.amount - activeCartProductItem.value.quantity
					: selectedSize.value.amount;

				if (enteredAmount && enteredAmount > availabelAmount) {
					buyNumber.value = availabelAmount;
				} else {
					buyNumber.value = enteredAmount || 1;
				}
			};

			const increaseProductQuality = () => {
				if (buyNumber.value < selectedSize.value.amount) {
					buyNumber.value++;
				}
			};

			const decreaseProductQuality = () => {
				if (buyNumber.value > 1) {
					buyNumber.value--;
				}
			};

			const buyNowHandler = async () => {
				if (!selectedSize.value) {
					notification.error({
						message: "Vui lòng chọn kích thước giày",
					});
					return;
				}

				isAddingToCart.value = true;

				const availabelAmount = activeCartProductItem.value
					? selectedSize.value.amount - activeCartProductItem.value.quantity
					: selectedSize.value.amount;

				const amount =
					buyNumber.value > availabelAmount ? availabelAmount : buyNumber.value;

				const product = {
					id: props.id,
					name: props.name,
					price: props.price,
					size: selectedSize.value.code,
					discount: props.discount,
					image: props.image,
					amount,
				};

				if (amount <= 0) {
					isAddingToCart.value = false;
					notification.error({
						message: "Bạn đã thêm tối đa số giày cho phép",
					});
					return;
				}

				const isSuccess = await store.dispatch("cart/createBuyNowSession", {
					product,
					sizes: props.sizes,
				});
				if (isSuccess) {
					router.push({
						path: "/thanh-toan",
						query: {
							buyNow: true,
						},
					});
				} else {
					notification.error({
						message: "Có lỗi xảy ra, vui lòng thử lại",
					});
				}
				isAddingToCart.value = false;
			};

			const addToCartHandler = async () => {
				if (!selectedSize.value) {
					notification.error({
						message: "Vui lòng chọn kích thước giày",
					});
					return;
				}

				isAddingToCart.value = true;

				const availabelAmount = activeCartProductItem.value
					? selectedSize.value.amount - activeCartProductItem.value.quantity
					: selectedSize.value.amount;
				const amount =
					buyNumber.value > availabelAmount ? availabelAmount : buyNumber.value;

				const product = {
					id: props.id,
					name: props.name,
					price: props.price,
					size: selectedSize.value.code,
					discount: props.discount,
					image: props.image,
					amount,
				};

				if (amount <= 0) {
					isAddingToCart.value = false;
					notification.error({
						message: "Bạn đã thêm tối đa số giày cho phép",
					});
					return;
				}

				const isSuccess = await store.dispatch("cart/addProductToCart", {
					product,
					sizes: props.sizes,
				});
				if (isSuccess) {
					notification.success({
						message: "Thêm sản phẩm vào giỏ hàng thành công",
					});
					buyNumber.value = 1;
					selectedSize.value = null;
					emit("reset");
				} else {
					notification.error({
						message: "Có lỗi xảy ra, vui lòng thử lại",
					});
				}
				isAddingToCart.value = false;
			};

			return {
				buyNumber,
				selectedSize,
				breadcrumbs,
				formattedCurrentPrice,
				formattedOriginPrice,
				isAddingToCart,
				uid,
				increaseProductQuality,
				decreaseProductQuality,
				buyNowHandler,
				addToCartHandler,
				numberUpdateHandler,
			};
		},
	};
</script>

<style lang="scss" scoped>
	.product-info {
		& > &__breadcrumbs {
			margin: 0 0 0.5rem;
			a {
				font-size: 0.85rem;

				text-transform: uppercase;
				color: rgba(102, 102, 102, 0.7);

				&:hover {
					color: #111;
				}
			}
		}

		& > h2 {
			font-weight: 700;
			color: #1c1c1c;
			font-size: 1.7rem;
			line-height: 1.3;
			margin-bottom: 0.5rem;
		}

		&__divider {
			height: 3px;
			display: block;
			background-color: rgba(0, 0, 0, 0.1);
			margin: 1em 0 1em;
			width: 100%;
			max-width: 30px;
		}

		&__price {
			white-space: nowrap;
			color: #111;
			font-weight: bold;
			line-height: 1.3;
			font-size: 1.5rem;
			margin-bottom: 1rem;

			&--new-price {
			}

			&--origin-price {
				margin-right: 1.5rem;

				> span {
					text-decoration: line-through;
				}

				> sup {
					text-decoration: none;
					color: white;
					background-color: #c30005;
					padding: 0.5rem;
					border-radius: 50%;
				}
			}
		}

		&__quality {
			display: flex;
			align-items: center;
			gap: 1rem;
			margin-bottom: 2rem;

			> span {
				font-size: 1rem;
			}

			> .ant-input-number-group-wrapper {
				width: 150px;
			}

			:deep(.ant-input-number-group-addon) {
				padding: 0;
			}

			:deep(.ant-input-number) {
				font-size: 1rem;
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

		&__size {
			margin-bottom: 1rem;

			> p {
				font-size: 1rem;
				font-weight: 700;
			}

			> .ant-radio-group {
				display: flex;
				flex-flow: row wrap;
				gap: 1rem;

				> .ant-radio-button-wrapper {
					height: auto;

					padding: 0.5rem 1rem;

					font-size: 1rem;
					line-height: 1.5;

					&:hover,
					&.ant-radio-button-wrapper-checked {
						font-weight: 700;
						color: #fff;
						background-color: #c30005;
						border-color: #c30005;
					}
				}
			}
		}

		&__actions {
			font-size: 1rem;
			margin-bottom: 1rem;

			> .ant-btn {
				width: calc(50% - 0.5rem);
				height: auto;
				padding: 0.5rem 1rem;
				font-size: 1rem;
				font-weight: 700;
				color: #fff;

				> :deep(span) {
					text-transform: uppercase;
				}

				&:first-child {
					margin-right: 0.5rem;
				}

				&:last-child {
					margin-left: 0.5rem;
				}

				&:disabled {
					color: rgba(0, 0, 0, 0.25);
				}
			}
		}

		&__buy-now {
			background-color: #d26e4b;
			border-color: #d26e4b;

			&:hover,
			&:focus {
				background-color: transparent;
				border-color: #d26e4b;
				color: #d26e4b;
			}
		}

		&__add-to-cart {
			background-color: #c30005;
			border-color: #c30005;

			&:hover,
			&:focus {
				background-color: transparent;
				border-color: #c30005;
				color: #c30005;
			}
		}

		&__logos {
			font-weight: 700;
			font-size: 1rem;
		}

		&__meta {
			margin-top: 1rem;
			> p {
				border-top: 1px dotted #ddd;
				padding: 5px 0;
				margin-bottom: 0;

				color: #353535;

				a {
					color: inherit;
				}
			}
		}
	}
</style>
