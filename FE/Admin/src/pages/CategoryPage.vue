<template>
	<div>
		<div class="flex justify-between items-center mb-4">
			<h1 class="text-3xl">Danh mục sản phẩm</h1>
			<div>
				<a-button
					class="rounded inline-block mx-2 py-1 bg-sky-400 text-base text-white h-auto px-6 ml-4 align-middle hover:text-white hover:bg-sky-500"
					:loading="isExporting"
					@click="exportExcelHandler"
					>Xuất excel</a-button
				>
				<a-button
					class="rounded inline-block mx-2 py-1 bg-sky-400 text-base text-white h-auto px-6 ml-4 align-middle hover:text-white hover:bg-sky-500"
					@click="getCategoryList"
					>Làm mới</a-button
				>
				<a-button class="category-add-btn" @click="showAddCategoryDialog">
					<span>+ Thêm danh mục sản phẩm</span>
				</a-button>
			</div>
		</div>
		<Search placeholder="Nhập tên danh mục muốn tìm kiếm" class="my-6 w-3/5" />
		<CategoryTable
			class="my-5"
			:data="categories"
			:loading="isGettingData"
			:pagination="pagination"
			@deleteCategory="showDeleteDialog"
			@editCategory="showEditCategoryDialog"
		/>
		<ConfirmDeleteDialog ref="rfDeleteCategoryDialog" @delete="deleteCategory" />
		<AddCategoryDialog ref="rfAddCategoryDialog" @ok="editCategory" />

		<ThePagination :data="pagination" />
	</div>
</template>

