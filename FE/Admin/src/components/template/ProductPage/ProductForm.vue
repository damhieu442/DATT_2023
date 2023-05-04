<template>
	<a-form
		ref="rfForm"
		class="product-form"
		:label-col="{ span: 5 }"
		:wrapper-col="{ span: 19 }"
		:model="form"
		:rules="rules"
		:colon="false"
		label-align="left"
	>
		<a-row :gutter="16">
			<a-col :span="6">
				<a-form-item :wrapper-col="{ span: 24 }">
					<a-upload
						ref="rfFileUploader"
						multiple
						:show-upload-list="false"
						list-type="picture-card"
						class="product-form__media-file"
						:file-list="mediaFileList"
						:custom-request="generatePreviewLink"
					/>
					<div class="relative w-full h-64 mb-4 image-container">
						<button
							v-if="form.images.length > 0"
							class="delete-file-btn"
							:disabled="disabled"
							@click="deleteMediaFile(0, form.images[0])"
						>
							<i class="fas fa-times" />
						</button>
						<div v-if="form.images.length === 0" class="rounded-lg upload-placeholder">
							<PictureFilled />
						</div>
						<img
							v-else
							:src="form.images[0]"
							alt=""
							class="rounded-lg object-cover w-full h-full hover:cursor-pointer border border-solid border-black"
							@click="previewFile(form.images[0])"
						/>
						<div
							class="absolute left-0 bottom-0 h-9 bg-gray-400 w-full opacity-75 rounded-b-lg"
						>
							<a-dropdown :trigger="['click']" :disabled="disabled">
								<div
									class="w-full h-full flex justify-center items-center text-xl hover:cursor-pointer"
								>
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
					<a-row :gutter="[16, 16]">
						<template v-for="(image, i) in form.images" :key="i">
							<a-col v-if="i !== 0" :span="8">
								<div class="relative image-container">
									<button
										class="delete-file-btn"
										:disabled="disabled"
										@click="deleteMediaFile(i, image)"
									>
										<i class="fas fa-times" />
									</button>
									<img
										:src="image"
										alt=""
										class="cursor-pointer w-[90px] h-[90px] object-fill object-center border rounded border-solid border-black"
										height="40"
										width="40"
										@click="previewFile(image)"
									/>
								</div>
							</a-col>
						</template>
					</a-row>
				</a-form-item>
			</a-col>
			<a-col :span="18">
				<Card>
					<h2 class="text-lg font-bold title">Thông tin chung</h2>
					<a-form-item name="name" label="Tên sản phẩm">
						<a-input
							v-model:value="form.name"
							:disabled="disabled"
							placeholder="Nhập tên sản phẩm"
						/>
					</a-form-item>
					<a-form-item name="shoeCode" label="Mã sản phẩm">
						<a-input
							v-model:value="form.shoeCode"
							:disabled="disabled"
							placeholder="Nhập mã sản phẩm"
						/>
					</a-form-item>
					<a-form-item name="categoryCode" label="Danh mục sản phẩm">
						<a-tree-select
							v-model:value="form.categoryCode"
							show-search
							allow-clear
							tree-default-expand-all
							placeholder="Danh mục giày"
							dropdownClassName="[&_.ant-select-empty]:py-8 [&_.ant-select-empty]:text-center"
							:disabled="disabled"
							:tree-data="categories"
							:field-names="{
								children: 'children',
								label: 'label',
								value: 'code',
							}"
						>
							<template v-if="isGettingCategories" #notFoundContent>
								<a-spin size="large" />
							</template>
						</a-tree-select>
					</a-form-item>
					<a-form-item name="price" label="Giá sản phẩm">
						<a-input
							v-model:value="productPrice"
							addon-after="đ"
							placeholder="Nhập giá sản phẩm"
							:disabled="disabled"
						/>
					</a-form-item>
					<a-form-item name="discount" label="Khuyến mại">
						<a-input-number
							v-model:value="form.discount"
							placeholder="Nhập khuyến mại"
							addon-after="%"
							class="w-full"
							:disabled="disabled"
						/>
					</a-form-item>
					<a-form-item label="Thực giá">
						<p class="m-0">{{ actualPrice }} đ</p>
					</a-form-item>
				</Card>
				<Card class="card">
					<h2 class="text-lg font-bold title">Thuộc tính</h2>
					<a-row v-for="(size, sizeIndex) in form.size" :key="size.id" :gutter="16">
						<a-col :span="8">
							<a-form-item
								label="Kích thước"
								:name="['size', sizeIndex, 'name']"
								:label-col="{ span: 10 }"
								:wrapper-col="{ span: 14 }"
								:rules="{
									validator: formItemValidate,
									trigger: 'blur',
								}"
							>
								<a-input
									v-model:value="size.name"
									placeholder="Kích thước"
									:disabled="disabled"
								/>
							</a-form-item>
						</a-col>
						<a-col :span="8">
							<a-form-item
								label="Số lượng"
								:label-col="{ span: 8 }"
								:wrapper-col="{ span: 16 }"
							>
								<a-input
									v-model:value="size.amount"
									placeholder="Nhập số lượng"
									:disabled="disabled"
								/>
							</a-form-item>
						</a-col>
						<a-col :span="7">
							<a-form-item
								label="Đã bán"
								:label-col="{ span: 5 }"
								:wrapper-col="{ span: 19 }"
							>
								<a-input :value="size.sold" placeholder="" disabled />
							</a-form-item>
						</a-col>
						<a-col v-if="store.state.user.role === ERole.SUPER_ADMIN" :span="1">
							<button
								v-if="size.deletable"
								class="mt-1 text-black bg-transparent border-0 hover:text-gray-600 hover:cursor-pointer disabled:cursor-not-allowed"
								:disabled="disabled"
								@click="deleteSize(i)"
							>
								<i class="far fa-trash-alt" />
							</button>
						</a-col>
					</a-row>
					<a-button class="add-element-btn" :disabled="disabled" @click="addSize">
						<i class="fas fa-plus-circle" /> <span class="ml-2">Thêm mới</span>
					</a-button>
				</Card>
				<Card class="card">
					<h4 class="text-lg font-bold title">Mô tả sản phẩm</h4>
					<a-form-item
						name="material"
						label="Chất liệu"
						:label-col="{ span: 24 }"
						:wrapper-col="{ span: 24 }"
					>
						<a-input
							v-model:value="form.material"
							placeholder="Nhập chất liệu sản phẩm"
							:disabled="disabled"
						/>
					</a-form-item>
					<a-form-item
						name="description"
						label="Mô tả"
						:label-col="{ span: 24 }"
						:wrapper-col="{ span: 24 }"
					>
						<a-textarea
							v-model:value="form.description"
							:disabled="disabled"
							:auto-size="{ minRows: 5, maxRows: 6 }"
							class="h-96 resize-none"
						/>
					</a-form-item>
				</Card>
			</a-col>
		</a-row>
		<div v-if="!disabled" class="flex justify-end">
			<a-button type="link" class="product-form__btn cancel-btn mr-4" @click="cancelSubmit">
				Hủy
			</a-button>
			<a-button class="product-form__btn submit-btn" :loading="loading" @click="submitForm">
				{{ product ? "Cập nhật" : "Lưu" }}
			</a-button>
		</div>
		<a-modal
			:footer="null"
			:visible="!!filePreview"
			class="product-form__preview-modal"
			@cancel="hidePreviewModal"
		>
			<button type="button" class="close-modal-btn" @click="hidePreviewModal">
				<i class="fas fa-times" />
			</button>
			<img :src="filePreview || ''" alt="" class="w-full h-auto hover:cursor-pointer" />
		</a-modal>
	</a-form>
