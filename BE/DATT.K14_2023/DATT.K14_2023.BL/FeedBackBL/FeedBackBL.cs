using DATT.k14_2023.COMMON.Entities;
using DATT.k14_2023.DL.FeedBackDL;
using DATT.K14_2023.BL.IBaseBL;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.K14_2023.BL.FeedBackBL
{
    public class FeedBackBL : BaseBL<FeedBack>, IFeedBackBL
    {
        private IFeedBackDL _FeedBackDL;
        public FeedBackBL(IFeedBackDL feedBackDL) : base(feedBackDL)
        {
            _FeedBackDL = feedBackDL;
        }
        public dynamic ExportExcel(List<Guid>? listId)
        {
            var listFeedBack = _FeedBackDL.ExportExcel(listId);

            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet 1");
                workSheet.Cells.LoadFromCollection(listFeedBack, false);
                FomatStyleExcel(listFeedBack, workSheet);
                package.Save();
            }

            stream.Position = 0;

            return stream;
        }

        private void FomatStyleExcel(List<FeedBack> listFeedBack, ExcelWorksheet workSheet)
        {
            workSheet.Cells["A:F"].Style.Font.SetFromFont("Times New Roman", 11);

            workSheet.Cells["A1:F1"].Merge = true;
            workSheet.Cells["A1:F1"].Style.Font.SetFromFont("Arial", 16);
            workSheet.Cells["A1:F1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[1, 1].Value = "DANH SÁCH PHẢN HỒI";
            workSheet.Cells["A2:F2"].Merge = true;
            workSheet.Cells[2, 1].Value = "";
            workSheet.Cells["A3:F3"].Style.Font.Bold = true;

            int number = listFeedBack.Count + 3;
            workSheet.Cells[$"A3:F{number}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[$"A3:F{number}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[$"A3:F{number}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[$"A3:F{number}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            // Tạo header
            workSheet.Cells[3, 1].Value = "STT";
            workSheet.Cells[3, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[3, 2].Value = "Họ và tên";
            workSheet.Cells[3, 3].Value = "Email";
            workSheet.Cells[3, 4].Value = "Số điện thoại";
            workSheet.Cells[3, 5].Value = "Địa chỉ";
            workSheet.Cells[3, 6].Value = "Góp ý";
            workSheet.Cells["G:J"].Clear();

            using (var range = workSheet.Cells["A3:F3"])
            {
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                range.Style.Font.SetFromFont("Arial", 10);
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(200, 200, 200));
                range.Style.Font.Bold = true;
            }

            // Đẩy data vào file excel
            for (int i = 0; i < listFeedBack.Count; i++)
            {
                workSheet.Column(i + 1).AutoFit();
                var item = listFeedBack[i];
                workSheet.Cells[i + 4, 1].Value = i + 1;
                workSheet.Cells[i + 4, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[i + 4, 2].Value = item.FullName;
                workSheet.Cells[i + 4, 3].Value = item.Email;
                workSheet.Cells[i + 4, 4].Value = item.PhoneNumber;
                workSheet.Cells[i + 4, 5].Value = item.Address;
                workSheet.Cells[i + 4, 6].Value = item.Description;
            }
        }
    }
}
