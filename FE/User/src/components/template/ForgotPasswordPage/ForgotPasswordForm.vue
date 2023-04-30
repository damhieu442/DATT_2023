<template>
	<div>
		<p>
			Quên mật khẩu? Vui lòng nhập địa chỉ email. Bạn sẽ nhận được một liên kết tạo mật khẩu
			mới qua email.
		</p>
		<a-form
			:model="form"
			layout="vertical"
			class="forgot-pass-form"
			@finish="forgotPasswordHandler"
		>
			<a-form-item
				name="email"
				label="Địa chỉ email"
				:rules="[
					{
						required: true,
						type: 'email',
						message: 'Vui lòng nhập địa chỉ email hợp lệ',
					},
				]"
			>
				<a-input v-model:value="form.email" :disabled="isSentForgotPasswordForm" />
			</a-form-item>
			<a-form-item
				name="token"
				label="Mã cập nhật"
				:hidden="!isSentForgotPasswordForm"
				:rules="[
					{
						required: isSentForgotPasswordForm,
						message: 'Vui lòng nhập mã cập nhật được gửi trong email hợp lệ',
					},
				]"
			>
				<a-input v-model:value="form.token" />
			</a-form-item>

			<a-form-item>
				<a-button htmlType="submit" type="primary">Đặt lại mật khẩu</a-button>
			</a-form-item>
		</a-form>
	</div>
</template>

<script setup>
	import { auth } from "@/api";
	import { notification } from "ant-design-vue";
	import { reactive, ref } from "vue";

	const emits = defineEmits(["reset-password"]);

	const form = reactive({ email: "", token: "" });

	const isSentForgotPasswordForm = ref(false);

	const forgotPasswordHandler = () => {
		if (!isSentForgotPasswordForm.value) {
			getResetToken();
		} else {
			confirmResetTokenPassword();
		}
	};

	const getResetToken = async () => {
		try {
			const response = await auth.getResetPasswordToken({ email: form.email });

			if (response.status > 199 && response.status < 300) {
				const isSuccess = response.data;
				if (isSuccess) {
					notification.success({
						message:
							"Mã cập nhật đã được gửi đến địa chỉ email của bạn, vui lòng kiểm tra địa chỉ email",
					});

					isSentForgotPasswordForm.value = true;
				} else {
					// For case when system change response status
					notification.error({ message: "Email không tồn tại trong hệ thống" });
				}
			} else {
				throw new Error();
			}
		} catch (error) {
			// 500 Status code will be seen as rejected promise by axios
			if (error.name === "AxiosError" && !error.response.data) {
				notification.error({ message: "Email không tồn tại trong hệ thống" });

				return;
			}

			notification.error({ message: "Có lỗi xảy ra, vui lòng thử lại" });
		}
	};

	const confirmResetTokenPassword = async () => {
		try {
			const response = await auth.resetForgotPassword({ ...form });

			if (response.status > 199 && response.status < 300) {
				const isSuccess = response.data;
				if (isSuccess) {
					emits("reset-password", { email: form.email });
				} else {
					// For case when system change response status
					notification.error({ message: "Sai mã cập nhật, vui lòng thử lại" });
				}
			} else {
				throw new Error();
			}
		} catch (error) {
			if (error.name === "AxiosError" && !error.response.data) {
				notification.error({ message: "Sai mã cập nhật, vui lòng thử lại" });

				return;
			}

			notification.error({ message: "Có lỗi xảy ra, vui lòng thử lại" });
		}
	};
</script>

<style lang="scss" scoped>
	.forgot-pass-form {
		width: 50%;

		:deep(label.ant-form-item-required) {
			font-weight: 700;

			&::before {
				display: none !important;
			}

			&::after {
				content: "*" !important;
				display: inline-block;
				margin-left: 0.25rem;
				color: #333;
				font-size: 1rem;
				line-height: 1;
			}
		}
	}

	.ant-btn {
		background-color: #c30005;
		margin-bottom: 1em;
		color: #fff;
		border-color: rgba(0, 0, 0, 0.05);
		position: relative;
		display: inline-block;
		text-transform: uppercase;
		font-size: 0.97em;
		letter-spacing: 0.03em;
		touch-action: none;
		cursor: pointer;
		font-weight: bolder;
		text-align: center;
		text-decoration: none;
		border: 1px solid transparent;
		vertical-align: middle;
		border-radius: 0;
		margin-top: 0;
		margin-right: 1em;
		text-shadow: none;
		line-height: 2.4em;
		min-height: 2.5em;
		padding: 0 1.2em;
		max-width: 100%;
		transition: transform 0.3s, border 0.3s, background 0.3s, box-shadow 0.3s, opacity 0.3s,
			color 0.3s;
		text-rendering: optimizeLegibility;
		box-sizing: border-box;
		text-transform: uppercase;

		> :deep(span) {
			text-transform: inherit;
		}

		&:hover {
			outline: none;
			opacity: 1;
			color: #fff;
			background-color: #c30005;
			box-shadow: inset 0 0 0 100px rgba(0, 0, 0, 0.2);
		}
	}
</style>
