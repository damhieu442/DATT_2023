<template>
	<a-form layout="vertical" :form="form" @submit.prevent="formSubmit">
		<a-row :gutter="24">
			<a-col :span="15">
				<a-form-item label="Tìm kiếm" name="search">
					<a-input v-model:value="form.search" placeholder="Nhập tên sản phẩm">
						<template #suffix>
							<SearchOutlined />
						</template>
					</a-input>
				</a-form-item>
			</a-col>
			<a-col :span="9">
				<a-form-item label="Danh mục giày" name="category">
					<a-tree-select
						v-model:value="form.category"
						show-search
						style="width: 100%"
						placeholder="Danh mục giày"
						allow-clear
						tree-default-expand-all
						dropdownClassName="[&_.ant-select-empty]:py-8 [&_.ant-select-empty]:text-center"
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
			</a-col>
		</a-row>
		<a-row
			type="flex"
			justify="space-between"
			align="bottom"
			class="[&_.ant-form-item]:mb-0"
			:gutter="24"
		>
			<a-col :span="8">
				<a-form-item
					:label="
						'Khoảng giá: ' +
						priceFormatter(form.minPrice) +
						' - ' +
						priceFormatter(form.maxPrice)
					"
				>
					<a-slider
						v-model:value="priceRange"
						range
						:min="defaultFilterForm.minPrice"
						:max="defaultFilterForm.maxPrice"
						:step="1000"
						:tipFormatter="priceFormatter"
					/>
				</a-form-item>
			</a-col>
			<a-col :span="7">
				<a-form-item label="Sắp xếp theo">
					<a-select
						v-model:value="form.sort"
						size="middle"
						defaultValue=""
						class=""
						dropdownClassName=""
						:options="sortOptions"
					></a-select>
				</a-form-item>
			</a-col>
			<a-col :span="9">
				<a-button
					type="primary"
					class="w-full submit-btn"
					html-type="submit"
					:loading="loading"
				>
					Tìm kiếm
				</a-button>
			</a-col>
		</a-row>
	</a-form>
</template>

<script setup>
	import _cloneDeep from "lodash/cloneDeep";
	import _assign from "lodash/assign";
	import _forOwn from "lodash/forOwn";
	import { useStore } from "vuex";
	import { computed } from "@vue/reactivity";
	import { reactive } from "vue";
	import { useRoute, useRouter } from "vue-router";
	import { SearchOutlined } from "@ant-design/icons-vue";

	const emits = defineEmits(["submit"]);

	const props = defineProps({ loading: Boolean });

	const defaultFilterForm = {
		search: "",
		category: undefined,
		minPrice: 0,
		maxPrice: 10_000_000,
		sort: "",
	};

	const sortOptions = [
		{
			value: "",
			label: "Thứ tự mặc định",
		},
		{
			value: "popularity",
			label: "Thứ tự theo mức độ phổ biến",
		},
		{
			value: "rating",
			label: "Thứ tự theo điểm đánh giá",
		},
		{
			value: "date",
			label: "Mới nhất",
		},
		{
			value: "price",
			label: "Thứ tự theo giá: thấp đến cao",
		},
		{
			value: "price-desc",
			label: "Thứ tự theo giá: cao xuống thấp",
		},
	];

	const store = useStore();
	const route = useRoute();
	const router = useRouter();

	const categories = computed(() => store.state.category.categories);
	const isGettingCategories = computed(() => store.state.category.loading);
	const form = reactive(_cloneDeep(defaultFilterForm));
	const priceRange = computed({
		get() {
			return [form.minPrice, form.maxPrice];
		},

		set(range) {
			form.minPrice = range[0];
			form.maxPrice = range[1];
		},
	});

	const priceFormatter = (price) => {
		return price.toLocaleString();
	};

	const formSubmit = () => {
		const query = _assign({}, route.query);

		_forOwn(form, (filterVal, filterKey) => {
			if (filterVal) {
				query[filterKey] = filterVal;
			} else {
				delete query[filterKey];
			}
		});

		emits("submit", { ...form });

		router.push({
			query,
		});
	};
</script>

<style lang="scss" scoped>
	.submit-btn {
		@apply bg-blue-500  active:bg-blue-600 hover:bg-blue-400 border-none #{!important};
	}
</style>
