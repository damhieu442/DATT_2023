<template>
	<a-modal
		v-model:visible="visible"
		:title="null"
		:width="800"
		class="edit-feedback-dialog"
		ok-text="Xác nhận"
		cancel-text="Hủy"
		@ok="submitEditHandler"
		@cancel="close"
	>
		<div class="mt-2 flex justify-between items-center">
			<a-divider />
			<p class="m-0 whitespace-nowrap text-2xl text-black">Cập nhật góp ý</p>
			<a-divider />
		</div>
		<div class="mt-4">
			<a-form
				class="edit-feedback-form"
				ref="rfForm"
				layout="vertical"
				:model="form"
				:rules="rules"
			>
				<a-row :gutter="16">
					<a-col :span="12">
						<a-form-item name="createAt" label="Ngày tạo">
							<a-input :value="form.createAt" placeholder="" disabled />
						</a-form-item>
					</a-col>
					<a-col :span="12">
						<a-form-item name="updateAt" label="Ngày sửa">
							<a-input :value="form.updateAt" placeholder="" disabled />
						</a-form-item>
					</a-col>
				</a-row>
				<a-row :gutter="16">
					<a-col :span="12">
						<a-form-item name="username" label="Người tạo">
							<a-input :value="form.username" placeholder="" disabled />
						</a-form-item>
					</a-col>
					<a-col :span="12">
						<a-form-item name="email" label="Email">
							<a-input v-model:value="form.email" placeholder="" />
						</a-form-item>
					</a-col>
				</a-row>
				<a-form-item name="phone" label="Điện thoại">
					<a-input :value="form.phone" placeholder="" disabled />
				</a-form-item>
				<a-form-item name="address" label="Địa chỉ">
					<a-input :value="form.address" placeholder="" disabled />
				</a-form-item>
				<a-form-item name="content" label="Nội dung">
					<a-textarea
						:value="form.content"
						:auto-size="{ minRows: 3, maxRows: 5 }"
						placeholder=""
						disabled
					/>
				</a-form-item>
				<a-form-item name="status" label="Trạng thái">
					<a-select v-model:value="form.status" placeholder="Chọn danh mục cha">
						<a-select-option
							v-for="(status, statusCode) in EFeedbackStatus"
							:key="statusCode"
							:value="statusCode"
							>{{ status }}</a-select-option
						>
					</a-select>
				</a-form-item>
				<a-form-item name="note" label="Ghi chú">
					<a-textarea
						v-model:value="form.note"
						:auto-size="{ minRows: 3, maxRows: 5 }"
						placeholder=""
					/>
				</a-form-item>
			</a-form>
		</div>
	</a-modal>
</template>

<script setup>
	import { reactive, ref } from "vue";
	import _cloneDeep from "lodash/cloneDeep";
	import { EFeedbackStatus } from "@/constants/feedback";

	const emits = defineEmits(["ok"]);

	const defaultForm = {
		id: "",
		username: "",
		email: "",
		address: "",
		content: "",
		phone: "",
		note: "",
		status: "",
		createAt: "",
		updateAt: "",
	};

	const visible = ref(false);
	const rfForm = ref(null);
	const form = reactive(_cloneDeep(defaultForm));
	const rules = {};

	const submitEditHandler = async () => {
		try {
			await rfForm.value.validate();
			emits("ok", { ...form });
		} catch (error) {
			console.log("Error: ", error);
		}
	};

	const open = (feedback) => {
		Object.assign(form, feedback);
		visible.value = true;
	};

	const close = () => {
		visible.value = false;
	};

	defineExpose({
		open,
		close,
	});
</script>

<style lang="scss">
	.edit-feedback-dialog {
		top: 5rem;
		.ant-divider {
			@apply my-5 bg-gray-700 min-w-0 w-1/3 #{!important};
		}

		.edit-feedback-form {
			.ant-input {
				color: #000;
			}
		}
	}
</style>
