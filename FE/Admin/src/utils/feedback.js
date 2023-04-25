import { EFeedback, EFeedbackValueMap } from "@/constants/feedback";
import dayjs from "dayjs";

class FeedbackAdapter {
	mapFeedbacksResponseToData(feedback) {
		return {
			id: feedback.FeedBackId,
			username: feedback.FullName,
			email: feedback.Email,
			address: feedback.Address,
			content: feedback.Description,
			phone: feedback.PhoneNumber,
			note: feedback.Reply,
			status: EFeedback[feedback.Status] || EFeedback[0],
			createAt: feedback.CreatedDate
				? dayjs(feedback.CreatedDate).format("YYYY/MM/DD HH:mm")
				: "-",
			updateAt: feedback.ModifiedDate
				? dayjs(feedback.ModifiedDate).format("YYYY/MM/DD HH:mm")
				: "-",
		};
	}

	transformUpdatedFeedbackToRequestBody(feedback, updateUser) {
		return {
			FeedBackId: feedback.id,
			FullName: feedback.username,
			Email: feedback.email,
			PhoneNumber: feedback.phone,
			Address: feedback.address,
			Description: feedback.content,
			Status: Number(EFeedbackValueMap[feedback.status]) || EFeedbackValueMap["OPEN"],
			Reply: feedback.note || "",
			ModifiedBy: updateUser,
		};
	}

	generateFeedbackEmail(feedback) {
		const username = feedback.username;
		const status = feedback.status;
		const note = feedback.note;

		return {
			to: feedback.email,
			subject: "Phản hồi góp ý của khách hàng",
			body: `Thân gửi ${username}
Đơn góp ý của bạn đã được chúng tôi xem xét vào đánh giá
Trạng thái đơn: ${status}
Phản hồi: ${note}`,
		};
	}
}

export const feedbackAdapter = new FeedbackAdapter();
