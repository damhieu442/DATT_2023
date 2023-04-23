<template>
	<a-modal
		v-model:visible="visible"
		title=""
		class="delete-product-dialog"
		ok-text="Xóa"
		cancel-text="Quay lại"
		@ok="handleOk"
		@cancel="close"
	>
		<div class="mt-2 flex justify-between items-center">
			<a-divider />
			<p class="m-0 mx-4 whitespace-nowrap text-2xl text-black">Xoá sản phẩm?</p>
			<a-divider />
		</div>
		<div class="text-center mt-5">
			<p>
				Bạn có chắc chắc muốn xoá giày
				<span class="text-sky-500">{{ product?.name }}</span> ?
			</p>
			<p>Lưu ý: Bạn không thể hoàn tác hành động này</p>
		</div>
	</a-modal>
</template>

<script setup>
	import { ref } from "vue";

	const emits = defineEmits(["delete"]);
	const visible = ref(false);
	const product = ref(null);

	const open = (prod) => {
		visible.value = true;
		product.value = prod;
	};

	const close = () => {
		visible.value = false;
		product.value = null;
	};

	const handleOk = () => {
		visible.value = false;
		emits("delete", product.value);
	};

	defineExpose({
		open,
		close,
	});
</script>

<style lang="scss">
	.delete-product-dialog {
		.ant-divider {
			@apply my-5 bg-gray-700 min-w-0 w-1/3 #{!important};
		}
	}
</style>
