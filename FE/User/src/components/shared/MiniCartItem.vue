<template>
	<figure class="mini-cart-item">
		<img :src="product.image" alt="" width="300" height="225" class="mini-cart-item__img" />
		<figcaption>
			<p class="mini-cart-item__name">{{ product.name }}</p>
			<p class="mini-cart-item__price">{{ product.quantity }} x {{ formattedPrice }} đ</p>
		</figcaption>
		<div>
			<button class="mini-cart-item__btn" @click="deleteProduct">x</button>
		</div>
	</figure>
</template>

<script setup>
	const props = defineProps({
		product: {
			type: Object,
			default() {
				return { image: "", name: "", price: 0, quantity: 1 };
			},
		},
	});

	const emit = defineEmits(["delete"]);

	const deleteProduct = () => {
		emit("delete", props.product.id);
	};

	const formattedPrice = new Intl.NumberFormat().format(props.product.price);
</script>

<style lang="scss">
	.mini-cart-item {
		display: flex;
		justify-content: space-between;
		align-items: flex-start;
		gap: 1rem;

		&__img {
			width: 3.75rem;
			height: 3.75rem;
			object-fit: cover;
			margin: auto;
			object-position: center;
		}

		&__name {
			font-size: 1rem;
			color: #000;
			margin-bottom: 0.25rem;
			display: -webkit-box;
			-webkit-line-clamp: 2;
			-webkit-box-orient: vertical;
			overflow: hidden;
			text-overflow: ellipsis;
		}

		&__price {
			color: #777;
			margin-bottom: 0;
		}

		&__btn {
			display: inline-block;
			width: 1.5rem;
			height: 1.5rem;
			padding: 0.25rem;
			line-height: 1;
			vertical-align: middle;
			border: 1px solid #333;
			border-radius: 50%;
			color: #333;
			background-color: transparent;
			cursor: pointer;

			&:hover {
				border-color: transparent;
				color: #fff;
				background-color: #333;
			}
		}
	}
</style>
