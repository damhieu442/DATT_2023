using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DATT.k14_2023.COMMON.Constants.BAttribute;

namespace DATT.k14_2023.COMMON.Entities
{
    public class Size
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        public Guid? SizeId { get; set; }

        /// <summary>
        /// Tên size
        /// </summary>
        [NotEmpty("Tên size không được để trống")]
        public string? SizeName { get; set; }

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
