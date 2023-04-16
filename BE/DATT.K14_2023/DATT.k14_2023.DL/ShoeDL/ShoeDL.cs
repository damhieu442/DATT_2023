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

        protected override string queryDeleteMany()
        {
            return "DELETE FROM shoe WHERE CustomerId IN ('{0}')";
        }

        public List<Shoe> ExportExcel(List<Guid> listId)
        {
            dynamic shoe;

            if (listId.Count > 0)
            {
                string sql = "SELECT ShoeCode, ShoeName, Descriptions, Price, Discount, (s.Price - (s.Price * (s.Discount / 100)))  AS TotalPrice, Material, SoldNumber, CategoryCode, CategoryName, s.CreatedDate, s.ModifiedDate FROM shoe s LEFT JOIN category c ON s.CategoyId = c.CategoryId WHERE CategoryId IN ('{0}')";
                using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
                {
                    var result = mySqlConnection.QueryMultiple(string.Format(sql, string.Join("','", listId)));
                    shoe = result.Read<Shoe>().ToList();
                }
            }
            else
            {
                string storedProcedureName = String.Format(ProcedureName.Get, typeof(Shoe).Name, "All");

                using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
                {
                    var result = mySqlConnection.QueryMultiple(storedProcedureName, commandType: CommandType.StoredProcedure);
                    shoe = result.Read<Shoe>().ToList();
                }
            }
            return shoe;
        }
        #endregion
    }
}
