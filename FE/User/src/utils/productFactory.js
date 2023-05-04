import dayjs from "dayjs";

const IMAGE_BASE_URL = process.env.VUE_APP_API_URL;

class ProductFactory {
	transformProductAPIResponseToCategoryProduct(data) {
		const images = data.ImgName.split(",").map((image) =>
			image ? IMAGE_BASE_URL.concat("/api/Shoes/imgName/", image.split(".")[0]) : "",
		);

		if (!images[images.length - 1]) {
			images.pop();
		}

		const [image, hoverImage] = images;

		return {
			id: data.ShoeId,
			name: data.ShoeName,
			price: data.Price,
			discount: data.Discount,
			image,
			hoverImage,
		};
	}

	transformProductAPIResponseToPromotedProduct(data) {
		const images = data.ImgName.split(",").map((image) =>
			IMAGE_BASE_URL.concat("/shoe/", image),
		);
		const [image] = images;

		const link = "/san-pham/" + data.ShoeId;

		return { id: data.ShoeId, name: data.ShoeName, price: data.Price, image, link };
	}

	transformResponseToProductDetail(data) {
		const shoeSizes = JSON.parse(data.Size);
		const images = data.ImgName.split(",").map((image) =>
			image ? IMAGE_BASE_URL.concat("/api/Shoes/imgName/", image.split(".")[0]) : "",
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
					amount: Number(size.Amount),
					sold: Number(size.SoldNumber),
				})),
				categories: [data.ParentId, data.CategoryCode],
				additionInfos: {
					sku: data.Description || "-",
					material: data.Material,
					target: "-",
					description: data.Descriptions || "-",
				},
				image: images[0],
			},
			images,
		};
	}

	/**
	 * @typedef {Object} IEvaluate
	 * @property {string} id
	 * @property {string} comment
	 * @property {string} shoeID
	 * @property {number} rate
	 * @property {Object} user
	 * @property {string} user.fullname
	 * @property {string} user.username
	 * @property {string} user.email
	 * @property {string} createAt
	 *
	 * @typedef {Object} IAPIResponse
	 * @property {string} EvaluateId
	 * @property {number} Star
	 * @property {string} FullName
	 * @property {string} Email
	 * @property {string} Comment
	 * @property {string} ShoeId
	 * @property {string} CreatedDate
	 * @property {string} CreatedBy
	 * @property {string} ShoeName
	 *
	 * @param {IAPIResponse} data
	 *
	 * @returns {IEvaluate}
	 */
	transformEvaluateResponseToEvaluate(data) {
		const createAt = dayjs(data.CreatedDate).format("DD/MM/YYYY HH:mm");

		return {
			id: data.EvaluateId,
			comment: data.Comment,
			rate: data.Star,
			shoeID: data.ShoeId,
			user: {
				email: data.Email,
				fullname: data.FullName || "",
				username: data.CreatedBy || "",
			},
			createAt,
		};
	}
}

const productFactory = new ProductFactory();

export default productFactory;
