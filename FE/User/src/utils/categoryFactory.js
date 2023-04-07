class CategoryFactory {
	transformCategoryAPIResponseToCategory(data) {
		return {
			id: data.CategoryId,
			code: data.CategoryCode,
			name: data.CategoryName,
			parent: data.ParentId,
			categoryCode: data.Code,
			children: [],
		};
	}
}

export default new CategoryFactory();
