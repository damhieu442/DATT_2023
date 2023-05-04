import dayjs from "dayjs";

const IMAGE_BASE_URL = process.env.VUE_APP_API_URL;
class OrderAdapter {
	transformAPIResponseToOrderData(data) {
		return {
			id: data.BillId,
			name: data.FullName,
			phone: data.PhoneNumber,
			address: `${data.Address}, ${data.City}, ${data.Country}`,
			note: data.Note,
			state: data.Status,
			total: data.TotalPrice?.toLocaleString(),
			user: data.CustomerId,
			createTime: dayjs(data.CreatedDate).format("DD/MM/YYYY HH:mm"),
		};
	}

	transformAPIResponseToOrderDetailData(orderDetail, products) {
		return {
			id: orderDetail.BillId,
			name: orderDetail.FullName,
			phone: orderDetail.PhoneNumber,
			address: `${orderDetail.Address}, ${orderDetail.City}, ${orderDetail.Country}`,
			note: orderDetail.Note,
			state: orderDetail.Status,
			total: orderDetail.TotalPrice?.toLocaleString(),
			user: orderDetail.CustomerId,
			email: orderDetail.Email,
			isPayment: !!orderDetail.IsPayment,
			paymentMethod: orderDetail.PaymentMethod,
			createTime: dayjs(orderDetail.CreatedDate).format("DD/MM/YYYY HH:mm"),
			reason: orderDetail.Reason,
			orders: products.map((item) => ({
				id: item.ShoeId,
				name: item.ShoeName,
				amount: item.Quantity,
				price: item.Price.toLocaleString(),
				discount: item.Discount,
				image: IMAGE_BASE_URL.concat(
					"/api/Shoes/imgName/",
					item.ImgName.split(",")[0].split(".")[0],
				),
				size: item.Size,
				totalPrice: item.TotalPrice.toLocaleString(),
			})),
		};
	}

	generateOrderCancelEmail(order) {
		const username = order.name;
		const note = order.reason;
		const email = order.email;
		const id = order.id;
		return {
			to: email,
			subject: "Phản hồi góp ý của khách hàng",
			body: `Thân gửi ${username}
Chúng tôi rất tiếc phải thông báo với bạn rằng, đơn hàng bạn đã bị hủy
Mã đơn hàng: ${id}
Lý do: ${note}`,
		};
	}
}

export const orderAdapter = new OrderAdapter();
