<template>
	<div><h1 class="text-2xl">Danh sách đơn hàng</h1></div>
	<div class="my-5 px-7 py-6 border rounded-md bg-cyan-200">
		<OrderFilter />
	</div>
	<OrderListPage :loading="isGettingData" :data="orders" class="my-5" />
	<ThePagination :data="pagination" />
</template>

<script setup>
	import { useRoute } from "vue-router";
	import { onMounted, reactive, ref, watch } from "vue";
	import { notification } from "ant-design-vue";

	import ORDER_STATE from "@/constants/order";
	import OrderFilter from "@/components/template/OrderPage/OrderFilter.vue";
	import ThePagination from "@/components/shared/ThePagination.vue";
	import OrderListPage from "@/components/template/OrderPage/OrderList.vue";

	const MAX_RECORD = 12;
	const DEFAULT_PAGE = 1;

	const route = useRoute();

	const pagination = reactive({
		page: Number(route.query.pageNum) || DEFAULT_PAGE,
		total: 0,
		perPage: Number(route.query.pageSize) || MAX_RECORD,
	});
	const isGettingData = ref(false);
	const orders = ref([]);

	const getData = async () => {
		isGettingData.value = true;

		const { pageNum: page, pageSize: limit, search } = route.query;
		const query = {
			pageNumber: page || DEFAULT_PAGE,
			pageSize: limit || MAX_RECORD,
			keyWord: search || "",
		};
		const body = {
			Field: "CreatedDate",
			Order: "DESC",
			operation: "",
			DataType: "",
			Type: "sort",
		};

		try {
			const [deliverying, processing, complete, rejected] = Object.keys(ORDER_STATE);

			const data = await new Promise((res) =>
				setTimeout(res, 1500, [
					{
						id: "6278bca38bd5470009e5ded6",
						orders: [
							{
								id: 1,
								name: "Tên sản phẩm 1",
								image: "https://images.unsplash.com/photo-1644982653424-1bfed7f972a2?ixlib=rb-1.2.1&ixid=MnwxMjA3fDF8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=789&q=80",
								category: "Nước ép hạt",
								createTime: 1652149368173,
								price: 100,
								amount: 10,
							},
							{
								id: 2,
								name: "Tên sản phẩm 1",
								image: "https://images.unsplash.com/photo-1644982653424-1bfed7f972a2?ixlib=rb-1.2.1&ixid=MnwxMjA3fDF8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=789&q=80",
								category: "Nước ép hạt",
								createTime: 1652149368173,
								price: 100,
								amount: 10,
							},
							{
								id: 3,
								name: "Tên sản phẩm 1",
								image: "https://images.unsplash.com/photo-1644982653424-1bfed7f972a2?ixlib=rb-1.2.1&ixid=MnwxMjA3fDF8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=789&q=80",
								category: "Nước ép hạt",
								createTime: 1652149368173,
								price: 100,
								amount: 10,
							},
						],
						discount: 0,
						name: "we wre",
						phone: "01718890326",
						address: "wer, werwe",
						note: "abc",
						state: processing,
						amount: 1581,
						shipping: 20,
						total: 1601,
						user: "619f77b553cc5a1858ef3670",
						createTime: "2022-05-09T06:04:52.338Z",
					},
					{
						id: "627411b17c482b000ae67328",
						orders: [
							{
								id: 1,
								name: "Tên sản phẩm 1",
								image: "https://images.unsplash.com/photo-1644982653424-1bfed7f972a2?ixlib=rb-1.2.1&ixid=MnwxMjA3fDF8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=789&q=80",
								category: "Nước ép hạt",
								createTime: 1652149368173,
								price: 100,
								amount: 10,
							},
							{
								id: 2,
								name: "Tên sản phẩm 1",
								image: "https://images.unsplash.com/photo-1644982653424-1bfed7f972a2?ixlib=rb-1.2.1&ixid=MnwxMjA3fDF8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=789&q=80",
								category: "Nước ép hạt",
								createTime: 1652149368173,
								price: 100,
								amount: 10,
							},
							{
								id: 3,
								name: "Tên sản phẩm 1",
								image: "https://images.unsplash.com/photo-1644982653424-1bfed7f972a2?ixlib=rb-1.2.1&ixid=MnwxMjA3fDF8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=789&q=80",
								category: "Nước ép hạt",
								createTime: 1652149368173,
								price: 100,
								amount: 10,
							},
						],
						phone: "01718890326",
						discount: 0,
						name: "S. Sakib",
						address: "Dhaka, Bangladesh",
						note: "abc",
						state: complete,
						amount: 1581,
						shipping: 20,
						total: 1601,
						user: "627411731e1b3e000905bf40",
						createTime: "2022-05-06T06:04:52.338Z",
					},
					{
						id: "616f3377f43dc900168e37ad",
						orders: [
							{
								id: 1,
								name: "Tên sản phẩm 1",
								image: "https://images.unsplash.com/photo-1644982653424-1bfed7f972a2?ixlib=rb-1.2.1&ixid=MnwxMjA3fDF8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=789&q=80",
								category: "Nước ép hạt",
								createTime: 1652149368173,
								price: 100,
								amount: 10,
							},
							{
								id: 2,
								name: "Tên sản phẩm 1",
								image: "https://images.unsplash.com/photo-1644982653424-1bfed7f972a2?ixlib=rb-1.2.1&ixid=MnwxMjA3fDF8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=789&q=80",
								category: "Nước ép hạt",
								createTime: 1652149368173,
								price: 100,
								amount: 10,
							},
							{
								id: 3,
								name: "Tên sản phẩm 1",
								image: "https://images.unsplash.com/photo-1644982653424-1bfed7f972a2?ixlib=rb-1.2.1&ixid=MnwxMjA3fDF8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=789&q=80",
								category: "Nước ép hạt",
								createTime: 1652149368173,
								price: 100,
								amount: 10,
							},
						],
						phone: "01718890326",
						discount: 0,
						name: "S. Sakib",
						address: "Dhaka, Bangladesh",
						note: "abc",
						state: deliverying,
						amount: 1581,
						shipping: 20,
						total: 1601,
						user: "61531a4f1c38473378ab0825",
						createTime: "2022-05-06T06:04:52.338Z",
					},
					{
						id: "616c9a889eb30500169d5c0f",
						orders: [
							{
								id: 1,
								name: "Tên sản phẩm 1",
								image: "https://images.unsplash.com/photo-1644982653424-1bfed7f972a2?ixlib=rb-1.2.1&ixid=MnwxMjA3fDF8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=789&q=80",
								category: "Nước ép hạt",
								createTime: 1652149368173,
								price: 100,
								amount: 10,
							},
							{
								id: 2,
								name: "Tên sản phẩm 1",
								image: "https://images.unsplash.com/photo-1644982653424-1bfed7f972a2?ixlib=rb-1.2.1&ixid=MnwxMjA3fDF8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=789&q=80",
								category: "Nước ép hạt",
								createTime: 1652149368173,
								price: 100,
								amount: 10,
							},
							{
								id: 3,
								name: "Tên sản phẩm 1",
								image: "https://images.unsplash.com/photo-1644982653424-1bfed7f972a2?ixlib=rb-1.2.1&ixid=MnwxMjA3fDF8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=789&q=80",
								category: "Nước ép hạt",
								createTime: 1652149368173,
								price: 100,
								amount: 10,
							},
						],
						phone: "01718890326",
						discount: 0,
						name: "S. Sakib",
						address: "Dhaka, Bangladesh",
						note: "abc",
						state: rejected,
						amount: 1581,
						shipping: 20,
						total: 1601,
						user: "61531a4f1c38473378ab0829",
						createTime: "2022-05-06T06:04:52.338Z",
					},
					{
						id: "616c9a889gh30500169d5c0f",
						orders: [
							{
								id: 1,
								name: "Tên sản phẩm 1",
								image: "https://images.unsplash.com/photo-1644982653424-1bfed7f972a2?ixlib=rb-1.2.1&ixid=MnwxMjA3fDF8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=789&q=80",
								category: "Nước ép hạt",
								createTime: 1652149368173,
								price: 100,
								amount: 10,
							},
							{
								id: 2,
								name: "Tên sản phẩm 1",
								image: "https://images.unsplash.com/photo-1644982653424-1bfed7f972a2?ixlib=rb-1.2.1&ixid=MnwxMjA3fDF8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=789&q=80",
								category: "Nước ép hạt",
								createTime: 1652149368173,
								price: 100,
								amount: 10,
							},
							{
								id: 3,
								name: "Tên sản phẩm 1",
								image: "https://images.unsplash.com/photo-1644982653424-1bfed7f972a2?ixlib=rb-1.2.1&ixid=MnwxMjA3fDF8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=789&q=80",
								category: "Nước ép hạt",
								createTime: 1652149368173,
								price: 100,
								amount: 10,
							},
						],
						phone: "01718890326",
						discount: 0,
						name: "S. Sakib",
						address: "Dhaka, Bangladesh",
						note: "abc",
						state: processing,
						amount: 1581,
						shipping: 20,
						total: 1601,
						user: "61531a4f1c38473378ab0829",
						createTime: "2022-05-06T06:04:52.338Z",
					},
				]),
			);
			orders.value = data;

			Object.assign(pagination, {
				page: Number(data.CurrentPage),
				total: Number(data.TotalRecord),
				perPage: Number(data.CurrentPageRecords),
			});
		} catch (error) {
			console.log(error);
			notification.error({ message: "Có lỗi xảy ra, không thế lấy danh sách đơn hàng" });
		} finally {
			isGettingData.value = false;
		}
	};

	onMounted(() => {
		getData();
	});

	watch(
		() => route.query,
		() => {
			getData();
		},
	);
</script>
