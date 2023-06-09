<template>
	<aside class="sidebar-filter">
		<div class="sidebar-filter__section">
			<h4 class="sidebar-filter__section__title">LỌC THEO GIÁ</h4>
			<a-slider
				class="sidebar-filter__section__slider"
				v-model:value="priceRange"
				range
				:min="0"
				:max="10000000"
				:step="1000"
			/>

			<div class="sidebar-filter__section__actions">
				<a-button @click="filterProductInRangeHandler">Lọc</a-button>
				<p style="font-size: 14px">
					Giá
					<strong>
						{{ formattedPriceRange[0] }} ₫ — {{ formattedPriceRange[1] }} ₫
					</strong>
				</p>
			</div>
		</div>

		<div class="sidebar-filter__section">
			<h4 class="sidebar-filter__section__title">SẢN PHẨM</h4>
			<ul class="sidebar-filter__section__prod-list">
				<template v-if="loading">
					<li v-for="i in 5" :key="i">
						<a-space>
							<a-skeleton-image active />
							<a-skeleton :paragraph="{ rows: 1 }" style="width: 150px" />
						</a-space>
					</li>
				</template>
				<template v-else>
					<li v-for="product in widgetProducts" :key="product.id">
						<ProductWidgetItem :product="product" />
					</li>
				</template>
			</ul>
		</div>
	</aside>
</template>

<script setup>
	import { computed, ref } from "vue";
	import ProductWidgetItem from "@/components/shared/ProductWidgetItem.vue";

	const props = defineProps(["widgetProducts", "loading"]);
	const emit = defineEmits(["filter"]);

	const priceRange = ref([0, 10_000_000]);

	const formattedPriceRange = computed(() => {
		const numberFormatter = new Intl.NumberFormat();

		return [
			numberFormatter.format(priceRange.value[0]),
			numberFormatter.format(priceRange.value[1]),
		];
	});

	const filterProductInRangeHandler = () => {
		emit("filter", priceRange.value);
	};
</script>

<style lang="scss" scoped>
	.sidebar-filter {
		& > &__section:first-child {
			margin-top: 0;
		}
		&__section {
			margin: 1rem 0;
			&__title {
				font-weight: bold;
				line-height: 1.05;
				letter-spacing: 0.05em;
				text-transform: uppercase;
				font-size: 1rem;
				margin-bottom: 1rem;
			}

			&__slider {
				margin-bottom: 1rem;

				> :deep(.ant-slider-track),
				> :deep(.ant-slider-handle) {
					background-color: #666;
					border-color: #666;
				}
			}

			&__prod-list {
				border: 1px solid #ddd;
				border-radius: 3px;
				padding: 15px;
				background-color: rgba(221, 221, 221, 0.1);
				margin-bottom: 0;

				> li {
					border-bottom: 1px dashed #ddd;

					&:last-child {
						border-bottom: 0;
					}
				}
			}

			&__actions {
				display: flex;
				justify-content: space-between;
				align-items: center;

				> .ant-btn {
					padding: 0 1.2rem;
					margin-right: 1em;
					font-weight: bolder;
					text-align: center;
					letter-spacing: 0.03em;
					border-radius: 99px;
					background-color: #666;
					font-size: 0.85rem;
					color: #fff;

					> :deep(span) {
						text-transform: uppercase;
					}
				}

				p {
					margin-bottom: 0;
				}
			}
		}
	}
</style>
