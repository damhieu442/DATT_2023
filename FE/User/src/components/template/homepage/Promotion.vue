<template>
	<section class="promotion">
		<div class="promotion__banner">
			<div>
				<h5>MONA SNE<i class="fa fa-star-o" aria-hidden="true"></i>KER</h5>
				<h2>KHUYẾN MÃI <span class="highlight">GIẢM GIÁ 50%</span></h2>
				<div style="display: block; height: auto; padding-top: 30px"></div>
				<router-link to="/danh-muc/tre-em/" target="_self">Xem sản phẩm</router-link>
			</div>
		</div>
		<div style="display: block; height: auto; padding-top: 50px"></div>
		<h3 class="promotion__title">SẢN PHẨM GIẢM GIÁ</h3>
		<a-row type="flex">
			<a-col :span="6" v-for="item in products" :key="item.id">
				<ProductItem :product="item" />
			</a-col>
		</a-row>
	</section>
</template>

<script>
	import ProductItem from "@/components/shared/ProductItem.vue";
	export default {
		name: "Promotion",
		props: ["products"],
		components: { ProductItem },
	};
</script>

<style lang="scss" scoped>
	.promotion {
		padding-top: 50px;
		padding-bottom: 30px;

		&__banner {
			display: flex;
			justify-content: center;
			align-items: center;

			width: 100%;
			min-height: 500px;

			background-image: url("@/assets/img/banner-1-1.jpg");
			background-size: cover;
			background-repeat: no-repeat;
			background-position: 50% 50%;

			color: #fff;

			h2,
			h5,
			a {
				color: inherit;
			}

			> div {
				text-align: center;
				font-size: 1rem;

				> h5 {
					padding: 0;
					margin-top: 1em;
					margin-bottom: 1em;
					letter-spacing: 5px;
					font-weight: 700;
					font-size: 1rem;
					font-family: "Roboto", sans-serif;
				}

				> h2 {
					font-size: 3.5rem;
					font-weight: 700;
					margin-top: 0.2em;
					margin-bottom: 0.2em;
					line-height: 1;
					padding: 0;
				}

				> a {
					display: inline-block;
					margin: 0.5em 0;
					background-color: transparent;
					border: 2px solid currentColor;
					line-height: 2.19em;
					text-transform: none;
					font-size: 0.97em;
					font-weight: 700;
					letter-spacing: 0.03em;
					vertical-align: middle;
					border-radius: 0;
					min-height: 2.5em;
					padding: 0 1.2em;
					max-width: 100%;
					transition: transform 0.3s, border 0.3s, background 0.3s, box-shadow 0.3s,
						opacity 0.3s, color 0.3s;
					text-rendering: optimizeLegibility;
					box-sizing: border-box;

					&:hover {
						background: #000;
						color: #fff;
						border: solid 2px #000;
					}
				}
			}

			.highlight {
				color: yellow;
			}
		}

		&__title {
			color: #1c1c1c;
			border-bottom: solid 2px #000;
			margin-bottom: 3em;
			padding-bottom: 10px;
			font-weight: 700;
			text-align: center;
			font-size: 1.25rem;
		}
	}
</style>
