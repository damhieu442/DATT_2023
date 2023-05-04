<template>
	<div>
		<div class="flex justify-between items-center mb-4">
			<h1 class="text-2xl">Góp ý</h1>
			<div>
				<a-button
					class="rounded inline-block mx-2 py-1 bg-sky-400 text-base text-white h-auto px-6 ml-4 align-middle hover:text-white hover:bg-sky-500"
					:loading="isExporting"
					@click="exportExcelHandler"
					>Xuất excel</a-button
				>
				<a-button
					class="rounded inline-block mx-2 py-1 bg-sky-400 text-base text-white h-auto px-6 ml-4 align-middle hover:text-white hover:bg-sky-500"
					@click="getEvaluateList"
					>Làm mới</a-button
				>
			</div>
		</div>
		<Search placeholder="Nhập góp ý muốn tìm kiếm" class="my-6 w-3/5" />
		<EvaluateList
			:loading="isGettingData"
			:data="feedbacks"
			@edit="openFeedbackHandler"
			@delete="openConfirmDeleteDialog"
		/>
		<ThePagination :data="pagination" />
		<EditModal ref="rfEditModal" @ok="updateFeedBackHandler" />
		<DeleteFeedback ref="rfDeleteModal" @delete="deleteFeedback" />
	</div>
</template>

<script setup>
	import { feedback, mics } from "@/api";
	import { useStore } from "vuex";
	import { useRoute } from "vue-router";
	import { onMounted, reactive, ref, watch } from "vue";
	import { notification } from "ant-design-vue";

	import Search from "@/components/shared/Search.vue";
	import EvaluateList from "@/components/template/EvaluatePage/EvaluateList.vue";
	import ThePagination from "@/components/shared/ThePagination.vue";
	import EditModal from "@/components/template/EvaluatePage/EditModal.vue";
	import DeleteFeedback from "@/components/template/EvaluatePage/ConfirmDeleteFeedbackModal.vue";

	import { feedbackAdapter } from "@/utils/feedback";

	const MAX_RECORD = 12;
	const DEFAULT_PAGE = 1;

	const route = useRoute();
	const store = useStore();

	const isExporting = ref(false);
	const isGettingData = ref(false);
	const feedbacks = ref([]);
	const rfEditModal = ref(null);
	const rfDeleteModal = ref(null);
	const pagination = reactive({
		page: Number(route.query.pageNum) || DEFAULT_PAGE,
		total: 0,
		perPage: Number(route.query.pageSize) || MAX_RECORD,
	});

	const openFeedbackHandler = (feedback) => {
		rfEditModal.value.open(feedback);
	};

	const openConfirmDeleteDialog = (feedback) => {
		rfDeleteModal.value.open(feedback);
	};

	const getEvaluateList = async () => {
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

			const response = await feedback.getFilterList(query, [body]);

			if (response.status > 199 && response.status < 300) {
				if ("Data" in response.data) {
					const data = response.data;

					feedbacks.value = data.Data.map(feedbackAdapter.mapFeedbacksResponseToData);

					Object.assign(pagination, {
						page: Number(data.CurrentPage),
						total: Number(data.TotalRecord),
						perPage: Number(data.CurrentPageRecords),
					});
				} else {
					feedbacks.value = [];
					notification.error({
						message: "Không tìm được kết quả phù hợp",
					});
				}
			} else {
				feedbacks.value = [];
				notification.error({
					message: "Có lỗi xảy ra, vui lòng thử lại",
				});
			}
		} catch (error) {
			feedbacks.value = [];
			notification.error({
				message: "Có lỗi xảy ra, vui lòng thử lại",
			});
		} finally {
			isGettingData.value = false;
		}
	};

	const updateFeedBackHandler = async (updateFeedback) => {
		try {
			const sendForm = feedbackAdapter.transformUpdatedFeedbackToRequestBody(
				updateFeedback,
				store.state.user.username,
			);

			const response = await feedback.updateFeedBack(updateFeedback.id, sendForm);
			if (response.status > 199 && response.status < 300) {
				notification.success({
					message: "Cập nhật thành công",
				});
				rfEditModal.value.close();
				getEvaluateList();
				mics.sendEmail(feedbackAdapter.generateFeedbackEmail(updateFeedback));
			}
		} catch (error) {
			if (error.name === "AxiosError") {
				notification.error({
					message:
						error.response.data.MoreInfo?.join?.(".") ||
						error.response.data.UserMsg ||
						"Có lỗi xảy ra, vui lòng thử lại",
				});

				return;
			}

			notification.error({ message: "Có lỗi xảy ra, vui lòng thử lại" });
		}
	};

	const deleteFeedback = async (activeFeedback) => {
		try {
			const { id } = activeFeedback;
			const response = await feedback.deleteFeedBack(id);

			if (response.status > 199 && response.status < 300) {
				notification.success({ message: "Xóa góp ý thành công" });
				getEvaluateList();
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
			const result = await feedback.exportExcel();

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

	onMounted(() => {
		getEvaluateList();
	});

	watch(
		() => route.query,
		() => {
			getEvaluateList();
		},
		{ deep: true },
	);
</script>
