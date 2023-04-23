<template>
	<div class="ct-pagination flex justify-between items-center space-x-4 mt-4">
		<div>
			<span>Hiển thị tối đa</span>
			<a-select
				v-model:value="value"
				class="w-32 mx-2"
				placeholder=""
				@change="onPageSizeChange"
			>
				<a-select-option v-for="item in PAGE_OPTIONS" :key="item" :value="item">
					<span>{{ item }}</span>
				</a-select-option>
			</a-select>
			<span>/ {{ data ? data.total : 30 }} bản ghi</span>
		</div>
		<a-pagination
			v-model:current="pagination.page"
			:total="pagination.total"
			:page-size="pagination.perPage || 12"
			:item-render="itemRender"
			@change="handleChangePage"
		/>
	</div>
</template>

<script>
	import _assign from "lodash/assign";
	import _clondeDeep from "lodash/cloneDeep";

	const PAGE_OPTIONS = [12, 25, 50, 100];

	export default {
		props: {
			data: {
				type: Object,
				default: () => ({
					page: 1,
					total: 30,
					perPage: 12,
				}),
			},
			pageNumQuery: {
				type: String,
				default: "pageNum",
			},
			pageSizeQuery: {
				type: String,
				default: "pageSize",
			},
		},

		data() {
			return {
				pagination: _clondeDeep({
					...this.data,
					page: parseInt(this.data?.page, 10),
				}),
				PAGE_OPTIONS,
				value: this.data.perPage,
			};
		},

		watch: {
			data: {
				handler(value) {
					this.pagination = _clondeDeep(value);
				},
				deep: true,
			},
		},

		methods: {
			handleChangePage(page) {
				this.$router.push(this.to(page));
			},

			to(page) {
				const path = this.$route.path;

				const otherParams = _assign({}, this.$route.query);
				delete otherParams[this.pageNumQuery];

				const query =
					page !== 1
						? _assign({}, otherParams, { [this.pageNumQuery]: page })
						: otherParams;

				return { path, query };
			},

			onPageSizeChange() {
				this.$router.push({
					query: _assign({}, this.$route.query, {
						[this.pageSizeQuery]: this.value,
					}),
				});
			},

			itemRender({ type, originalElement }) {
				// console.log("Type", page, type, originalElement);
				if (type === "prev") {
					return "Trước";
				}
				if (type === "next") {
					return "Sau";
				}
				return originalElement;
			},
		},
	};
</script>

<style lang="scss">
	.ct-pagination {
		.ant-pagination {
			&-item {
				border: none;
			}
			&-item,
			&-prev,
			&-next {
				@apply text-black #{!important};
			}

			&-disabled {
				@apply text-gray-400 #{!important};
			}
		}
	}
</style>
