using DATT.k14_2023.COMMON.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static DATT.k14_2023.COMMON.Constants.BAttribute;

namespace DATT.k14_2023.COMMON.Entities
{
    public class Customer
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Tên đăng nhập
        /// </summary>
        [NotEmpty("Tên đăng nhập không được để trống")]
        public string UserName { get; set; }

        /// <summary>
        /// Mật khẩu
        /// </summary>
        [JsonIgnore]
        [NotEmpty("Mật khẩu không được để trống")]
        public string Password { get; set; }

        /// <summary>
        /// Tên hiển thị
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
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Phân quyền
        /// </summary>
        public Role? Role { get; set; }

        /// <summary>
        /// File ảnh
        /// </summary>
        public IFormFile? Img { get; set; }

        /// <summary>
        /// Tên ảnh
        /// </summary>
        public string? ImgName { get; set; }

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
