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

namespace DATT.k14_2023.DL.FeedBackDL
{
    public class FeedBackDL : BaseDL<FeedBack>, IFeedBackDL
    {
        protected override string queryDeleteMany()
        {
            return "DELETE FROM feedback WHERE FeedBackId IN ('{0}')";
        }

        public List<FeedBack> ExportExcel(List<Guid> listId)
        {
            dynamic feedBack;

            if (listId.Count > 0)
            {
                string sql = "SELECT * FROM feedback WHERE FeedBackId IN ('{0}')";
                using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
                {
                    var result = mySqlConnection.QueryMultiple(string.Format(sql, string.Join("','", listId)));
                    feedBack = result.Read<FeedBack>().ToList();
                }
            }
            else
            {
                string storedProcedureName = String.Format(ProcedureName.Get, typeof(FeedBack).Name, "All");

                using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
                {
                    var result = mySqlConnection.QueryMultiple(storedProcedureName, commandType: CommandType.StoredProcedure);
                    feedBack = result.Read<FeedBack>().ToList();
                }
            }
            return feedBack;
        }
    }
}
