<template>
	<a-form
		class="user-form"
		:label-col="{ span: 5 }"
		:wrapper-col="{ span: 19 }"
		label-align="left"
		:colon="false"
	>
		<a-row :gutter="24">
			<a-col :span="6">
				<a-form-item :wrapper-col="{ span: 24 }">
					<a-upload
						ref="rfFileUploader"
						list-type="picture-card"
						class="user-form__media-file"
						:show-upload-list="false"
						:file-list="avatarList"
						:custom-request="uploadImageHandler"
						:before-upload="cleanUpObjectURL"
					/>
					<div
						class="relative w-64 h-64 mb-4 mx-auto border rounded-full overflow-hidden media-file"
					>
						<button
							v-if="formState.avatarURL"
							class="delete-file-btn"
							:disabled="disabled"
							:style="{
								top: (2 - Math.sqrt(2)) * 25 + '%',
								right: (2 - Math.sqrt(2)) * 25 + '%',
							}"
							@click="deleteAvatar"
						>
							<i class="fas fa-times" />
						</button>
						<div
							v-if="isUploadingFile"
							class="h-full w-full text-3xl flex justify-center items-center"
						>
							<LoadingOutlined />
						</div>
						<div v-else-if="!formState.avatarURL" class="rounded-lg upload-placeholder">
							<PictureFilled />
						</div>
						<img
							v-else
							:src="formState.avatarURL"
							alt=""
							class="object-cover w-full h-full hover:cursor-pointer"
							@click="openPreviewImage"
						/>
						<div class="absolute left-0 bottom-0 h-9 bg-gray-400 w-full opacity-75">
							<a-dropdown :trigger="['click']" :disabled="disabled">
								<div class="w-full h-full flex justify-center items-center text-xl">
									<button
										class="border-0 bg-transparent cursor-pointer disabled:cursor-not-allowed"
										:disabled="disabled"
									>
										<CameraFilled />
									</button>
								</div>
								<template #overlay>
									<a-menu @click="fileDropdownHandler">
										<a-menu-item :key="FILE_DROPDOWN_KEYS.UPLOAD">
											Chọn File...
										</a-menu-item>
									</a-menu>
								</template>
							</a-dropdown>
						</div>
					</div>
				</a-form-item>
			</a-col>
			<a-col :span="18">
				<Card class="user-form__card">
					<a-form-item label="Tài khoản" v-bind="form.validateInfos.username">
						<a-input
							v-model:value="formState.username"
							:disabled="!!user"
							:placeholder="'Nhập họ tên' + (isAdmin ? '' : ' khách hàng')"
						/>
					</a-form-item>
					<a-form-item label="Mật khẩu" v-bind="form.validateInfos.password">
						<a-input-password
							v-model:value="formState.password"
							:disabled="disabled"
							placeholder="Nhập mật khẩu"
						/>
					</a-form-item>
					<a-form-item label="Họ tên" v-bind="form.validateInfos.fullName">
						<a-input
							v-model:value="formState.fullName"
							:disabled="disabled"
							:placeholder="'Nhập họ tên' + (isAdmin ? '' : ' khách hàng')"
						/>
					</a-form-item>
					<a-form-item label="Địa chỉ" v-bind="form.validateInfos.address">
						<a-input
							v-model:value="formState.address"
							:placeholder="'Nhập địa chỉ' + (isAdmin ? '' : ' khách hàng')"
							:disabled="disabled"
						/>
					</a-form-item>
					<a-form-item label="Số điện thoại" v-bind="form.validateInfos.phoneNumer">
						<a-input
							v-model:value="formState.phoneNumer"
							:placeholder="'Nhập số điện thoại' + (isAdmin ? '' : ' khách hàng')"
							:disabled="disabled"
						/>
					</a-form-item>
					<a-form-item label="Email" v-bind="form.validateInfos.email">
						<a-input
							v-model:value="formState.email"
							:placeholder="'Nhập email' + (isAdmin ? '' : ' khách hàng')"
							:disabled="disabled"
						/>
					</a-form-item>
					<a-form-item label="Quyền" v-bind="form.validateInfos.role"
						><a-radio-group
							v-model:value="formState.role"
							:options="roleOptionsList"
							:disabled="disabled"
						/>
					</a-form-item>
				</Card>
			</a-col>
		</a-row>
		<div v-if="!disabled" class="flex justify-end mt-5">
			<a-button type="link" class="user-form__btn cancel-btn mr-4" @click="cancelSubmit">
				Hủy
			</a-button>
			<a-button class="user-form__btn submit-btn" @click="submitForm">
				{{ !user ? "Thêm mới" : "Cập nhật" }}
			</a-button>
		</div>
		<a-modal
			:footer="null"
			:visible="isPreviewImage"
			class="user-form__preview-modal"
			:closable="false"
			@cancel="hidePreviewModal"
		>
			<button type="button" class="close-modal-btn" @click="hidePreviewModal">
				<i class="fas fa-times" />
			</button>
			<img :src="formState.avatarURL" alt="" class="w-full hover:cursor-pointer" />
		</a-modal>
	</a-form>
