using DATT.k14_2023.COMMON.Entities;
using DATT.k14_2023.COMMON.ViewModel;
using DATT.k14_2023.DL.ShoeDL;
using DATT.K14_2023.BL.IBaseBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.K14_2023.BL.ShoeBL
{
    public class ShoeBL : BaseBL<Shoe>, IShoeBL
    {
        #region Field
        private IShoeDL _shoeDL;
        #endregion

        #region Constructor
        public ShoeBL(IShoeDL shoeDL) : base(shoeDL)
        {
            _shoeDL = shoeDL;
        }
        #endregion

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
        public int CheckImgName(string? imgName)
        {
            return _shoeDL.CheckImgName(imgName);
        }

        /// <summary>
        /// Hàm Custom filter riêng
        /// </summary>
        /// <param name="record">Đối tượng bản ghi</param>
        /// <returns>
        /// rỗng nếu không có lỗi
        /// </returns>
        /// Created By: DVHIEU (23/03/2023)
        protected internal override dynamic CustomParam(List<CustomParams>? customParams)
        {
            string sql;
            string queryString = "";
            string type = "";
            foreach (var item in customParams)
            {
                queryString =  String.Format("ORDER BY {0} {1}", item.Field, item.Order);
                type = item.Type;
            }

            return new List<dynamic>() { queryString, type };
        }
        #endregion
    }
}
