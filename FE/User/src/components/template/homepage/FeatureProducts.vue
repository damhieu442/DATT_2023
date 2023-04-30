<template>
	<section class="feature-products">
		<a-tabs type="card">
			<a-tab-pane v-for="tab in tabList" :key="tab.tabCode" :tab="tab.tabName">
				<a-carousel
					class="feature-products__carousel"
					:dots="false"
					swipe
					autoplay
					draggable
					:slidesToShow="4"
				>
					<ProductItem
						v-for="product in tab.products"
						:key="product.id"
						:product="product"
					/>
				</a-carousel>
			</a-tab-pane>
		</a-tabs>
	</section>
</template>

<script>
	import ProductItem from "@/components/shared/ProductItem.vue";

	export default {
		props: ["tabList"],
		functional: true,
		components: { ProductItem },
	};
</script>

<style lang="scss" scoped>
	.feature-products {
		margin-top: 3.75rem;
	}
</style>

<style lang="scss">
	.feature-products {
		.ant-tabs.ant-tabs-top > .ant-tabs-nav {
			&::before {
				border-bottom-color: #000;
				border-bottom-width: 2px;
			}
		}

		.ant-tabs-nav > .ant-tabs-nav-wrap {
			justify-content: center;

			.ant-tabs-tab {
				background-color: transparent;
				color: rgba(102, 102, 102, 0.85);
				border: 0;

				> &-btn {
					font-weight: 700;
					letter-spacing: 0.02em;
					text-transform: uppercase;
					font-size: 1rem;
					color: inherit;
					transition: all 0.2s;
				}

				&:hover {
					background-color: #000;
					color: #fff;
				}

				&.ant-tabs-tab-active {
					background-color: #000;
					> .ant-tabs-tab-btn {
						color: #fff;
					}
				}
			}
		}

		&__carousel {
			min-height: 450px;

			.slick-slide {
				padding: 0 0.5rem;
			}
		}
	}
</style>
