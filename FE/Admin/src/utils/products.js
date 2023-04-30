import dayjs from "dayjs";

const ROOT_URL = process.env.VUE_APP_API_URL;

class ProductFactory {
	transformResponseToProductData(data) {
		const images = data.ImgName.split(",").map((image) =>
			ROOT_URL.concat("/api/Shoes/imgName/", image.split(".")[0]),
		);
		const [image] = images;
		return {
			id: data.ShoeId,
			code: data.ShoeCode,
			name: data.ShoeName,
			image,
			price: data.Price.toLocaleString() + "đ",
			discount: data.Discount + "%",
			category: data.CategoryName,
			sold: data.SoldNumber || "-",
			rating: data.AvgStar || "-",
			createAt: data.CreatedDate ? dayjs(data.CreatedDate).format("DD/MM/YYYY HH:mm") : "-",
			createdBy: data.CreatedBy,
			updateAt: data.ModifiedDate ? dayjs(data.ModifiedDate).format("DD/MM/YYYY HH:mm") : "-",
			updatedBy: data.ModifiedBy || "-",
		};
	}

	transformProductDetailResponse(data) {
		const images = (
			data.ImgName.endsWith(",")
				? data.ImgName.slice(0, data.ImgName.length - 1)
				: data.ImgName
		)
			.split(",")
			.map((imgName) => ROOT_URL.concat("/api/Shoes/imgName/", imgName.split(".")[0]));

		const size = JSON.parse(data.Size).map((size) => ({
			id: size.SizeId,
			name: size.SizeName,
			amount: size.Amount,
			sold: size.SoldNumber,
			deletable: true,
		}));

		return {
			id: data.ShoeId,
			shoeCode: data.ShoeCode,
			name: data.ShoeName,
			description: data.Descriptions,
			images,
			price: data.Price,
			discount: data.Discount,
			material: data.Material,
			size,
			categoryCode: data.CategoryCode,
		};
	}

	getProductUpdateData({ form, files, updatedBy, createdBy }) {
		// if (!files || files.length === 0) {
		// 	const sendForm = {
		// 		ShoeCode: form.shoeCode,
		// 		ShoeName: form.name,
		// 		Descriptions: form.description,
		// 		ImgName: form.images.join(","),
		// 		Price: Number(form.price),
		// 		Discount: Number(form.discount),
		// 		Material: form.material,
		// 		Size: JSON.stringify(form.size),
		// 		CategoryCode: form.category,
		// 		ModifiedBy: updatedBy,
		// 	};
		// 	form.id && (sendForm.ShoeId = form.id);
		// 	createdBy && (sendForm.CreatedBy = createdBy);

		// 	return sendForm;
		// } else {
		const sendForm = new FormData();
		sendForm.append("ShoeCode", form.shoeCode);
		sendForm.append("ShoeName", form.name);
		sendForm.append("Descriptions", form.description);
		sendForm.append("ImgName", form.images.join(","));
		sendForm.append("Price", Number(form.price));
		sendForm.append("Discount", Number(form.discount));
		sendForm.append("Material", form.material);
		sendForm.append(
			"Size",
			JSON.stringify(
				form.size.map((size) => ({
					SizeId: size.id,
					SizeName: size.name,
					Amount: Number(size.amount),
					SoldNumber: size.sold || 0,
				})),
			),
		);
		sendForm.append("CategoryCode", form.categoryCode);
		sendForm.append("CategoryId", form.category);
		sendForm.append("ModifiedBy", updatedBy);
		form.id && sendForm.append("ShoeId", form.id);
		createdBy && sendForm.append("CreatedBy", createdBy);
		for (const file of files) {
			sendForm.append("Img", file);
		}

		return sendForm;
		// }
	}
}

export const productFactory = new ProductFactory();
