using DATT.k14_2023.COMMON.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DATT.k14_2023.COMMON.Constants.BAttribute;

namespace DATT.k14_2023.COMMON.Entities
{
    public class Evaluate
    {
        [Key]
        public Guid? EvaluateId { get; set; }

        public Star? Star { get; set; }

        [NotEmpty("Tên không được để trống")]
        public string? FullName { get; set; }

        public string? Email { get; set; }

        [NotEmpty("Bình luận không được để trống")]
        public string? Comment { get; set; }

        public Guid? ShoeId { get; set; }

        public string? ShoeName { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? CreatedBy { get; set; }
    }
}
