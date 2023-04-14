using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.k14_2023.COMMON.Entities
{
    public class Cart
    {
        [Key]
        public Guid? CartId { get; set; }
        public Guid? CustomerId { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
