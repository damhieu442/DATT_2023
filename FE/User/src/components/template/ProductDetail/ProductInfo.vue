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
			<del v-if="!!promotionAmount" class="product-info__price--origin-price"
				>{{ formattedOriginPrice }} đ</del
			>
			<ins class="product-info__price--new-price">{{ formattedCurrentPrice }} đ</ins>
		</div>

		<div class="product-info__quality">
			<a-input-number
				v-model:value="buyNumber"
				:controls="false"
				:disabled="!selectedSize || selectedSize.amount === 0"
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
				{{ selectedSize.amount > 0 ? "Còn hàng" : "Hết hàng" }}
				<!-- Số lượng: {{ selectedSize.amount }} -->
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
				@click="buyNowHandler"
				class="product-info__btns product-info__buy-now"
				:disabled="!selectedSize || selectedSize.amount === 0"
				>Mua ngay</a-button
			>
			<a-button
				class="product-info__btns product-info__add-to-cart"
				:loading="isAddingToCart"
				:disabled="!selectedSize || selectedSize.amount === 0"
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
					return [{ code: 34, amount: 12 }];
				},
			},
			promotionAmount: Number,
			code: String,
			discount: Number,
			totalPrice: Number,
			image: String,
		},
		setup(props) {
			const store = useStore();

			const buyNumber = ref(1);
			const selectedSize = ref(null);
			const isAddingToCart = ref(false);

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

			const productPrice = props.promotionAmount
				? Math.ceil((props.product.price * (100 - props.promotionAmount)) / 100)
				: props.price;

			const numberFormatter = new Intl.NumberFormat();

			const formattedCurrentPrice = numberFormatter.format(productPrice);
			const formattedOriginPrice = numberFormatter.format(props.price);

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

			const buyNowHandler = () => {
				if (!selectedSize.value) {
					notification.error({
						message: "Vui lòng chọn kích thước giày",
					});
					return;
				}
			};

			const addToCartHandler = async () => {
				if (!selectedSize.value) {
					notification.error({
						message: "Vui lòng chọn kích thước giày",
					});
					return;
				}

				isAddingToCart.value = true;

				const product = {
					id: props.id,
					name: props.name,
					price: props.price,
					size: selectedSize.value.code,
					image: props.image,
					amount: buyNumber.value,
				};

				await store.dispatch("cart/addProductToCart", product);
				isAddingToCart.value = false;
				notification.success({
					message: "Thêm sản phẩm vào giỏ hàng thành công",
				});
			};

			return {
				buyNumber,
				selectedSize,
				breadcrumbs,
				formattedCurrentPrice,
				formattedOriginPrice,
				isAddingToCart,
				increaseProductQuality,
				decreaseProductQuality,
				buyNowHandler,
				addToCartHandler,
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
