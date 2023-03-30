﻿using DATT.k14_2023.COMMON.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.k14_2023.COMMON.Entities
{
    public class Customer
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        public Guid? CustomerId { get; set; }

        /// <summary>
        /// Tên đăng nhập
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// Mật khẩu
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// Tên hiển thị
        /// </summary>
        public string? FullName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
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
        public IFormFile Img { get; set; }

        /// <summary>
        /// Tên ảnh
        /// </summary>
        public string? ImgName { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }
    }
}
