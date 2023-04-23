<template>
	<div class="authenticated-menu">
		<a-dropdown :trigger="['click']">
			<a-button type="text" class="authenticated-menu__btn">
				Xin chào, {{ userAuthInfo.fullName }}</a-button
			>
			<template #overlay>
				<a-menu @click="menuClickHandler">
					<a-menu-item :key="EMenuCommandKeys.ACCOUNT"> Tài khoản </a-menu-item>
					<a-menu-item :key="EMenuCommandKeys.BILL"> Đơn hàng </a-menu-item>
					<a-menu-item :key="EMenuCommandKeys.LOGOUT">Đăng xuất</a-menu-item>
				</a-menu>
			</template>
		</a-dropdown>
	</div>
</template>

<script setup>
	import { useRouter } from "vue-router";
	import { useStore } from "vuex";

	const props = defineProps({ userAuthInfo: { type: Object } });

	const EMenuCommandKeys = {
		ACCOUNT: "ACCOUNT",
		BILL: "BILL",
		LOGOUT: "LOGOUT",
	};

	const store = useStore();
	const router = useRouter();

	const menuClickHandler = (event) => {
		switch (event.key) {
			case EMenuCommandKeys.ACCOUNT:
				router.push("/tai-khoan");

				break;
			case EMenuCommandKeys.BILL:
				router.push("/tai-khoan/don-hang");

				break;
			case EMenuCommandKeys.LOGOUT:
				store.dispatch("user/signOut");

				break;

			default:
				break;
		}
	};
</script>

<style lang="scss">
	.authenticated-menu {
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
	}
</style>
