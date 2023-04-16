using DATT.k14_2023.COMMON.Entities;
using DATT.k14_2023.COMMON.Entities.DTO;
using DATT.k14_2023.COMMON.Enums;
using DATT.k14_2023.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.k14_2023.DL.CustomerDL
{
    public interface ICustomerDL : IBaseDL<Customer>
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

        int CheckUserName(Guid? id, string? userName);

        int CheckPassWord(Guid? id, string? password);

        int UpdatePassWord(Guid id, string passWord);

        dynamic isUserName(string userName, string passWord, int role);

        List<Customer> ExportExcel(List<Guid>? listId);
        #endregion
    }
}
