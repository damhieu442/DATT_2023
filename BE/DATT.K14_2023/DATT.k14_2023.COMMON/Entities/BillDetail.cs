using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.k14_2023.COMMON.Entities
{
    public class BillDetail
    {
        [Key]
        public Guid? BillDetailId { get; set; }
        public Guid? ShoeId { get; set; }
        public Guid? BillId { get; set; }
        public int? Quantity { get; set; }
        public string? Size { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ShoeCode { get; set; }
        public string? ShoeName { get; set; }
        public string? Descriptions { get; set; }
        public string? ImgName { get; set; }
        public int? Price { get; set; }
        public int? Discount { get; set; }
        public int? Total { get; set; }
        public int? TotalPrice { get; set; }
    }
}
