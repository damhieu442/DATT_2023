<template>
	<main class="main">
		<h2 className="text-2xl">Đơn hàng của tôi</h2>
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
	import { computed, ref, watch } from "vue";
	import { useRoute, useRouter } from "vue-router";
	import dayjs from "dayjs";
	import { EOrderState, OrderLabels } from "@/constants/orders";
	import { EKeys } from "@/constants/config";
	import LoadingPlaceholder from "@/components/shared/LoadingPlaceholder.vue";
	import OrderList from "@/components/template/OrderManagementPage/OrderList.vue";

	const router = useRouter();
	const route = useRoute();
	const isLoading = ref(false);
	const orders = ref([]);
	const activeOrderTab = computed({
		get() {
			return route.query[EKeys.order] || EOrderState.ALL;
		},
		set(tab) {
			const query = { ...route.query };

			switch (tab) {
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

	watch(
		activeOrderTab,
		async (tab) => {
			isLoading.value = true;
			const numberFormater = new Intl.NumberFormat();
			try {
				await new Promise((res) => setTimeout(res, 1500));
				orders.value = new Array(10).fill("").map((_, i) => ({
					deliveryAddress: "",
					items: [
						{
							id: "f90a2385-3e7b-4e99-b5bd-b8eff1d81c5c",
							name: "Shoe",
							price: `${numberFormater.format(1_500_000)} đ`,
							img: "https://localhost:7050/api/Shoes/imgName/men-psy-1-300x225",
						},
					],
					note: "",
					paymentMethod: "",
					customerPhone: "",
					state: tab,
					totalPrice: `${numberFormater.format(1_500_000)} đ`,
					customerName: "hoangzzzsss",
					id: "order.id-" + i,
					createTime: dayjs(new Date(2023, 10 + i, 25)).format("DD/MM/YYYY HH:mm"),
				}));
			} catch (error) {}
			isLoading.value = false;
		},
		{ immediate: true },
	);
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
