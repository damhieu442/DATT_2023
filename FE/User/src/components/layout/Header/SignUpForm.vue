<template>
	<a-form
		:model="form"
		name="sign-up-form"
		autocomplete="off"
		@finish="onSignin"
		layout="vertical"
		class="sign-up-form"
	>
		<h5>ĐĂNG KÝ</h5>
		<a-form-item
			label="Địa chỉ email"
			name="email"
			:rules="[{ required: true, message: 'Please input your username!' }]"
		>
			<a-input v-model:value="form.email" />
		</a-form-item>

		<a-form-item
			label="Mật khẩu"
			name="password"
			:rules="[{ required: true, message: 'Please input your password!' }]"
		>
			<a-input-password v-model:value="form.password" />
		</a-form-item>

		<a-row type="flex"
			><a-col :span="24">
				<a-button type="primary" html-type="submit" class="sign-up-form__submit"
					>Đăng ký</a-button
				>
			</a-col>
		</a-row>
	</a-form>
</template>

<script setup>
	import { reactive } from "vue";

	const form = reactive({
		email: "",
		password: "",
		isRemeberPassword: false,
	});

	const onSignin = () => {};
</script>

<style lang="scss" scoped>
	.sign-up-form {
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
	}
</style>
