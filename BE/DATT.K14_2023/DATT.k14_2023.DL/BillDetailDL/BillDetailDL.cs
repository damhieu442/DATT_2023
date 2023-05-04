using Dapper;
using DATT.k14_2023.COMMON.Constants;
using DATT.k14_2023.COMMON.Entities;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DATT.k14_2023.DL.BillDetailDL
{
    public class BillDetailDL : IBillDetailDL
    {
        public dynamic GetRecordAll(Guid id)
        {
            string storedProcedureName = "Proc_BillDetail_GetAll";

            var parameters = new DynamicParameters();
            parameters.Add("p_CustomerId", id);

            dynamic record;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                var result = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                record = result.Read<BillDetail>().ToList();
            }
            return record;
        }
        public int InsertRecord(Guid id, List<CartDetail> record)
        {
            string Proc = "Proc_BillDetail_Insert";
            dynamic billDetail = BillDetailId(id);
            Guid billId = billDetail.BillId;
            var listCartDetailId = record.Select(x => x.CartDetailId);
            string sqlDelMany = "DELETE FROM cartdetail WHERE CartDetailId IN ('{0}')";
            var parameters = new DynamicParameters();
            int numberOfAffectedRows = 0;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                foreach (var item in record)
                {
                    parameters.Add("p_BillDetailId", Guid.NewGuid());
                    parameters.Add("p_ShoeId", item.ShoeId);
                    parameters.Add("p_Quantity", item.Amount);
                    parameters.Add("p_Size", item.Size);
                    parameters.Add("p_BillId", billId);
                    numberOfAffectedRows = mySqlConnection.Execute(Proc, parameters, commandType: CommandType.StoredProcedure);
                }
                using (var transaction = mySqlConnection.BeginTransaction())
                {
                    try
                    {
                        int numberOfAffectedRow = mySqlConnection.Execute(string.Format(sqlDelMany, string.Join("','", listCartDetailId)), transaction: transaction);
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

        private dynamic BillDetailId(Guid id)
        {
            string storedProcedureName = String.Format(ProcedureName.Insert, typeof(BillDetail).Name, null);
            var parameters = new DynamicParameters();
            parameters.Add("p_CustomerId", id);
            dynamic billDetailId;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                var res = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                billDetailId = res.Read<BillDetail>().Single();
            }

            return billDetailId;
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
