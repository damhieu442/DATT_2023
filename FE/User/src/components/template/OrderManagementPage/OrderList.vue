<template>
	<a-table :dataSource="data" :columns="tableColumns" :pagination="false">
		<template #bodyCell="{ column, record }">
			<template v-if="column.dataIndex === 'id'">
				<router-link :to="`/tai-khoan/don-hang/${record.id}`">{{ record.id }}</router-link>
			</template>

			<template v-if="column.dataIndex === 'items'">
				<div :style="{ minWidth: '250px' }">
					<router-link
						v-for="item in record.items"
						:key="item.id"
						className="block mb-2 text-current w-full"
						:to="`/san-pham/${item.id}`"
					>
						{{ item.name }}
					</router-link>
				</div>
			</template>

			<template v-if="column.dataIndex === 'state'">
				<p className="m-0">{{ orderStates[record.state] }}</p>
			</template>
		</template>
	</a-table>
</template>

<script>
	import { OrderLabels } from "@/constants/orders";
	const tableColumns = [
		{
			title: "Mã đơn hàng",
			dataIndex: "id",
			key: "order-id",
			width: 200,
		},
		{
			title: "Sản phẩm",
			dataIndex: "items",
			key: "order-product-name",
			ellipsis: true,
		},
		{
			title: "Ngày tạo",
			dataIndex: "createTime",
			key: "order-create",
			width: 150,
		},
		{
			title: "Tổng tiền",
			dataIndex: "totalPrice",
			key: "order-total",
			width: 150,
		},
		{
			title: "Trạng thái",
			dataIndex: "state",
			key: "order-state",
			width: 200,
		},
	];

	export default {
		props: {
			data: {
				type: Array,
				default() {
					return [];
				},
			},
		},

		computed: {
			orderStates() {
				return OrderLabels;
			},

			tableColumns() {
				return tableColumns;
			},
		},
	};
</script>
