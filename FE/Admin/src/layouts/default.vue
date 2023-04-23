<template>
	<a-layout class="h-screen">
		<a-layout-header class="header">
			<Header />
		</a-layout-header>
		<a-layout class="content">
			<a-layout-sider width="260">
				<Sidebar />
			</a-layout-sider>
			<a-layout-content class="h-full overflow-auto">
				<div class="z-0 flex-grow p-6 pb-5">
					<router-view />
				</div>
			</a-layout-content>
		</a-layout>
	</a-layout>
</template>

<script>
	import Sidebar from "@/components/layout/Sidebar.vue";
	import Header from "@/components/layout/Header.vue";

	export default {
		name: "DefaultLayout",
		components: { Sidebar, Header },
	};
</script>

<style lang="scss" scoped>
	.header {
		@apply bg-blue-400 px-6 py-4 leading-none h-auto;
	}

	.content {
		@apply bg-white;
	}
</style>
