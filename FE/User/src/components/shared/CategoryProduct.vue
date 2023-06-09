<template>
	<figure class="category-product">
		<router-link :to="{ path: '/san-pham/' + product.id }" class="category-product__avatar">
			<img
				:src="product.image"
				alt=""
				width="300"
				height="225"
				class="category-product__image"
				:class="{
					'category-product__image-hide-on-hover': !!product.hoverImage,
				}"
			/>
			<img
				v-if="product.hoverImage"
				:src="product.hoverImage"
				class="category-product__image category-product__image-hover"
				alt=""
				width="300"
				height="225"
			/>
		</router-link>

		<figcaption class="category-product__info">
			<router-link :to="{ path: '/san-pham/' + product.id }" class="category-product__link">
				<p class="category-product__name" :title="product.name">{{ product.name }}</p>
			</router-link>
			<div class="category-product__price">
				<del v-if="!!product.discount" class="category-product__price--origin-price"
					>{{ formattedOriginPrice }} đ</del
				>
				<strong class="category-product__price--new-price"
					>{{ formattedCurrentPrice }} đ</strong
				>
			</div>
			<div class="category-product__actions">
				<a-button
					type="primary"
					danger
					@click="addProductToCart"
					:loading="isAddingProductToCart"
					class="category-product__actions__add-to-cart"
					>Xem chi tiết</a-button
				>
			</div>
		</figcaption>
	</figure>
</template>

<script setup>
	import { ref, computed } from "vue";
	import { useRouter } from "vue-router";

	const router = useRouter();

	const props = defineProps({
		product: {
			type: Object,
			default() {
				return {
					id: "1",
					name: "Chuck Taylor Classic",
					price: 1_250_000,
					image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-2-300x225.jpg",
					hoverImage:
						"http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/women-classic-2-1-300x225.jpg",
					discount: 10,
				};
			},
		},
	});

	const isAddingProductToCart = ref(false);

	const numberFormatter = new Intl.NumberFormat();

	const formattedCurrentPrice = computed(() =>
		numberFormatter.format(
			props.product.discount
				? Math.ceil((props.product.price * (100 - props.product.discount)) / 100)
				: props.product.price,
		),
	);
	const formattedOriginPrice = computed(() => numberFormatter.format(props.product.price));

	const addProductToCart = () => {
		router.push({ path: "/san-pham/" + props.product.id });
	};
</script>

<style lang="scss" scoped>
	$image-gutter: 0.25rem;
	.category-product {
		box-shadow: 0 1px 3px -2px rgba(0, 0, 0, 0.12), 0 1px 2px rgba(0, 0, 0, 0.24);
		font-size: 1rem;
		border: 1px solid transparent;

		&:hover {
			border-color: #ddd;
		}

		&__avatar {
			position: relative;
			padding: $image-gutter;
			display: inline-block;
			width: 100%;
			height: 100%;
		}

		&__image {
			height: 160px;
			width: 100%;
			object-fit: cover;
			object-position: center;
			transition: filter 0.6s, opacity 0.6s, transform 0.6s, box-shadow 0.3s;

			&-hide-on-hover {
				opacity: 1;

				&:hover {
					opacity: 0;
				}
			}

			&-hover {
				position: absolute;
				top: 0;
				left: 0;
				width: 100%;
				height: 100%;
				padding: $image-gutter;
				object-fit: cover;
				object-position: center;
				opacity: 0;
				transition: filter 0.6s, opacity 0.6s, transform 0.6s, box-shadow 0.3s;

				&:hover {
					opacity: 1;
				}
			}
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
			min-height: 3rem;

			display: -webkit-box;
			-webkit-line-clamp: 2;
			-webkit-box-orient: vertical;
			overflow: hidden;
		}

		&__price {
			color: #111;
			line-height: 1.2;

			> del {
				opacity: 0.6;
				font-weight: normal;
				margin-right: 0.3em;
				color: #111;
				text-decoration: line-through currentColor;
			}

			&--new-price {
				font-weight: 700;
			}
		}

		&__actions {
			margin-top: 1em;
			transition: 0.3s ease;
			overflow: hidden;

			& > &__add-to-cart {
				background-color: #c30005;
				text-transform: uppercase;
				font-weight: bolder;
			}
		}

		&__go-to-cart {
			display: inline-block;
			color: #334862;
			font-weight: 700;
			line-height: 32px;
			vertical-align: middle;
			height: 32px;
			padding: 4px 15px;
			font-size: 14px;
		}
	}
</style>
