<template>
	<div class="bg-blue-50 min-h-screen flex justify-center items-center">
		<div class="container overflow-hidden w-1/2 bg-white rounded-lg p-10 my-4 card">
			<div class="w-full h-full">
				<router-view />
			</div>
		</div>
	</div>
</template>

<script>
	export default {
		name: "AuthLayout",
	};
</script>

<style lang="scss" scoped>
	.card {
		min-width: 512px;
	}
</style>
