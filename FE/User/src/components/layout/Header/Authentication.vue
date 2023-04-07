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
		>
			<a-row justify="space-around">
				<a-col :span="11"> <SignInForm @submit="signInHandler" /> </a-col>
				<a-divider type="vertical" class="authentication__divider" />
				<a-col :span="11"> <SignUpForm @submit="signUpHandler" /> </a-col>
			</a-row>
		</a-modal>
	</div>
</template>

<script setup>
	import { ref } from "vue";
	import SignInForm from "./SignInForm.vue";
	import SignUpForm from "./SignUpForm.vue";

	const isShowModal = ref(false);

	const showAuthenticationModal = () => {
		isShowModal.value = true;
	};

	const signInHandler = (form) => {
		console.log("Sign in: ", form);
	};

	const signUpHandler = (form) => {
		console.log("Sign up: ", form);
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
