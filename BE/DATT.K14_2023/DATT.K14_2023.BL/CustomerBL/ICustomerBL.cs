using DATT.k14_2023.COMMON.Entities;
using DATT.k14_2023.COMMON.Entities.DTO;
using DATT.K14_2023.BL.IBaseBL;
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
        public Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);

        public Task<object> CustomerInfo();

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

        ServiceResult UpdatePassWord(Guid id, string oldPass, string newPass);

        dynamic ExportExcel(List<Guid>? listId);
        int FogotPassword(string password);
        int UpdateToken(string email, string token, DateTime tokenDate);
        int resetPass(string email, string passWord);
        int ConfirmToken(string email, string token, DateTime date);
        #endregion
    }
}
