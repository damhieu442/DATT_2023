<template>
	<div class="comments">
		<h4 class="comments__title">Đánh giá sản phẩm</h4>
		<div class="comments__list">
			<p v-if="comments.length === 0" class="comments__list__empty">Chưa có đánh giá nào.</p>
			<div v-else>
				<a-comment v-for="comment in comments" :key="comment.id" class="comments__comment">
					<template #actions> </template>
					<template #author>
						<div>
							<p class="comments__comment__author">
								{{ comment.user.fullname || comment.user.username }}
								<a-rate :value="comment.rate" disabled />
							</p>
							<p class="comments__comment__create-at">{{ comment.createAt }}</p>
						</div>
					</template>
					<template #avatar> </template>
					<template #content>
						<p>
							{{ comment.comment }}
						</p>
					</template>
				</a-comment>
			</div>
		</div>

		<a-form
			:model="formData"
			name="basic"
			autocomplete="off"
			layout="vertical"
			class="comments__form"
			@finish="submitForm"
		>
			<h5 v-if="comments.length === 0">Hãy là người đầu tiên nhận xét “{{ name }}”</h5>
			<h5 v-else>Đánh giá</h5>

			<div v-if="disabled" class="comments__form__notify-layer">
				<p>
					Chỉ những khách hàng đã đăng nhập và mua sản phẩm này mới có thể đưa ra đánh
					giá.
				</p>
			</div>

			<a-form-item
				label="Đánh giá của bạn"
				name="rating"
				:rules="[{ required: true, message: 'Vui lòng đánh giá sản phẩm!' }]"
			>
				<a-rate
					v-model:value="formData.rating"
					:tooltips="['Rất tệ', 'Tệ', 'Bình thường', 'Tốt', 'Rất tốt']"
					:disabled="disabled"
				/>
			</a-form-item>

			<a-form-item
				label="Nhận xét của bạn"
				name="comment"
				:rules="[
					{ required: true, whitespace: true, message: 'Vui lòng nhận xét sản phẩm!' },
				]"
			>
				<a-textarea
					v-model:value="formData.comment"
					class="comments__form__content"
					:disabled="disabled"
				/>
			</a-form-item>

			<a-row :gutter="16">
				<a-col :span="12">
					<a-form-item
						label="Tên"
						name="username"
						:rules="[
							{
								required: true,
								whitespace: true,
								message: 'Vui lòng nhập tên của bạn!',
							},
						]"
					>
						<a-input v-model:value="formData.username" :disabled="disabled" />
					</a-form-item>
				</a-col>

				<a-col :span="12">
					<a-form-item
						label="Email"
						name="email"
						:rules="[
							{ required: true, message: 'Vui lòng nhập email của bạn!' },
							{ type: 'email', message: 'Vui lòng nhập email hợp lệ' },
						]"
					>
						<a-input v-model:value="formData.email" :disabled="disabled" />
					</a-form-item>
				</a-col>
			</a-row>
			<a-form-item>
				<a-button class="comments__form__submit" html-type="submit" :disabled="disabled"
					>Gửi đi</a-button
				>
			</a-form-item>
		</a-form>
	</div>
</template>

<script setup>
	import { computed } from "@vue/reactivity";
	import { reactive } from "vue";
	import { useStore } from "vuex";

	const props = defineProps({
		comments: {
			type: Array,
			default() {
				return [];
			},
		},
		name: { type: String, default: "" },
		disabled: { type: Boolean, default: false },
	});

	const emits = defineEmits(["submit"]);

	const store = useStore();

	const userInfo = computed(() => store.getters["user/authInfo"]);

	const formData = reactive({
		rating: undefined,
		comment: "",
		username: userInfo.value.fullName || "",
		email: userInfo.value.email || "",
	});

	const submitForm = (event) => {
		emits("submit", { ...event });
	};

	const resetForm = () => {
		Object.assign(formData, {
			rating: undefined,
			comment: "",
			username: userInfo.value.fullName || "",
			email: userInfo.value.email || "",
		});
	};

	defineExpose({
		resetForm,
	});
</script>

<style lang="scss" scoped>
	.comments {
		&__title {
			color: #1c1c1c;
			font-weight: 700;
			font-size: 1.25rem;
			margin-bottom: 0.5em;
		}

		&__list {
			font-size: 1rem;
			margin-bottom: 1.3em;
			color: #353535;
		}

		&__form {
			border: 2px solid #c30005;
			position: relative;
			padding: 15px 30px 30px;

			> h5 {
				color: #1c1c1c;
				font-weight: 700;
				font-size: 1.25rem;
				margin-bottom: 0.5em;
			}

			.ant-rate {
				color: #d26e4b;
			}

			:deep(.ant-form-item-label) {
				> label {
					font-weight: 700;

					&::before {
						display: none !important;
					}

					&::after {
						display: inline-block;
						margin-left: 4px;
						color: #ff4d4f;
						font-size: 14px;
						font-family: SimSun, sans-serif;
						line-height: 1;
						content: "*";
					}
				}
			}

			&__content {
				height: 150px;
				resize: none;
			}

			&__submit {
				background-color: #c30005;
				color: white;
				font-size: 1rem;
				font-weight: 700;
				min-height: 2.5em;
				padding: 0 1.2em;

				> :deep(span) {
					text-transform: uppercase;
				}
			}

			&__notify-layer {
				position: absolute;
				top: 0;
				left: 0;
				width: 100%;
				height: 100%;
				background-color: #fffc;
				z-index: 10;
				display: flex;
				justify-content: center;
				align-items: center;

				> p {
					font-size: 1rem;
				}
			}
		}

		&__comment {
			&__create-at {
				font-size: 0.85rem;
			}

			&__author {
				font-size: 1rem;
				> .ant-rate {
					font-size: 0.85em;
				}
			}

			> :deep(.ant-comment-avatar) {
				margin-right: 1rem;
			}
		}
	}
</style>
