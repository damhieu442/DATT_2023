<template>
	<section class="accessory">
		<h3>TRẺ EM</h3>
		<a-row type="flex">
			<a-col :span="6" v-for="item in data" :key="item.id">
				<ProductItem :product="item" />
			</a-col>
		</a-row>
		<router-link to="/danh-muc/tre-em" class="accessory__view-all">Xem tất cả</router-link>
	</section>
</template>

<script>
	import ProductItem from "@/components/shared/ProductItem.vue";
	export default {
		name: "OtherAccessory",
		props: ["data"],
		components: { ProductItem },
	};
</script>

<style lang="scss" scoped>
	.accessory {
		padding: 0 15px 30px;
		> h3 {
			color: #1c1c1c;
			border-bottom: solid 2px #000;
			margin-bottom: 3em;
			padding-bottom: 10px;
			font-weight: 700;
			text-align: center;
		}

		&__view-all {
			color: rgba(102, 102, 102, 0.85);
			border: 2px solid currentColor;
			background-color: transparent;
			line-height: 2.19em;
			font-size: 0.97rem;
			letter-spacing: 0.03em;
			font-weight: bolder;
			text-transform: none;
			min-height: 2.5em;
			padding: 0 1.2em;
			display: block;
			margin: 0 auto;
			width: max-content;
			transition: transform 0.3s, border 0.3s, background 0.3s, box-shadow 0.3s, opacity 0.3s,
				color 0.3s;

			&:hover {
				background: #000;
				color: #fff;
				border: solid 2px #000;
			}
		}
	}
</style>
