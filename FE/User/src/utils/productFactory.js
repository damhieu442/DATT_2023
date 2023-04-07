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

	/* {
			ShoeId: "f90a2385-3e7b-4e99-b5bd-b8eff1d81c5c",
			ShoeCode: "164212C-1",
			ShoeName: "Chuck 70 Psy-Kicks Ox",
			Description: null,
			ImgName:
				"men-psy-1-300x225.jpg,women-psy-1-1-300x225.jpg,women-psy-1-2-300x225.jpg,women-psy-1-3-300x225.jpg,",
			Price: 1250000,
			Discount: 0,
			TotalPrice: 1250000.0,
			Material: "Suede",
			Size: '[{"SizeId": "6fd9fb63-898d-447b-9884-cf05715b574f","SizeName": "34","Amount": 100},{"SizeId": "5f69db40-d6b4-41e6-963d-f4a935d0a66a","SizeName": "35","Amount": "110"},{"SizeId": "7b74e988-f73d-4e44-86ee-7c6a22c845a9","SizeName": "36","Amount": "120"},{"SizeId": "7b74e988-f73d-4e44-86ee-7c6a22c845a9","SizeName": "36","Amount": "130"}]',
			CategoyId: "82128b6c-7324-409c-b2a8-d524950f99af",
			CreatedDate: "2023-04-03T21:27:27",
			CreatedBy: "DVHIEU",
			ModifiedDate: "2023-04-03T21:27:27",
			ModifiedBy: "DVHIEU",
			CategoryCode: 11005,
			CategoryName: "PSY-Kicks",
			Description: null,
			ParentId: 11000,
			Code: "/2/5/",
		}; */
	transformResponseToProductDetail(data) {
		const shoeSizes = JSON.parse(data.Size);
		const images = data.ImgName.split(",").map((image) =>
			image ? ROOT_URL.concat("/api/Shoes/imgName/", image.split(".")[0]) : "",
		);

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
			},
			images,
		};
	}
}

const productFactory = new ProductFactory();

export default productFactory;
