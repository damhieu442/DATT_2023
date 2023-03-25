using DATT.k14_2023.COMMON.Entities;
using DATT.K14_2023.BL.BaseBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.K14_2023.BL.CustomerBL
{
    public interface ICustomerBL : IBaseBL<Customer>
    {
        #region Method
        /// <summary>
        /// Hàm kiểm tra ảnh đã tồn tại
        /// </summary>
        /// <param name="imgName">Tên ảnh</param>
        /// <returns>
        /// 0: nếu ảnh chưa có
        /// >0: nếu có ảnh
        /// </returns>
        /// Created By: DVHIEU (23/03/2023)
        int CheckImgName(string? imgName);
        #endregion
    }
}