</template>

<script>
	import _cloneDeep from "lodash/cloneDeep";
	import phoneValidator from "@/utils/phoneValidator";
	import Card from "@/components/shared/Card.vue";
	import { nextTick, reactive, ref, toRaw, watch } from "vue";
	import { Form } from "ant-design-vue";
	import { useRouter } from "vue-router";
	import { LoadingOutlined, PictureFilled, CameraFilled } from "@ant-design/icons-vue";
	import { ERole, RoleLabels } from "@/constants/config";
	const FILE_DROPDOWN_KEYS = {
		UPLOAD: "upload",
	};

	const defaultForm = {
		username: "",
		fullName: "",
		phoneNumer: "",
		email: "",
		avatarURL: "",
		address: "",
		password: "",
		role: ERole.USER,
	};

	export default {
		name: "UserDetailForm",
		props: {
			user: {
				type: Object,
			},
			disabled: {
				type: Boolean,
				default: false,
			},
			isAdmin: {
				type: Boolean,
				default: false,
			},
		},
		components: { Card, LoadingOutlined, CameraFilled, PictureFilled },

		emits: ["submit"],

		setup(props, { emit }) {
			const router = useRouter();

			const formState = reactive(
				Object.assign({}, props.user || defaultForm, { password: "" }),
			);

			const isUploadingFile = ref(false);
			const isPreviewImage = ref(false);
			const avatarList = ref([]);
			const formRules = reactive({
				username: [
					{
						required: !props.user,
						message: "Vui lòng tên tài khoản",
						trigger: "change",
					},
					{
						validator: (_, value) => {
							if (!value) {
								return Promise.resolve();
							}

							if (!value.trim()) {
								return Promise.reject("Vui lòng nhập tài khoản");
							}

							return Promise.resolve();
						},
						trigger: "change",
					},
				],
				fullName: [
					{
						required: true,
						message: "Vui lòng nhập họ tên người dùng",
						trigger: "change",
					},
				],
				password: [
					{
						required: !props.user,
						message: "Vui lòng mật khẩu tài khoản",
						trigger: "change",
					},
					{
						validator: (_, value) => {
							if (!value) {
								return Promise.resolve();
							}

							if (!value.trim()) {
								return Promise.reject("Vui lòng nhập mật khẩu họp lệ");
							}

							return Promise.resolve();
						},
						trigger: "change",
					},
				],
				email: [
					{
						required: true,
						type: "email",
						message: "Vui lòng nhập địa chỉ email hợp lệ",
						trigger: "blur",
					},
				],
				address: [],
				role: [],
				phoneNumer: [
					{
						validator: (_, value) => {
							if (!value) {
								return Promise.resolve();
							}

							if (!phoneValidator(value)) {
								return Promise.reject("Vui lòng nhập đúng định dạng số điện thoại");
							}
							return Promise.resolve();
						},
						trigger: "change",
					},
				],
			});
			const roleOptionsList = RoleLabels.map((label, i) => ({ label, value: i }));

			const form = Form.useForm(formState, formRules);

			const rfFileUploader = ref(null);

			// Kích hoạt sự kiện upload file
			const activeUploadFile = () => {
				// Tự động kích hoạt sự kiện upload trong upload của ant
				rfFileUploader.value.$el.querySelector("input[type=file]").click();
			};

			const fileDropdownHandler = ({ key }) => {
				switch (key) {
					case FILE_DROPDOWN_KEYS.UPLOAD:
						activeUploadFile();
						break;

					default:
						break;
				}
			};

			const openPreviewImage = () => {
				isPreviewImage.value = true;
			};

			const hidePreviewModal = () => {
				isPreviewImage.value = false;
			};

			const deleteAvatar = () => {
				avatarList.value = [];
				formState.avatarURL = "";
			};

			const uploadImageHandler = ({ file: image }) => {
				const imageURL = URL.createObjectURL(image);
				formState.avatarURL = imageURL;
				avatarList.value = [image];

				return {};
			};

			const cleanUpObjectURL = () => {
				if (formState.avatarURL.startsWith("blob:")) {
					URL.revokeObjectURL(formState.avatarURL);
				}

				return true;
			};

			const cancelSubmit = () => {
				router.back();
			};

			const submitForm = async () => {
				try {
					await form.validate();

					emit("submit", { ...toRaw(formState), avatarFile: avatarList.value[0] });
				} catch (error) {}
			};

			watch(
				() => props.user,
				(user) => {
					Object.assign(formState, user);

					nextTick(() => {
						form.clearValidate();
					});
				},
			);

			return {
				form,
				formState,
				isUploadingFile,
				isPreviewImage,
				avatarList,
				FILE_DROPDOWN_KEYS: {
					UPLOAD: "upload",
				},
				rfFileUploader,
				roleOptionsList,
				cleanUpObjectURL,
				activeUploadFile,
				fileDropdownHandler,
				openPreviewImage,
				hidePreviewModal,
				deleteAvatar,
				uploadImageHandler,
				cancelSubmit,
				submitForm,
			};
		},
	};
