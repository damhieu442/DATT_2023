using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.k14_2023.COMMON.ViewModel
{
    public class CustomParams
    {
        /// <summary>
        /// Trường sắp xếp
        /// </summary>
        public string? Field { get; set; }

        /// <summary>
        /// Kiểu sắp xếp
        /// </summary>
        public string? Order { get; set; }

        /// <summary>
        /// Toán tử
        /// </summary>
        public string? operation { get; set; }

        /// <summary>
        /// Kiểu dữ liệu
        /// </summary>
        public string? DataType { get; set; }

        /// <summary>
        /// Kiểu lọc
        /// </summary>
        public string? Type { get; set; }
    }
}
