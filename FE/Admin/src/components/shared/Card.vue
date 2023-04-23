<template>
	<a-card class="card-global">
		<slot />
	</a-card>
</template>

<script>
	export default {
		name: "TheCard",
	};
</script>

<style lang="scss">
	div.card-global {
		@apply py-6 px-10 bg-cyan-200 rounded-lg;

		.ant-card-body {
			@apply p-0;
		}
	}
</style>
