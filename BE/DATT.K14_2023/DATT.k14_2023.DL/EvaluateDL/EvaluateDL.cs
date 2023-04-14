using Dapper;
using DATT.k14_2023.COMMON.Constants;
using DATT.k14_2023.COMMON.Entities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DATT.k14_2023.DL.EvaluateDL
{
    public class EvaluateDL : IEvaluateDL
    {
        public dynamic GetRecordAll()
        {
            string storedProcedureName = String.Format(ProcedureName.Get, typeof(Evaluate).Name, "All");

            dynamic record;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                var result = mySqlConnection.QueryMultiple(storedProcedureName, commandType: CommandType.StoredProcedure);
                record = result.Read<Evaluate>().ToList();
            }
            return record;
        }

        public dynamic GetRecordByFilterAndPaging(int pageSize, int pageNumber, string? keyword, string? queryString, string? type)
        {
            string storedProcedureName = String.Format(ProcedureName.Get, typeof(Evaluate).Name, "ByPagingAndFilter");

            var parameters = new DynamicParameters();
            parameters.Add("p_KeyWord", keyword);
            parameters.Add("p_PageSize", pageSize);
            parameters.Add("p_PageNumber", pageNumber);
            parameters.Add("p_CustomParam", queryString);
            parameters.Add("p_Type", type);

            dynamic records;
            int totalRecord;

            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                var result = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                totalRecord = result.Read<int>().Single();
                records = result.Read<Evaluate>().ToList();
            }

            List<dynamic> dataRecords = new List<dynamic>() { records, totalRecord };

            return dataRecords;
        }

        public dynamic GetRecordById(Guid id)
        {
            string storedProcedureName = String.Format(ProcedureName.GetById, typeof(Evaluate).Name);

            var parameters = new DynamicParameters();
            parameters.Add($"p_ShoeId", id);

            dynamic records;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                var result = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                records = result.Read<Evaluate>().ToList();
            }

            return records;
        }

        public int InsertRecord(Evaluate record)
        {
            string storedProcedureName = String.Format(ProcedureName.Insert, typeof(Evaluate).Name, null);

            var parameters = new DynamicParameters();
            var properties = typeof(Evaluate).GetProperties();
            foreach (var property in properties)
            {
                parameters.Add($"p_{property.Name}", property.GetValue(record));
            }
            GeneratePrimaryKeyValue(parameters, properties, null);

            int numberOfAffectedRows;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                numberOfAffectedRows = mySqlConnection.Execute(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }

            return numberOfAffectedRows;
        }

        public int DeleteRecordOne(Guid id)
        {
            string storedProcedureName = String.Format(ProcedureName.Delete, typeof(Evaluate).Name);

            var parameters = new DynamicParameters();
            var properties = typeof(Evaluate).GetProperties();
            GeneratePrimaryKeyValue(parameters, properties, id);

            int numberOfAffectedRows;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                numberOfAffectedRows = mySqlConnection.Execute(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }

            return numberOfAffectedRows;
        }

        protected virtual void GeneratePrimaryKeyValue(DynamicParameters parameters, PropertyInfo[] properties, Guid? id)
        {
            foreach (var property in properties)
            {
                var keyAttribute = (KeyAttribute?)property.GetCustomAttributes(typeof(KeyAttribute), false).FirstOrDefault();
                if (keyAttribute != null)
                {
                    if (id != null)
                    {
                        parameters.Add($"p_{property.Name}", id);
                    }
                    else
                    {
                        parameters.Add($"p_{property.Name}", Guid.NewGuid());
                    }
                }
            }
        }

        public int DeleteRecordMany(List<Guid> listId)
        {
            string sql = "DELETE FROM evaluate WHERE EvaluateId IN ('{0}')";
            int numberOfAffectedRows = 0;

            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                using (var transaction = mySqlConnection.BeginTransaction())
                {
                    try
                    {
                        numberOfAffectedRows = mySqlConnection.Execute(string.Format(sql, string.Join("','", listId)), transaction: transaction);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        transaction.Rollback();
                    }
                }
                mySqlConnection.Close();
            }

            return numberOfAffectedRows;
        }

        public List<Evaluate> ExportExcel(List<Guid> listId)
        {
            dynamic evaluate;

            if (listId.Count > 0)
            {
                string sql = "SELECT e.EvaluateId,e.Star,e.FullName,e.Email,e.Comment,e.ShoeId,s.ShoeName FROM evaluate e LEFT JOIN shoe s ON e.ShoeId = s.ShoeId WHERE EvaluateId IN ('{0}')";
                using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
                {
                    var result = mySqlConnection.QueryMultiple(string.Format(sql, string.Join("','", listId)));
                    evaluate = result.Read<Evaluate>().ToList();
                }
            }
            else
            {
                string sql = "SELECT e.EvaluateId,e.Star,e.FullName,e.Email,e.Comment,e.ShoeId,s.ShoeName FROM evaluate e LEFT JOIN shoe s ON e.ShoeId = s.ShoeId";

                using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
                {
                    var result = mySqlConnection.QueryMultiple(sql);
                    evaluate = result.Read<Evaluate>().ToList();
                }
            }

            return evaluate;
        }
    }
}
