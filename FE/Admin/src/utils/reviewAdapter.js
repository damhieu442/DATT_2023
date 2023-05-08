import dayjs from "dayjs";

class ProductReviewAdapter {
	transformListResponseToReview(data) {
		return {
			id: data.EvaluateId,
			rating: data.Star,
			username: data.FullName,
			email: data.Email,
			comment: data.Comment,
			productId: data.ShoeId,
			productName: data.ShoeName,
			createdAt: dayjs(
				data.CreatedDate.endsWith("Z") ? data.CreatedDate : data.CreatedDate.concat("Z"),
			).format("DD/MM/YYYY HH:mm"),
			status: data.Status,
		};
	}

	transformReviewToUpdateRequestBody(item, status, user) {
		return {
			EvaluateId: item.id,
			Star: item.rating,
			FullName: item.username,
			Email: item.email,
			Comment: item.comment,
			ShoeId: item.productId,
			ShoeName: item.productName,
			CreatedDate: dayjs(item.createdAt, "DD/MM/YYYY HH:mm").toISOString(),
			Status: status,
			CreatedBy: user,
		};
	}

	generateRejectReviewEmail(review, reason) {
		const username = review.username;
		const note = reason;
		const email = review.email;
		const prodName = review.productName;

		return {
			to: email,
			subject: "Phản hồi đánh giá của khách hàng",
			body: `Thân gửi ${username}
Chúng tôi rất tiếc phải thông báo với bạn rằng, đánh giá của bạn với sản phẩm ${prodName} đã bị từ chối
Lý do: ${note}`,
		};
	}
}

export const productReviewAdapter = new ProductReviewAdapter();
