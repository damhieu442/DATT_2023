export const EOrderState = {
	ALL: "ALL",
	PROCESSING: "PROCESSING",
	DELIVERYING: "DELIVERYING",
	COMPLETE: "COMPLETE",
	CANCEL: "CANCEL",
};

export const OrderLabels = {
	[EOrderState.ALL]: "Tất cả",
	[EOrderState.PROCESSING]: "Đang xử lý",
	[EOrderState.DELIVERYING]: "Đang giao hàng",
	[EOrderState.COMPLETE]: "Hoàn thành",
	[EOrderState.CANCEL]: "Đã hủy",
};