<script setup>
	import dayjs from "dayjs";
	import { useRoute } from "vue-router";
	import { notification } from "ant-design-vue";
	import { onMounted, reactive, ref, watch } from "vue";

	import Search from "@/components/shared/Search.vue";
	import ThePagination from "@/components/shared/ThePagination.vue";
	import CategoryTable from "@/components/template/CategoryPage/CategoryList.vue";
	import ConfirmDeleteDialog from "@/components/template/CategoryPage/ConfirmDeleteCategoryModal.vue";
	import AddCategoryDialog from "@/components/template/CategoryPage/AddCategory.vue";

	import { category } from "@/api";
	import { ECategoryLabels } from "@/constants/category";
	import { useStore } from "vuex";

	const MAX_RECORD = 12;
	const DEFAULT_PAGE = 1;

	const route = useRoute();
	const store = useStore();

	const pagination = reactive({
		page: Number(route.query.pageNum) || DEFAULT_PAGE,
		total: 0,
		perPage: Number(route.query.pageSize) || MAX_RECORD,
	});
	const isExporting = ref(false);
	const isGettingData = ref(false);
	const categories = ref([]);
	const rfDeleteCategoryDialog = ref(null);
	const rfAddCategoryDialog = ref(null);

	const showAddCategoryDialog = () => {
		rfAddCategoryDialog.value.open();
	};

	const showDeleteDialog = (category) => {
		rfDeleteCategoryDialog.value.open(category);
	};

	const showEditCategoryDialog = (category) => {
		rfAddCategoryDialog.value.open({
			id: category.index,
			name: category.name,
			description: category.rawDesc,
			code: String(category.code),
			parent: category.parent === "-" ? null : String(category.rawParent),
		});
	};

	const getCategoryList = async () => {
		isGettingData.value = true;
		try {
			const { pageNum: page, pageSize: limit, search } = route.query;
			const query = {
				pageNumber: page || DEFAULT_PAGE,
				pageSize: limit || MAX_RECORD,
				keyWord: search || "",
			};
			const body = {
				Field: "CreatedDate",
				Order: "DESC",
				operation: "",
				DataType: "",
				Type: "sort",
			};

			const response = await category.getCategoryProducts(query, [body]);

			if (response.status > 199 && response.status < 300) {
				if ("Data" in response.data) {
					const data = response.data;

					categories.value = data.Data.map((category) => ({
						index: category.CategoryId,
						name: category.CategoryName,
						parent: ECategoryLabels[category.ParentId] || "-",
						rawDesc: category.Description,
						rawParent: category.ParentId,
						description: category.Description || "-",
						code: category["CategoryCode"],
						createAt: dayjs(category.CreatedDate).format("YYYY-MM-DD HH:mm:ss"),
						createBy: category.CreatedBy,
						updateAt: category.ModifiedDate
							? dayjs(category.ModifiedDate).format("YYYY-MM-DD HH:mm:ss")
							: "-",
						updateBy: category.ModifiedBy || "-",
					}));

					Object.assign(pagination, {
						page: Number(data.CurrentPage),
						total: Number(data.TotalRecord),
						perPage: Number(data.CurrentPageRecords),
					});
				} else {
					categories.value = [];
					notification.error({
						message: "Không tìm được kết quả phù hợp",
					});
				}
			} else {
				categories.value = [];
				notification.error({
					message: "Có lỗi xảy ra, vui lòng thử lại",
				});
			}
		} catch (error) {
			categories.value = [];
			notification.error({
				message: "Có lỗi xảy ra, vui lòng thử lại",
			});
		} finally {
			isGettingData.value = false;
		}
	};

	const editCategory = async (categoryForm) => {
		try {
			const { isEdit, form } = categoryForm;
			const sendForm = {
				CategoryName: form.name,
				Description: form.description,
				ParentId: Number(form.parent) || form.parent,
				CategoryCode: form.code,
				ModifiedBy: store.state.user.username,
			};

			const request = isEdit ? category.updateCategory : category.createCategory;

			if (isEdit) {
				sendForm.CategoryId = form.id;
			} else {
				sendForm.CreatedBy = store.state.user.username;
			}

			const response = await request(sendForm);
			if (response.status > 199 && response.status < 300) {
				notification.success({
					message: isEdit ? "Cập nhật danh mục thành công" : "Thêm danh mục thành công",
				});
				rfAddCategoryDialog.value.close();
				getCategoryList();
			}
		} catch (error) {
			console.log("Error: ", error);

			if (error.name === "AxiosError") {
				console.log("error.response.data.MoreInfo", error.response.data.MoreInfo);

				notification.error({
					message:
						error.response.data.MoreInfo?.join?.(".")
							.replace("<", "")
							.replace(">", "") ||
						error.response.data.UserMsg ||
						"Có lỗi xảy ra, vui lòng thử lại",
				});

				return;
			}

			notification.error({ message: "Có lỗi xảy ra, vui lòng thử lại" });
		}
	};

	const deleteCategory = async (activeCategory) => {
		try {
			const { index: id } = activeCategory;
			const response = await category.deleteCategory(id);

			if (response.status > 199 && response.status < 300) {
				notification.success({ message: "Xóa danh mục thành công" });
				rfDeleteCategoryDialog.value.close();
				getCategoryList();
			} else {
				throw new Error();
			}
		} catch (error) {
			notification.error({ message: "Có lỗi xảy ra, vui lòng thử lại" });
		}
	};

	const exportExcelHandler = async () => {
		isExporting.value = true;

		try {
			const result = await category.exportExcel();

			if (result.status === 200) {
				const blob = new Blob([result.data], {
					type:
						result.data.type ||
						"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
				});

				const downloadURL = URL.createObjectURL(blob);
				const aEl = document.createElement("a");

				aEl.href = downloadURL;
				aEl.download = "bao_cao";
				aEl.style.display = "none";
				document.body.appendChild(aEl);
				aEl.click();
				aEl.remove();
			} else if (result.status > 499) {
				throw new Error("Server error");
			}
		} catch (error) {
			notification.error({ message: "Có lỗi xảy ra, vui lòng thử lại" });
		} finally {
			isExporting.value = false;
		}
	};

	onMounted(() => getCategoryList());

	watch(
		() => route.query,
		() => {
			getCategoryList();
		},
		{ deep: true },
	);
</script>

<style lang="scss" scoped>
	.category-add-btn {
		@apply py-1 px-10 rounded text-base bg-blue-500 text-white h-auto;
	}
</style>
