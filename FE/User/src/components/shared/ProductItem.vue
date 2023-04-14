<template>
	<figure class="product-item">
		<div v-if="!!product.promotionAmount" class="product-item__discount">
			<div class="product-item__discount__inner">-{{ product.promotionAmount }}%</div>
		</div>

		<img
			class="product-item__img"
			:src="product.image"
			:alt="product.name"
			:srcset="productSrcSets"
			width="1920"
			height="1229"
		/>

		<figcaption class="product-item__info">
			<router-link :to="{ path: '/san-pham/' + product.id }" class="product-item__link">
				<p class="product-item__name">{{ product.name }}</p>
			</router-link>
			<div class="product-item__price">
				<del v-if="!!product.promotionAmount" class="product-item__price--origin-price"
					>{{ formattedOriginPrice }} đ</del
				>
				<ins class="product-item__price--new-price">{{ formattedCurrentPrice }} đ</ins>
			</div>
			<div class="product-item__actions">
				<a-button
					type="primary"
					danger
					:loading="isAddingProductToCart"
					class="product-item__actions__add-to-cart"
					@click="addProductToCart"
					>Xem chi tiết</a-button
				>
			</div>
		</figcaption>
	</figure>
</template>

<script>
	import { computed, ref } from "vue";
	import { useStore } from "vuex";
	import { ArrowRightOutlined } from "@ant-design/icons-vue";
	import { useRouter } from "vue-router";

	export default {
		name: "ProductItem",
		props: ["product"],
		components: { ArrowRightOutlined },

		setup(props) {
			const router = useRouter();
			const isAddingProductToCart = ref(false);

			const productPrice = props.product.promotionAmount
				? Math.ceil((props.product.price * (100 - props.product.promotionAmount)) / 100)
				: props.product.price;

			const numberFormatter = new Intl.NumberFormat();

			const formattedCurrentPrice = numberFormatter.format(productPrice);
			const formattedOriginPrice = numberFormatter.format(props.product.price);

			const productSrcSets = computed(() => {
				if (!props.product.extraLinks || props.product.extraLinks.length === 0) {
					return "";
				}

				return props.product.extraLinks.reduce(
					(link, srcLink) => link.concat(`${srcLink.link} ${srcLink.size},`),
					"",
				);
			});

			const addProductToCart = () => {
				router.push({ path: "/san-pham/" + props.product.id });
			};

			return {
				productSrcSets,
				productPrice,
				formattedCurrentPrice,
				isAddingProductToCart,
				formattedOriginPrice,
				addProductToCart,
			};
		},
	};
</script>

<style lang="scss" scoped>
	.product-item {
		font-size: 1rem;
		position: relative;

		&__discount {
			display: table;
			height: 2.8rem;
			width: 2.8rem;

			position: absolute;
			left: 0;
			top: 0;
			z-index: 2;

			pointer-events: none;
			font-size: 1rem;
			backface-visibility: hidden;
			margin-left: -0.4em;

			font-weight: bolder;
			white-space: nowrap;

			&__inner {
				display: table-cell;
				vertical-align: middle;
				text-align: center;
				width: 100%;
				height: 100%;

				padding: 2px;
				background-color: #c30005;
				border-radius: 999px;
				line-height: 0.85;
				color: #fff;
			}
		}

		&__link {
			font: inherit;
			color: inherit;
		}

		&__img,
		&__name {
			cursor: pointer;
		}

		&__img {
			width: 100%;
			object-fit: cover;
		}

		&__info {
			font-size: 0.9em;
			text-align: center;
			padding: 0.7em 0.85em 1.4em;
		}

		&__name {
			color: #334862;
			margin-top: 0.1em;
			margin-bottom: 0.1em;
		}

		&__price {
			color: #c30005;
			line-height: 1.2;

			> del {
				opacity: 0.6;
				font-weight: normal;
				margin-right: 0.3em;
				color: #c30005;
				text-decoration: line-through currentColor;
			}

			&--new-price {
				font-weight: 700;
			}
		}

		&__actions {
			margin-top: 1em;
			transform: scale(0);
			transition: 0.3s ease;
			overflow: hidden;

			& > &__add-to-cart {
				background-color: #c30005;
				text-transform: uppercase;
				font-weight: bolder;
			}
		}

		&__go-to-cart {
			color: #334862;
			font-weight: 700;
			line-height: 1.3;
		}

		&:hover &__actions {
			transform: scale(1);
		}
	}
</style>
