class HomepageAdapter {
	transformProductsToSaleProducts(product) {
		return {
			id: product.id,
			name: product.name,
			price: product.price,
			sold: product.sold,
			rating: product.rating,
			discount: product.discount,
		};
	}
}

export const homepageAdapter = new HomepageAdapter();
