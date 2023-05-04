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
					:disabled="productList.length === 0"
					@submit="orderHandler"
				/>
				<p v-if="!!paymentId">
					Mã giao dịch: {{ paymentId }}
					<br />
					Vui lòng lưu giữ mã giao dịch cẩn thận
				</p>
			</a-col>
		</a-row>
		<CheckoutModal
			ref="rfCheckoutModal"
			v-if="isShowCheckout"
			:totalPrice="totalPrice"
			@success="successPayment"
		/>
	</main>
</template>

<script setup>
	import { useStore } from "vuex";
	import { useRouter, useRoute } from "vue-router";
	import { ref, computed, nextTick, onMounted, onBeforeUnmount, watch } from "vue";

	import { notification } from "ant-design-vue";

	import { EPaymentMethod } from "@/constants/payment";

	import { cart as cartAPI, product as productAPI } from "@/api";

	import BillingForm from "@/components/template/PaymentPage/BillingForm.vue";
	import OrderReview from "@/components/template/PaymentPage/OrderReview.vue";
	import CheckoutModal from "@/components/shared/CheckoutModal.vue";

	const store = useStore();
	const route = useRoute();
	const router = useRouter();

	const deliveryAddress = ref(null);
	const rfCheckoutModal = ref(null);
	const isShowCheckout = ref(false);
	const isGettingCheckoutKey = ref(false);
	const paymentId = ref("");
	const productSizes = ref(new Map());
	let paymentForm = null;

	const productList = computed(() => {
		if (route.query.buyNow && store.state.cart.buyNowProduct) {
			return [store.state.cart.buyNowProduct];
		}

		return store.state.cart.productList;
	});
	const isLoggedIn = computed(() => !!store.state.user.uid);
	const totalPrice = computed(() => {
		if (route.query.buyNow && store.state.cart.buyNowProduct) {
			const buyNowProduct = store.state.cart.buyNowProduct;

			return Math.trunc(
				buyNowProduct.quantity * buyNowProduct.price * (1 - buyNowProduct.discount / 100),
			);
		}

		return store.getters["cart/totalPrice"];
	});

	const getDetailCartProducts = async () => {
		console.log("Called: ", productList);

		try {
			const productResponses = await Promise.all(
				productList.value.map((product) => productAPI.getDetail(product.id)),
			);

			for (const response of productResponses) {
				productSizes.value.set(response.data.ShoeId, response.data.Size);
			}
		} catch (error) {}
	};

	const addCartProductsToOrder = async () => {
		try {
			const uid = store.state.user.uid;
			const orderList = productList.value.map((item) => ({
				CartDetailId: item.cartId,
				ShoeId: item.id,
				ShoeName: item.name,
				Price: item.price,
				Amount: item.quantity,
				Size: item.size,
				Discount: item.discount,
			}));
			const response = await cartAPI.addProductToOrder(uid, orderList);

			const updateProducts = productList.value.map((product) => {
				const productSize = JSON.parse(productSizes.value.get(product.id));
				const selectedSize = productSize.find((size) => size.SizeName === product.size);
				selectedSize.Amount -= product.quantity;
				selectedSize.SoldNumber += product.quantity;
				return {
					ShoeId: product.id,
					Size: JSON.stringify(productSize),
				};
			});

			const productUpdateResponse = Promise.all(
				updateProducts.map((item) => productAPI.updateProduct(item.ShoeId, item)),
			).then(console.log);

			if (response.status > 199 && response.status < 300) {
				store.dispatch("cart/reset");
				return true;
			} else {
				throw new Error();
			}
		} catch (error) {
			return false;
		}
	};

	const createBillHandler = async (form, isOnlinePayment) => {
		const uid = store.state.user.uid;

		const sendForm = {
			CustomerId: uid,
			FullName: form.fullName,
			Email: form.email,
			Address: form.address,
			PhoneNumber: form.phone,
			TotalPrice: totalPrice.value,
			PaymentMethod: EPaymentMethod.OFFLINE,
			Country: form.country,
			City: form.city,
			Note: form.note,
		};

		if (isOnlinePayment) {
			sendForm.IsPayment = 1;
			sendForm.PaymentMethod = EPaymentMethod.ONLINE;
			sendForm.PaymentId = paymentId.value;
		}

		try {
			const response = await cartAPI.createBill(sendForm, uid);
			if (response.status > 199 && response.status < 300) {
				return await addCartProductsToOrder();
			} else {
				throw new Error();
			}
		} catch (error) {
			return false;
		}
	};

	const orderHandler = async (paymentMethod) => {
		const form = await deliveryAddress.value.submit();
		const uid = store.state.user.uid;

		if (!form) {
			return;
		}

		const sendForm = {
			CustomerId: uid,
			FullName: form.fullName,
			Email: form.email,
			Address: form.address,
			PhoneNumber: form.phone,
			TotalPrice: totalPrice.value,
			PaymentMethod: EPaymentMethod.OFFLINE,
			Country: form.country,
			City: form.city,
			Note: form.note,
		};

		if (paymentMethod === EPaymentMethod.OFFLINE) {
			paymentForm = null;
			const isSuccess = await createBillHandler(form);

			if (isSuccess) {
				notification.success({
					message:
						"Đơn hàng đã đang được xử lý, bạn có thể kiểm tra trong quản lý đơn hàng",
				});
				router.replace("/");
			} else {
				notification.error({
					message: "Có lỗi xảy ra, vui lòng thử lại",
				});
			}
			return;
		}
		isShowCheckout.value = true;
		paymentForm = form;
		// When set isShowCheckout to true, but at that time, Checkout modal is not render yet
		// Need to defer checkout modal show to next frame, when checkout modal is rendered
		await nextTick();
		rfCheckoutModal.value.open(form);
	};

	const successPayment = async (payId) => {
		paymentId.value = payId;

		notification.success({ message: `Thanh toán thành công, mã giao dịch ${payId}` });
		rfCheckoutModal.value.close();

		const isSuccess = await createBillHandler(paymentForm, true);
		if (isSuccess) {
			notification.success({
				message: "Đơn hàng đã đang được xử lý, bạn có thể kiểm tra trong quản lý đơn hàng",
			});
			paymentForm = null;
			router.replace("/");
		} else {
			notification.error({
				message:
					"Có lỗi xảy ra, vui lòng gửi phản hồi với chúng tôi tại mục Liên hệ và đính kèm mã giao dịch của bạn vào mục nội dung",
			});
		}

		// store.dispatch()
	};

	watch(
		productList,
		() => {
			console.log("Change: ");
			productSizes.value.clear();
			getDetailCartProducts();
		},
		{ immediate: true },
	);

	onBeforeUnmount(() => {
		if (route.query.buyNow) {
			store.dispatch("cart/removeBuyNowProduct");
		}
	});
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
