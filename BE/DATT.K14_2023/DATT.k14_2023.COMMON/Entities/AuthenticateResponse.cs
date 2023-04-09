using DATT.k14_2023.COMMON.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.k14_2023.COMMON.Entities
{
    public class AuthenticateResponse
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid CustomerId { get; set; }

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

        /// <summary>
        /// JWT
        /// </summary>
        public string Token { get; set; }

        public AuthenticateResponse(Customer customer, string token)
        {
            CustomerId = customer.CustomerId;
            FullName = customer.FullName;
            Email = customer.Email;
            PhoneNumber = customer.PhoneNumber;
            Address = customer.Address;
            Role = customer.Role;
            ImgName = customer.ImgName;
            CreatedDate = customer.CreatedDate;
            CreatedBy = customer.CreatedBy;
            ModifiedDate = customer.ModifiedDate;
            ModifiedBy = customer.ModifiedBy;
            Token = token;
        }
    }
}
