using Dapper;
using DATT.k14_2023.COMMON.Constants;
using DATT.k14_2023.COMMON.Entities;
using DATT.k14_2023.COMMON.Enums;
using DATT.k14_2023.DL.BaseDL;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.k14_2023.DL.CustomerDL
{
    public class CustomerDL : BaseDL<Customer>, ICustomerDL
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
            string storedProcedureName = String.Format(ProcedureName.Check, typeof(Customer).Name, "ImgName");

            var parameters = new DynamicParameters();
            parameters.Add("p_ImgName", imgName);

            int count;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                var result = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                count = result.Read<int>().Single();
            }

            return count;
        }

        public int CheckUserName(Guid? id, string? userName)
        {
            string storedProcedureName = String.Format(ProcedureName.Check, typeof(Customer).Name, "CheckUserName");
            var parameters = new DynamicParameters();
            parameters.Add("p_CustomerId", id);
            parameters.Add("p_UserName", userName);

            int count;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                var result = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                count = result.Read<int>().Single();
            }

            return count;
        }

        public int CheckPassWord(Guid? id, string? passWord)
        {
            string storedProcedureName = String.Format(ProcedureName.Check, typeof(Customer).Name, "PassWord");
            var parameters = new DynamicParameters();
            parameters.Add("p_CustomerId", id);
            parameters.Add("p_PassWord", passWord);

            int count;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                var result = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                count = result.Read<int>().Single();
            }

            return count;
        }

        public dynamic isUserName(string userName, string passWord, int role)
        {
            string storedProcedureName = String.Format(ProcedureName.Check, typeof(Customer).Name, "Authen");

            var parameters = new DynamicParameters();
            parameters.Add("p_UserName", userName);
            parameters.Add("p_PassWord", passWord);
            parameters.Add("p_Role", role);

            dynamic record;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                var result = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                record = result.Read<Customer>();
            }

            return record;
        }

        protected override string queryDeleteMany()
        {
            return "DELETE FROM customer WHERE CustomerId IN ('{0}')";
        }

        public List<Customer> ExportExcel(List<Guid> listId)
        {
            dynamic customer;

            if (listId.Count > 0)
            {
                string sql = "SELECT CustomerId, UserName, FullName, Email, PhoneNumber, Address, CreatedDate, ModifiedDate Role FROM customer WHERE CustomerId IN ('{0}')";
                using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
                {
                    var result = mySqlConnection.QueryMultiple(string.Format(sql, string.Join("','", listId)));
                    customer = result.Read<Customer>().ToList();
                }
            }
            else
            {
                string storedProcedureName = String.Format(ProcedureName.Get, typeof(Customer).Name, "All");

                using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
                {
                    var result = mySqlConnection.QueryMultiple(storedProcedureName, commandType: CommandType.StoredProcedure);
                    customer = result.Read<Customer>().ToList();
                }
            }
            return customer;
        }

        public int UpdatePassWord(Guid id, string passWord)
        {
            string storedProcedureName = String.Format("Proc_{0}_UpdatePassWord", typeof(Customer).Name);
            var parameters = new DynamicParameters();
            parameters.Add("p_CustomerId", id);
            parameters.Add("p_Password", passWord);

            int numberOfAffectedRows;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                var result = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                numberOfAffectedRows = result.Read<int>().Single();
            }

            return numberOfAffectedRows;

        }

        public int CheckEmailUnique(Guid id, string email)
        {
            string storedProcedureName = String.Format(ProcedureName.Check, typeof(Customer).Name, "EmailUnique");
            var parameters = new DynamicParameters();
            parameters.Add("p_CustomerId", id);
            parameters.Add("p_Email", email);

            int count;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                var result = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                count = result.Read<int>().Single();
            }

            return count;
        }

        public int CheckEmail(string email)
        {
            string storedProcedureName = String.Format(ProcedureName.Check, typeof(Customer).Name, "Email");
            var parameters = new DynamicParameters();
            parameters.Add("p_Email", email);

            int count;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                var result = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                count = result.Read<int>().Single();
            }

            return count;
        }

        public int UpdateToken(string email, string token, DateTime tokenDate)
        {
            string storedProcedureName = "Proc_Customer_UpdateToken";
            var parameters = new DynamicParameters();
            parameters.Add("p_Email", email);
            parameters.Add("p_Token", token);
            parameters.Add("p_TokenDate", tokenDate);

            int count;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                var result = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                count = result.Read<int>().Single();
            }

            return count;
        }

        public int ConfirmToken(string email, string token, DateTime date)
        {
            string storedProcedureName = "Proc_Customer_ConfirmToken";
            var parameters = new DynamicParameters();
            parameters.Add("p_Email", email);
            parameters.Add("p_Token", token);
            parameters.Add("p_TokenDate", date);

            int count;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                var result = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                count = result.Read<int>().Single();
            }

            return count;
        }
        #endregion
    }
}
