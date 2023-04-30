<template>
	<li className="ordered__item">
		<div className="ordered__order-info">
			<figure>
				<img :src="item.img" alt="" className="ordered__order-img" />
				<figcaption>
					<h4>
						<router-link :to="`/san-pham/${item.id}`" className="text-current">
							{{ item.name }}
						</router-link>
					</h4>
					<p>x {{ item.amount }} - Size: {{ item.size }}</p>
					<p>
						{{ item.price }} VNĐ
						<span v-if="item.discount">(-{{ item.discount }}%)</span> / 1c
					</p>
				</figcaption>
			</figure>
			<div className="ordered__order-price hightlight">{{ item.totalPrice }} VNĐ</div>
		</div>
	</li>
</template>

<script>
	export default {
		props: {
			item: { type: Object },
		},
	};
</script>

<style lang="scss">
	.ordered {
		&__item {
			padding: 1rem 0;
		}

		&__order {
			&-info {
				display: flex;
				justify-content: space-between;
				figure {
					width: 85%;
					display: flex;
					justify-content: space-between;
					figcaption {
						width: 80%;
						> h4,
						> p {
							line-height: 1.5;
						}
					}
				}
			}

			&-img {
				max-height: 150px;
				width: 15%;
				object-fit: cover;
			}

			&-price {
				width: 20%;
				text-align: right;
			}
		}
	}
</style>
