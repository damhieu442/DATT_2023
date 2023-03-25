using DATT.k14_2023.COMMON.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.k14_2023.COMMON.Entities.DTO
{
    public class ServiceResult
    {
        /// <summary>
        /// Kiểm tra thành công
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Mã lỗi
        /// </summary>
        public ErrorCode? ErrorCode { get; set; }

        /// <summary>
        /// Dữ liệu lỗi
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Thông báo lỗi
        /// </summary>
        public string Message { get; set; }
    }
}
