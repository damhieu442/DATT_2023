<template>
	<Card>
		<h4>{{ title }}</h4>

		<ul class="py-4 list-none px-0">
			<li
				v-for="product in products"
				:key="product.id"
				class="py-2 border-solid border-0 border-b border-b-gray-500"
			>
				<div class="flex justify-between items-center">
					<div class="flex-auto">
						<p class="m-0">{{ product.name }}</p>
						<slot name="product-detail" :product="product"> </slot>
					</div>
					<router-link
						:to="'/san-pham/' + product.id"
						class="text-gray-500 h-10 w-10 flex justify-center items-center bg-transparent transition-all duration-200 rounded-full hover:text-gray-600 hover:bg-blue-300"
					>
						<RightOutlined />
					</router-link>
				</div>
			</li>
		</ul>
	</Card>
</template>

<script>
	import Card from "@/components/shared/Card.vue";
	import { RightOutlined } from "@ant-design/icons-vue";

	export default {
		name: "ProductStatisticList",
		props: {
			products: {
				type: Array,
				required: true,
			},
			title: {
				type: String,
			},
		},
		components: { Card, RightOutlined },
	};
</script>
