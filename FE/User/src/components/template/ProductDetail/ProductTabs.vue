<template>
	<section class="product-tabs">
		<a-tabs
			v-model:activeKey="activeTab"
			type="card"
			class="product-tabs__tabs-control"
			destroyInactiveTabPane
		>
			<a-tab-pane key="additional-info" tab="Thông tin bổ sung">
				<ProductAdditionInfo v-bind="infos" />
			</a-tab-pane>
			<a-tab-pane key="comment" tab="Đánh giá">
				<Comments
					ref="rfComment"
					name="Chuck Taylor Classic"
					:comments="comments"
					:disabled="!isLoggedIn"
					@submit="createComment"
				/>
			</a-tab-pane>
		</a-tabs>
	</section>
</template>

<script setup>
	import { ref } from "vue";
	import ProductAdditionInfo from "./ProductAdditionInfo.vue";
	import Comments from "./Comments.vue";

	const props = defineProps({
		infos: { type: Object },
		comments: Array,
		isLoggedIn: Boolean,
	});

	const emits = defineEmits(["create-comment"]);

	const activeTab = ref("additional-info");

	const rfComment = ref(null);

	const resetCommentForm = () => {
		rfComment.value.resetForm();
	};

	const createComment = (form) => {
		emits("create-comment", form);
	};

	defineExpose({
		resetCommentForm,
	});
</script>

<style lang="scss" scoped>
	.product-tabs {
		&__tabs-control {
			:deep(.ant-tabs-nav) {
				margin-bottom: 0;

				.ant-tabs-tab {
					color: rgba(102, 102, 102, 0.85);
					border-color: #ddd;
					border-bottom-color: transparent;
					/* font-size: 0.8rem; */
					letter-spacing: 0.02em;
					font-weight: bolder;

					> div {
						text-transform: uppercase;
						color: inherit;
					}

					&.ant-tabs-tab-active {
						border-top: 2px solid #c30005;
						color: rgba(17, 17, 17, 0.85);
					}
				}
			}

			:deep(.ant-tabs-content) {
				border: 1px solid #ddd;
				border-top: 0;
				background-color: #fff;
				padding: 30px;
			}
		}
	}
</style>
