using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DATT.k14_2023.COMMON.Constants.BAttribute;

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
        [NotEmpty("Mã giày không được để trống")]
        public string? ShoeCode { get; set; }

        /// <summary>
        /// Tên giày
        /// </summary>
        [NotEmpty("Tên giày không được để trống")]
        public string? ShoeName { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Descriptions { get; set; }

        /// <summary>
        /// Tên ảnh
        /// </summary>
        public string? ImgName { get; set; }

        /// <summary>
        /// Source ảnh
        /// </summary>
        [NotEmpty("Ảnh không được để trống")]
        public List<IFormFile> Img { get; set; }

        /// <summary>
        /// Giá bán
        /// </summary>
        [NotEmpty("Giá không được để trống")]
        public long? Price { get; set; }

        /// <summary>
        /// Giảm giá
        /// </summary>
        public int? Discount { get; set; }

        /// <summary>
        /// Giá bán sau khi giảm giá
        /// </summary>
        public long? TotalPrice { get; set; }

        /// <summary>
        /// Chất liệu
        /// </summary>
        public string? Material { get; set; }

        /// <summary>
        /// size detail
        /// </summary>
        [NotEmpty("Size không được để trống")]
        public string? Size { get; set; }

        /// <summary>
        /// Khóa phụ danh mục
        /// </summary>
        [NotEmpty("Mã danh mục không được để trống")]
        public Guid? CategoryId { get; set; }

        public int? CategoryCode { get; set; }

        public string? CategoryName { get; set; }

        public string? Description { get; set; }

        public int? ParentId { get; set; }

        public string? Code { get; set; }

        /// <summary>
        /// Số lượng đã bán
        /// </summary>
        public int? SoldNumber { get; set; }

        /// <summary>
        /// Điểm đánh giá trung bình
        /// </summary>
        public int? AvgStar { get; set; }

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
