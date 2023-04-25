<template>
	<a-modal v-model:visible="isShow" :width="700" title="Thanh toán">
		<!-- <StripeElementPayment
			ref="rfPaymentEl"
			:pk="stripePublichKey"
			:elements-options="elementsOptions"
		/> -->
		<label for="email_field">Credit Card</label>
		<div id="stripe-element-mount-point" class="nes-input" />
		<a-button @click="submit"> Thanh toán</a-button>
	</a-modal>
</template>

<script setup>
	import { nextTick, onMounted, reactive, ref } from "vue";
	import Stripe from "@/utils/payment";

	import { cart } from "@/api";
	import { notification } from "ant-design-vue";

	const emits = defineEmits(["success"]);

	const isShow = ref(false);
	let stripe = null;
	let elements = null;
	let addressInfo = null;

	const getPaymentIntent = async () => {
		try {
			const response = await cart.getPaymentIntent();

			if (response.status > 199 && response.status < 300) {
				return response.data.secret;
			} else {
				throw new Error();
			}
		} catch (error) {
			throw error;
		}
	};

	const submit = async () => {
		try {
			const secret = await getPaymentIntent();

			console.log("Secret: ", secret);

			const cardElement = elements.getElement("card");
			const paymentMethodReq = await stripe.createPaymentMethod({
				type: "card",
				card: cardElement,
				billing_details: {
					name: addressInfo.fullName,
					email: addressInfo.email,
					phone: addressInfo.phone,
				},
			});

			const response = await stripe.confirmCardPayment(secret, {
				payment_method: paymentMethodReq.paymentMethod.id,
			});

			if (response.paymentIntent.status === "succeeded") {
				emits("success", response.paymentIntent.id);
			}

			// rfPaymentEl.value.submit();
		} catch (error) {
			console.log("Error", error);

			notification.error({ message: "Có lỗi xảy ra, vui lòng tải lại trang" });
		}
	};

	/**
     * 
     * @param {Object} form : {
            fullName: string;
            age: string;
            country: string;
            address: string;
            city: string;
            phone: string;
            email: string;
            note: string;
        } 
     */
	const openModal = (form) => {
		isShow.value = true;
		addressInfo = form;
		nextTick(() => {
			const ELEMENT_TYPE = "card";

			const element = elements.create(ELEMENT_TYPE, {
				style: {
					base: {
						iconColor: "#000",
						color: "#000",
						fontWeight: "800",
						fontFamily: "Arial",
						fontSize: "22px",
						fontSmoothing: "antialiased",
						":-webkit-autofill": {
							color: "#fce883",
						},
						"::placeholder": {
							color: "green",
						},
					},
					invalid: {
						iconColor: "#FFC7EE",
						color: "red",
					},
				},
			});
			element.mount("#stripe-element-mount-point");
		});
	};

	const closeModal = () => {
		isShow.value = false;
	};

	onMounted(async () => {
		stripe = await Stripe;

		elements = stripe.elements();
	});

	defineExpose({
		open: openModal,
		close: closeModal,
	});
</script>
