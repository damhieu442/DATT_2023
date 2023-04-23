class OrderFactory {
	transformCategoryAPIResponseToCategory(data) {
		return {
			deliveryAddress: data.address,
			items: data.order_items.map((item) => ({
				id: item.product_id,
				name: item.product.name,
				price: item.product.price - item.product.price * item.product.sale_percent,
				img: item.product.thumbnail_url,
			})),
			note: order?.note,
			paymentMethod: data.payment_method,
			customerPhone: data.phone_number,
			state: data.status,
			totalPrice: data.amount,
			customerName: data.recipient_name,
			id: data.id,
			createTime: data.created_at,
		};
	}
}

export default new OrderFactory();
