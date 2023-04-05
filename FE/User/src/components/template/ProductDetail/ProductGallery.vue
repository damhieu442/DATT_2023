<template>
	<div class="product-gallery">
		<a-carousel
			class="product-gallery__gallery"
			dots
			swipe
			draggable
			:arrows="false"
			effect="fade"
			dots-class="product-gallery__thumbs"
		>
			<template #customPaging="props">
				<img :src="images[props.i]" />
			</template>
			<div v-for="image in images" :key="image" class="product-gallery__gallery__container">
				<img
					:src="image"
					alt=""
					width="600"
					height="384"
					class="product-gallery__gallery__image"
				/>

				<button
					class="product-gallery__gallery__image__preview"
					@click="() => previewImageHandler(image)"
				>
					<ArrowsAltOutlined />
				</button>
			</div>
		</a-carousel>

		<a-image
			:width="800"
			:style="{ display: 'none' }"
			:preview="{
				visible: !!previewImg,
				onVisibleChange: closeImageHandler,
			}"
			:src="previewImg"
		/>
	</div>
</template>

<script setup>
	import { ref } from "vue";
	import { ArrowsAltOutlined } from "@ant-design/icons-vue";

	const props = defineProps({
		images: {
			type: Array,
			default() {
				return [""];
			},
		},
	});

	const previewImg = ref("");

	const previewImageHandler = (imageLink) => {
		previewImg.value = imageLink;
	};

	const closeImageHandler = (isShow) => {
		if (!isShow) {
			previewImg.value = "";
		}
	};
</script>

<style lang="scss" scoped>
	.product-gallery {
		max-width: 100%;
		&__gallery {
			&__container {
				padding: 1rem;
				position: relative;
			}

			&__image {
				width: 100%;
				object-fit: fill;
				object-position: center;

				&__preview {
					position: absolute;
					bottom: 1rem;
					left: 1rem;

					color: silver;
					font-size: 1.25rem;
					margin-left: 0.12rem;
					margin-right: 0.12rem;
					min-width: 2.5rem;
					min-height: 2.5rem;
					cursor: pointer;

					border: 2px solid currentColor;
					background-color: transparent;
					border-radius: 999px;
					object-fit: cover;
					transition: transform 0.3s, border 0.3s, background 0.3s, box-shadow 0.3s,
						opacity 0.3s, color 0.3s;
					&:hover {
						border-color: #c30005;
						background-color: #c30005;
						color: white;
					}
				}
			}

			:deep(.slick-dots) {
				position: relative;
				height: auto;
			}

			:deep(.product-gallery__thumbs.slick-dots) {
				bottom: 0px;
				margin: 0;

				> li {
					width: 20%;
					height: 100px;
					img {
						width: 100%;
						height: 100%;
						filter: opacity(60%);
						display: block;
						transition: transform 0.6s, filter 0.6s;

						&:hover {
							filter: opacity(100%);
							transform: translateY(-5px);
							border: 1px solid gray;
						}
					}

					&.slick-active {
						img {
							filter: opacity(100%);
							transform: translateY(-5px);
							border: 1px solid gray;
						}
					}
				}
			}
		}
	}
</style>
