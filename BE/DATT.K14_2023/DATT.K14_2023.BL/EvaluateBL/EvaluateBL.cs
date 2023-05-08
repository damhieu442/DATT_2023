using DATT.k14_2023.COMMON;
using DATT.k14_2023.COMMON.Entities;
using DATT.k14_2023.COMMON.Entities.DTO;
using DATT.k14_2023.COMMON.Enums;
using DATT.k14_2023.DL.EvaluateDL;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DATT.k14_2023.COMMON.Constants.BAttribute;

namespace DATT.K14_2023.BL.EvaluateBL
{
    public class EvaluateBL : IEvaluateBL
    {
        private IEvaluateDL _evaluateDL;

        public EvaluateBL(IEvaluateDL evaluateDL)
        {
            _evaluateDL = evaluateDL;
        }

        public dynamic GetRecordAll()
        {
            return _evaluateDL.GetRecordAll();
        }

        public PagingResult<Evaluate> GetRecordByFilterAndPaging(int pageSize, int pageNumber, string? keyword, int? status)
        {
            dynamic dataRecord = _evaluateDL.GetRecordByFilterAndPaging(pageSize, pageNumber, keyword, status);
            double totalPage = Convert.ToDouble(dataRecord[1]) / pageSize;

            return new PagingResult<Evaluate>
            {
                CurrentPage = pageNumber,
                CurrentPageRecords = pageSize,
                TotalPage = Convert.ToInt32(Math.Ceiling(totalPage)),
                TotalRecord = dataRecord[1],
                Data = dataRecord[0],
            };
        }

        public dynamic GetRecordById(Guid id)
        {
            return _evaluateDL.GetRecordById(id);
        }

        public ServiceResult InsertRecord(Evaluate record)
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

            int numberOfAffectedRows = _evaluateDL.InsertRecord(record);
            if (numberOfAffectedRows > 0)
            {
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
                    ErrorCode = k14_2023.COMMON.Enums.ErrorCode.InsertFailed,
                    Message = Resource.Insert_Failed,
                };
            }
        }

        public int DeleteRecordOne(Guid id)
        {
            return _evaluateDL.DeleteRecordOne(id);
        }

        private List<String> ValidateRequestData(Evaluate? record)
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

        protected internal virtual ServiceResult ValidateCustom(Evaluate? record)
        {
            return new ServiceResult { IsSuccess = true };
        }

        public int DeleteRecordMany(List<Guid> listId)
        {
            return _evaluateDL.DeleteRecordMany(listId);
        }

        public dynamic ExportExcel(List<Guid>? listId)
        {
            var listEvaluate = _evaluateDL.ExportExcel(listId);

            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet 1");
                workSheet.Cells.LoadFromCollection(listEvaluate, false);
                FomatStyleExcel(listEvaluate, workSheet);
                package.Save();
            }

            stream.Position = 0;

            return stream;
        }

        private void FomatStyleExcel(List<Evaluate> listEvaluate, ExcelWorksheet workSheet)
        {
            workSheet.DefaultColWidth = 17;
            workSheet.Cells["A:F"].Style.Font.SetFromFont("Times New Roman", 11);

            workSheet.Cells["A1:F1"].Merge = true;
            workSheet.Cells["A1:F1"].Style.Font.SetFromFont("Arial", 16);
            workSheet.Cells["A1:F1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[1, 1].Value = "THỐNG KÊ ĐÁNH GIÁ SẢN PHẨM";
            workSheet.Cells["A2:F2"].Merge = true;
            workSheet.Cells[2, 1].Value = "";
            workSheet.Cells["A3:F3"].Style.Font.Bold = true;

            int number = listEvaluate.Count + 3;
            workSheet.Cells[$"A3:F{number}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[$"A3:F{number}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[$"A3:F{number}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[$"A3:F{number}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            // Tạo header
            workSheet.Cells[3, 1].Value = "STT";
            workSheet.Cells[3, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[3, 2].Value = "Chất lượng";
            workSheet.Cells[3, 3].Value = "Họ và tên";
            workSheet.Cells[3, 4].Value = "Email";
            workSheet.Cells[3, 5].Value = "Đánh giá";
            workSheet.Cells[3, 6].Value = "Tên sản phẩm";
            workSheet.Cells["G:I"].Clear();

            using (var range = workSheet.Cells["A3:F3"])
            {
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                range.Style.Font.SetFromFont("Arial", 10);
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(200, 200, 200));
                range.Style.Font.Bold = true;
            }

            // Đẩy data vào file excel
            for (int i = 0; i < listEvaluate.Count; i++)
            {
                workSheet.Column(i + 1).AutoFit();
                var item = listEvaluate[i];
                workSheet.Cells[i + 4, 1].Value = i + 1;
                workSheet.Cells[i + 4, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[i + 4, 2].Value = item.Star;
                workSheet.Cells[i + 4, 3].Value = item.FullName;
                workSheet.Cells[i + 4, 4].Value = item.Email;
                workSheet.Cells[i + 4, 5].Value = item.Comment;
                workSheet.Cells[i + 4, 6].Value = item.ShoeName;
            }
        }

        public ServiceResult UpdateRecord(Guid id, Evaluate record)
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
            int numberOfAffectedRows = _evaluateDL.UpdateRecord(id, record);
            if (numberOfAffectedRows > 0)
            {
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
                    ErrorCode = k14_2023.COMMON.Enums.ErrorCode.UpdateFailed,
                    Message = Resource.Update_Failed,
                };
            }
        }
    }
}
