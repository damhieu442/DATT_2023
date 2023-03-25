using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT._2023.COMMOM.Enities
{
    public class Img
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        public Guid? ImgId { get; set; }

        /// <summary>
        /// khóa phụ 
        /// </summary>
        public Guid? RecordId { get; set; }

        /// <summary>
        /// Tên ảnh
        /// </summary>
        public string? ImgName { get; set; }

        /// <summary>
        /// Đường dẫn ảnh
        /// </summary>
        public string? ImgPath { get; set; }
    }
}
