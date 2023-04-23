<template>
	<div>
		<a-spin :spinning="isGettingData">
			<div v-if="!notFound">
				<div class="flex justify-between items-center">
					<h1 class="text-xl font-bold">
						<button class="cursor-pointer mr-4 bg-transparent border-0" @click="goBack">
							<i class="fas fa-arrow-left" />
						</button>
						<span>{{ product?.name || "" }}</span>
					</h1>
					<router-link
						:to="'/san-pham/' + productId + '/chinh-sua'"
						class="py-2 px-10 rounded-md bg-blue-500 hover:bg-blue-400 text-white hover:text-white"
					>
						Chỉnh sửa
					</router-link>
				</div>

				<ProductForm
					disabled
					:product="product"
					@submit="submitFormHandler"
					@cancel="goBack"
				/>
			</div>
			<div v-else>
				<h1 class="text-3xl text-center mt-52">
					Không tìm thấy thông tin sản phẩm trong CSDL
				</h1>
				<router-link
					class="py-2 px-10 rounded-md bg-blue-500 hover:bg-blue-400 text-white hover:text-white block mx-auto my-5 w-fit"
					to="/san-pham"
				>
					Quay lại trang danh sách sản phẩm
				</router-link>
			</div>
		</a-spin>
	</div>
</template>

<script setup>
	import { onMounted, ref } from "vue";
	import { useRoute, useRouter } from "vue-router";
	import { product as productAPI } from "@/api";
	import { notification } from "ant-design-vue";
	import ProductForm from "@/components/template/ProductPage/ProductForm.vue";
	import { productFactory } from "@/utils/products";

	const notFound = ref(false);
	const product = ref(null);
	const isGettingData = ref(false);

	const route = useRoute();
	const router = useRouter();

	const productId = route.params.id;

	const goBack = () => {
		router.back();
	};

	const getProductData = async () => {
		try {
			isGettingData.value = true;

			const response = await productAPI.getDetail(productId);

			console.log("response: ", response);
			if (response.status > 199 && response.status < 300) {
				const data = response.data;
				product.value = productFactory.transformProductDetailResponse(data);
			} else {
				throw new Error();
			}
		} catch (error) {
			notFound.value = true;
			notification.error({ message: "Có lỗi xảy ra, vui lòng thử lại" });
		} finally {
			isGettingData.value = false;
		}
	};

	const submitFormHandler = (data) => {
		console.log("Data: ", data);
	};

	onMounted(() => getProductData());
</script>
