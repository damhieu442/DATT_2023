<template>
	<a-modal
		v-model:visible="isShow"
		:width="700"
		:footer="null"
		:closable="false"
		title="Thanh toán online"
	>
		<p>Thẻ tín dụng</p>
		<div id="stripe-element-mount-point" />
		<a-button type="primary" class="submit-btn" :loading="isSubmitting" @click="submit">
			Thanh toán</a-button
		>
	</a-modal>
</template>

<script setup>
	import { nextTick, onMounted, ref } from "vue";
	import Stripe from "@/utils/payment";
	import { cart } from "@/api";
	import { notification } from "ant-design-vue";

	const emits = defineEmits(["success"]);
	const props = defineProps({ totalPrice: Number });

	const isShow = ref(false);
	const isSubmitting = ref(false);

	let stripe = null;
	let elements = null;
	let addressInfo = null;

	const getPaymentIntent = async () => {
		try {
			const response = await cart.getPaymentIntent(props.totalPrice);

			if (response.status > 199 && response.status < 300) {
				return response.data;
			} else {
				throw new Error();
			}
		} catch (error) {
			throw error;
		}
	};

	const submit = async () => {
		isSubmitting.value = true;
		try {
			const secret = await getPaymentIntent();

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

			console.log("response", response);

			if (response.paymentIntent.status === "succeeded") {
				emits("success", response.paymentIntent.id);
			}
		} catch (error) {
			console.log("Error", error);

			notification.error({ message: "Có lỗi xảy ra, vui lòng tải lại trang" });
		}
		isSubmitting.value = false;
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
						// iconColor: "#000",
						// color: "#000",
						// fontWeight: "800",
						fontFamily: "Ideal Sans, system-ui, sans-serif",
						// fontSize: "22px",
						// fontSmoothing: "antialiased",
						// ":-webkit-autofill": {
						// 	color: "#fce883",
						// },
						// "::placeholder": {
						// 	color: "green",
						// },
						border: "1px solid #E0E6EB",
						borderRadius: "4px",
					},
					invalid: {
						iconColor: "#FFC7EE",
						color: "red",
					},

					rules: {
						".Input": {
							border: "1px solid #E0E6EB",
							boxShadow:
								"0px 1px 1px rgba(0, 0, 0, 0.03), 0px 3px 6px rgba(18, 42, 66, 0.02)",
						},
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

<style lang="scss" scoped>
	.submit-btn {
		margin-top: 1rem;
		margin-left: auto;
		display: block;
		width: fit-content;
	}

	#stripe-element-mount-point {
		border: 1px solid #e0e6eb;
		box-shadow: 0px 1px 1px rgba(0, 0, 0, 0.03), 0px 3px 6px rgba(18, 42, 66, 0.02);
		padding: 0.5rem;
	}
</style>
