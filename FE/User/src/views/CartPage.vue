<template>
	<main class="main">
		<div v-if="isLoadingData" class="main__loader"><PageLoader /></div>
		<div v-else-if="productList.length === 0" class="main__empty">
			<p>Chưa có sản phẩm nào trong giỏ hàng</p>
			<router-link to="/san-pham">Quay lại cửa hàng</router-link>
		</div>
		<div v-else class="main__content">
			<a-row>
				<a-col :span="16"><ShopTable :products="productList" /></a-col>
				<a-col :span="8"
					><CartTotal :total-money="totalPrice" :total-amount="totalProduct"
				/></a-col>
			</a-row>
		</div>
	</main>
</template>

<script setup>
	import { useStore } from "vuex";
	import PageLoader from "@/components/shared/PageLoader.vue";
	import { computed, ref } from "vue";
	import ShopTable from "@/components/template/CartPage/ShopTable.vue";
	import CartTotal from "@/components/template/CartPage/CartTotal.vue";

	const store = useStore();

	const productList = store.state.cart.productList;
	const isLoadingData = ref(false);
	const totalProduct = computed(() => store.getters["cart/totalProduct"]);
	const totalPrice = computed(() => store.getters["cart/totalPrice"]);
</script>

<style lang="scss" scoped>
	.main {
		max-width: 1230px;
		width: 100%;
		margin: 0 auto;
		font-family: "Roboto", sans-serif;

		&__loader {
			position: absolute;
			top: 50%;
			transform: translateY(-50%);
		}

		&__empty {
			margin: 3.75rem 0;
			text-align: center;
			color: #353535;
			font-size: 1rem;

			> a {
				display: inline-block;
				background-color: #c30005;
				color: #fff;
				text-transform: uppercase;
				font-size: 0.97rem;
				letter-spacing: 0.03rem;
				cursor: pointer;
				font-weight: bolder;
				text-align: center;
				line-height: 2.4em;
				min-height: 2.5em;
				padding: 0 1.2em;

				&:hover {
					box-shadow: inset 0 0 0 100px rgba(0, 0, 0, 0.2);
				}
			}
		}

		&__content {
			padding: 30px 0 3.75rem;

			> .ant-row {
				> .ant-col {
					padding: 0 30px;
					&:first-child {
						border-right: 1px solid #ececec;
					}
					&:last-child {
						border-left: 1px solid #ececec;
					}
				}
			}
		}
	}
</style>