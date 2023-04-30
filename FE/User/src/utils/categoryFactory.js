class CategoryFactory {
	transformCategoryAPIResponseToCategory(data) {
		return {
			id: data.CategoryId,
			code: data.Code,
			name: data.CategoryName,
			parent: data.ParentId,
			categoryCode: data.CategoryCode || data.Code,
			children: [],
		};
	}
}

export default new CategoryFactory();
