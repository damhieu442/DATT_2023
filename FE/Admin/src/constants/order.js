export const keys = {
	deliverying: 1,
	processing: 0,
	complete: 2,
	rejected: 3,
};

export const COLORS = {
	[keys.deliverying]: "#22c55e",
	[keys.processing]: "#22d3ee",
	[keys.complete]: "#14b8a6",
	[keys.rejected]: "#ef4444",
};

export default {
	[keys.deliverying]: "Đang giao hàng",
	[keys.processing]: "Đang xử lý",
	[keys.complete]: "Hoàn tất",
	[keys.rejected]: "Đã hủy ",
};

export const EPaymentMethod = {
	OFFLINE: 0,
	ONLINE: 1,
};

export const PaymentMethodLabels = ["Trực tiếp", "Online"];
