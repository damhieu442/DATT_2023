using Dapper;
using DATT.k14_2023.COMMON.Constants;
using DATT.k14_2023.COMMON.Entities;
using DATT.k14_2023.DL.BaseDL;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.k14_2023.DL.ShoeDL
{
    public class ShoeDL :  BaseDL<Shoe>, IShoeDL
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
        public int CheckImgName(string? imgName)
        {
            string storedProcedureName = String.Format(ProcedureName.Check, typeof(Shoe).Name, "ImgName");

            var parameters = new DynamicParameters();
            parameters.Add("p_ImgName", imgName);

            int count;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                count = mySqlConnection.Execute(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }

            return count;
        }
        #endregion
    }
}
