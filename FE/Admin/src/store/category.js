import { category as categoryAPI } from "@/api";

export const EMutationTypes = {
	UPDATE_CATEGORY: "UPDATE_CATEGORY",
};

const category = {
	namespaced: true,
	state: () => ({
		categories: [],
		loading: false,
		error: undefined,
	}),
	getters: {},
	mutations: {},
	actions: {
		async getCategoriesList({ state }) {
			try {
				state.loading = true;
				const response = await categoryAPI.getList();
				const categoriesList = response.data.map((category) => ({
					code: category.CategoryCode,
					label: category.CategoryName,
					parentId: category.ParentId,
					id: category.CategoryId,
					children: [],
				}));

				const categoryMap = new Map();

				for (const category of categoriesList) {
					categoryMap.set(category.code, category);
				}

				for (const category of categoriesList) {
					if (category.parentId) {
						categoryMap.get(category.parentId).children.push(category);
						categoryMap.get(category.code).children = undefined;

						categoryMap.delete(category.code);
					}
				}

				state.categories = Array.from(categoryMap.values());

				return [categoriesList, undefined];
			} catch (error) {
				console.log("Error: ", error);

				state.error = error;
				return [undefined, error];
			} finally {
				state.loading = false;
			}
		},
	},
};

export default category;
