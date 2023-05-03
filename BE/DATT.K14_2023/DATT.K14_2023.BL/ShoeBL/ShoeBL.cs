using DATT.k14_2023.COMMON.Entities;
using DATT.k14_2023.COMMON.ViewModel;
using DATT.k14_2023.DL.ShoeDL;
using DATT.K14_2023.BL.IBaseBL;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.K14_2023.BL.ShoeBL
{
    public class ShoeBL : BaseBL<Shoe>, IShoeBL
    {
        #region Field
        private IShoeDL _shoeDL;
        #endregion

        #region Constructor
        public ShoeBL(IShoeDL shoeDL) : base(shoeDL)
        {
            _shoeDL = shoeDL;
        }
        #endregion

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
            return _shoeDL.CheckImgName(imgName);
        }

        /// <summary>
        /// Hàm Custom filter riêng
        /// </summary>
        /// <param name="record">Đối tượng bản ghi</param>
        /// <returns>
        /// rỗng nếu không có lỗi
        /// </returns>
        /// Created By: DVHIEU (23/03/2023)
        protected internal override dynamic CustomParam(List<CustomParams>? customParams)
        {
            string sql;
            string queryString = "";
            string type = "";
            foreach (var item in customParams)
            {
                queryString =  String.Format("ORDER BY {0} {1}", item.Field, item.Order);
                type = item.Type;
            }

            return new List<dynamic>() { queryString, type };
        }
        public dynamic ExportExcel(List<Guid>? listId)
        {
            var listShoe = _shoeDL.ExportExcel(listId);

            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet 1");
                workSheet.Cells.LoadFromCollection(listShoe, false);
                FomatStyleExcel(listShoe, workSheet);
                package.Save();
            }

            stream.Position = 0;

            return stream;
        }

        private void FomatStyleExcel(List<Shoe> listShoe, ExcelWorksheet workSheet)
        {
            workSheet.DefaultColWidth = 17;
            workSheet.Cells["A:K"].Style.Font.SetFromFont("Times New Roman", 11);

            workSheet.Cells["A1:K1"].Merge = true;
            workSheet.Cells["A1:K1"].Style.Font.SetFromFont("Arial", 16);
            workSheet.Cells["A1:K1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[1, 1].Value = "DANH SÁCH GIÀY";
            workSheet.Cells["A2:K2"].Merge = true;
            workSheet.Cells[2, 1].Value = "";
            workSheet.Cells["A3:K3"].Style.Font.Bold = true;

            int number = listShoe.Count + 3;
            workSheet.Cells[$"A3:K{number}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[$"A3:K{number}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[$"A3:K{number}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[$"A3:K{number}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            // Tạo header
            workSheet.Cells[3, 1].Value = "STT";
            workSheet.Cells[3, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[3, 2].Value = "Mã giày";
            workSheet.Cells[3, 3].Value = "Tên giày";
            workSheet.Cells[3, 4].Value = "Mô tả";
            workSheet.Cells[3, 5].Value = "Giá bán";
            workSheet.Cells[3, 6].Value = "Phần trăm giảm giá";
            workSheet.Cells[3, 7].Value = "Tổng tiền sau khi giảm";
            workSheet.Cells[3, 8].Value = "Chất liệu";
            workSheet.Cells[3, 9].Value = "Số lượng bán";
            workSheet.Cells[3, 10].Value = "Ngày tạo";
            workSheet.Cells[3, 11].Value = "Ngày sửa";
            workSheet.Cells["L:W"].Clear();

            using (var range = workSheet.Cells["A3:K3"])
            {
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                range.Style.Font.SetFromFont("Arial", 10);
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(200, 200, 200));
                range.Style.Font.Bold = true;
            }

            // Đẩy data vào file excel
            for (int i = 0; i < listShoe.Count; i++)
            {
                workSheet.Column(i + 1).AutoFit();
                var item = listShoe[i];
                workSheet.Cells[i + 4, 1].Value = i + 1;
                workSheet.Cells[i + 4, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[i + 4, 2].Value = item.ShoeCode;
                workSheet.Cells[i + 4, 3].Value = item.ShoeName;
                workSheet.Cells[i + 4, 4].Value = item.Descriptions;
                workSheet.Cells[i + 4, 5].Value = item.Price;
                workSheet.Cells[i + 4, 6].Value = item.Discount;
                workSheet.Cells[i + 4, 7].Value = item.TotalPrice;
                workSheet.Cells[i + 4, 8].Value = item.Material;
                workSheet.Cells[i + 4, 9].Value = item.TotalSold;
                workSheet.Cells[i + 4, 10].Value = item.CreatedDate;
                workSheet.Cells[i + 4, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[i + 4, 11].Value = item.ModifiedDate;
                workSheet.Cells[i + 4, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }

            workSheet.Column(1).Width = 5;
            workSheet.Column(5).Width = 10;
            workSheet.Column(10).Width = 15;
            workSheet.Column(11).Width = 15;

            workSheet.Cells[4, 10, listShoe.Count + 4, 10].Style.Numberformat.Format = "DD/MM/YYYY";
            workSheet.Cells[4, 11, listShoe.Count + 4, 11].Style.Numberformat.Format = "DD/MM/YYYY";
        }

        public int UpdateSoldNumber(Guid id, int soldNumber)
        {
            return _shoeDL.UpdateSoldNumber(id, soldNumber);
        }
        #endregion
    }
}
