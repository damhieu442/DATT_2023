export const EOrderState = {
	ALL: "",
	PROCESSING: 0,
	DELIVERYING: 1,
	COMPLETE: 2,
	CANCEL: 3,
};

export const EOrderStateColors = {
	[EOrderState.DELIVERYING]: "#22c55e",
	[EOrderState.PROCESSING]: "#ff9600",
	[EOrderState.COMPLETE]: "#0500cb",
	[EOrderState.CANCEL]: "#ef4444",
};

export const OrderLabels = {
	[EOrderState.ALL]: "Tất cả",
	[EOrderState.PROCESSING]: "Đang xử lý",
	[EOrderState.DELIVERYING]: "Đang giao hàng",
	[EOrderState.COMPLETE]: "Hoàn thành",
	[EOrderState.CANCEL]: "Đã hủy",
};
