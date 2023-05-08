<template>
	<a-table
		:loading="loading"
		:pagination="false"
		class="review-list-table"
		:row-class-name="calcStripedRowTable"
		row-key="id"
		:columns="columns"
		:data-source="data"
		:scroll="{ x: true }"
	>
		<template #bodyCell="{ column, text, record, index }">
			<template v-if="column.key === 'id'">
				{{ index + 1 }}
			</template>
			<template v-if="column.dataIndex === 'productId'">
				<router-link :to="'/san-pham/' + text">
					{{ record.productName }}
				</router-link>
			</template>
			<template v-if="column.key === 'status'">
				{{ ReviewStatusLabels[text] }}
			</template>
			<template v-if="column.key === 'action'">
				<a-dropdown>
					<MoreOutlined />
					<template #overlay>
						<a-menu @click="(event) => dropdownMenuItemClickHandler(event, record)">
							<a-menu-item
								:key="EReviewStatus.APPROVED"
								:disabled="record.status === EReviewStatus.APPROVED"
							>
								Phê duyệt
							</a-menu-item>
							<a-menu-item
								:key="EReviewStatus.DECLINED"
								:disabled="record.status === EReviewStatus.DECLINED"
							>
								Từ chối
							</a-menu-item>
							<a-menu-item key="delete"> Xóa </a-menu-item>
						</a-menu>
					</template>
				</a-dropdown>
			</template>
		</template>
	</a-table>
</template>

<script setup>
	import { MoreOutlined } from "@ant-design/icons-vue";

	import { ReviewStatusLabels, EReviewStatus } from "@/constants/review";

	const props = defineProps({
		data: {
			type: Array,
			required: true,
		},
		loading: {
			type: Boolean,
		},
	});

	const emits = defineEmits(["update:approve", "update:reject", "delete"]);

	const columns = [
		{
			dataIndex: "id",
			key: "id",
			title: "STT",
			width: 50,
		},
		{
			dataIndex: "productId",
			key: "productId",
			title: "Sản phẩm",
			width: 250,
		},
		{
			dataIndex: "username",
			key: "username",
			title: "Người đánh giá",
			width: 200,
		},
		{
			title: "Email",
			dataIndex: "email",
			key: "email",
			width: 200,
		},
		{
			title: "Nội dung",
			dataIndex: "comment",
			key: "comment",
			minWidth: 350,
		},
		{
			title: "Đánh giá",
			dataIndex: "rating",
			key: "rating",
			width: 150,
		},
		{
			title: "Ngày tạo",
			dataIndex: "createdAt",
			key: "createdAt",
			width: 250,
		},
		{
			title: "Trạng thái",
			key: "status",
			dataIndex: "status",
			width: 150,
		},
		{
			title: "",
			key: "action",
			width: 50,
		},
	];

	const dropdownMenuItemClickHandler = ({ key: selectedOption }, item) => {
		switch (selectedOption) {
			case EReviewStatus.APPROVED:
				emits("update:approve", item);
				break;

			case EReviewStatus.DECLINED:
				emits("update:reject", item);
				break;

			default:
				emits("delete", item);
				break;
		}
	};

	const calcStripedRowTable = (_, i) => {
		return i % 2 === 0 ? "bg-white" : "bg-blue-50";
	};
</script>

<style lang="scss">
	.review-list-table {
		td {
			white-space: nowrap;
		}

		.ant-table-thead {
			th {
				@apply bg-cyan-400 text-white #{!important};
			}
		}
	}
</style>
