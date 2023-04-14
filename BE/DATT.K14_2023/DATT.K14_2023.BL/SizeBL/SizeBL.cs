using DATT.k14_2023.COMMON.Entities;
using DATT.k14_2023.DL.SizeDL;
using DATT.K14_2023.BL.IBaseBL;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using OfficeOpenXml.Style;
using System.Drawing;

namespace DATT.K14_2023.BL.SizeBL
{
    public class SizeBL : BaseBL<k14_2023.COMMON.Entities.Size>, ISizeBL
    {
        private ISizeDL _sizeDL;
        public SizeBL(ISizeDL sizeDL) : base(sizeDL)
        {
            _sizeDL = sizeDL;
        }

        public dynamic ExportExcel(List<Guid>? listId)
        {
            var listSize = _sizeDL.ExportExcel(listId);

            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet 1");
                workSheet.Cells.LoadFromCollection(listSize, false);
                FomatStyleExcel(listSize, workSheet);
                package.Save();
            }

            stream.Position = 0;

            return stream;
        }

        private void FomatStyleExcel(List<k14_2023.COMMON.Entities.Size> listSize, ExcelWorksheet workSheet)
        {
            workSheet.Cells["A:B"].Style.Font.SetFromFont("Times New Roman", 11);

            workSheet.Cells["A1:B1"].Merge = true;
            workSheet.Cells["A1:B1"].Style.Font.SetFromFont("Arial", 16);
            workSheet.Cells["A1:B1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[1, 1].Value = "DANH SÁCH SIZE";
            workSheet.Cells["A2:B2"].Merge = true;
            workSheet.Cells[2, 1].Value = "";
            workSheet.Cells["A3:B3"].Style.Font.Bold = true;

            int number = listSize.Count + 3;
            workSheet.Cells[$"A3:B{number}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[$"A3:B{number}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[$"A3:B{number}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[$"A3:B{number}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            // Tạo header
            workSheet.Cells[3, 1].Value = "STT";
            workSheet.Cells[3, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[3, 2].Value = "Tên size";
            workSheet.Cells["C:F"].Clear();

            using (var range = workSheet.Cells["A3:B3"])
            {
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                range.Style.Font.SetFromFont("Arial", 10);
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(200, 200, 200));
                range.Style.Font.Bold = true;
            }

            // Đẩy data vào file excel
            for (int i = 0; i < listSize.Count; i++)
            {
                workSheet.Column(i + 1).AutoFit();
                var item = listSize[i];
                workSheet.Cells[i + 4, 1].Value = i + 1;
                workSheet.Cells[i + 4, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[i + 4, 2].Value = item.SizeName;
            }
        }
    }
}
