<template>
	<a-table
		:loading="loading"
		:pagination="false"
		class="order-table"
		:row-class-name="calcStripedRowTable"
		row-key="id"
		:columns="columns"
		:data-source="data"
		><template #bodyCell="{ column, text, record }">
			<template v-if="column.dataIndex === 'id'">
				<router-link :to="'/san-pham/' + text" class="text-black underline">
					{{ text }}
				</router-link>
			</template>

			<template v-else-if="column.dataIndex === 'name'">
				<p class="m-0" style="min-width: 200px">
					{{ text }}
				</p>
			</template>
			<template v-else-if="column.dataIndex === 'image'">
				<img :src="text" :alt="record.name" width="100" class="w-24 h-24 object-fill" />
			</template>
			<template v-else-if="column.dataIndex === 'price'">
				<p class="m-0">{{ text }} VNĐ</p>
			</template>
			<template v-else-if="column.dataIndex === 'discount'">
				<p class="m-0">- {{ text || 0 }} %</p>
			</template>
			<template v-else-if="column.dataIndex === 'totalPrice'">
				<p class="m-0 text-red-500">{{ text }} VNĐ</p>
			</template>
		</template>
	</a-table>
</template>

<script setup>
	// import orderStates, { COLORS as orderStateColors } from "~/constants/order";

	const props = defineProps({
		data: {
			type: Array,
			required: true,
		},
		loading: {
			type: Boolean,
		},
	});

	const columns = [
		{
			dataIndex: "id",
			key: "id",
			title: "Mã sản phẩm",
			width: 150,
		},
		{
			dataIndex: "image",
			key: "image",
			title: "Ảnh minh họa",
			width: 100,
		},
		{
			dataIndex: "name",
			key: "name",
			title: "Tên sản phẩm",
			width: 250,
		},
		{
			dataIndex: "size",
			key: "size",
			title: "Kích thước",
			width: 150,
		},

		{
			title: "Số lượng",
			dataIndex: "amount",
			key: "amount",
			width: 100,
		},
		{
			title: "Giá thành",
			dataIndex: "price",
			key: "price",

			width: 150,
		},
		{
			title: "Giảm giá",
			dataIndex: "discount",
			key: "discount",

			width: 150,
		},
		{
			title: "Thành tiền",
			key: "totalPrice",
			dataIndex: "totalPrice",

			width: 175,
		},
	];

	const calcStripedRowTable = (_, i) => {
		return i % 2 === 0 ? "bg-white" : "bg-blue-50";
	};
</script>

<style lang="scss">
	.order-table {
		.ant-table-thead {
			th {
				@apply bg-cyan-400 text-white #{!important};
			}
		}
	}
</style>
