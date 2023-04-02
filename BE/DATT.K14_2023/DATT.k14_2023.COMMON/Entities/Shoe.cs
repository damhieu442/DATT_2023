using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.k14_2023.COMMON.Entities
{
    public class Shoe
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        public Guid? ShoeId { get; set; }

        /// <summary>
        /// Mã giày
        /// </summary>
        public string? ShoeCode { get; set; }

        /// <summary>
        /// Tên giày
        /// </summary>
        public string? ShoeName { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Tên ảnh
        /// </summary>
        public string? ImgName { get; set; }

        /// <summary>
        /// Source ảnh
        /// </summary>
        public List<IFormFile> Img { get; set; }

        /// <summary>
        /// Giá bán
        /// </summary>
        public long? Price { get; set; }

        /// <summary>
        /// Giảm giá
        /// </summary>
        public int? Discount { get; set; }

        /// <summary>
        /// Chất liệu
        /// </summary>
        public string? Material { get; set; }

        /// <summary>
        /// Khóa phụ size
        /// </summary>
        public Guid? SizeId { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        public long? Amount { get; set; }

        /// <summary>
        /// Khóa phụ danh mục
        /// </summary>
        public Guid? CategoryId { get; set; }

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
