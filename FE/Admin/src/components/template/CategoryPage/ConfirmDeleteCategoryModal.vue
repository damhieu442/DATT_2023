<template>
	<a-modal
		v-model:visible="visible"
		title=""
		class="delete-dialog"
		ok-text="Xóa"
		cancel-text="Quay lại"
		@ok="handleOk"
		@cancel="close"
	>
		<div class="mt-2 flex justify-between items-center">
			<a-divider />
			<p class="m-0 mx-4 whitespace-nowrap text-2xl text-black">Xoá danh mục sản phẩm?</p>
			<a-divider />
		</div>
		<div class="text-center mt-5">
			<p v-if="isSingleCategory">Bạn có chắc chắc muốn xoá danh mục {{ category?.name }}?</p>
			<p v-else>Bạn có chắc chắc muốn xoá {{ categoriesNumber }} mục?</p>
			<p>Lưu ý: Bạn không thể hoàn tác hành động này</p>
		</div>
	</a-modal>
</template>

<script setup>
	import { computed, ref } from "vue";

	const emits = defineEmits(["delete"]);
	const visible = ref(false);
	const category = ref(null);

	const categoriesNumber = computed(() => {
		return category.value?.length || "";
	});

	const isSingleCategory = computed(() => {
		return !Array.isArray(category.value);
	});

	const open = (cate) => {
		visible.value = true;
		category.value = cate;
	};

	const close = () => {
		visible.value = false;
		category.value = null;
	};

	const handleOk = () => {
		visible.value = false;
		emits("delete", category.value);
	};

	defineExpose({
		open,
		close,
	});
</script>

<style lang="scss">
	.delete-dialog {
		.ant-divider {
			@apply my-5 bg-gray-700 min-w-0 w-1/3 #{!important};
		}
	}
</style>
