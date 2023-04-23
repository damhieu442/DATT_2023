<template>
	<a-modal
		v-model:visible="visible"
		:title="null"
		class="add-category-dialog"
		ok-text="Xác nhận"
		cancel-text="Hủy"
		@ok="handleOk"
		@cancel="close"
	>
		<div class="mt-2 flex justify-between items-center">
			<a-divider />
			<p class="m-0 whitespace-nowrap text-2xl text-black">
				{{ isEdit ? "Sửa" : "Thêm" }} mục sản phẩm
			</p>
			<a-divider />
		</div>
		<div class="mt-5">
			<a-form
				class="add-category-form"
				ref="rfForm"
				layout="vertical"
				:model="form"
				:rules="rules"
			>
				<a-form-item name="name" label="Tên danh mục">
					<a-input v-model:value="form.name" placeholder="Nhập tên loại sản phẩm" />
				</a-form-item>
				<a-form-item name="code" label="Mã danh mục (Mã danh mục chỉ bao gồm các số 0 - 9)">
					<a-input v-model:value="form.code" place holder="Nhập mã danh mục" />
				</a-form-item>
				<a-form-item name="parent" label="Danh mục cha">
					<a-select v-model:value="form.parent" placeholder="Chọn danh mục cha">
						<a-select-option
							v-for="(category, categoryCode) in ECategoryLabels"
							:key="categoryCode"
							:value="categoryCode"
							>{{ category }}</a-select-option
						>
					</a-select>
				</a-form-item>

				<a-form-item name="description" label="Mô tả">
					<a-textarea
						v-model:value="form.description"
						:auto-size="{ minRows: 5, maxRows: 6 }"
						placeholder="Nhập mô tả sản phẩm"
					/>
				</a-form-item>
			</a-form>
		</div>
	</a-modal>
</template>
<script setup>
	import { ECategoryLabels } from "@/constants/category";
	import _cloneDeep from "lodash/cloneDeep";
	import { reactive, ref } from "vue";

	const emits = defineEmits(["ok"]);

	const defaultForm = {
		name: "",
		description: "",
		code: "",
		parent: null,
	};

	const visible = ref(false);
	const isEdit = ref(false);
	const rfForm = ref(null);
	const form = reactive({});
	const rules = {
		name: [
			{
				required: true,
				message: "Vui lòng nhập tên danh mục sản phẩm",
				transform: (categoryName) => categoryName.trim(),
				trigger: "change",
			},
		],
		code: [
			{
				required: true,
				message: "Vui lòng nhập mã danh mục hợp lệ",
				transform(value) {
					return value?.trim?.();
				},
				pattern: /^\d+$/,
				trigger: "change",
			},
		],
	};
	const open = (category) => {
		visible.value = true;
		if (!category) {
			isEdit.value = false;
			Object.assign(form, _cloneDeep(defaultForm));

			return;
		}
		isEdit.value = true;
		Object.assign(form, _cloneDeep(category));
	};
	const close = () => {
		visible.value = false;
	};

	const handleOk = async () => {
		try {
			await rfForm.value.validate();
			emits("ok", { form: { ...form }, isEdit: isEdit.value });
		} catch (error) {
			console.log("Error: ", error);
		}
	};

	defineExpose({
		open,
		close,
	});
</script>

<style lang="scss">
	.add-category-dialog {
		.ant-divider {
			@apply my-5 bg-gray-700 min-w-0 w-1/3 #{!important};
		}
	}
</style>
