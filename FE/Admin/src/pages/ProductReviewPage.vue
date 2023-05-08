<template>
	<div>
		<div class="mb-4">
			<div class="flex justify-between items-center">
				<h1 class="text-3xl">Đánh giá sản phẩm</h1>
				<a-button
					class="rounded inline-block mx-2 py-1 bg-sky-400 text-base text-white h-auto px-6 ml-4 align-middle hover:text-white hover:bg-sky-500"
					>Làm mới</a-button
				>
			</div>
			<div class="my-5 px-7 py-6 border rounded-md bg-cyan-200">
				<Filter @submit="getData" />
			</div>
		</div>

		<ReviewList
			:loading="isGettingData"
			:data="reviewsList"
			@update:approve="approveReviewHandler"
			@update:reject="openRejectModal"
			@delete="deleteReviewHandler"
		/>

		<ThePagination
			:data="pagination"
			@update:page="(page) => getData({ page })"
			@update:size="(size) => getData({ size })"
		/>
		<DeclineReviewModal ref="rfCancelModal" @submit="rejectReviewHandler" />
	</div>
</template>

<script setup>
	import { useRoute } from "vue-router";
	import { onMounted, reactive, ref } from "vue";
	import { Modal, notification } from "ant-design-vue";

	import { evaluate as reviewAPI, mics as micsAPI } from "@/api";

	import Filter from "@/components/template/ProductReview/ReviewFIlter.vue";
	import ThePagination from "@/components/shared/ThePagination.vue";
	import ReviewList from "@/components/template/ProductReview/ReviewList.vue";
	import DeclineReviewModal from "@/components/template/ProductReview/DeclineReviewModal.vue";

	import { productReviewAdapter } from "@/utils/reviewAdapter";
	import { EReviewStatus } from "@/constants/review";
	import { useStore } from "vuex";

	const route = useRoute();
	const store = useStore();
	const MAX_RECORD = 12;
	const DEFAULT_PAGE = 1;
	const isGettingData = ref(false);
	const reviewsList = ref([]);
	const rfCancelModal = ref(null);
	const pagination = reactive({
		page: Number(route.query.pageNum) || DEFAULT_PAGE,
		total: 0,
		perPage: Number(route.query.pageSize) || MAX_RECORD,
	});

	const getData = async (queries) => {
		isGettingData.value = true;
		try {
			const { pageNum: page, pageSize: limit, keyword, status } = route.query;

			const query = {
				pageNumber: page || DEFAULT_PAGE,
				pageSize: limit || MAX_RECORD,
				keyWord: keyword || undefined,
				status: status || undefined,
			};

			if (queries) {
				query.keyWord = queries.keyword || undefined;
				query.status =
					queries.status !== undefined && queries.status !== ""
						? queries.status
						: undefined;
				query.pageNumber = queries.page || page || DEFAULT_PAGE;
				query.pageSize = queries.size || limit || MAX_RECORD;
			}

			const response = await reviewAPI.getFilterList(query);

			if (response.status > 199 && response.status < 300) {
				if ("Data" in response.data) {
					const data = response.data;

					reviewsList.value = data.Data.map(
						productReviewAdapter.transformListResponseToReview,
					);

					Object.assign(pagination, {
						page: Number(data.CurrentPage),
						total: Number(data.TotalRecord),
						perPage: Number(data.CurrentPageRecords),
					});
				} else {
					reviewsList.value = [];
					notification.error({
						message: "Không tìm được kết quả phù hợp",
					});
				}
			} else {
				reviewsList.value = [];
				notification.error({
					message: "Có lỗi xảy ra, vui lòng thử lại",
				});
			}
		} catch (error) {
			reviewsList.value = [];
			notification.error({
				message: "Có lỗi xảy ra, vui lòng thử lại",
			});
		} finally {
			isGettingData.value = false;
		}
	};

	const deleteReviewHandler = (deleteItem) => {
		Modal.confirm({
			content: `Bạn có chắc muốn xóa đánh giá của người dùng ${deleteItem.username}?`,
			icon: null,
			maskClosable: true,
			onOk: async () => {
				const response = await reviewAPI.deleteItem(deleteItem.id);
				if (response.status > 199 && response.status < 300) {
					getData();
				} else {
					notification.error({ message: "Có lỗi xảy ra, vui lòng thử lại" });
				}
			},
			okText: "Xóa",
			okButtonProps: { danger: true },
			cancelText: "Hủy",
		});
	};

	const approveReviewHandler = async (approveItem) => {
		const sendForm = productReviewAdapter.transformReviewToUpdateRequestBody(
			approveItem,
			EReviewStatus.APPROVED,
			store.state.user.username,
		);
		try {
			const response = await reviewAPI.updateItem(sendForm, approveItem.id);
			if (response.status > 199 && response.status < 300) {
				getData();
				notification.success({ message: "Phê duyệt thành công" });
			} else {
				notification.error({ message: "Có lỗi xảy ra, vui lòng thử lại" });
			}
		} catch (error) {
			notification.error({ message: "Có lỗi xảy ra, vui lòng thử lại" });
		}
	};

	const openRejectModal = (rejectItem) => {
		rfCancelModal.value.open(rejectItem);
	};

	const rejectReviewHandler = async (reasonForm, rejectItem) => {
		const sendForm = productReviewAdapter.transformReviewToUpdateRequestBody(
			rejectItem,
			EReviewStatus.DECLINED,
			store.state.user.username,
		);

		const sendEmail = productReviewAdapter.generateRejectReviewEmail(
			rejectItem,
			reasonForm.reason,
		);

		try {
			const response = await reviewAPI.updateItem(sendForm, rejectItem.id);
			micsAPI.sendEmail(sendEmail);
			if (response.status > 199 && response.status < 300) {
				getData();
			} else {
				notification.error({ message: "Có lỗi xảy ra, vui lòng thử lại" });
			}
		} catch (error) {
			notification.error({ message: "Có lỗi xảy ra, vui lòng thử lại" });
		}
	};

	onMounted(() => {
		getData();
	});
</script>
