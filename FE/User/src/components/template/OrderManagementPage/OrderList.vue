<template>
	<a-table :dataSource="data" :columns="tableColumns" :pagination="false">
		<template #bodyCell="{ column, record, text }">
			<template v-if="column.dataIndex === 'index'">
				<router-link :to="`/tai-khoan/don-hang/${record.id}`">{{ text }}</router-link>
			</template>
			<template v-if="column.dataIndex === 'paymentMethod'">
				<span>
					{{ PaymentMethodLabels[text] || "-" }}
				</span>
			</template>

			<template v-if="column.dataIndex === 'state'">
				<p className="m-0" :style="{ color: EOrderStateColors[record.state] }">
					{{ orderStates[record.state] }}
				</p>
			</template>
		</template>
	</a-table>
</template>

<script>
	import { OrderLabels, EOrderStateColors } from "@/constants/orders";
	import { PaymentMethodLabels } from "@/constants/payment";

	const tableColumns = [
		{
			title: "STT",
			dataIndex: "index",
			key: "order-id",
			width: 100,
		},
		{
			title: "Người nhận",
			dataIndex: "customerName",
			key: "order-receiver-name",
			ellipsis: true,
			minWidth: 200,
		},
		{
			title: "Ngày đặt",
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
			title: "Phương thức thanh toán",
			dataIndex: "paymentMethod",
			key: "order-payment-method",
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

			PaymentMethodLabels() {
				return PaymentMethodLabels;
			},

			EOrderStateColors() {
				return EOrderStateColors;
			},
		},
	};
</script>
