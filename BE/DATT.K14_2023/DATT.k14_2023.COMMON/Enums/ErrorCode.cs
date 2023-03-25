using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.k14_2023.COMMON.Enums
{
    public enum ErrorCode
    {
        /// <summary>
        /// Lỗi server
        /// </summary>
        Exception = 0,

        /// <summary>
        /// Không có dữ liệu trả về
        /// </summary>
        None = 1,

        /// <summary>
        /// Trùng mã
        /// </summary>
        Duplicate = 2,

        /// <summary>
        /// Thêm mới thất bại
        /// </summary>
        InsertFailed = 3,

        /// <summary>
        /// Dữ liệu đầu vào không hợp lệ
        /// </summary>
        APIParameterNullOrInvalid = 4,

        /// <summary>
        /// Sửa dữ liệu thất bại
        /// </summary>
        UpdateFailed = 5,
    }
}
