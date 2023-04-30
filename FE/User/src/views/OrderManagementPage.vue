<template>
	<main class="main">
		<h2 className="text-2xl">Đơn hàng của tôi</h2>
		<p>Nhấn vào STT để xem chi tiết đơn hàng</p>
		<a-tabs v-model:activeKey="activeOrderTab">
			<a-tab-pane v-for="(tabLabel, tabKey) in OrderLabels" :key="tabKey">
				<template #tab> {{ tabLabel }} </template>
			</a-tab-pane>
		</a-tabs>

		<LoadingPlaceholder :isLoading="isLoading">
			<div>
				<div v-if="orders.length === 0" class="main__empty-order">
					<div>
						<img
							src="@/assets/img/empty-order.png"
							width="208"
							height="208"
							className="w-52 h-52"
						/>
						<p className="text-center text-xl text-gray-400 my-4">
							Chưa có đơn hàng nào
						</p>
					</div>
				</div>
				<div v-else>
					<OrderList :data="orders" />
				</div>
			</div>
		</LoadingPlaceholder>
	</main>
</template>

<script setup>
	import dayjs from "dayjs";
	import { useStore } from "vuex";
	import { computed, onMounted, ref, watch } from "vue";
	import { notification } from "ant-design-vue";
	import { useRoute, useRouter } from "vue-router";

	import { cart as cartAPI } from "@/api";
	import { EKeys } from "@/constants/config";
	import { EOrderState, OrderLabels } from "@/constants/orders";

	import LoadingPlaceholder from "@/components/shared/LoadingPlaceholder.vue";
	import OrderList from "@/components/template/OrderManagementPage/OrderList.vue";

	const router = useRouter();
	const route = useRoute();
	const store = useStore();
	const isLoading = ref(false);
	const orders = ref([]);

	const activeOrderTab = computed({
		get() {
			return route.query[EKeys.order] || EOrderState.ALL;
		},
		set(tab) {
			const query = { ...route.query };

			const activeTab = tab ? Number(tab) : tab;

			switch (activeTab) {
				case EOrderState.ALL:
					delete query[EKeys.order];
					break;
				case EOrderState.PROCESSING:
					query[EKeys.order] = EOrderState.PROCESSING;
					break;
				case EOrderState.DELIVERYING:
					query[EKeys.order] = EOrderState.DELIVERYING;
					break;
				case EOrderState.COMPLETE:
					query[EKeys.order] = EOrderState.COMPLETE;
					break;
				case EOrderState.CANCEL:
					query[EKeys.order] = EOrderState.CANCEL;
					break;
				default:
					break;
			}

			router.replace({ query });
		},
	});

	const uid = computed(() => store.state.user.uid);

	const getData = async () => {
		isLoading.value = true;
		const numberFormater = new Intl.NumberFormat();
		try {
			const queries = {
				CustomerId: uid.value,
				status: activeOrderTab.value || undefined,
				pageSize: 10,
				pageNumber: 1,
			};

			const response = await cartAPI.getUserOrdersList(queries);
			if (response.status > 199 && response.status < 300) {
				if ("Data" in response.data) {
					orders.value = response.data.Data.map((bill, i) => ({
						index: i + 1,
						deliveryAddress: bill.Address,
						note: bill.Note,
						paymentMethod: bill.PaymentMethod,
						customerPhone: bill.PhoneNumber,
						state: bill.Status,
						totalPrice: `${numberFormater.format(bill.TotalPrice)} đ`,
						customerName: bill.FullName,
						id: bill.BillId,
						createTime: dayjs(
							bill.CreatedDate.endsWith("Z")
								? bill.CreatedDate
								: bill.CreatedDate + "Z",
						).format("DD/MM/YYYY HH:mm"),
					}));
				} else {
					orders.value = [];
				}
			}
		} catch (error) {
			notification.error({ message: "Có lỗi xảy ra, không lấy được danh sách" });
		} finally {
			isLoading.value = false;
		}
	};

	onMounted(() => {
		getData();
	});

	watch(activeOrderTab, () => {
		getData();
	});
</script>

<style lang="scss" scoped>
	.main {
		> h2 {
			font-size: 1.5rem; /* 24px */
			line-height: 2rem; /* 32px */
		}

		&__empty-order {
			display: flex;
			justify-content: center;
			align-items: center;
			min-height: 500px;

			> div {
				> img {
					width: 13rem;
					height: 13rem;
				}

				> p {
					text-align: center;
					font-size: 1.25rem; /* 20px */
					line-height: 1.75rem; /* 28px */
					color: rgb(156 163 175);
					margin: 1rem 0;
				}
			}
		}
	}
</style>
