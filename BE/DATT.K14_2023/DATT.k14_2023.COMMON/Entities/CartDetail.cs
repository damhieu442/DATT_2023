using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.k14_2023.COMMON.Entities
{
    public class CartDetail
    {
        [Key]
        public Guid? CartDetailId { get; set; }
        public Guid? ShoeId { get; set; }
        public string? ShoeCode { get; set; }
        public string? ShoeName { get; set; }
        public string? ImgName { get; set; }
        public long? Price { get; set; }
        public int? Discount { get; set; }
        public long? Total { get; set; }
        public long? TotalPrice { get; set; }
        public Guid? CartId { get; set; }
        public int? Amount { get; set; }
        public string? Size { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày sửa
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người sửa
        /// </summary>
        public string? ModifiedBy { get; set; }
    }
}
