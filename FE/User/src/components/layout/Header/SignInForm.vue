<template>
	<a-form
		:model="form"
		name="sign-in-form"
		autocomplete="off"
		@finish="onSignin"
		layout="vertical"
		class="sign-in-form"
	>
		<h5>ĐĂNG NHẬP</h5>
		<a-form-item
			label="Tên tài khoản hoặc email"
			name="email"
			:rules="[{ required: true, message: 'Vui lòng nhập tài khoản của bạn!' }]"
		>
			<a-input :disabled="disabled || loading" v-model:value="form.email" />
		</a-form-item>

		<a-form-item
			label="Mật khẩu"
			name="password"
			:rules="[{ required: true, message: 'Vui lòng nhập mật khẩu!' }]"
		>
			<a-input-password :disabled="disabled || loading" v-model:value="form.password" />
		</a-form-item>

		<a-row type="flex"
			><a-col :span="10">
				<a-button
					type="primary"
					html-type="submit"
					class="sign-in-form__submit"
					:loading="loading"
					:disabled="disabled"
					>Đăng nhập</a-button
				>
			</a-col>
			<a-col :span="14">
				<a-form-item name="isRemeberPassword">
					<a-checkbox
						v-model:checked="form.isRemeberPassword"
						class="sign-in-form__remember"
						:disabled="disabled || loading"
						>Ghi nhớ mật khẩu</a-checkbox
					>
				</a-form-item>
			</a-col>
		</a-row>
		<router-link
			to="/quen-mat-khau"
			class="sign-in-form__forgot-pass"
			@click="forgotPasswordHandler"
			>Quên mật khẩu?</router-link
		>
	</a-form>
</template>

<script setup>
	import { reactive } from "vue";

	const props = defineProps({ loading: Boolean, disabled: Boolean });

	const form = reactive({
		email: "",
		password: "",
		isRemeberPassword: false,
	});

	const emit = defineEmits(["submit", "exit"]);

	const onSignin = (form) => {
		emit("submit", { ...form });
	};

	const forgotPasswordHandler = () => {
		emit("exit");
	};
</script>

<style lang="scss" scoped>
	.sign-in-form {
		color: #1c1c1c;
		> h5 {
			color: #1c1c1c;
			font-weight: 700;
			font-size: 1.25rem;
			line-height: 1.2;
			text-transform: uppercase;
			letter-spacing: 0.05em;
			margin-bottom: 0.5em;
		}

		:deep(.ant-form-item-label) {
			padding-bottom: 0;
			> label {
				font-weight: 700;
				color: #222;
				font-size: 0.9rem;
				margin-bottom: 0.4rem;

				&::before {
					display: none !important;
				}

				&::after {
					display: inline-block;
					margin-left: 4px;
					color: inherit;
					font-size: 14px;
					font-family: SimSun, sans-serif;
					line-height: 1;
					content: "*";
				}
			}
		}

		:deep(.ant-input) {
			font-size: 1rem;
		}

		&__submit {
			background-color: #c30005;
			border-color: #c30005;
			color: white;
			font-size: 1rem;
			font-weight: 700;
			min-height: 2.5em;
			padding: 0 1.2em;
			transition: all 0.3s;

			> :deep(span) {
				text-transform: uppercase;
			}

			&:hover {
				background-color: transparent;
				border-color: #c30005;
				color: #c30005;
			}
		}

		&__remember {
			> :deep(span) {
				font-weight: 700;
			}
		}

		&__forgot-pass {
			margin-bottom: 0.5rem;
			color: #334862;
			font-size: 1rem;
		}
	}
</style>
