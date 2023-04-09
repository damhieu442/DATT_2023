const ROOT_URL = process.env.VUE_APP_API_URL;

class ProductFactory {
	transformProductAPIResponseToCategoryProduct(data) {
		const images = data.ImgName.split(",").map((image) =>
			image ? ROOT_URL.concat("/api/Shoes/imgName/", image.split(".")[0]) : "",
		);

		if (!images[images.length - 1]) {
			images.pop();
		}

		const [image, hoverImage] = images;

		return {
			id: data.ShoeId,
			name: data.ShoeName,
			price: data.Price,
			image,
			hoverImage,
		};
	}

	transformProductAPIResponseToPromotedProduct(data) {
		const images = data.ImgName.split(",").map((image) =>
			ROOT_URL.concat("/api/Shoes/imgName/", image.split(".")[0]),
		);
		const [image] = images;

		const link = "/san-pham/" + data.ShoeId;

		return { id: data.ShoeId, name: data.ShoeName, price: data.Price, image, link };
	}

	transformResponseToProductDetail(data) {
		const shoeSizes = JSON.parse(data.Size);
		const images = data.ImgName.split(",").map((image) =>
			image ? ROOT_URL.concat("/api/Shoes/imgName/", image.split(".")[0]) : "",
		);

		console.log("Data: ", data);

		if (!images[images.length - 1]) {
			images.pop();
		}

		return {
			info: {
				id: data.ShoeId,
				name: data.ShoeName,
				price: data.Price,
				discount: data.Discount,
				totalPrice: data.TotalPrice,
				code: data.ShoeCode,
				sizes: shoeSizes.map((size) => ({
					id: size.SizeId,
					code: size.SizeName,
					amount: size.Amount,
				})),
				categories: [data.ParentId, data.CategoryCode],
				additionInfos: {
					sku: data.Description || "-",
					material: data.Material,
					target: "-",
				},
			},
			images,
		};
	}
}

const productFactory = new ProductFactory();

export default productFactory;
