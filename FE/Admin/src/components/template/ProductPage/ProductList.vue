<template>
	<a-table
		:data-source="data"
		:pagination="false"
		class="evaluate-table"
		:loading="loading"
		:columns="tableColumns"
		:scroll="{ x: 2000 }"
		row-key="id"
	>
		<template #bodyCell="{ column, record, text }">
			<template v-if="column.dataIndex === 'code'">
				<router-link :to="'/san-pham/' + record.id" class="">
					{{ text }}
				</router-link>
			</template>
			<template v-else-if="column.dataIndex === 'image'">
				<img
					:src="text"
					alt=""
					width="250"
					class="w-60 h-auto max-h-40 object-fill object-center"
				/>
			</template>
			<template v-else-if="column.dataIndex === 'action'">
				<div class="flex justify-between">
					<router-link
						:to="'/san-pham/' + record.id + '/chinh-sua'"
						class="inline-block w-6 h-6 text-black text-center align-middle text-base hover:text-black"
					>
						<i class="fas fa-pen" />
					</router-link>
					<button
						v-if="store.state.user.role === ERole.SUPER_ADMIN"
						class="border-0 bg-transparent cursor-pointer text-base"
						@click="deleteProduct(record)"
					>
						<i class="fa-solid fa-trash" />
					</button>
				</div>
			</template> </template
	></a-table>
</template>

<script setup>
	import { ERole } from "@/constants/config";
	import { useStore } from "vuex";

	const store = useStore();

	const props = defineProps({
		loading: Boolean,
		data: {
			type: Array,
		},
	});

	const emits = defineEmits(["delete"]);

	const tableColumns = [
		{
			align: "center",
			title: "Mã giày",
			dataIndex: "code",
			width: 150,
		},
		{
			align: "center",
			title: "Tên giày",
			dataIndex: "name",
			width: 200,
		},
		{
			align: "center",
			title: "Ảnh đại diện",
			dataIndex: "image",
			width: 250,
		},
		{
			align: "center",
			title: "Danh mục",
			dataIndex: "category",
			width: 150,
		},
		{
			align: "center",
			title: "Giá",
			dataIndex: "price",
			width: 120,
		},
		{
			align: "center",
			title: "Giảm giá",
			dataIndex: "discount",
			width: 120,
		},
		{
			align: "center",
			title: "Đã bán",
			dataIndex: "sold",
			width: 120,
		},
		{
			align: "center",
			title: "Đánh giá",
			dataIndex: "rating",
			width: 120,
		},
		{
			align: "center",
			title: "Ngày tạo",
			dataIndex: "createAt",
			width: 175,
		},
		{
			align: "center",
			title: "Người tạo",
			dataIndex: "createdBy",
			width: 175,
		},
		{
			align: "center",
			title: "Ngày cập nhật",
			dataIndex: "updateAt",
			width: 175,
		},
		{
			align: "center",
			title: "Người cập nhật",
			dataIndex: "updatedBy",
			width: 175,
		},
		{
			align: "right",
			width: 100,
			dataIndex: "action",
		},
	];

	const deleteProduct = (product) => {
		emits("delete", product);
	};
</script>
