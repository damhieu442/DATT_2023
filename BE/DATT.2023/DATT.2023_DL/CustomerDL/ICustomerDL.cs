using DATT._2023.COMMOM.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT._2023.DL.Customter
{
    public interface ICustomerDL
    {
        #region Method
        /// <summary>
        /// Hàm lấy dữ liệu người dùng theo id
        /// </summary>
        /// <param name="id">id người dùng</param>
        /// <returns>
        /// Thông tin người dùng: nếu get thành công
        /// rỗng : nếu get thất bại
        /// </returns>
        /// CREATED BY: DVHIEU - 22/03/2023
        dynamic GetCustomerById(Guid id);

        /// <summary>
        /// Hàm thêm mới người dùng
        /// </summary>
        /// <param name="customter">Đối tượng người dùng thêm mới</param>
        /// <returns>
        /// 1: Nếu insert thành công
        /// 2: Nếu insert thất bại
        /// </returns>
        /// CREATED BY: DVHIEU - 22/03/2023
        int InsertCustomer(Customer customter, string? imgName);
        #endregion
    }
}
