<template>
	<a-table
		:loading="loading"
		:pagination="false"
		class="order-table"
		:row-class-name="calcStripedRowTable"
		row-key="id"
		:columns="columns"
		:data-source="data"
		:scroll="{ x: true }"
	>
		<template #bodyCell="{ column, text, record }">
			<template v-if="column.dataIndex === 'id'">
				<router-link :to="'/don-hang/' + text" class="text-black underline">
					{{ text }}
				</router-link>
			</template>
			<template v-if="column.dataIndex === 'name'">
				<p class="m-0" style="min-width: 200px">
					{{ text }}
				</p>
			</template>
			<template v-if="column.dataIndex === 'createTime'">
				<p class="m-0">
					{{ text }}
				</p>
			</template>
			<template v-if="column.dataIndex === 'total'">
				<p class="m-0">{{ text }} VNĐ</p>
			</template>
			<template v-if="column.dataIndex === 'state'">
				<a-tag :color="orderStateColors[text]" class="!text-sm">
					{{ orderStates[text] }}
				</a-tag>
			</template>
			<template v-if="column.key === 'action'">
				<router-link :to="'/don-hang/' + record.id" class="text-black underline">
					<EyeOutlined />
				</router-link>
			</template>
		</template>
	</a-table>
</template>

<script setup>
	import { EyeOutlined } from "@ant-design/icons-vue";

	import orderStates, { COLORS as orderStateColors } from "@/constants/order";
	import { useRouter } from "vue-router";

	const columns = [
		{
			dataIndex: "id",
			key: "id",
			title: "Mã đơn hàng",
			width: 240,
		},
		{
			dataIndex: "name",
			key: "name",
			title: "Người nhận",
		},
		{
			title: "Địa chỉ",
			dataIndex: "address",
			key: "address",
			width: 300,
		},
		{
			title: "Điện thoại",
			dataIndex: "phone",
			key: "phone",
			width: 150,
		},
		{
			title: "Tổng tiền",
			dataIndex: "total",
			key: "total",
			width: 150,
		},
		{
			title: "Ngày tạo",
			dataIndex: "createTime",
			key: "createTime",
			width: 250,
		},
		{
			title: "Trạng thái",
			key: "state",
			dataIndex: "state",
			width: 150,
		},
		{
			title: "",
			key: "action",
			width: 50,
		},
	];
	// | datetimeFormat("hh:mm - DD/MM/YYYY")  | numberFormat
	const props = defineProps({
		data: {
			type: Array,
			required: true,
		},
		loading: {
			type: Boolean,
		},
	});

	const router = useRouter();

	const navigateToDetail = ({ id }) => {
		router.push(`/don-hang/${id}`);
	};

	const calcStripedRowTable = (_, i) => {
		return i % 2 === 0 ? "bg-white" : "bg-blue-50";
	};
</script>

<style lang="scss">
	.order-table {
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
