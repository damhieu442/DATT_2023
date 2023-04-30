<template>
	<div class="widget-product">
		<router-link :to="product.link" class="widget-product__containter">
			<img
				width="100"
				height="100"
				:src="product.image"
				class="widget-product__image"
				alt=""
			/>
			<p class="widget-product__name" :title="product.name">{{ product.name }}</p>
			<p class="widget-product__price">{{ formattedProductPrice }}&nbsp;đ</p>
		</router-link>
	</div>
</template>

<script setup>
	const props = defineProps({
		product: {
			type: Object,
			default() {
				return {
					link: "/san-pham/1",
					image: "http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/men-psy-1-100x100.jpg",
					name: "Chuck 70 Psy-Kicks Ox",
					price: 2_800_000,
				};
			},
		},
	});

	const formattedProductPrice = new Intl.NumberFormat().format(props.product.price);
</script>

<style lang="scss" scoped>
	.widget-product {
		&__containter {
			display: grid;
			grid-template-rows: 1fr 1fr;
			grid-template-columns: 1fr 3fr;
			grid-template-areas:
				"image name"
				"image price";
		}

		&__image {
			grid-area: image;
			width: 3.75rem;
			height: 3.75rem;
			object-fit: cover;
			object-position: 50% 50%;
		}

		&__name {
			grid-area: name;
			display: -webkit-box;
			-webkit-box-orient: vertical;
			-webkit-line-clamp: 2;
			overflow: hidden;
			margin-left: 1rem;
			color: #334862;
		}

		&__price {
			grid-area: price;
			margin-left: 1rem;
			color: #111;
			font-weight: 700;
		}
	}
</style>
