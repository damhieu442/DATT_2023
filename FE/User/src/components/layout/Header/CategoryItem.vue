<template>
	<a-popover trigger="hover" overlayClassName="category-dropdown" placement="bottom">
		<router-link
			class="nav-top-link"
			:to="'/danh-muc/' + categoryCode"
			active-class="current-menu-item"
		>
			{{ categoryName }}
			<i class="fa-solid fa-angle-down icon-angle-down" style="color: #5e5e5e"></i>
		</router-link>
		<template #content>
			<ul>
				<li v-for="category in categoryChildren" :key="category.id">
					<router-link
						class="category-dropdown__link"
						:to="`/danh-muc/${categoryCode}/${category.id}`"
					>
						{{ category.name }}
					</router-link>
				</li>
			</ul>
		</template>
	</a-popover>
</template>

<script>
	import { ECategoriesName } from "@/constants/Category.js";
	import { computed } from "vue";
	import { useStore } from "vuex";

	export default {
		props: {
			categoryCode: {
				validator(cateCode) {
					return ["nam", "nu", "tre-em"].includes(cateCode);
				},
			},
		},

		setup(prop) {
			const store = useStore();

			const categoryName = computed(() => {
				return ECategoriesName[prop.categoryCode];
			});

			const category = store.state.category.rootCategories[prop.categoryCode];

			const categoryChildren = computed(() => {
				return category.children.map((category) => ({
					id: category.id,
					name: category.name,
					code: category.code,
				}));
			});

			return { categoryName, category, categoryChildren };
		},
	};
</script>

<style lang="scss">
	.category-dropdown {
		font-size: 1rem;
		.ant-popover-inner-content {
			padding: 0;
			min-width: 260px;
			box-shadow: 1px 1px 1rem rgba(0, 0, 0, 0.15);
			border: 2px solid #ddd;
			color: #777;

			ul {
				padding: 1.25rem 0;
				margin: 0;

				> li {
					margin: 0;
				}
			}
		}

		&__link {
			display: block;
			color: inherit;
			padding: 10px 20px;
			padding-top: 1rem;
			border-bottom: 1px solid #eee;

			&:hover {
				background-color: rgba(0, 0, 0, 0.03);
				color: #c30005;
			}
		}
	}
</style>
