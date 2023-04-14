<template>
	<section class="page-title">
		<a-breadcrumb class="page-title__breadcrumbs">
			<a-breadcrumb-item v-for="(item, index) in breadcrumbs" :key="index">
				<router-link v-if="item.link" :to="item.link">
					{{ item.name }}
				</router-link>
				<span v-else>
					{{ item.name }}
				</span>
			</a-breadcrumb-item>
		</a-breadcrumb>
		<div class="page-title__filter">
			<span> Hiển thị 1-12 trong 22 kết quả </span>
			<a-select
				v-model:value="seletedFilter"
				size="middle"
				style="width: 300px"
				defaultValue=""
				class="page-title__filter__dropdown"
				dropdownClassName="page-title__filter__dropdown__popup"
				:options="options"
			></a-select>
		</div>
	</section>
</template>

<script>
	import { useRoute, useRouter } from "vue-router";
	import { ECategoriesName } from "@/constants/Category.js";
	import { computed, watch } from "vue";
	import { useStore } from "vuex";

	export default {
		emits: ["filter"],
		props: {
			orderBy: String,
			breadcrumbs: { type: Array, required: false, default: null },
		},
		setup(props, context) {
			const store = useStore();
			const route = useRoute();
			const router = useRouter();

			const breadcrumbs = computed(() => {
				if (props.breadcrumbs) {
					return props.breadcrumbs;
				}

				const breadcrumbs = [
					{
						link: "/",
						name: "Trang chủ",
					},
				];

				if ("category" in route.params) {
					const rootCategory = store.state.category.rootCategories[route.params.slug];

					const category = rootCategory.children?.find?.(
						(category) => category.id === route.params.category,
					);

					breadcrumbs.push(
						{
							link: `/danh-muc/${route.params.slug}`,
							name: ECategoriesName[route.params.slug] || route.params.slug,
						},
						{
							name: category?.name || route.params.category,
						},
					);
				} else {
					breadcrumbs.push({
						name: ECategoriesName[route.params.slug] || route.params.slug,
					});
				}

				return breadcrumbs;
			});

			const filterOptions = [
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

			const seletedFilter = computed({
				get() {
					return props.orderBy;
				},

				set(value) {
					context.emit("filter", value);
				},
			});

			watch(seletedFilter, (orderBy) => {
				const query = { ...route.query };

				if (orderBy) {
					query.orderBy = orderBy;
				} else {
					delete query.orderBy;
				}

				router.push({ query });
			});

			return { breadcrumbs, options: filterOptions, seletedFilter };
		},
	};
</script>

<style lang="scss" scoped>
	.page-title {
		display: flex;
		justify-content: space-between;
		align-items: center;
		padding: 1.25rem 0;

		&__breadcrumbs {
			font-size: 1.15rem;
			color: #222;
			font-weight: bold;
			letter-spacing: 0px;
			padding: 0;
			.ant-breadcrumb-link {
				> a,
				> span {
					text-transform: uppercase;
				}
			}
		}

		&__filter {
			font-size: 1rem;

			> .ant-select {
				margin-left: 0.5rem;
				font-size: 0.97rem;
			}

			&__dropdown {
				&__popup {
					.ant-select-item {
						font-size: 1rem;
					}
				}
			}
		}
	}
</style>
