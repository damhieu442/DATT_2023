<template>
	<div>
		<div class="flex justify-between items-center mb-4">
			<h1 class="text-3xl">Góp ý</h1>
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
	import dayjs from "dayjs";
	import { feedback } from "@/api";
	import { useRoute } from "vue-router";
	import { onMounted, reactive, ref, watch } from "vue";
	import { notification } from "ant-design-vue";

	import Search from "@/components/shared/Search.vue";
	import EvaluateList from "@/components/template/EvaluatePage/EvaluateList.vue";
	import ThePagination from "@/components/shared/ThePagination.vue";
	import EditModal from "@/components/template/EvaluatePage/EditModal.vue";
	import DeleteFeedback from "@/components/template/EvaluatePage/ConfirmDeleteFeedbackModal.vue";

	const MAX_RECORD = 12;
	const DEFAULT_PAGE = 1;

	const route = useRoute();

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

					feedbacks.value = data.Data.map((feedback) => ({
						id: feedback.FeedBackId,
						username: feedback.FullName,
						email: feedback.Email,
						address: feedback.Address,
						content: feedback.Description,
						phone: feedback.PhoneNumber,
						note: "",
						status: "OPEN",
						createAt: feedback.CreatedDate
							? dayjs(feedback.CreatedDate).format("YYYY/MM/DD HH:mm")
							: "-",
						updateAt: feedback.ModifiedDate
							? dayjs(feedback.ModifiedDate).format("YYYY/MM/DD HH:mm")
							: "-",
					}));

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
			const sendForm = {
				FeedBackId: updateFeedback.id,
				FullName: updateFeedback.username,
				Email: updateFeedback.email,
				PhoneNumber: updateFeedback.phone,
				Address: updateFeedback.address,
				Description: updateFeedback.content,
			};

			const response = await feedback.updateFeedBack(updateFeedback.id, sendForm);
			if (response.status > 199 && response.status < 300) {
				notification.success({
					message: "Cập nhật thành công",
				});
				rfEditModal.value.close();
				getEvaluateList();
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
