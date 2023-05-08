<template>
	<a-form layout="vertical" :form="form" @submit.prevent="submitForm">
		<a-row :gutter="24">
			<a-col :span="16">
				<a-form-item label="Tìm kiếm" name="keyword">
					<a-input
						v-model:value="form.keyword"
						placeholder="Tìm kiếm theo tên người đánh giá, tên sản phẩm"
					/>
				</a-form-item>
			</a-col>
			<a-col :span="8">
				<a-form-item label="Trạng thái" name="status">
					<a-select v-model:value="form.status" placeholder="Chọn trạng thái đánh giá">
						<a-select-option
							v-for="status in reviewStatus"
							:key="status.value"
							:value="status.value"
						>
							{{ status.label }}
						</a-select-option>
					</a-select>
				</a-form-item>
			</a-col>
			<a-col :span="8" :offset="16">
				<a-button type="primary" class="w-full submit-btn" html-type="submit">
					Tìm kiếm
				</a-button>
			</a-col>
		</a-row>
	</a-form>
</template>

<script setup>
	import { computed, onMounted, reactive } from "vue";
	import { EReviewStatus, ReviewStatusLabels } from "@/constants/review";
	import { useRoute, useRouter } from "vue-router";

	const route = useRoute();
	const router = useRouter();

	const defaultForm = {
		keyword: "",
		status: "",
	};

	const emits = defineEmits(["submit"]);

	const form = reactive(Object.assign({}, defaultForm));

	const reviewStatus = computed(() => {
		const status = [{ value: "", label: "Tất cả" }];
		for (const statusKey in EReviewStatus) {
			status.push({
				label: ReviewStatusLabels[EReviewStatus[statusKey]],
				value: EReviewStatus[statusKey],
			});
		}

		return status;
	});

	const submitForm = () => {
		const query = Object.assign({}, route.query);

		for (const filter in form) {
			const filterValue = form[filter];

			if (filterValue !== "") {
				query[filter] = filterValue;
			} else {
				delete query[filter];
			}
		}

		router.push({ query });
		emits("submit", query);
	};

	onMounted(() => {
		const query = route.query;
		form.keyword = query.keyword || "";
		form.status = Number(query.status) || undefined;
	});
</script>

<style lang="scss" scoped>
	.submit-btn {
		@apply bg-blue-500  active:bg-blue-600 hover:bg-blue-400 border-none #{!important};
	}
</style>
