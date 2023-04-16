using DATT.k14_2023.COMMON;
using DATT.k14_2023.COMMON.Entities;
using DATT.k14_2023.COMMON.Entities.DTO;
using DATT.k14_2023.DL.CategoryDL;
using DATT.K14_2023.BL.IBaseBL;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.K14_2023.BL.CategoryBL
{
    public class CategoryBL : BaseBL<Category>, ICategoryBL
    {
        #region Field
        private ICategoryDL _categoryDL;
        #endregion

        #region Constructor
        public CategoryBL(ICategoryDL categoryDL) : base(categoryDL)
        {
            _categoryDL = categoryDL;
        }
        #endregion

        #region Method
        /// <summary>
        /// Hàm thực hiện validate riêng 
        /// </summary>
        /// <param name="category">đối tượng category</param>
        /// <returns>
        /// rỗng nếu không trùng mã nhân viên
        /// thông báo lỗi nếu trùng mã nhân viên
        /// </returns>
        /// CREATED BY: DVHIEU (02/04/2023)
        protected internal override ServiceResult ValidateCustom(Category? category)
        {
            int numberOfAffectedRows = _categoryDL.CheckEmployeeCode(category.CategoryCode, category.CategoryId);

            if (numberOfAffectedRows > 0)
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    ErrorCode = k14_2023.COMMON.Enums.ErrorCode.Duplicate,
                    Data = new List<string>() { string.Format(Resource.Msg_DuplicateEmpCode, category.CategoryCode) }
                };
            }

            return new ServiceResult { IsSuccess = true };
        }

        public dynamic ExportExcel(List<Guid>? listId)
        {
            var listCategory = _categoryDL.ExportExcel(listId);

            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet 1");
                workSheet.Cells.LoadFromCollection(listCategory, false);
                FomatStyleExcel(listCategory, workSheet);
                package.Save();
            }

            stream.Position = 0;

            return stream;
        }

        private void FomatStyleExcel(List<Category> listCategory, ExcelWorksheet workSheet)
        {
            workSheet.Cells["A:F"].Style.Font.SetFromFont("Times New Roman", 11);

            workSheet.Cells["A1:F1"].Merge = true;
            workSheet.Cells["A1:F1"].Style.Font.SetFromFont("Arial", 16);
            workSheet.Cells["A1:F1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[1, 1].Value = "DANH SÁCH DANH MỤC";
            workSheet.Cells["A2:F2"].Merge = true;
            workSheet.Cells[2, 1].Value = "";
            workSheet.Cells["A3:F3"].Style.Font.Bold = true;

            int number = listCategory.Count + 3;
            workSheet.Cells[$"A3:F{number}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[$"A3:F{number}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[$"A3:F{number}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[$"A3:F{number}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            // Tạo header
            workSheet.Cells[3, 1].Value = "STT";
            workSheet.Cells[3, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[3, 2].Value = "Mã danh mục";
            workSheet.Cells[3, 3].Value = "Tên danh mục";
            workSheet.Cells[3, 4].Value = "Mô tả";
            workSheet.Cells[3, 5].Value = "Ngày tạo";
            workSheet.Cells[3, 6].Value = "Ngày sửa";
            workSheet.Cells["G:L"].Clear();

            using (var range = workSheet.Cells["A3:F3"])
            {
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                range.Style.Font.SetFromFont("Arial", 10);
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(200, 200, 200));
                range.Style.Font.Bold = true;
            }

            // Đẩy data vào file excel
            for (int i = 0; i < listCategory.Count; i++)
            {
                workSheet.Column(i + 1).AutoFit();
                var item = listCategory[i];
                workSheet.Cells[i + 4, 1].Value = i + 1;
                workSheet.Cells[i + 4, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[i + 4, 2].Value = item.CategoryCode;
                workSheet.Cells[i + 4, 3].Value = item.CategoryName;
                workSheet.Cells[i + 4, 4].Value = item.Description;
                workSheet.Cells[i + 4, 5].Value = item.CreatedDate;
                workSheet.Cells[i + 4, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[i + 4, 6].Value = item.ModifiedDate;
                workSheet.Cells[i + 4, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }

            workSheet.Cells[4, 7, listCategory.Count + 4, 5].Style.Numberformat.Format = "DD/MM/YYYY";
            workSheet.Cells[4, 8, listCategory.Count + 4, 6].Style.Numberformat.Format = "DD/MM/YYYY";
        }
        #endregion
    }
}
