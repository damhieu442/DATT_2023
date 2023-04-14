<template>
	<div style="display: flex; justify-content: center; align-items: center">
		<ul class="header-nav header-nav-main nav nav-right nav-uppercase">
			<li class="cart-item has-icon has-dropdown" :class="false ? 'current-dropdown' : ''">
				<a-popover placement="bottom" overlayClassName="cart-item__popup">
					<template #content>
						<p v-if="products.length === 0" class="cart-item__empty">
							Chưa có sản phẩm nào trong giỏ hàng
						</p>
						<ul
							v-else
							class="cart-item__list"
							:class="{ 'cart-item__list--loading': isLoading }"
						>
							<li v-for="product in products" :key="product.id">
								<MiniCartItem :product="product" @delete="deleteProduct" />
							</li>
							<p class="cart-item__list__total">Tổng phụ: {{ totalPrice }}</p>
							<router-link to="/gio-hang" class="cart-item__list__cart"
								>Xem giỏ hàng</router-link
							>
							<router-link to="/thanh-toan" class="cart-item__list__purchase"
								>Thanh toán</router-link
							>
						</ul>
					</template>

					<a href="#" title="Giỏ hàng" class="header-cart-link is-small">
						<span class="header-cart-title">
							Giỏ hàng /&nbsp;
							<span class="cart-price">
								<span class="woocommerce-Price-amount amount">
									{{ totalPrice }}
									₫
								</span></span
							>
						</span>
						<span class="cart-icon image-icon">
							<strong>{{ totalProduct }}</strong>
						</span>
					</a>
				</a-popover>
			</li>
		</ul>
	</div>
</template>

<script setup>
	import { useStore } from "vuex";
	import MiniCartItem from "@/components/shared/MiniCartItem.vue";
	import { computed, ref } from "vue";

	const store = useStore();
	const isLoading = ref(false);

	const products = computed(() => store.state.cart.productList.slice(0, 5));

	const totalProduct = computed(() => store.getters["cart/totalProduct"]);
	const totalPrice = computed(() =>
		new Intl.NumberFormat().format(store.getters["cart/totalPrice"]),
	);

	const deleteProduct = async (id) => {
		isLoading.value = true;
		const result = await store.dispatch("cart/removeProductFromCart", id);

		isLoading.value = false;
	};
</script>

<style lang="scss">
	.cart-item {
		&__empty {
			color: #777;
			font-size: 1rem;
			line-height: 1.5;
			margin: 0;
			text-align: center;
		}

		&__popup {
			max-width: 260px;
		}

		&__list {
			&--loading {
				position: relative;
				opacity: 0.5;

				&:before {
					position: absolute;
					top: 50%;
					left: 50%;
					margin-left: -15px;
					margin-top: -15px;
					z-index: 99;

					content: "";
					margin: 0px auto;
					font-size: 10px;
					text-indent: -9999em;
					border-top: 3px solid rgba(0, 0, 0, 0.1);
					border-right: 3px solid rgba(0, 0, 0, 0.1);
					border-bottom: 3px solid rgba(0, 0, 0, 0.1);
					opacity: 0.8;
					border-left: 3px solid #446084;
					animation: spinning 0.6s infinite linear;
					border-radius: 50%;
					width: 30px;
					height: 30px;
					pointer-events: none;

					@keyframes spinning {
						from {
							transform: rotate(0deg);
						}
						to {
							transform: rotate(360deg);
						}
					}
				}

				&:before {
					border-color: #c30005;
				}
			}

			&__total {
				font-weight: 700;
				font-size: 1rem;
				color: #777;
				text-align: center;
				padding: 10px 0;
				border-top: 1px solid #ececec;
				border-bottom: 2px solid #ececec;
				margin-bottom: 0.5em;
			}

			&__purchase,
			&__cart {
				width: 100%;
				margin: 0.5em 0 0;
				text-transform: uppercase;
				display: block;
				font-size: 0.97rem;
				letter-spacing: 0.03rem;
				cursor: pointer;
				font-weight: bolder;
				text-align: center;
				line-height: 2.4em;
				min-height: 2.5em;
				padding: 0 1.2em;
				color: #fff;

				&:hover {
					color: #fff;
					box-shadow: inset 0 0 0 100px rgba(0, 0, 0, 0.2);
				}
			}

			&__cart {
				background-color: #c30005;
			}

			&__purchase {
				background-color: #d26e4b;
			}
		}
	}

	.header-cart-link {
		&:hover > .cart-icon > strong {
			background-color: #fff;
			color: #446084;

			&:after {
				height: 10px;
			}
		}
	}

	.image-icon {
		height: auto;
		vertical-align: middle;
		position: relative;

		span + & {
			margin-left: 10px;
		}
	}

	.cart-icon {
		display: inline-block;
		&:hover > strong {
			background-color: #fff;
			color: #446084;
		}

		> strong {
			border-radius: 0;
			font-weight: bold;
			margin: 0.3em 0;
			border: 2px solid #446084;
			color: #446084;
			position: relative;
			display: inline-block;
			vertical-align: middle;
			text-align: center;
			width: 2.2em;
			height: 2.2em;
			font-size: 1em;
			line-height: 1.9em;
			font-family: Helvetica, Arial, Sans-serif;
			color: #fff;
			border-color: #fff;

			&:after {
				transition: height 0.1s ease-out;
				bottom: 100%;
				margin-bottom: 0;
				margin-left: -9px;
				height: 8px;
				width: 14px;
				left: 50%;
				content: " ";
				position: absolute;
				pointer-events: none;
				border: 2px solid #446084;
				border-top-left-radius: 99px;
				border-top-right-radius: 99px;
				border-bottom: 0;
			}
		}
	}
</style>
