<template>
	<main class="main">
		<a-row :gutter="32">
			<a-col :span="14">
				<BillingForm ref="deliveryAddress" />
			</a-col>
			<a-col :span="10">
				<OrderReview
					:products="productList"
					:isLoggedIn="isLoggedIn"
					@submit="orderHandler"
				/>
			</a-col>
		</a-row>
	</main>
</template>

<script setup>
	import BillingForm from "@/components/template/PaymentPage/BillingForm.vue";
	import OrderReview from "@/components/template/PaymentPage/OrderReview.vue";
	import { computed } from "@vue/reactivity";
	import { ref } from "vue";
	import { useStore } from "vuex";

	const store = useStore();

	const productList = store.state.cart.productList;

	const deliveryAddress = ref(null);

	const isLoggedIn = computed(() => !!store.state.user.uid);

	const orderHandler = async (paymentMethod) => {
		const form = await deliveryAddress.value.submit();

		if (!form) {
			return;
		}
	};
</script>

<style lang="scss" scoped>
	.main {
		max-width: 1230px;
		width: 100%;
		margin: 1.5rem auto;
		font-family: "Roboto", sans-serif;
		position: relative;
		font-size: 1rem;

		&__loader {
			position: absolute;
			top: 50%;
			transform: translateY(-50%);
		}

		&__overview {
			padding-bottom: 1.5rem;
		}
	}
</style>
