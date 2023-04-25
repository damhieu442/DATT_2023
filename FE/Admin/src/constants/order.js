const keys = {
    deliverying: 'deliverying',
    processing: 'processing',
    complete: 'complete',
    rejected: 'rejected',
};

export const COLORS = {
    [keys.deliverying]: '#22c55e',
    [keys.processing]: '#22d3ee',
    [keys.complete]: '#14b8a6',
    [keys.rejected]: '#ef4444',
};

export default {
    [keys.deliverying]: 'Đang giao hàng',
    [keys.processing]: 'Đang xử lý',
    [keys.complete]: 'Hoàn tất',
    [keys.rejected]: 'Đã hủy ',
};
