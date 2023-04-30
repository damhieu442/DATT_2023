<template>
	<div class="authentication">
		<a-button type="text" @click="showAuthenticationModal" class="authentication__btn"
			>ĐĂNG NHẬP / ĐĂNG KÝ</a-button
		>
		<a-modal
			v-model:visible="isShowModal"
			width="875px"
			:footer="null"
			class="authentication__modal"
			destroyOnClose
		>
			<a-row justify="space-around">
				<a-col :span="11">
					<SignInForm
						:loading="isLoggingIn"
						:disabled="isSigningUp"
						@submit="signInHandler"
						@exit="closeAuthenticationModal"
					/>
				</a-col>
				<a-divider type="vertical" class="authentication__divider" />
				<a-col :span="11">
					<SignUpForm
						@submit="signUpHandler"
						:loading="isSigningUp"
						:disabled="isLoggingIn"
					/>
				</a-col>
			</a-row>
		</a-modal>
	</div>
</template>

<script setup>
	import { notification } from "ant-design-vue";
	import { ref } from "vue";
	import { useStore } from "vuex";
	import SignInForm from "./SignInForm.vue";
	import SignUpForm from "./SignUpForm.vue";

	const store = useStore();

	const isShowModal = ref(false);
	const isLoggingIn = ref(false);
	const isSigningUp = ref(false);

	const showAuthenticationModal = () => {
		isShowModal.value = true;
	};

	const closeAuthenticationModal = () => {
		isShowModal.value = false;
	};

	const signInHandler = async (form) => {
		isLoggingIn.value = true;
		const isLoggedIn = await store.dispatch("user/signIn", form);
		if (isLoggedIn) {
			isShowModal.value = false;
		} else {
			notification.error({
				message: "Có lỗi xảy ra, đăng nhập không thành công, vui lòng thử lại",
			});
		}
		isLoggingIn.value = false;
	};

	const signUpHandler = async (form) => {
		isSigningUp.value = true;
		const isSignedUp = await store.dispatch("user/signUp", form);
		if (isSignedUp) {
			isShowModal.value = false;
			notification.success({ message: "Đăng ký tài khoản thành công" });
		} else {
			notification.error({
				message: "Có lỗi xảy ra, đăng ký không thành công, vui lòng thử lại",
			});
		}
		isSigningUp.value = false;
	};
</script>

<style lang="scss">
	.authentication {
		& > &__btn {
			color: #fffc;
			font-weight: 700;
			font-family: "Roboto", sans-serif;
			font-size: 0.8rem;

			> span {
				letter-spacing: 0.02em;
				text-transform: uppercase;
			}

			&:hover {
				color: #fffc;
			}

			&:focus {
				color: #fffc;
			}
		}

		&__modal {
			font-family: "Roboto", sans-serif;

			.ant-modal-close {
				display: none;
			}

			.ant-modal-body {
				padding: 30px 20px;
			}

			& .authentication__divider {
				height: auto;
			}
		}
	}
</style>
