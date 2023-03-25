using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.k14_2023.COMMON.Entities
{
    public class AuthenticateRequest
    {
        /// <summary>
        /// tk đăng nhập
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Mật khẩu
        /// </summary>
        public string Password { get; set; }
    }
}
