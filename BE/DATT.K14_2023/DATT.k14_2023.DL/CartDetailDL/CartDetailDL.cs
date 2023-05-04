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

namespace DATT.k14_2023.DL.CartDetailDL
{
    public class CartDetailDL : ICartDetailDL
    {
        public int DeleteRecordOne(Guid id)
        {
            string storedProcedureName = String.Format(ProcedureName.Delete, typeof(CartDetail).Name);

            var parameters = new DynamicParameters();
            var properties = typeof(CartDetail).GetProperties();
            GeneratePrimaryKeyValue(parameters, properties, id);

            int numberOfAffectedRows;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                numberOfAffectedRows = mySqlConnection.Execute(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }

            return numberOfAffectedRows;
        }

        public dynamic GetRecordAll(Guid id)
        {
            string storedProcedureName = String.Format(ProcedureName.Get, typeof(CartDetail).Name, "All");
            
            var parameters = new DynamicParameters();
            parameters.Add("p_CustomerId", id);

            dynamic record;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                var result = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                record = result.Read<CartDetail>().ToList();
            }
            return record;
        }

        public int InsertRecord(Guid id, CartDetail record)
        {
            string storedProcedureName = String.Format(ProcedureName.Insert, typeof(CartDetail).Name, null);

            var parameters = new DynamicParameters();
            var properties = typeof(CartDetail).GetProperties();
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

        public int UpdateRecord(Guid id, CartDetail record)
        {
            string storedProcedureName = String.Format(ProcedureName.Update, typeof(CartDetail).Name);

            var parameters = new DynamicParameters();
            var properties = typeof(CartDetail).GetProperties();
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
