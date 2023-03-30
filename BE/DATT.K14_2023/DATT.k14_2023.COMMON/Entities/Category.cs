using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.k14_2023.COMMON.Entities
{
    public class Category
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        public Guid? CategoryId { get; set; }

        /// <summary>
        /// Mã danh mục
        /// </summary>
        public int? CategoryCode { get; set; }

        /// <summary>
        /// Tên danh mục
        /// </summary>
        public string? CategoryName { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Cha - con
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Cha - con
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }
    }
}
