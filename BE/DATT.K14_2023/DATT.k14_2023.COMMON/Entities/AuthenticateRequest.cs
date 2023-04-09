using DATT.k14_2023.COMMON.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// Mật khẩu
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Role
        /// </summary>
        [Required]
        public int Role { get; set; }
    }
}
