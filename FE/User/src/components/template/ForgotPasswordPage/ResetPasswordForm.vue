<template>
	<div>
		<p>Nhập mật khẩu mới</p>
		<a-form
			:model="form"
			layout="vertical"
			class="forgot-pass-form"
			@finish="submitNewPassword"
		>
			<a-form-item name="email" label="Địa chỉ email">
				<a-input v-model:value="formState.email" class="email-input" disabled />
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

			<a-form-item>
				<a-button htmlType="submit" type="primary" :loading="isLoading"
					>Đặt lại mật khẩu</a-button
				>
			</a-form-item>
		</a-form>
	</div>
</template>

<script setup>
	import { auth } from "@/api";
	import { Form, notification } from "ant-design-vue";
	import { reactive, ref } from "vue";
	import { useRouter } from "vue-router";

	const props = defineProps({
		email: { type: String },
	});

	const router = useRouter();

	const isLoading = ref(false);

	const formState = reactive({
		email: props.email,
		password: "",
		confirmPassword: "",
	});
	const formRule = reactive({
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
			const email = props.email;
			const sendData = { Email: email, Password: newPassword };

			const isSuccess = await auth.updatePassword(sendData);
			if (isSuccess) {
				notification.success({ message: "Cập nhật mật khẩu thành công" });
				router.push("/");
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

	.email-input {
		color: #333;

		&:disabled {
			color: #333;
		}
	}
</style>
