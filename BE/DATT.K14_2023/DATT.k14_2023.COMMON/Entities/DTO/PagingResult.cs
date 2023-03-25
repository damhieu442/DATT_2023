using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.k14_2023.COMMON.Entities.DTO
{
    /// <summary>
    /// Dữ liệu trả về và phân trang
    /// </summary>
    public class PagingResult
    {
        /// <summary>
        /// Số trang
        /// </summary>
        public int TotalPage { get; set; }

        /// <summary>
        /// Tổng số bản ghi phù hợp
        /// </summary>
        public int TotalRecord { get; set; }

        /// <summary>
        /// Trang hiện tại
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Số bản ghi trên 1 trang 
        /// </summary>
        public int CurrentPageRecords { get; set; }

        /// <summary>
        /// Dữ liệu trả về
        /// </summary>
        public List<dynamic> Data { get; set; }
    }
}