</template>

<script setup>
	import _cloneDeep from "lodash/cloneDeep";
	import _deburr from "lodash/deburr";
	import _kebabCase from "lodash/kebabCase";
	import { computed, onBeforeUnmount, reactive, ref, watch } from "vue";
	import { v4 as uuidV4 } from "uuid";
	import { useStore } from "vuex";
	import Card from "@/components/shared/Card.vue";
	import { CameraFilled, PictureFilled } from "@ant-design/icons-vue";
	import { ERole } from "@/constants/config";

	const props = defineProps({
		disabled: {
			type: Boolean,
			default: false,
		},
		product: {
			type: Object,
		},
		loading: Boolean,
	});

	const emits = defineEmits(["submit", "cancel"]);

	const size = { id: "", name: "", amount: 0, sold: 0, deletable: true };

	const defaultForm = {
		name: "",
		shoeCode: "",
		// Loại sản phẩm
		categoryCode: undefined, // đặt undefined để hiện placeholder
		price: "",
		discount: "",
		material: "",
		// Thuộc tính
		size: [size],
		// Mô tả
		description: "",
		// Danh sách file media (ảnh + video)
		// Chỉ lưu link và định dạng của file
		images: [],
	};

	const store = useStore();

	const form = reactive(props.product ? _cloneDeep(props.product) : _cloneDeep(defaultForm));
	const filePreview = ref(null);
	const rfFileUploader = ref(null);
	const mediaFileList = ref([]);
	const rfForm = ref(null);

	const rules = {
		name: [
			{
				required: true,
				message: "Vui lòng nhập tên sản phẩm",
				trigger: "change",
			},
		],
		shoeCode: [
			{
				required: true,
				message: "Vui lòng nhập mã sản phẩm",
				trigger: "change",
			},
		],
		price: [
			{
				required: true,
				message: "Vui lòng nhập giá sản phẩm",
				trigger: "change",
			},
			{
				validator: (_, value) => {
					if (value && value < 0) {
						return Promise.reject("Vui lòng nhập đúng đơn giá sản phẩm");
					}
					return Promise.resolve();
				},
				trigger: "change",
			},
		],
		categoryCode: [
			{
				required: true,
				message: "Vui lòng chọn danh mục sản phẩm",
				trigger: "change",
			},
		],
		description: [
			{
				required: true,
				message: "Vui lòng nhập mô tả sản phẩm",
				trigger: "change",
			},
		],
		discount: [
			{
				validator: (_, value) => {
					if (value && (value < 0 || value > 100)) {
						return Promise.reject("Vui lòng nhập đúng khuyến mại sản phẩm(0 - 100%)");
					}
					return Promise.resolve();
				},
				trigger: "change",
			},
		],
		size: [],
	};

	const FILE_DROPDOWN_KEYS = {
		UPLOAD: "upload",
	};

	const productPrice = computed({
		get() {
			if (!form.price) {
				return "";
			}

			return String(form.price).replace(/\B(?=(\d{3})+(?!\d))/g, ",");
		},

		set(price) {
			form.price = price.replace(/\D/g, "");
		},
	});

	const categories = computed(() => store.state.category.categories);

	const isGettingCategories = computed(() => store.state.category.loading);

	const actualPrice = computed(() => {
		if (!form.price) {
			return "0";
		}

		return Math.ceil(
			Number(form.price) * (1 - (Number(form.discount) || 0) / 100),
		).toLocaleString();
	});

	/* async (_, value) => {
										const currentSize = form.size[sizeIndex];
										for (let index = 0; index < sizeIndex; index++) {
											const size = form.size[index];
											if (size.name === currentSize.name) {
												throw new Error('Kích thước giày bị trùng');
											}
										}

										return undefined;
									} */
	const formItemValidate = (rule, value) => {
		console.log("Field: ", rule, value);
		return Promise.resolve();
	};

	const deleteSize = (index) => {
		form.size.splice(index, 1);
	};

	const addSize = () => {
		form.size.push({ ...size, id: uuidV4() });
	};

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

	const previewFile = (link) => {
		filePreview.value = link;
	};

	const hidePreviewModal = () => {
		filePreview.value = null;
	};

	const deleteMediaFile = (index, link) => {
		form.images.splice(index, 1);
		mediaFileList.value.splice(
			mediaFileList.value.findIndex((file) => file.url === link),
			1,
		);

		if (link.startsWith("blob:")) {
			// Remove browser generated link for blob
			URL.revokeObjectURL(link);
		}
	};

	const generatePreviewLink = async ({ file: image }) => {
		const imageURL = URL.createObjectURL(image);
		form.images.push(imageURL);
		mediaFileList.value.push({ file: image, url: imageURL });
	};

	const cancelSubmit = () => {
		emits("cancel");
	};

	const submitForm = async () => {
		try {
			await rfForm.value.validate();

			const submitForm = _cloneDeep(form);
			const uploadedFiles = mediaFileList.value.map((file) => file.file);

			// Removed local links, it's useless for upload
			submitForm.images = submitForm.images.filter((image) => !image.startsWith("blob:"));

			for (const category of categories.value) {
				if (category.code === form.categoryCode) {
					submitForm.category = category.id;
				} else if (category.children) {
					let isFound = false;
					for (const cate of category.children) {
						if (cate.code === form.categoryCode) {
							submitForm.category = cate.id;
							isFound = true;
							break;
						}
					}

					if (isFound) {
						break;
					}
				}
			}

			emits("submit", { form: submitForm, files: uploadedFiles });
		} catch (error) {
			console.log("Error: ", error);
		}
	};

	const resetForm = () => {
		Object.assign(form, _cloneDeep(defaultForm));
	};

	watch(
		() => props.product,
		() => {
			Object.assign(
				form,
				props.product ? _cloneDeep(props.product) : _cloneDeep(defaultForm),
			);
		},
	);

	onBeforeUnmount(() => {
		form.images.forEach((url) => {
			// the url generated from URL.createURL need to free manually
			URL.revokeObjectURL(url);
		});
	});

	defineExpose({ reset: resetForm });
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

	.image-container {
		> .delete-file-btn {
			@apply opacity-0 transition-opacity;
		}

		&:hover > .delete-file-btn {
			@apply opacity-100;
		}
	}

	.card {
		@apply mt-8 mb-4;
	}

	.title {
		@apply mb-6;
	}

	.cancel-btn {
		@apply text-black mr-4 hover:bg-blue-400 hover:text-white;
	}

	.submit-btn {
		@apply px-20 py-2 bg-blue-500 text-white hover:bg-blue-400 hover:text-white;
	}

	.close-modal-btn {
		@apply h-10 w-10 flex justify-center items-center bg-white rounded-full absolute top-0 right-0 text-xl transition-all duration-200 hover:cursor-pointer  hover:bg-blue-400 hover:text-white;
		transform: translate(50%, -50%);
	}

	.add-element-btn {
		@apply border-0 text-black font-bold bg-transparent;
	}

	.delete-file-btn {
		@apply w-6 h-6 p-0 absolute top-2 right-2 leading-5 align-middle text-center text-black bg-white rounded-full hover:cursor-pointer hover:bg-blue-500 hover:text-white  disabled:cursor-not-allowed;
	}
</style>

<style lang="scss">
	.product-form {
		@apply max-w-7xl mx-4 #{!important};

		&__media-file {
			@apply hidden #{!important};
		}

		.ant-form-item-label {
			@apply font-bold;
		}

		.ant-input-number-handler-wrap {
			@apply hidden #{!important};
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
	}
</style>
