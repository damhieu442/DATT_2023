using Dapper;
using DATT._2023.COMMOM.Enities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DATT._2023.DL.Customter
{
    public class CustomerDL : ICustomerDL
    {
        #region Method

        /// <summary>
        /// Hàm lấy dữ liệu người dùng theo id
        /// </summary>
        /// <param name="id">id người dùng</param>
        /// <returns>
        /// Thông tin người dùng: nếu get thành công
        /// rỗng : nếu get thất bại
        /// </returns>
        /// CREATED BY: DVHIEU - 22/03/2023
        public dynamic GetCustomerById(Guid id)
        {
            string storedProcedureName = "Proc_Customer_GetById";

            var parameters = new DynamicParameters();
            var properties = typeof(Customer).GetProperties();
            GeneratePrimaryKeyValue(parameters, properties, id);

            dynamic customer;
            string connectionString = "Server=127.0.0.1;Port=3306;Database=datt_giay;Uid=root;Pwd=Vanhieu1!;";
            using (var mySqlConnection = new MySqlConnection(connectionString))
            {
                customer = mySqlConnection.QueryFirstOrDefault(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }

            return customer;
        }
        /// <summary>
        /// Hàm thêm mới người dùng
        /// </summary>
        /// <param name="customter">Đối tượng người dùng thêm mới</param>
        /// <returns>
        /// 1: Nếu insert thành công
        /// 2: Nếu insert thất bại
        /// </returns>
        /// CREATED BY: DVHIEU - 22/03/2023
        public int InsertCustomer(Customer customter, string? imgName)
        {
            string storedProcedureName = "Proc_Customer_InsertOne";

            var parameters = new DynamicParameters();
            var properties = typeof(Customer).GetProperties();
            foreach (var property in properties)
            {
                if (property.Name == "ImgName")
                {
                    parameters.Add($"p_{property.Name}", imgName);
                }
                else
                {
                    parameters.Add($"p_{property.Name}", property.GetValue(customter));
                }
            }
            GeneratePrimaryKeyValue(parameters, properties, null);

            string connectionString = "Server=127.0.0.1;Port=3306;Database=datt_giay;Uid=root;Pwd=Vanhieu1!;";
            int numberOfAffectedRows;
            using (var mySqlConnection = new MySqlConnection(connectionString))
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
        #endregion
    }
}
