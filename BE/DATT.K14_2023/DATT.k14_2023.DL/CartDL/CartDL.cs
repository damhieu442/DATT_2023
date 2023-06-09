﻿using Dapper;
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

namespace DATT.k14_2023.DL.CartDL
{
    public class CartDL : ICartDL
    {
        public int InsertRecord(Cart record)
        {
            string storedProcedureName = String.Format(ProcedureName.Insert, typeof(Cart).Name);

            var parameters = new DynamicParameters();
            var properties = typeof(Cart).GetProperties();
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
