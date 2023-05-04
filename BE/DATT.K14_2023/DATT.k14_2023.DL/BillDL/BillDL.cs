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

namespace DATT.k14_2023.DL.BillDL
{
    public class BillDL : IBillDL
    {
        public int DeleteRecordMany(List<Guid> listId)
        {
            string sql = "DELETE FROM bill WHERE BillId IN ('{0}')";
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

        public int DeleteRecordOne(Guid id)
        {
            string storedProcedureName = String.Format(ProcedureName.Delete, typeof(Bill).Name);

            var parameters = new DynamicParameters();
            var properties = typeof(Bill).GetProperties();
            GeneratePrimaryKeyValue(parameters, properties, id);

            int numberOfAffectedRows;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                numberOfAffectedRows = mySqlConnection.Execute(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }

            return numberOfAffectedRows;
        }

        public List<Bill> ExportExcel(List<Guid>? listId)
        {
            dynamic bill;

            if (listId.Count > 0)
            {
                string sql = "SELECT * FROM bill WHERE BillId IN ('{0}')";
                using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
                {
                    var result = mySqlConnection.QueryMultiple(string.Format(sql, string.Join("','", listId)));
                    bill = result.Read<Bill>().ToList();
                }
            }
            else
            {
                string storedProcedureName = String.Format(ProcedureName.Get, typeof(Bill).Name, "All");

                using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
                {
                    var result = mySqlConnection.QueryMultiple(storedProcedureName, commandType: CommandType.StoredProcedure);
                    bill = result.Read<Bill>().ToList();
                }
            }
            return bill;
        }

        public dynamic GetRecordAll()
        {
            string storedProcedureName = String.Format(ProcedureName.Get, typeof(Bill).Name, "All");

            dynamic record;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                var result = mySqlConnection.QueryMultiple(storedProcedureName, commandType: CommandType.StoredProcedure);
                record = result.Read<Bill>().ToList();
            }

            return record;
        }

        public dynamic GetRecordByCustomerId(Guid id, int pageSize, int pageNumber, string? keyword, int? status)
        {
            string storedProcedureName = String.Format(ProcedureName.Get, typeof(Bill).Name, "ByCustomer");

            var parameters = new DynamicParameters();
            parameters.Add("p_KeyWord", keyword);
            parameters.Add("p_CustomerId", id);
            parameters.Add("p_Status", status);
            parameters.Add("p_PageSize", pageSize);
            parameters.Add("p_PageNumber", pageNumber);

            dynamic records;
            int totalRecord;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                var result = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                totalRecord = result.Read<int>().Single();
                records = result.Read<Bill>().ToList();
            }

            List<dynamic> dataRecords = new List<dynamic>() { records, totalRecord };

            return dataRecords;
        }

        public dynamic GetRecordByFilterAndPaging(int pageSize, int pageNumber, string? id, string? name, string? phone, int? status)
        {
            string storedProcedureName = String.Format(ProcedureName.Get, typeof(Bill).Name, "ByPagingAndFilter");

            var parameters = new DynamicParameters();
            parameters.Add("p_Id", id);
            parameters.Add("p_Name", name);
            parameters.Add("p_Phone", phone);
            parameters.Add("p_PageSize", pageSize);
            parameters.Add("p_PageNumber", pageNumber);
            parameters.Add("p_Status", status);

            dynamic records;
            int totalRecord;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                var result = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                totalRecord = result.Read<int>().Single();
                records = result.Read<Bill>().ToList();
            }

            List<dynamic> dataRecords = new List<dynamic>() { records, totalRecord };

            return dataRecords;
        }

        public dynamic GetRecordById(Guid id)
        {
            string storedProcedureName = String.Format(ProcedureName.GetById, typeof(Bill).Name);

            var parameters = new DynamicParameters();
            var properties = typeof(Bill).GetProperties();
            GeneratePrimaryKeyValue(parameters, properties, id);

            dynamic record;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                record = mySqlConnection.QueryFirstOrDefault(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }

            return record;
        }

        public int InsertRecord(Guid id, Bill record)
        {
            string storedProcedureName = String.Format(ProcedureName.Insert, typeof(Bill).Name, null);

            var parameters = new DynamicParameters();
            var properties = typeof(Bill).GetProperties();
            foreach (var property in properties)
            {
                parameters.Add($"p_{property.Name}", property.GetValue(record));
            }
            GeneratePrimaryKeyValue(parameters, properties, null);
            parameters.Add("p_CustomerId", id);

            int numberOfAffectedRows;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                numberOfAffectedRows = mySqlConnection.Execute(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }

            return numberOfAffectedRows;
        }

        public int UpdateRecord(Guid id, Bill record)
        {
            string storedProcedureName = String.Format(ProcedureName.Update, typeof(Bill).Name);

            var parameters = new DynamicParameters();
            var properties = typeof(Bill).GetProperties();
            foreach (var property in properties)
            {
                parameters.Add($"p_{property.Name}", property.GetValue(record));
            }
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
    }
}
