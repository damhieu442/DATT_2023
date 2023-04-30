<template>
	<a-table
		:data-source="data"
		:pagination="false"
		class="evaluate-table"
		:loading="loading"
		:columns="tableColumns"
		:scroll="{ x: 1500 }"
		row-key="id"
		><template #bodyCell="{ column, record, index }">
			<template v-if="column.dataIndex === 'id'">
				<span>{{ index + 1 }}</span>
			</template>
			<template v-else-if="column.dataIndex === 'action'">
				<div class="flex">
					<button
						class="border-0 bg-transparent cursor-pointer mr-2 text-base"
						@click="editFeedback(record)"
					>
						<i class="fas fa-pen" />
					</button>
					<button
						v-if="store.state.role === ERole.SUPER_ADMIN"
						class="border-0 bg-transparent cursor-pointer text-base"
						@click="deleteFeedback(record)"
					>
						<i class="fa-solid fa-trash" />
					</button>
				</div>
			</template>
		</template>
	</a-table>
</template>

<script setup>
	import { ERole } from "@/constants/config";
	import { useStore } from "vuex";

	const store = useStore();

	const props = defineProps({
		loading: Boolean,
		data: {
			type: Array,
			default() {
				return [
					{
						id: "",
						username: "",
						email: "",
						address: "",
						content: "",
						note: "",
						status: "",
						createAt: "",
						updateAt: "",
					},
				];
			},
		},
	});

	const emits = defineEmits(["edit", "delete"]);

	const tableColumns = [
		{
			align: "center",
			title: "STT",
			dataIndex: "id",
			width: "150px",
		},
		{
			align: "center",
			title: "Người gửi",
			dataIndex: "username",
			width: "200px",
		},
		{
			align: "center",
			title: "Nội dung",
			dataIndex: "content",
			ellipsis: true,
			width: 250,
		},
		{
			align: "center",
			title: "Ghi chú",
			dataIndex: "note",
			ellipsis: true,
			width: 300,
		},
		{
			align: "center",
			title: "Trạng thái",
			dataIndex: "status",
			ellipsis: true,
			width: 120,
		},
		{
			align: "center",
			title: "Ngày tạo",
			dataIndex: "createAt",
			width: "175px",
		},
		{
			align: "center",
			title: "Ngày cập nhật",
			dataIndex: "updateAt",
			width: "175px",
		},
		{
			align: "right",
			width: "100px",
			dataIndex: "action",
		},
	];

	const editFeedback = (feedback) => {
		emits("edit", feedback);
	};

	const deleteFeedback = (feedback) => {
		emits("delete", feedback);
	};
</script>

<style lang="scss">
	.evaluate-table {
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
