<template>
	<section class="carousel">
		<a-carousel class="carousel__container" dots swipe draggable>
			<div v-for="(carouselItem, index) in caroselList" :key="carouselItem.id">
				<div
					:style="{
						'background-image': `url(${carouselItem.imgURL})`,
					}"
					class="carousel__item"
				>
					<div class="carousel__text-box" :class="['carousel__text-box--' + index]">
						<h1>MONA SNE<i class="fa fa-star-o" aria-hidden="true"></i>KER</h1>
						<p>{{ carouselItem.description }}</p>
					</div>
				</div>
			</div>
		</a-carousel>
	</section>
</template>

<script>
	export default {
		name: "Carousel",
		props: ["caroselList"],
		functional: true,
	};
</script>

<style lang="scss">
	.carousel {
		padding: 2.5rem 0;

		&__container {
			height: 100%;
		}

		&__item {
			height: 500px;
			position: relative;
			color: white;
			background-position: 50% 50%;
			background-size: cover;
			background-repeat: no-repeat;
			cursor: grab;

			&:active {
				cursor: grabbing;
			}
		}

		&__text-box {
			width: 45%;
			font-size: 1rem;
			position: absolute;

			> h1 {
				color: white;
				font-size: 3.5em;
				letter-spacing: 5px;
				margin: 0.2em 0;
			}

			> p {
				color: #f1f1f1;
			}

			&--0 {
				top: 50%;
				left: 5%;
				transform: translateY(-50%);
			}

			&--1 {
				top: 50%;
				right: 5%;
				transform: translateY(-50%);
				text-align: right;
			}

			&--2 {
				top: 10%;
				right: 5%;
				left: 5%;
				display: flex;
				width: 90%;
				align-items: flex-start;
				justify-content: space-between;

				> h1 {
					width: 50%;
					margin: 0;
				}

				> p {
					width: 37.5%;
					text-align: right;
				}
			}
		}

		> .ant-carousel .slick-dots {
			bottom: 0;
			gap: 0.5rem;
			> li {
				display: inline-block;
				width: 0.75rem;
				height: 0.75rem;
				border: 3px solid #fff;
				border-radius: 50%;
				opacity: 0.4;
				cursor: pointer;
				transition: opacity 0.3s;
				box-sizing: border-box;
				margin: 0;

				&.slick-active {
					width: 0.75rem;
					background-color: #fff;
					opacity: 1;
				}

				> button {
					width: 100%;
					height: 100%;
				}
			}
		}
	}
</style>
