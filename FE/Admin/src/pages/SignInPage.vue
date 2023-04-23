<template>
	<div class="login">
		<h4 class="text-xl text-center text-blue-400">Mona</h4>
		<div class="my-2 flex justify-between items-center">
			<a-divider />
			<p class="whitespace-nowrap text-xl m-0">Đăng nhập</p>
			<a-divider />
		</div>
		<a-form class="login-form">
			<a-form-item v-bind="form.validateInfos.username">
				<a-input
					v-model:value="formState.username"
					name="username"
					placeholder="Nhập tài khoản của bạn"
					allow-clear
					@pressEnter="submitForm"
				>
					<template #prefix>
						<i class="mr-4 fa-solid fa-user"></i>
					</template>
				</a-input>
			</a-form-item>
			<a-form-item v-bind="form.validateInfos.password">
				<a-input-password
					v-model:value="formState.password"
					placeholder="Nhập mật khẩu"
					name="password"
					allow-clear
					@pressEnter="submitForm"
				>
					<template #prefix>
						<i class="mr-4 fa-solid fa-lock" />
					</template>
				</a-input-password>
			</a-form-item>

			<div class="flex justify-between items-center w-full my-5">
				<a-form-item name="isRemeberPassword" class="mb-0">
					<a-checkbox v-model:checked="formState.isRemeberPassword"
						>Ghi nhớ mật khẩu</a-checkbox
					>
				</a-form-item>
				<router-link to="/quen-mat-khau"> Quên mật khẩu? </router-link>
			</div>

			<a-button
				type="primary"
				class="login-btn login-btn--info"
				:loading="isSubmitting"
				:disabled="isSubmitting"
				@click="submitForm"
			>
				Đăng nhập
			</a-button>
		</a-form>
	</div>
</template>

<script>
	import { Form, notification } from "ant-design-vue";
	import { reactive, ref } from "vue";
	import { useRouter } from "vue-router";
	import { useStore } from "vuex";

	export default {
		name: "LoginPage",

		setup() {
			const store = useStore();
			const router = useRouter();

			const isSubmitting = ref(false);
			const formState = reactive({ username: "", password: "", isRemeberPassword: false });
			const formRules = reactive({
				username: [
					{
						required: true,
						message: "Vui lòng nhập tài khoản của bạn",
						trigger: "change",
					},
				],
				password: [
					{ required: true, message: "Vui lòng nhập mật khẩu", trigger: "change" },
				],
				isRemeberPassword: [],
			});

			const form = Form.useForm(formState, formRules);

			const submitForm = async () => {
				try {
					isSubmitting.value = true;

					await form.validate();

					const isLoggedIn = await store.dispatch("user/signIn", { ...formState });
					if (isLoggedIn === true) {
						router.replace("/");
					} else {
						console.log("is log", isLoggedIn);
						if (typeof isLoggedIn === "boolean") {
							notification.error({
								message:
									"Có lỗi xảy ra, đăng nhập không thành công, vui lòng thử lại",
							});
						} else {
							const { message } = isLoggedIn;
							if (message === "Username or password is incorrect") {
								notification.error({
									message: "Sai tài khoản hoặc mật khẩu",
								});

								return;
							}
							notification.error({
								message:
									"Có lỗi xảy ra, đăng nhập không thành công, vui lòng thử lại",
							});
						}
					}
				} catch (error) {
					if ("errorFields" in error) {
						return;
					}
				} finally {
					isSubmitting.value = false;
				}
			};

			return { form, formState, isSubmitting, submitForm };
		},
	};
</script>

<style lang="scss">
	.login {
		.ant-divider {
			@apply my-5 bg-blue-600 min-w-0 w-1/3 #{!important};
		}

		&-btn {
			@apply w-full #{!important};

			&--info {
				@apply bg-sky-400 #{ !important};
			}
		}
	}
</style>
