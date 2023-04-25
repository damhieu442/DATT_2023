<template>
	<a-modal v-model:visible="isShow" :title="null" :footer="null" @cancel="closeModalHandler">
		<h3>Bạn có chắc chắn muốn hủy đơn hàng</h3>
		<a-form
			layout="vertical"
			ref="rfForm"
			:model="form"
			:rules="formRules"
			@finish="submitForm"
		>
			<a-form-item label="Lý do" name="reason">
				<a-input v-model:value="form.reason" placeholder="Lý do từ chối"
			/></a-form-item>
			<a-button type="primary" class="w-full submit-btn" html-type="submit">
				Xác nhận
			</a-button>
		</a-form>
	</a-modal>
</template>

<script setup>
	import { reactive, ref } from "vue";

	const emits = defineEmits(["submit"]);

	const isShow = ref(false);
	const rfForm = ref(null);

	const form = reactive({
		reason: "",
	});

	const formRules = reactive({
		reason: [
			{
				required: true,
				message: "Vui lòng nhập lý do từ chối",
				trigger: "blur",
			},
		],
	});

	const closeModalHandler = () => {
		isShow.value = false;
		form.reason = "";
		rfForm.clearValidate();
	};

	const submitForm = () => {
		emits("submit", { ...form });
		isShow.value = false;
		form.reason = "";
		rfForm.clearValidate();
	};

	const open = () => {
		isShow.value = true;
	};

	defineExpose({
		open,
	});
</script>
