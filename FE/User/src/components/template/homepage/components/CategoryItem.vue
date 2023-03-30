<template>
	<figure class="category">
		<img :src="category.titleImg" alt="" />
		<div
			:style="{
				backgroundImage: `url(${category.backgroundImg})`,
			}"
			class="category__illusion"
		>
			<div>
				<router-link :to="category.url" class="category__link">Xem sản phẩm</router-link>
			</div>
		</div>
	</figure>
</template>

<script>
	export default {
		name: "CategoryItem",
		props: ["category"],
	};
</script>

<style lang="scss" scoped>
	.category {
		width: 100%;
		background-position: 50% 50%;
		background-size: cover;
		background-repeat: no-repeat;

		padding: 8%;
		margin: 0;
		border: 1px solid #fff;

		&:hover {
			border-color: #000;
		}

		> img {
			display: block;
			width: 50%;
			margin: 0 auto 2rem auto;
		}

		&__illusion {
			background-size: cover;
			background-repeat: no-repeat;
			background-position: 50% 50%;
			background-color: #ccc;
			width: 100%;
			min-height: 245px;
			position: relative;
			font-size: 8.5px;

			> div {
				width: 60%;
				position: absolute;
				top: 20%;
				left: 50%;
				transform: translateX(-50%);
			}
		}

		&:hover &__link {
			transform: scale(1);
		}

		&__link {
			color: #fff;
			background: #000;
			font-size: 1.8em;
			line-height: 2.4em;
			margin: 0.5em 0;
			padding: 0 1.2em;
			min-height: 2.5em;
			text-align: center;
			box-sizing: border-box;
			transform: scale(0);
			transition: 0.3s ease;
			overflow: hidden;
			display: block;
			width: max-content;
			margin: 0 auto;
		}
	}
</style>
