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
					:loading="isGettingCheckoutKey"
					@submit="orderHandler"
				/>
				<p v-if="!!paymentId">
					Mã giao dịch: {{ paymentId }}
					<br />
					Vui lòng lưu giữ mã giao dịch cẩn thận
				</p>
			</a-col>
		</a-row>
		<CheckoutModal ref="rfCheckoutModal" v-if="isShowCheckout" @success="successPayment" />
	</main>
</template>

<script setup>
	import { ref, computed, nextTick } from "vue";
	import { useStore } from "vuex";
	import BillingForm from "@/components/template/PaymentPage/BillingForm.vue";
	import OrderReview from "@/components/template/PaymentPage/OrderReview.vue";
	import CheckoutModal from "@/components/shared/CheckoutModal.vue";
	import { notification } from "ant-design-vue";
	import { EPaymentMethod } from "@/constants/payment";

	const store = useStore();

	const productList = store.state.cart.productList;

	const deliveryAddress = ref(null);
	const rfCheckoutModal = ref(null);
	const isShowCheckout = ref(false);
	const isGettingCheckoutKey = ref(false);
	const paymentId = ref("");

	const isLoggedIn = computed(() => !!store.state.user.uid);

	const orderHandler = async (paymentMethod) => {
		const form = await deliveryAddress.value.submit();

		if (!form) {
			return;
		}

		if (paymentMethod === EPaymentMethod.OFFLINE) {
			return;
		}

		isShowCheckout.value = true;
		await nextTick();
		rfCheckoutModal.value.open(form);
	};

	const successPayment = (payId) => {
		paymentId.value = payId;
		notification.success({ message: `Thanh toán thành công, mã giao dịch ${paymentId}` });
		rfCheckoutModal.value.close();
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
