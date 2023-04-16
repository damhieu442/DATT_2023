<template>
	<div class="user-info-form">
		<a-form layout="vertical" class="user-info-form__form">
			<a-form-item v-bind="form.validateInfos.uid" hidden>
				<a-input v-model:value="formState.uid" disabled />
			</a-form-item>

			<a-row :gutter="16">
				<a-col :span="12">
					<a-form-item label="Tên tài khoản" v-bind="form.validateInfos.username">
						<a-input
							v-model:value="formState.username"
							class="user-info-form__form__username"
							disabled
						/>
					</a-form-item>
					<a-form-item label="Họ &amp; Tên" v-bind="form.validateInfos.fullName">
						<a-input v-model:value="formState.fullName" />
					</a-form-item>
				</a-col>
				<a-col :span="12">
					<button class="user-info-form__avatar" @click="openUploadAvatarModal">
						<img
							v-if="avatarURL"
							:src="avatarURL"
							className="w-full h-full object-cover object-center"
						/>
						<i v-else class="user-info-form__avatar--skeleton fa-solid fa-user"></i>
						<!-- onClick="{openAvatarModal}" -->
					</button>
				</a-col>
			</a-row>
			<a-row :gutter="16">
				<a-col :span="12">
					<a-form-item label="Số điện thoại" v-bind="form.validateInfos.phone">
						<a-input v-model:value="formState.phone" />
					</a-form-item>
				</a-col>
				<a-col :span="12">
					<a-form-item label="Email" v-bind="form.validateInfos.userEmail">
						<a-input v-model:value="formState.userEmail" />
					</a-form-item>
				</a-col>
			</a-row>
			<a-form-item label="Địa chỉ" v-bind="form.validateInfos.address">
				<a-input v-model:value="formState.address" />
			</a-form-item>

			<a-modal
				:visible="isShowAvatarModal"
				:title="null"
				:footer="null"
				:closable="false"
				wrapClassName="user-info-form__avatar__upload-modal"
				@cancel="closeUploadAvatarModal"
				><h2 className="text-2xl">Cập nhật ảnh đại diện</h2>
				<a-divider />
				<div>
					<a-form-item v-bind="form.validateInfos.image">
						<a-upload-dragger
							v-model:fileList="uploadAvatar"
							:showUploadList="false"
							:customRequest="uploadImageHandler"
							accept="image/*"
						>
							<p className="ant-upload-drag-icon">
								<InboxOutlined />
							</p>
							<p className="ant-upload-text">
								Nhấn để chọn hoặc kéo thả hình ảnh vào khung này
							</p>
						</a-upload-dragger>
						<!-- <a-input v-model:value="formState.image" /> -->
					</a-form-item>
				</div>
			</a-modal>

			<a-button
				class="user-info-form__submit"
				type="primary"
				:loading="loading"
				@click="updateInfo"
			>
				Cập nhật thay đổi
			</a-button>
		</a-form>
	</div>
</template>

<script setup>
	import { computed, reactive, ref, toRaw, watch } from "vue";
	import phoneValidator from "@/utils/phoneValidator";
	import { Form } from "ant-design-vue";
	import { InboxOutlined } from "@ant-design/icons-vue";

	const props = defineProps({
		user: Object,
		loading: Boolean,
	});

	const emits = defineEmits(["submit"]);

	const avatarURL = ref(props.user.image);

	const isShowAvatarModal = ref(false);

	const formState = reactive({
		username: props.user.username || "",
		userEmail: props.user.userEmail || "",
		fullName: props.user.fullName || "",
		uid: props.user.uid || "",
		phone: props.user.phone || "",
		address: props.user.address || "",
		image: props.user.image || "",
	});

	const formRule = reactive({
		username: [],
		userEmail: [
			{ required: true, message: "Vui lòng nhập địa chỉ email của bạn!" },
			{ type: "email", message: "Vui lòng nhập địa chỉ email hợp lệ" },
		],
		fullName: [{ required: true, message: "Vui lòng nhập họ tên của bạn!", trigger: "change" }],
		uid: [],
		phone: [
			{
				required: true,
				message: "Vui lòng nhập số điện thoại",
				trigger: "change",
			},
			{
				validator: (_, phone) => {
					if (phone === "") {
						return Promise.resolve();
					}

					if (phone.trim() === "") {
						return Promise.reject(new Error("Số điện thoại không được để trống"));
					}

					if (!phoneValidator(phone)) {
						return Promise.reject(
							new Error("Số điện thoại không đúng định dang (VD: +84 123 456 789)"),
						);
					}

					return Promise.resolve();
				},
			},
		],
		address: [],
		image: [],
	});

	const form = Form.useForm(formState, formRule);

	const uploadAvatar = computed({
		set(images) {
			if (images.length === 0) {
				formState.image = "";
				return;
			}

			formState.image = images[0];
		},
		get() {
			if (!formState.image) {
				return [];
			}

			return [formState.image];
		},
	});

	const openUploadAvatarModal = () => {
		isShowAvatarModal.value = true;
	};

	const closeUploadAvatarModal = () => {
		isShowAvatarModal.value = false;
	};

	const uploadImageHandler = ({ file, onSuccess }) => {
		const fileBlob = new Blob([file]);
		const url = URL.createObjectURL(fileBlob);
		avatarURL.value = url;
		uploadAvatar.value = [file];
		onSuccess(url);
		closeUploadAvatarModal();
	};

	const updateInfo = async () => {
		try {
			await form.validate();
			emits("submit", { ...toRaw(formState) });
		} catch (error) {}
	};

	const clearFormInfo = () => {
		form.resetFields();
	};

	watch(props.user, (user) => {
		Object.assign(formState, {
			username: user.username || "",
			userEmail: user.userEmail || "",
			fullName: user.fullName || "",
			uid: user.uid || "",
			phone: user.phone || "",
			address: user.address || "",
			image: user.image || "",
		});
	});

	defineExpose({
		resetForm: clearFormInfo,
	});
</script>

<style lang="scss">
	.user-info-form {
		&__avatar {
			margin-left: 2rem;
			width: 7rem;
			height: 7rem;
			background-color: rgb(191 219 254);
			border: 2px solid currentColor;
			color: rgb(59 130 246);
			display: flex;
			justify-content: center;
			align-items: center;
			border-radius: 99999px;
			margin-right: 1.25rem;
			overflow: hidden;
			cursor: pointer;

			&--skeleton {
				font-size: 3rem;
				text-align: center;
			}

			&__upload-modal {
				border-radius: 0.5rem;
			}

			> img {
				width: 100%;
				height: 100%;
				object-fit: cover;
				object-position: center;
			}
		}

		&__form {
			& &__username {
				&:disabled {
					color: #222;
				}
			}

			.ant-form-item-label {
				> label {
					font-weight: 700;
					font-size: 0.9rem;
					color: #222;

					&.ant-form-item-required {
						&::before {
							display: none !important;
						}

						&::after {
							display: inline-block !important;
							margin-left: 4px;
							color: #222;
							font-size: 14px;
							font-family: SimSun, sans-serif;
							line-height: 1;
							content: "*";
						}
					}
				}
			}
		}

		&__submit {
			display: block !important;
			margin: 1.25rem 0;
			height: auto !important;
		}
	}
</style>
