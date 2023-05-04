using Dapper;
using DATT.k14_2023.COMMON.Entities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.k14_2023.DL.DashBoardDL
{
    public class DashBoardDL : IDashBoardDL
    {
        public dynamic getDashBoard()
        {
            string storedProcedureName = "Proc_dashboard_get";
            dynamic record;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                var result = mySqlConnection.QueryMultiple(storedProcedureName, commandType: CommandType.StoredProcedure);
                record = result.Read<DashBoard>().ToList();
            }

            return record;
        }

        public dynamic getDashBoardChart()
        {
            string storedProcedureName = "Proc_DashBoard_GetData";
            dynamic record;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                var result = mySqlConnection.QueryMultiple(storedProcedureName, commandType: CommandType.StoredProcedure);
                record = result.Read<Month>().ToList();
            }

            return record;
        }
    }
}
