using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.k14_2023.COMMON.Entities
{
    public class SizeDetail
    {
        public Guid SizeId { get; set; }
        public string SizeName { get; set; }
        public int Amount { get; set; }
        public int SoldNumber { get; set; }
    }
}
