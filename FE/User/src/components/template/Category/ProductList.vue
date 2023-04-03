<template>
	<section class="product-list">
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
	});

	const pageChangeHandler = (page) => {
		emit("update:page", page);
	};
</script>

<style lang="scss" scoped>
	.product-list {
		&__pagination {
			width: max-content;
			margin: 0 auto;

			> :deep(li) {
				margin-bottom: 0;

				&.ant-pagination-options {
					display: none;
				}
			}
		}
	}
</style>
