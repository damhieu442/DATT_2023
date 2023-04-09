using Dapper;
using DATT.k14_2023.COMMON.Constants;
using DATT.k14_2023.COMMON.Entities;
using DATT.k14_2023.COMMON.Enums;
using DATT.k14_2023.DL.BaseDL;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.k14_2023.DL.CustomerDL
{
    public class CustomerDL : BaseDL<Customer>, ICustomerDL
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
            string storedProcedureName = String.Format(ProcedureName.Check, typeof(Customer).Name, "ImgName");

            var parameters = new DynamicParameters();
            parameters.Add("p_ImgName", imgName);

            int count;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                count = mySqlConnection.Execute(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }

            return count;
        }

        public int CheckUserName(Guid? id, string? userName)
        {
            string storedProcedureName = String.Format(ProcedureName.Check, typeof(Customer).Name, "CheckUserName");
            var parameters = new DynamicParameters();
            parameters.Add("p_CustomerId", id);
            parameters.Add("p_UserName", userName);

            int count;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                count = mySqlConnection.Execute(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }

            return count;
        }

        public dynamic isUserName(string userName, string passWord, int role)
        {
            string storedProcedureName = String.Format(ProcedureName.Check, typeof(Customer).Name, "Authen");

            var parameters = new DynamicParameters();
            parameters.Add("p_UserName", userName);
            parameters.Add("p_PassWord", passWord);
            parameters.Add("p_Role", role);

            dynamic record;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                var result = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                record = result.Read<Customer>();
            }

            return record;
        }
        #endregion
    }
}
