<template>
	<section class="related-product">
		<h3 class="related-product__title">Sản phẩm tương tự</h3>

		<a-carousel
			class="related-product__carousel"
			:dots="false"
			swipe
			draggable
			:slidesToShow="5"
		>
			<ProductItem v-for="product in products" :key="product.id" :product="product" />
		</a-carousel>
	</section>
</template>

<script setup>
	import ProductItem from "@/components/shared/CategoryProduct.vue";

	const props = defineProps({
		products: {
			type: Array,
			default() {
				return [];
			},
		},
	});
</script>

<style lang="scss" scoped>
	.related-product {
		margin-top: 2rem;
		border-top: 1px solid #ececec;

		&__title {
			padding: 1rem 0;
			text-transform: uppercase;
			line-height: 1.2;
			letter-spacing: 0.05em;
			font-size: 1.25rem;
			font-weight: 700;

			color: #1c1c1c;
		}

		&__carousel {
			:deep(.slick-slide) {
				padding: 0 0.5em;

				> div > .category-product {
					border: 2px solid #ddd;
				}
			}
		}
	}
</style>
