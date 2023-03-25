using DATT._2023.COMMOM.Enities;
using DATT._2023.DL.Customter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT._2023_BL.CustomerBL
{
    public class CustomerBL : ICustomerBL
    {
        #region Field
        private ICustomerDL _customerDL;
        #endregion

        #region Constructor
        public CustomerBL(ICustomerDL customerDL)
        {
            _customerDL = customerDL;
        }
        #endregion

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
        public dynamic GetCustomerById(Guid id)
        {
            dynamic customer = _customerDL.GetCustomerById(id);

            return customer;
        }

        /// <summary>
        /// Hàm thêm mới người dùng
        /// </summary>
        /// <param name="customter">Đối tượng người dùng thêm mới</param>
        /// <returns>
        /// 1: Nếu insert thành công
        /// 2: Nếu insert thất bại
        /// </returns>
        /// CREATED BY: DVHIEU - 22/03/2023
        public int InsertCustomer(Customer customter, string? imgName)
        {
            
            int numberOfAffectedRows = _customerDL.InsertCustomer(customter, imgName);

            return numberOfAffectedRows;
        }
        #endregion
    }
}
