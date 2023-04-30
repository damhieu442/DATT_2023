import dayjs from "dayjs";
const ROOT_URL = process.env.VUE_APP_API_URL;
class OrderFactory {
	transformCategoryAPIResponseToCategory(orderData, orderList) {
		return {
			deliveryAddress: `${orderData.Address}, ${orderData.City}, ${orderData.Country}`,
			note: orderData.Note,
			paymentMethod: orderData.PaymentMethod,
			customerPhone: orderData.PhoneNumber,
			state: orderData.Status,
			totalPrice: orderData.TotalPrice.toLocaleString(),
			customerName: orderData.FullName,
			id: orderData.BillId,
			createTime: dayjs(
				orderData.CreatedDate.endsWith("Z")
					? orderData.CreatedDate
					: orderData.CreatedDate + "Z",
			).format("DD/MM/YYYY HH:mm"),
			items: orderList.map((item) => ({
				amount: item.Quantity,
				id: item.ShoeId,
				name: item.ShoeName,
				price: item.Price.toLocaleString(),
				discount: item.Discount,
				img: ROOT_URL.concat(
					"/api/Shoes/imgName/",
					item.ImgName.split(",")[0].split(".")[0],
				),
				size: item.Size,
				totalPrice: item.TotalPrice.toLocaleString(),
			})),
		};
	}
}

export const orderFactory = new OrderFactory();
