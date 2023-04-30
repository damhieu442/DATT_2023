using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.k14_2023.COMMON.Enums
{
    /// <summary>
    /// Phân quyền
    /// </summary>
    public enum Role
    {
        /// <summary>
        /// tk khách
        /// </summary>
        user = 0,

        /// <summary>
        /// tk quản trị 
        /// </summary>
        Admin = 1,

        /// <summary>
        /// tk nhân viên
        /// </summary>
        staff = 2,
    }
}
