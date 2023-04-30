import { category as categoryAPI } from "@/api";
import factory from "@/utils/categoryFactory";

import { ECategoriesCode, ECategoriesName } from "@/constants/Category";

const [nu, nam, treEm] = Object.keys(ECategoriesCode);
export const EMutationTypes = {
	UPDATE_CATEGORY: "UPDATE_CATEGORY",
};

const category = {
	namespaced: true,
	state: () => ({
		rootCategories: {
			nam: {
				id: "",
				code: nam,
				name: ECategoriesName.nam,
				categoryCode: undefined,
				children: [],
			},
			nu: {
				id: "",
				code: nu,
				name: ECategoriesName.nu,
				categoryCode: undefined,
				children: [],
			},
			"tre-em": {
				id: "",
				code: treEm,
				name: ECategoriesName["tre-em"],
				categoryCode: undefined,
				children: [],
			},
		},
	}),
	getters: {},
	mutations: {
		[EMutationTypes.UPDATE_CATEGORY](state, categories) {
			const categoryMap = new Map();
			categories.forEach((category) => {
				if (!category.parent) {
					switch (category.code) {
						case nam:
							state.rootCategories.nam.categoryCode = category.categoryCode;
							state.rootCategories.nam.id = category.id;
							break;
						case nu:
							state.rootCategories.nu.categoryCode = category.categoryCode;
							state.rootCategories.nu.id = category.id;
							break;
						case treEm:
							state.rootCategories["tre-em"].categoryCode = category.categoryCode;
							state.rootCategories["tre-em"].id = category.id;
							break;

						default:
							break;
					}
				}

				categoryMap.set(category.categoryCode, category);
			});

			categories.forEach((category) => {
				if (category.parent) {
					categoryMap.get(category.parent).children.push(category);
				}
			});

			state.rootCategories.nam.children = categoryMap.get(
				state.rootCategories.nam.categoryCode,
			).children;

			state.rootCategories.nu.children = categoryMap.get(
				state.rootCategories.nu.categoryCode,
			).children;

			state.rootCategories["tre-em"].children = categoryMap.get(
				state.rootCategories["tre-em"].categoryCode,
			).children;
		},
	},
	actions: {
		async getCategoriesList({ commit }) {
			try {
				const response = await categoryAPI.getList();
				const categoriesList = response.data.map(
					factory.transformCategoryAPIResponseToCategory,
				);

				commit(EMutationTypes.UPDATE_CATEGORY, categoriesList);

				return [categoriesList, undefined];
			} catch (error) {
				return [undefined, error];
			}
		},
	},
};

export default category;
