using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DATT.k14_2023.COMMON.Constants.BAttribute;

namespace DATT.k14_2023.COMMON.Entities
{
    public class FeedBack
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        public Guid? FeedBackId { get; set; }

        /// <summary>
        /// Họ và tên
        /// </summary>
        [NotEmpty("Họ và tên không được để trống")]
        public string? FullName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [NotEmpty("Email không được để trống")]
        public string? Email { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        [NotEmpty("Số điện thoại không được để trống")]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        [NotEmpty("Mô tả không được để trống")]
        public string? Description { get; set; }

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
