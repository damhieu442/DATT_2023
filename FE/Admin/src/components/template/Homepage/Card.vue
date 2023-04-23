<template>
	<Card style="height: 100%">
		<div class="font-semibold flex-auto uppercase">
			<slot name="title" />
			<div class="flex items-start">
				<div class="text-2xl mr-4">
					<i class="fas fa-chart-bar" />
				</div>
				<slot />
			</div>
		</div>
	</Card>
</template>

<script>
	import Card from "@/components/shared/Card.vue";
	export default {
		components: { Card },
	};
</script>
