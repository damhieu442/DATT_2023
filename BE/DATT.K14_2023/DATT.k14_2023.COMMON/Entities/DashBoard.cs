using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.k14_2023.COMMON.Entities
{
    public class DashBoard
    {
        public int? TotalOrder { get; set; }
        public int? ProcessingOrder { get; set; }
        public int? DeliveryingOrder { get; set; }
        public int? SuccessOrder { get; set; }
        public int? CancelOrder { get; set; }
    }
}
