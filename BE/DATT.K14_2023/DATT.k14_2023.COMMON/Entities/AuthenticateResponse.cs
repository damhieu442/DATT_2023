using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.k14_2023.COMMON.Entities
{
    public class AuthenticateResponse
    {
        public Guid CustomerID { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(Customer customer, string token)
        {
            CustomerID = customer.CustomerId;
            Token = token;
        }
    }
}
