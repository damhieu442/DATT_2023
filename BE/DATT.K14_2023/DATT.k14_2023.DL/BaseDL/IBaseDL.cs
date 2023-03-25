using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.k14_2023.DL.BaseDL
{
    public interface IBaseDL<T>
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
        dynamic GetRecordAll();

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
        dynamic GetRecordByFilterAndPaging(int pageSize, int pageNumber, string? keyword, long? minPrice, long? maxPrice);

        /// <summary>
        /// API lấy dữ liệu 1 bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns>
        /// Trả về bản ghi nếu thành công
        /// Trả về lỗi nếu thất bại
        /// </returns>
        /// Created By: DVHIEU (23/03/2023)
        dynamic GetRecordById(Guid id);

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
        int InsertRecord(T record, string? imgName);

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
        int UpdateRecord(Guid id, T record, string? imgName);

        /// <summary>
        /// API xóa 1 bản ghi
        /// </summary>
        /// <param name="id">Id bản ghi cần xóa</param>
        /// <returns>
        /// Trả về status200 nếu thành công
        /// Trả về lỗi nếu thất bại
        /// </returns>
        /// Created By: DVHIEU (23/03/2023)
        int DeleteRecordOne(Guid id);
        #endregion
    }
}