</script>

<style lang="scss" scoped>
	.upload-placeholder {
		@apply bg-gray-200 text-8xl h-full w-full flex justify-center items-center;
	}

	.thumbnail {
		@apply transition-all rounded-lg h-20 w-full object-cover hover:cursor-pointer;
		&:hover {
			box-shadow: 0px 4px 10px rgba(15, 3, 34, 0.25);
		}
	}

	.media-file {
		.delete-file-btn {
			@apply opacity-0 transition-opacity p-0 flex justify-center items-center;
			> i {
				vertical-align: center;
			}
		}

		&:hover .delete-file-btn {
			@apply opacity-100;
		}
	}

	.cancel-btn {
		@apply text-black mr-4 hover:bg-blue-400 hover:text-white;
	}

	.submit-btn {
		@apply bg-blue-500 text-white  px-20 py-2 hover:bg-blue-400 hover:text-white;
	}

	.close-modal-btn {
		@apply h-10 w-10 flex justify-center items-center bg-white rounded-full absolute top-0 right-0 text-xl transition-all duration-200 hover:cursor-pointer  hover:bg-blue-500 hover:text-white;
		transform: translate(50%, -50%);
	}

	.delete-file-btn {
		@apply w-5 h-5 absolute leading-5 align-middle text-center text-black bg-white rounded-full border hover:cursor-pointer hover:bg-blue-500 hover:text-white  disabled:cursor-not-allowed;
	}
</style>

<style lang="scss">
	.user-form {
		@apply max-w-7xl mx-4 #{!important};

		&__media-file {
			@apply hidden #{!important};
		}

		.ant-form-item-label {
			@apply font-bold;
		}

		&__description-input {
			label {
				@apply text-lg mb-5 #{!important};
			}

			textarea {
				@apply resize-none;
			}
		}

		&__preview-modal {
			.ant-modal-body {
				@apply relative p-0;
			}

			.ant-modal-close {
				@apply hidden;
			}
		}

		&__btn {
			@apply h-auto rounded-lg #{!important};
		}

		label.ant-form-item-required {
			&::before {
				@apply hidden #{!important};
			}

			&::after {
				@apply inline-block ml-1 text-black text-base leading-none;
				content: "*" !important;
			}
		}

		.ant-input {
			&:disabled {
				color: black;
			}
		}

		.ant-radio-wrapper-disabled > span {
			color: black;
		}

		&__card.card-global {
			@apply bg-white;
		}
	}
</style>
