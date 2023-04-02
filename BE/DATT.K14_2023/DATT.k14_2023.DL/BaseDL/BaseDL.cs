
using Dapper;
using DATT.k14_2023.COMMON.Constants;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DATT.k14_2023.DL.BaseDL
{
    public class BaseDL<T> : IBaseDL<T>
    {
        #region Method
        /// <summary>
        /// API lấy toàn bộ dữ liệu 
        /// </summary>
        /// <returns>
        /// Danh sách bản ghi nếu thành công
        /// Status500 nếu thất bại
        /// </returns>
        /// Created By: DVHIEU (23/03/2023)
        public dynamic GetRecordAll()
        {
            string storedProcedureName = String.Format(ProcedureName.Get, typeof(T).Name, "All");

            dynamic record;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                var result = mySqlConnection.QueryMultiple(storedProcedureName, commandType: CommandType.StoredProcedure);
                record = result.Read<T>().ToList();
            }

            return record;
        }

        /// <summary>
        /// API lấy dữ liệu theo bộ lọc và phân trang
        /// </summary>
        /// <param name="pageSize">Số bản ghi trên một trang</param>
        /// <param name="pageNumber">Trang hiện tại</param>
        /// <param name="keyword">Từ khóa để search</param>
        /// <param name="minPrice">Giá bé nhất</param>
        /// <param name="maxPrice">Giá lớn nhất</param>
        /// <returns>
        /// Danh sách bản ghi nếu thành công
        /// Status500 nếu thất bại
        /// </returns>
        /// Created By: DVHIEU (23/03/2023)
        public dynamic GetRecordByFilterAndPaging(int pageSize, int pageNumber, string? keyword, long? minPrice, long? maxPrice, long? CategoryCode)
        {
            string storedProcedureName = String.Format(ProcedureName.Get, typeof(T).Name, "ByPagingAndFilter");

            var parameters = new DynamicParameters();
            parameters.Add("p_KeyWord", keyword);
            parameters.Add("p_PageSize", pageSize);
            parameters.Add("p_PageNumber", pageNumber);
            parameters.Add("p_MinPrice", minPrice);
            parameters.Add("p_MaxPrice", maxPrice);
            parameters.Add("p_CategoryCode", CategoryCode);

            dynamic records;
            int totalRecord;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                var result = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                totalRecord = result.Read<int>().Single();
                records = result.Read<T>().ToList();
            }

            List<dynamic> dataRecords = new List<dynamic>() { records, totalRecord };

            return dataRecords;
        }

        /// <summary>
        /// API lấy dữ liệu 1 bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns>
        /// Trả về bản ghi nếu thành công
        /// Trả về lỗi nếu thất bại
        /// </returns>
        /// Created By: DVHIEU (23/03/2023)
        public dynamic GetRecordById(Guid id)
        {
            string storedProcedureName = String.Format(ProcedureName.GetById, typeof(T).Name);

            var parameters = new DynamicParameters();
            var properties = typeof(T).GetProperties();
            GeneratePrimaryKeyValue(parameters, properties, id);

            dynamic record;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                record = mySqlConnection.QueryFirstOrDefault(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }

            return record;
        }

        /// <summary>
        /// API thêm mới 1 bản ghi
        /// </summary>
        /// <param name="record">Bản ghi cần thêm</param>
        /// <param name="imgName">Danh sách tên ảnh</param>
        /// <returns>
        /// Trả về status201 nếu thành công
        /// Trả về lỗi nếu thất bại
        /// </returns>
        /// Created By: DVHIEU (23/03/2023)
        public int InsertRecord(T record, string? imgName)
        {
            string storedProcedureName = String.Format(ProcedureName.Insert, typeof(T).Name, null);

            var parameters = new DynamicParameters();
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                if (property.Name == "ImgName")
                {
                    parameters.Add($"p_{property.Name}", imgName);
                } else if (property.Name == "Img")
                {
                    parameters.Add($"p_{property.Name}", imgName);
                }
                else
                {
                    parameters.Add($"p_{property.Name}", property.GetValue(record));
                }
            }
            GeneratePrimaryKeyValue(parameters, properties, null);

            int numberOfAffectedRows;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                numberOfAffectedRows = mySqlConnection.Execute(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }

            return numberOfAffectedRows;
        }

        /// <summary>
        /// API sửa 1 bản ghi
        /// </summary>
        /// <param name="id">Id bản ghi cần sửa</param>
        /// <param name="record">đối tượng muốn sửa thành</param>
        /// <param name="imgName">Danh sách tên ảnh</param>
        /// <returns>
        /// Trả về status201 nếu thành công
        /// Trả về lỗi nếu thất bại
        /// </returns>
        /// Created By: DVHIEU (23/03/2023)
        public int UpdateRecord(Guid id, T record, string? imgName)
        {
            string storedProcedureName = String.Format(ProcedureName.Update, typeof(T).Name);

            var parameters = new DynamicParameters();
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                if (property.Name == "ImgName")
                {
                    parameters.Add($"p_{property.Name}", imgName);
                }
                else
                {
                    parameters.Add($"p_{property.Name}", property.GetValue(record));
                }
            }
            GeneratePrimaryKeyValue(parameters, properties, id);

            int numberOfAffectedRows;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                numberOfAffectedRows = mySqlConnection.Execute(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }

            return numberOfAffectedRows;
        }

        /// <summary>
        /// API xóa 1 bản ghi
        /// </summary>
        /// <param name="id">Id bản ghi cần xóa</param>
        /// <returns>
        /// Trả về status200 nếu thành công
        /// Trả về lỗi nếu thất bại
        /// </returns>
        /// Created By: DVHIEU (23/03/2023)
        public int DeleteRecordOne(Guid id)
        {
            string storedProcedureName = String.Format(ProcedureName.Delete, typeof(T).Name);

            var parameters = new DynamicParameters();
            var properties = typeof(T).GetProperties();
            GeneratePrimaryKeyValue(parameters, properties, id);

            int numberOfAffectedRows;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                numberOfAffectedRows = mySqlConnection.Execute(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }

            return numberOfAffectedRows;
        }

        /// <summary>
        /// Sinh dữ liệu cho khóa chính
        /// </summary>
        /// <param name="parameters">Các tham số đầu vào</param>
        /// <param name="properties">Các thuộc tính của đối tượng</param>
        /// Created by: DVHIEU (08/02/2023)
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
