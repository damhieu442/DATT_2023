<template>
	<a-table
		:data-source="data"
		:pagination="false"
		class="category-table"
		:row-class-name="calcStripedRowTable"
		:loading="loading"
		:columns="tableColumns"
		:scroll="{ x: 1500 }"
		row-key="index"
		><template #bodyCell="{ column, record }">
			<template v-if="column.dataIndex === 'name'">
				<p class="m-0">
					{{ record.name }}
				</p>
			</template>
			<template v-else-if="column.dataIndex === 'description'">
				<p class="m-0 whitespace-nowrap">
					{{ record.description }}
				</p>
			</template>
			<template v-else-if="column.dataIndex === 'action'">
				<div class="flex">
					<button
						class="border-0 bg-transparent cursor-pointer mr-2 text-base"
						@click="navigateToEdit(record)"
					>
						<i class="fas fa-pen" />
					</button>
					<button
						v-if="$store.state.user.role === 1"
						class="border-0 bg-transparent cursor-pointer text-base disabled:opacity-60 disabled:cursor-not-allowed"
						@click="deleteCategory(record)"
						:disabled="record.parent === '-'"
					>
						<i class="fa-solid fa-trash" />
					</button>
				</div>
			</template>
		</template>
	</a-table>
</template>
<script>
	const tableColumns = [
		{
			align: "center",
			title: "Mã danh mục",
			dataIndex: "code",
			width: "150px",
		},
		{
			align: "center",
			title: "Danh mục",
			dataIndex: "name",
			ellipsis: true,
			scopedSlots: { customRender: "name" },
			width: "200px",
		},
		{
			align: "center",
			title: "Danh mục cha",
			dataIndex: "parent",
			width: "150px",
		},
		{
			align: "center",
			title: "Mô tả",
			dataIndex: "description",
			ellipsis: true,
			scopedSlots: { customRender: "description" },
			minWidth: 300,
		},
		{
			align: "center",
			title: "Ngày tạo",
			dataIndex: "createAt",
			width: "175px",
		},
		{
			align: "center",
			title: "Người tạo",
			dataIndex: "createBy",
			width: "100px",
		},
		{
			align: "center",
			title: "Ngày cập nhật",
			dataIndex: "updateAt",
			width: "175px",
		},
		{
			align: "center",
			title: "Người cập nhật",
			dataIndex: "updateBy",
			width: "150px",
		},
		{
			align: "right",
			width: "100px",
			dataIndex: "action",
		},
	];

	export default {
		name: "CategoryList",
		props: {
			data: {
				type: Array,
				required: true,
			},
			loading: {
				type: Boolean,
				required: true,
			},
		},
		emits: ["deleteCategory", "editCategory"],

		computed: {
			tableColumns() {
				return tableColumns;
			},
		},

		methods: {
			navigateToEdit(category) {
				this.$emit("editCategory", category);
			},
			deleteCategory(category) {
				this.$emit("deleteCategory", category);
			},
			calcStripedRowTable(_, i) {
				return i % 2 === 0 ? "row--stripe" : "row";
			},
		},
	};
</script>

<style lang="scss">
	.category-table {
		.ant-table-thead {
			@apply bg-cyan-400;

			th {
				@apply bg-transparent text-white #{!important};
			}
		}

		.ant-table-pagination {
			display: none;
		}

		.row {
			@apply bg-white #{!important};

			&--stripe {
				@apply bg-blue-50 #{!important};
			}
		}
	}
</style>
