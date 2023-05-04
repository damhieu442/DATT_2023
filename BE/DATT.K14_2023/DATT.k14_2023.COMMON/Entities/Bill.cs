using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.k14_2023.COMMON.Entities
{
    public class Bill
    {
        [Key]
        public Guid? BillId { get; set; }
        public Guid? CustomerId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public long? TotalPrice { get; set; }
        public int? Status { get; set; }
        public int? IsPayment { get; set; }
        public int? PaymentMethod { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int? IsCancel { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Note { get; set; }
        public string? PaymentID { get; set; }
        public string? Reason { get; set; }
    }
}
