<template>
	<div class="flex justify-between items-center">
		<router-link class="block w-14" to="/">
			<img src="@/assets/images/logo.png" alt="" class="w-full" />
		</router-link>
		<a-dropdown :trigger="['click']">
			<div class="flex items-center cursor-pointer">
				<div class="text-white">
					<p class="mb-2">Xin chào,</p>
					<p class="m-0">{{ username }}</p>
				</div>
				<img :src="avatar" alt="" class="w-10 h-10 rounded-full mx-2" />
				<i class="fa-solid fa-caret-down" />
			</div>
			<template #overlay>
				<a-menu @click="dropdownMenuClickHandler" class="header__dropdown-menu">
					<a-menu-item :key="HEADER_KEYS.DETAIL">
						<i class="mr-4 fa-solid fa-user" />
						<span>Thông tin cá nhân</span>
					</a-menu-item>
					<a-menu-item :key="HEADER_KEYS.CHANGE_PASSWORD">
						<i class="mr-4 fa-solid fa-lock" />
						<span>Đổi mật khẩu</span>
					</a-menu-item>
					<a-menu-item :key="HEADER_KEYS.LOGOUT">
						<i class="mr-4 fa-solid fa-arrow-right-from-bracket" />
						<span>Đăng xuất</span>
					</a-menu-item>
				</a-menu>
			</template>
		</a-dropdown>
	</div>
</template>

<script setup>
	import { HEADER_KEYS } from "@/constants/dropdown-keys";
	import { computed } from "vue";
	import { useRouter } from "vue-router";
	import { useStore } from "vuex";
	import defaultAvatar from "@/assets/images/default-avatar.png";

	const router = useRouter();
	const store = useStore();

	const username = computed(() => store.state.user.fullName || store.state.user.username);
	const avatar = computed(() => store.state.user.image || defaultAvatar);

	const changePassword = () => {
		router.push("/doi-mat-khau");
	};

	const getDetail = () => {
		router.push("/thong-tin-ca-nhan");
	};

	const logout = () => {
		store.dispatch("user/signOut");
		router.push("/auth");
	};

	const dropdownMenuClickHandler = ({ key }) => {
		switch (key) {
			case HEADER_KEYS.DETAIL:
				getDetail();
				break;
			case HEADER_KEYS.CHANGE_PASSWORD:
				changePassword();
				break;
			case HEADER_KEYS.LOGOUT:
				logout();
				break;
			default:
				break;
		}
	};
</script>
