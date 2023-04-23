using DATT.k14_2023.COMMON;
using DATT.k14_2023.COMMON.Entities.DTO;
using DATT.k14_2023.COMMON.Enums;
using DATT.k14_2023.COMMON.ViewModel;
using DATT.k14_2023.DL.BaseDL;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DATT.k14_2023.COMMON.Constants.BAttribute;

namespace DATT.K14_2023.BL.IBaseBL
{
    public class BaseBL<T> : IBaseBL<T>
    {
        #region Field
        private IBaseDL<T> _baseDL;
        #endregion

        #region Constructor
        public BaseBL(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
        }
        #endregion

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
            return _baseDL.GetRecordAll();
        }

        /// <summary>
        /// API lấy dữ liệu theo bộ lọc và phân trang
        /// </summary>
        /// <param name="pageSize">Số bản ghi trên một trang</param>
        /// <param name="pageNumber">Trang hiện tại</param>
        /// <param name="keyWord">Từ khóa để search</param>
        /// <param name="minPrice">Giá bé nhất</param>
        /// <param name="maxPrice">Giá lớn nhất</param>
        /// <returns>
        /// Danh sách bản ghi nếu thành công
        /// Status500 nếu thất bại
        /// </returns>
        /// Created By: DVHIEU (23/03/2023)
        public PagingResult<T> GetRecordByFilterAndPaging(int pageSize, int pageNumber, string? keyWord, long? minPrice, long? maxPrice, long? CategoryCode, List<CustomParams>? customParams)
        {
            dynamic param = CustomParam(customParams);
            string queryString = param[0];
            string type = param[1];

            dynamic dataRecord = _baseDL.GetRecordByFilterAndPaging(pageSize, pageNumber, keyWord, minPrice, maxPrice, CategoryCode, queryString, type);
            double totalPage = Convert.ToDouble(dataRecord[1]) / pageSize;

            return new PagingResult<T>
            {
                CurrentPage = pageNumber,
                CurrentPageRecords = pageSize,
                TotalPage = Convert.ToInt32(Math.Ceiling(totalPage)),
                TotalRecord = dataRecord[1],
                Data = dataRecord[0],
            };
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
            return _baseDL.GetRecordById(id);
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
        public ServiceResult InsertRecord(T record, string? imgName)
        {
            return HandleEventInsertAndUpdate(Guid.Empty, record, imgName);
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
        public ServiceResult UpdateRecord(Guid id, T record, string? imgName)
        {
            return HandleEventInsertAndUpdate(id, record, imgName);
        }

        /// <summary>
        /// Hàm xử lý InsertAndUpdate
        /// </summary>
        /// <param name="id">Id bản ghi cần sửa</param>
        /// <param name="record">đối tượng muốn sửa thành</param>
        /// <param name="imgName">Danh sách tên ảnh</param>
        /// <returns>
        /// Trả về status201 nếu thành công
        /// Trả về lỗi nếu thất bại
        /// </returns>
        /// Created By: DVHIEU (23/03/2023)
        private ServiceResult HandleEventInsertAndUpdate(Guid id, T record, string? imgName)
        {
            var validateResults = ValidateRequestData(record);
            if (validateResults.Count > 0)
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    ErrorCode = k14_2023.COMMON.Enums.ErrorCode.APIParameterNullOrInvalid,
                    Message = Resource.InvalidData,
                    Data = validateResults,
                };
            }

            var validateCustom = ValidateCustom(record);
            if (!validateCustom.IsSuccess)
            {
                return validateCustom;
            }

            int numberOfAffectedRows;
            ErrorCode errorCode;
            string errorMessage;

            if (id == Guid.Empty)
            {
                numberOfAffectedRows = _baseDL.InsertRecord(record, imgName);
                errorCode = k14_2023.COMMON.Enums.ErrorCode.InsertFailed;
                errorMessage = Resource.Insert_Failed;
            }
            else
            {
                numberOfAffectedRows = _baseDL.UpdateRecord(id, record, imgName);
                errorCode = k14_2023.COMMON.Enums.ErrorCode.UpdateFailed;
                errorMessage = Resource.Update_Failed;
            }

            if (numberOfAffectedRows > 0)
            {
                int number = AfterSave(id, record);
                return new ServiceResult
                {
                    IsSuccess = true,
                };
            }
            else
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    ErrorCode = errorCode,
                    Message = errorMessage,
                };
            }
        }

        protected virtual dynamic AfterSave(Guid? id, T? record)
        {
            return 0;
        }

        /// <summary>
        /// Hàm Validate chung
        /// </summary>
        /// <param name="record">Đối tượng bản ghi</param>
        /// <returns>
        /// rỗng nếu không có lỗi
        /// </returns>
        /// Created By: DVHIEU (23/03/2023)
        private List<String> ValidateRequestData(T? record)
        {
            var props = record.GetType().GetProperties();
            var validateFailures = new List<string>();

            foreach (var prop in props)
            {
                var propValue = prop.GetValue(record);
                var notEmptyAttribute = (NotEmpty)prop.GetCustomAttributes(typeof(NotEmpty), false).FirstOrDefault();
                if (notEmptyAttribute != null && propValue == null)
                {
                    validateFailures.Add(notEmptyAttribute.ErrorMessage);
                }
                else if (notEmptyAttribute != null && string.IsNullOrEmpty(propValue.ToString().Trim()))
                {
                    validateFailures.Add(notEmptyAttribute.ErrorMessage);
                }
            }

            if (validateFailures.Count > 0)
            {
                return validateFailures;
            }

            return new List<string>();
        }

        /// <summary>
        /// Hàm Validate riêng
        /// </summary>
        /// <param name="record">Đối tượng bản ghi</param>
        /// <returns>
        /// rỗng nếu không có lỗi
        /// </returns>
        /// Created By: DVHIEU (23/03/2023)
        protected internal virtual ServiceResult ValidateCustom(T? record)
        {
            return new ServiceResult { IsSuccess = true };
        }

        /// <summary>
        /// Hàm Custom filter riêng
        /// </summary>
        /// <param name="record">Đối tượng bản ghi</param>
        /// <returns>
        /// rỗng nếu không có lỗi
        /// </returns>
        /// Created By: DVHIEU (23/03/2023)
        protected internal virtual dynamic CustomParam(List<CustomParams>? customParams)
        {
            return new List<string>() { "", ""};
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
            return _baseDL.DeleteRecordOne(id);
        }

        public int DeleteRecordMany(List<Guid> listId)
        {
            return _baseDL.DeleteRecordMany(listId);
        }
        #endregion
    }
}
