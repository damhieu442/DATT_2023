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

namespace DATT.k14_2023.DL.CategoryDL
{
    public class CategoryDL : BaseDL<Category>, ICategoryDL
    {
        #region Method
        /// <summary>
        /// Check trùng mã nhân viên
        /// </summary>
        /// <param name="CategoryCode">Mã danh mục</param>
        /// <param name="CategoryId">Id danh mục</param>
        /// <returns>
        /// 1 - trùng mã
        /// 0 - không trùng mã
        /// </returns>
        /// Created By: DVHIEU (02/04/2023)
        public int CheckEmployeeCode(int? CategoryCode, Guid? CategoryId)
        {
            string storedProcedureName = String.Format(ProcedureName.CheckCode, typeof(Category).Name, typeof(Category).Name);

            var parameters = new DynamicParameters();
            parameters.Add("p_CategoryCode", CategoryCode);
            parameters.Add("p_CategoryId", CategoryId);

            int numberOfAffectedRows;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                var result = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                numberOfAffectedRows = result.Read<int>().Single();
            }

            return numberOfAffectedRows;
        }
        #endregion
    }
}
