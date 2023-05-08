<template>
	<div class="the-sidebar">
		<a-menu ref="menu" mode="inline">
			<!-- :default-open-keys="defaultOpenKeys"
			:default-selected-keys="defaultSelectedKeys" -->
			<template v-for="menu in menuList">
				<a-menu-item v-if="!menu.child" :key="menu.link">
					<router-link :to="menu.link" style="color: inherit">
						{{ menu.title }}
					</router-link>
				</a-menu-item>
				<a-sub-menu v-else :key="menu.link">
					<template #title>
						<span>
							<i :class="menu.icon" class="w-8 text-xl" />
							<span>{{ menu.title }} </span>
						</span>
					</template>
					<a-menu-item v-for="submenu in menu.child" :key="submenu.link">
						<router-link v-if="!submenu.handler" :to="submenu.link">
							{{ submenu.title }}
						</router-link>
						<p v-else @click="submenu.handler">
							{{ submenu.title }}
						</p>
					</a-menu-item>
				</a-sub-menu>
			</template>
		</a-menu>
	</div>
</template>

<script>
	import { ERole } from "@/constants/config";
	import { useRoute } from "vue-router";
	import { useStore } from "vuex";

	export default {
		name: "SideBar",

		setup() {
			const route = useRoute();
			const store = useStore();

			const menuList = [
				{
					title: "Dashboard",
					link: "/",
				},
			];

			if (store.state.user.role === ERole.SUPER_ADMIN) {
				menuList.push({
					title: "Khách hàng",
					link: "/khach-hang",
				});
			}

			menuList.push(
				{
					title: "Sản phẩm",
					link: "san-pham",
					child: [
						{
							title: "Danh sách SP",
							link: "/san-pham",
						},
						{
							title: "Tạo SP",
							link: "/san-pham/tao-moi",
						},
					],
				},
				{
					title: "Đánh giá",
					link: "/danh-gia",
				},
				{
					title: "Đơn hàng",
					link: "/don-hang",
				},
				{
					title: "Danh mục sản phẩm",
					link: "/danh-muc",
				},
				{
					title: "Góp ý",
					link: "/gop-y",
				},
			);

			const defaultOpenKeys = () => {
				const keys = [];
				menuList.forEach((menuItem) => {
					if (route.path.includes(menuItem.link)) {
						keys.push(menuItem.link);
					}
				});

				return keys;
			};

			return {
				menuList,
				defaultOpenKeys: defaultOpenKeys(),
				defaultSelectedKeys: [route.path],
			};
		},
	};
</script>

<style lang="scss">
	.the-sidebar {
		@apply h-full overflow-y-auto overflow-x-hidden bg-blue-100;

		.ant-menu {
			@apply bg-transparent #{!important};

			.ant-menu-item-selected {
				@apply bg-transparent font-bold text-black #{!important};
				> a {
					color: inherit;

					&:hover {
						color: white;
					}
				}
			}
		}

		.ant-menu-item,
		.ant-menu-submenu-title {
			@apply text-black font-semibold;

			&:hover,
			&:focus {
				@apply bg-blue-200 #{!important};
			}

			&::after {
				@apply border-r-blue-400 #{!important};
			}
		}
	}
</style>
