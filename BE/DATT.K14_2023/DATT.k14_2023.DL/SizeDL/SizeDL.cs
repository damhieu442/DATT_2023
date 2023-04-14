using DATT.k14_2023.COMMON.Constants;
using DATT.k14_2023.COMMON;
using DATT.k14_2023.COMMON.Entities;
using DATT.k14_2023.DL.BaseDL;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DATT.k14_2023.DL.SizeDL
{
    public class SizeDL : BaseDL<Size>, ISizeDL 
    {
        protected override string queryDeleteMany()
        {
            return "DELETE FROM size WHERE SizeId IN ('{0}')";
        }
        public List<Size> ExportExcel(List<Guid> listId)
        {
            dynamic employee;

            if (listId.Count > 0)
            {
                string sql = "SELECT * FROM size WHERE SizeId IN ('{0}')";
                using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
                {
                    var result = mySqlConnection.QueryMultiple(string.Format(sql, string.Join("','", listId)));
                    employee = result.Read<Size>().ToList();
                }
            }
            else
            {
                string storedProcedureName = String.Format(ProcedureName.Get, typeof(Size).Name, "All");

                using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
                {
                    var result = mySqlConnection.QueryMultiple(storedProcedureName, commandType: CommandType.StoredProcedure);
                    employee = result.Read<Size>().ToList();
                }
            }

            return employee;
        }
    }
}
