<template>
	<div>
		<a-form
			:labelCol="{
				span: 5,
			}"
			:wrapperCol="{
				span: 16,
			}"
			labelAlign="left"
		>
			<a-form-item label="Mật khẩu cũ" v-bind="form.validateInfos.oldPassword">
				<a-input-password placeholder="Mật khẩu cũ" v-model:value="formState.oldPassword" />
			</a-form-item>
			<a-form-item label="Mật khẩu mới" v-bind="form.validateInfos.password">
				<a-input-password placeholder="Mật khẩu mới" v-model:value="formState.password" />
			</a-form-item>
			<a-form-item label="Nhập lại mật khẩu mới" v-bind="form.validateInfos.confirmPassword">
				<a-input-password
					placeholder="Nhập lại mật khẩu mới"
					v-model:value="formState.confirmPassword"
				/>
			</a-form-item>

			<a-button
				type="primary"
				:loading="isLoading"
				:disabled="isLoading"
				@click="submitNewPassword"
			>
				Lưu thay đổi
			</a-button>
		</a-form>
	</div>
</template>

<script setup>
	import { Form, notification } from "ant-design-vue";
	import { reactive, ref } from "vue";
	import { useStore } from "vuex";

	const store = useStore();
	const isLoading = ref(false);
	const formState = reactive({
		oldPassword: "",
		password: "",
		confirmPassword: "",
	});
	const formRule = reactive({
		oldPassword: [
			{
				required: true,
				message: "Vui lòng nhập mật khẩu cũ",
				trigger: "change",
			},
		],
		password: [
			{
				validator(_, password) {
					if (!password || password.trim() === "") {
						return Promise.reject(new Error("Mật khẩu không được để trống"));
					}

					return Promise.resolve();
				},
				trigger: "change",
			},
		],
		confirmPassword: [
			{
				validator(_, value) {
					if (!value || value.trim() === "") {
						return Promise.reject(new Error("Vui lòng nhập lại mật khẩu của bạn"));
					}

					if (formState.password !== value) {
						return Promise.reject(new Error("Nhập lại mật khẩu mới!"));
					}

					return Promise.resolve();
				},
				trigger: "change",
			},
		],
	});
	const form = Form.useForm(formState, formRule);

	const submitNewPassword = async () => {
		isLoading.value = true;
		try {
			await form.validate();

			const { password: newPassword } = formState;
			const customerId = store.state.user.uid;
			const sendData = { customerId, newPassword };
			console.log("Send data: ", sendData);
			const isSuccess = await store.dispatch("user/updateUserPassword", sendData);
			if (isSuccess) {
				notification.success({ message: "Cập nhật mật khẩu thành công" });
				form.resetFields();
			} else {
				throw new Error();
			}
		} catch (error) {
			console.log(error);

			if (!"errorFields" in error) {
				notification.error({ message: "Có lỗi xảy ra, thử lại sau" });
			}
		} finally {
			isLoading.value = false;
		}
	};
</script>
