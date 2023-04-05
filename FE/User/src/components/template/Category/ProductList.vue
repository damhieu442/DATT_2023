<template>
	<section class="product-list">
		<a-row type="flex" :gutter="[16, 16]">
			<a-col :span="6" v-for="product in products" :key="product.id">
				<CategoryProduct :product="product" />
			</a-col>
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
	});

	const pageChangeHandler = (page) => {
		emit("update:page", page);
	};
</script>

<style lang="scss" scoped>
	.product-list {
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
