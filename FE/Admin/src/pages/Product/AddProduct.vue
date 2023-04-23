<template>
	<div>
		<div>
			<div class="flex justify-between items-center">
				<h1 class="text-xl font-bold">
					<button class="cursor-pointer mr-4 bg-transparent border-0" @click="goBack">
						<i class="fas fa-arrow-left" />
					</button>
					<span>Thêm mới sản phẩm</span>
				</h1>
			</div>

			<ProductForm :loading="isSubmitting" @submit="submitFormHandler" @cancel="goBack" />
		</div>
	</div>
</template>

<script setup>
	import { useRouter } from "vue-router";
	import ProductForm from "@/components/template/ProductPage/ProductForm.vue";
	import { useStore } from "vuex";
	import { productFactory } from "@/utils/products";
	import { ref } from "vue";
	import { product } from "@/api";
	import { notification } from "ant-design-vue";

	const router = useRouter();
	const store = useStore();
	const username = store.state.user.username;

	const isSubmitting = ref(false);

	const goBack = () => {
		router.back();
	};

	const submitFormHandler = async (data) => {
		const sendForm = productFactory.getProductUpdateData({
			form: data.form,
			files: data.files,
			updatedBy: username,
			createdBy: username,
		});

		try {
			isSubmitting.value = true;
			const response = await product.createProduct(sendForm);
			if (response.status > 199 && response.status < 300) {
				notification.success({ message: "Tạo sản phẩm thành công" });
			} else {
				throw new Error();
			}
		} catch (error) {
			notification.error({ message: "Có lỗi xảy ra, vui lòng thử lại" });
		} finally {
			isSubmitting.value = false;
		}
	};
</script>
