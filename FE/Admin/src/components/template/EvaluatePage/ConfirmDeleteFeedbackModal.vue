<template>
	<a-modal
		v-model:visible="visible"
		title=""
		class="delete-feedback-dialog"
		ok-text="Xóa"
		cancel-text="Quay lại"
		@ok="handleOk"
		@cancel="close"
	>
		<div class="mt-2 flex justify-between items-center">
			<a-divider />
			<p class="m-0 mx-4 whitespace-nowrap text-2xl text-black">Xoá góp ý?</p>
			<a-divider />
		</div>
		<div class="text-center mt-5">
			<p>Bạn có chắc chắc muốn xoá góp ý của {{ feedback.username }}?</p>
			<p>Lưu ý: Bạn không thể hoàn tác hành động này</p>
		</div>
	</a-modal>
</template>

<script setup>
	import { ref } from "vue";

	const emits = defineEmits(["delete"]);
	const visible = ref(false);
	const feedback = ref(null);

	const open = (feedb) => {
		visible.value = true;
		feedback.value = feedb;
	};

	const close = () => {
		visible.value = false;
		feedback.value = null;
	};

	const handleOk = () => {
		visible.value = false;
		emits("delete", feedback.value);
	};

	defineExpose({
		open,
		close,
	});
</script>

<style lang="scss">
	.delete-feedback-dialog {
		.ant-divider {
			@apply my-5 bg-gray-700 min-w-0 w-1/3 #{!important};
		}
	}
</style>
