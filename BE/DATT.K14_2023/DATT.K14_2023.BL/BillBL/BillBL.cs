using DATT.k14_2023.COMMON;
using DATT.k14_2023.COMMON.Entities;
using DATT.k14_2023.COMMON.Entities.DTO;
using DATT.k14_2023.COMMON.Enums;
using DATT.k14_2023.DL.BillDL;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DATT.k14_2023.COMMON.Constants.BAttribute;

namespace DATT.K14_2023.BL.BillBL
{
    public class BillBL : IBillBL
    {
        private IBillDL _BillDL;
        public BillBL(IBillDL billDL)
        {
            _BillDL = billDL;
        }

        public int DeleteRecordMany(List<Guid> listId)
        {
            return _BillDL.DeleteRecordMany(listId);
        }

        public int DeleteRecordOne(Guid id)
        {
            return _BillDL.DeleteRecordOne(id);
        }

        public dynamic ExportExcel(List<Guid>? listId)
        {
            var listBill = _BillDL.ExportExcel(listId);

            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet 1");
                workSheet.Cells.LoadFromCollection(listBill, false);
                FomatStyleExcel(listBill, workSheet);
                package.Save();
            }

            stream.Position = 0;

            return stream;
        }

        private void FomatStyleExcel(List<Bill> listBill, ExcelWorksheet workSheet)
        {
            workSheet.DefaultColWidth = 17;
            workSheet.Cells["A:L"].Style.Font.SetFromFont("Times New Roman", 11);

            workSheet.Cells["A1:L1"].Merge = true;
            workSheet.Cells["A1:L1"].Style.Font.SetFromFont("Arial", 16);
            workSheet.Cells["A1:L1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[1, 1].Value = "DANH SÁCH ĐƠN HÀNG";
            workSheet.Cells["A2:L2"].Merge = true;
            workSheet.Cells[2, 1].Value = "";
            workSheet.Cells["A3:L3"].Style.Font.Bold = true;

            int number = listBill.Count + 3;
            workSheet.Cells[$"A3:L{number}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[$"A3:L{number}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[$"A3:L{number}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[$"A3:L{number}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            // Tạo header
            workSheet.Cells[3, 1].Value = "STT";
            workSheet.Cells[3, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[3, 2].Value = "Mã đơn";
            workSheet.Cells[3, 3].Value = "Tên người nhận";
            workSheet.Cells[3, 4].Value = "Email";
            workSheet.Cells[3, 5].Value = "Địa chỉ";
            workSheet.Cells[3, 6].Value = "Số điện thoại";
            workSheet.Cells[3, 7].Value = "Tổng tiền";
            workSheet.Cells[3, 8].Value = "Ghi chú";
            workSheet.Cells[3, 9].Value = "Lý do hủy đơn hàng";
            workSheet.Cells[3, 10].Value = "Trạng thái đơn hàng";
            workSheet.Cells[3, 11].Value = "Ngày tạo";
            workSheet.Cells[3, 12].Value = "Ngày sửa";
            workSheet.Cells["M:R"].Clear();

            using (var range = workSheet.Cells["A3:L3"])
            {
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                range.Style.Font.SetFromFont("Arial", 10);
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(200, 200, 200));
                range.Style.Font.Bold = true;
            }

            // Đẩy data vào file excel
            for (int i = 0; i < listBill.Count; i++)
            {
                workSheet.Column(i + 1).AutoFit();
                var item = listBill[i];
                workSheet.Cells[i + 4, 1].Value = i + 1;
                workSheet.Cells[i + 4, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[i + 4, 2].Value = item.BillId;
                workSheet.Cells[i + 4, 3].Value = item.FullName;
                workSheet.Cells[i + 4, 4].Value = item.Email;
                workSheet.Cells[i + 4, 5].Value = String.Format("{0}, {1}, {2}", item.Address, item.City, item.Country);
                workSheet.Cells[i + 4, 6].Value = item.PhoneNumber;
                workSheet.Cells[i + 4, 7].Value = item.TotalPrice;
                workSheet.Cells[i + 4, 8].Value = item.Note;
                workSheet.Cells[i + 4, 9].Value = item.Reason;
                string status;
                if(item.Status == 0)
                {
                    status = "Đang xử lý";
                } else if(item.Status == 1)
                {
                    status = "Đang giao hàng";
                } else if(item.Status == 2)
                {
                    status = "Hoàn thành";
                }
                else
                {
                    status = "Đã hủy";
                }
                workSheet.Cells[i + 4, 10].Value = status;
                workSheet.Cells[i + 4, 11].Value = item.CreatedDate;
                workSheet.Cells[i + 4, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[i + 4, 12].Value = item.ModifiedDate;
                workSheet.Cells[i + 4, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                workSheet.Column(1).Width = 5;

                workSheet.Cells[4, 11, listBill.Count + 4, 11].Style.Numberformat.Format = "DD/MM/YYYY";
                workSheet.Cells[4, 12, listBill.Count + 4, 12].Style.Numberformat.Format = "DD/MM/YYYY";
            }
        }

        public dynamic GetRecordAll()
        {
            return _BillDL.GetRecordAll();
        }

        public PagingResult<Bill> GetRecordByFilterAndPaging(int pageSize, int pageNumber, string? id, string? name, string? phone, int? status)
        {
            dynamic dataRecord = _BillDL.GetRecordByFilterAndPaging(pageSize, pageNumber, id, name, phone, status);
            double totalPage = Convert.ToDouble(dataRecord[1]) / pageSize;

            return new PagingResult<Bill>
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
            return _BillDL.GetRecordById(id);
        }

        public ServiceResult InsertRecord(Guid customerId, Bill record)
        {
            return HandleEventInsertAndUpdate(Guid.Empty, record, customerId);
        }

        public ServiceResult UpdateRecord(Guid id, Bill record)
        {
            return HandleEventInsertAndUpdate(id, record, Guid.Empty);
        }
        private ServiceResult HandleEventInsertAndUpdate(Guid id, Bill record, Guid ids)
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
                numberOfAffectedRows = _BillDL.InsertRecord(ids, record);
                errorCode = k14_2023.COMMON.Enums.ErrorCode.InsertFailed;
                errorMessage = Resource.Insert_Failed;
            }
            else
            {
                numberOfAffectedRows = _BillDL.UpdateRecord(id, record);
                errorCode = k14_2023.COMMON.Enums.ErrorCode.UpdateFailed;
                errorMessage = Resource.Update_Failed;
            }

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
                    ErrorCode = errorCode,
                    Message = errorMessage,
                };
            }
        }

        private List<String> ValidateRequestData(Bill? record)
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
        protected internal virtual ServiceResult ValidateCustom(Bill? record)
        {
            return new ServiceResult { IsSuccess = true };
        }

        public PagingResult<Bill> GetRecordByCustomerId(Guid id, int pageSize, int pageNumber, string? keyWord, int? status)
        {
            dynamic dataRecord = _BillDL.GetRecordByCustomerId(id, pageSize, pageNumber, keyWord, status);
            double totalPage = Convert.ToDouble(dataRecord[1]) / pageSize;

            return new PagingResult<Bill>
            {
                CurrentPage = pageNumber,
                CurrentPageRecords = pageSize,
                TotalPage = Convert.ToInt32(Math.Ceiling(totalPage)),
                TotalRecord = dataRecord[1],
                Data = dataRecord[0],
            };
        }
    }
}
