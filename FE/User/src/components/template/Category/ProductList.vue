<template>
	<section class="product-list">
		<a-row type="flex" :gutter="[16, 16]">
			<template v-if="loading">
				<a-col v-for="i in 12" :key="i" :span="6">
					<div class="product-list__skeleton">
						<a-skeleton-image active />
						<a-skeleton :paragraph="{ rows: 2 }" style="width: 100%" />
					</div>
				</a-col>
			</template>
			<template v-else>
				<a-col v-for="product in products" :key="product.id" :span="6">
					<CategoryProduct :product="product" />
				</a-col>
			</template>
		</a-row>

		<a-pagination
			class="product-list__pagination"
			:current="pagination.current"
			:total="pagination.total"
			@change="pageChangeHandler"
		/>
	</section>
</template>

<script setup>
	import { computed } from "vue";
	import CategoryProduct from "@/components/shared/CategoryProduct.vue";

	const emit = defineEmits(["update:page"]);

	const props = defineProps({
		pagination: {
			type: Object,
			default() {
				return {
					current: 1,
					total: 20,
					pageSize: 10,
				};
			},
		},

		products: { type: Array },

		loading: Boolean,
	});

	const pageChangeHandler = (page) => {
		emit("update:page", page);
	};
</script>

<style lang="scss" scoped>
	.product-list {
		&__skeleton {
			> .ant-skeleton {
				width: 100%;
			}

			:deep(.ant-skeleton-image) {
				width: 100%;
				height: 160px;
				padding: 0.25rem;
			}
		}

		&__pagination {
			width: max-content;
			margin: 1rem auto 0;

			> :deep(li) {
				margin-bottom: 0;
				font-weight: 700;

				&.ant-pagination-item,
				&.ant-pagination-prev > button,
				&.ant-pagination-next > button {
					border: 2px solid #334862;
					background-color: transparent;
					border-radius: 50%;
					color: #334862;

					> a {
						color: inherit;
					}

					&:hover {
						background-color: #c30005;
						color: white;
						border-color: #c30005;
					}
				}

				&.ant-pagination-item.ant-pagination-item-active {
					background-color: #c30005;
					color: white;
					border-color: #c30005;

					> a {
						color: inherit;
					}
				}

				&.ant-pagination-options {
					display: none;
				}

				& > .ant-pagination-item-link {
					&:disabled {
						opacity: 0.6;
					}
				}
			}
		}
	}
</style>
