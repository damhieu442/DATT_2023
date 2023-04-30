using DATT.k14_2023.COMMON.Constants;
using DATT.k14_2023.COMMON.Entities;
using DATT.k14_2023.DL.CustomerDL;
using DATT.K14_2023.BL.IBaseBL;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using DATT.k14_2023.DL.CartDL;
using DATT.k14_2023.COMMON.Entities.DTO;
using DATT.k14_2023.COMMON;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using DATT.k14_2023.COMMON.Enums;

namespace DATT.K14_2023.BL.CustomerBL
{
    public class CustomerBL : BaseBL<Customer>, ICustomerBL
    {
        #region Field
        private ICustomerDL _customerDL;
        private readonly AppSettings _appSettings;
        private IHttpContextAccessor _httpContextAccessor;
        private ICartDL _cartDL;
        #endregion

        #region Constructor
        public CustomerBL(ICustomerDL customerDL, IHttpContextAccessor httpContextAccessor,  IOptions<AppSettings> appSettings, ICartDL cartDL) : base(customerDL)
        {
            _customerDL = customerDL;
            _appSettings = appSettings.Value;
            _httpContextAccessor = httpContextAccessor;
            _cartDL = cartDL;
        }
        #endregion

        #region Method
        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            //Kiểm tra dưới database có tài khoản này không
            var username = model.Username;
            var password = model.Password;
            var role = model.Role;
            var res = _customerDL.isUserName(username, password, role);

            // return null if user not found
            if (res.Count == 0) 
            { 
                return null; 
            } 

            // authentication successful so generate jwt token
            var token = generateJwtToken((Customer)res[0]);

            return new AuthenticateResponse((Customer)res[0], token);
        }

        public async Task<object> CustomerInfo()
        {
            Dictionary<string, object> customerInfo = new Dictionary<string, object>();
            var customer = (Customer)_httpContextAccessor.HttpContext.Items["Customer"];
            customerInfo.Add("CustomerId", customer.CustomerId);
            
            return customerInfo;
        }

        private string generateJwtToken(Customer customer)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("CustomerId", customer.CustomerId.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

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
            return _customerDL.CheckImgName(imgName);
        }

        public dynamic ExportExcel(List<Guid>? listId)
        {
            var listCustomer = _customerDL.ExportExcel(listId);

            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet 1");
                workSheet.Cells.LoadFromCollection(listCustomer, false);
                FomatStyleExcel(listCustomer, workSheet);
                package.Save();
            }

            stream.Position = 0;

            return stream;
        }

        private void FomatStyleExcel(List<Customer> listCustomer, ExcelWorksheet workSheet)
        {
            workSheet.DefaultColWidth = 17;
            workSheet.Cells["A:H"].Style.Font.SetFromFont("Times New Roman", 11);

            workSheet.Cells["A1:H1"].Merge = true;
            workSheet.Cells["A1:H1"].Style.Font.SetFromFont("Arial", 16);
            workSheet.Cells["A1:H1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[1, 1].Value = "DANH SÁCH TÀI KHOẢN NGƯỜI DÙNG";
            workSheet.Cells["A2:H2"].Merge = true;
            workSheet.Cells[2, 1].Value = "";
            workSheet.Cells["A3:H3"].Style.Font.Bold = true;

            int number = listCustomer.Count + 3;
            workSheet.Cells[$"A3:H{number}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[$"A3:H{number}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[$"A3:H{number}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[$"A3:H{number}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            // Tạo header
            workSheet.Cells[3, 1].Value = "STT";
            workSheet.Cells[3, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[3, 2].Value = "Tên đăng nhập";
            workSheet.Cells[3, 3].Value = "Họ và tên";
            workSheet.Cells[3, 4].Value = "Email";
            workSheet.Cells[3, 5].Value = "Số điện thoại";
            workSheet.Cells[3, 6].Value = "Địa chỉ";
            workSheet.Cells[3, 7].Value = "Ngày tạo";
            workSheet.Cells[3, 8].Value = "Ngày sửa";
            workSheet.Cells["I:L"].Clear();

            using (var range = workSheet.Cells["A3:H3"])
            {
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                range.Style.Font.SetFromFont("Arial", 10);
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(200, 200, 200));
                range.Style.Font.Bold = true;
            }

            // Đẩy data vào file excel
            for (int i = 0; i < listCustomer.Count; i++)
            {
                workSheet.Column(i + 1).AutoFit();
                var item = listCustomer[i];
                workSheet.Cells[i + 4, 1].Value = i + 1;
                workSheet.Cells[i + 4, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[i + 4, 2].Value = item.UserName;
                workSheet.Cells[i + 4, 3].Value = item.FullName;
                workSheet.Cells[i + 4, 4].Value = item.Email;
                workSheet.Cells[i + 4, 5].Value = item.PhoneNumber;
                workSheet.Cells[i + 4, 6].Value = item.Address;
                workSheet.Cells[i + 4, 7].Value = item.CreatedDate;
                workSheet.Cells[i + 4, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[i + 4, 8].Value = item.CreatedDate;
                workSheet.Cells[i + 4, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }

            workSheet.Cells[4, 7, listCustomer.Count + 4, 7].Style.Numberformat.Format = "DD/MM/YYYY";
            workSheet.Cells[4, 8, listCustomer.Count + 4, 8].Style.Numberformat.Format = "DD/MM/YYYY";
        }

        public ServiceResult UpdatePassWord(Guid id, string oldPass, string newPass)
        {
            int res = _customerDL.CheckPassWord(id, oldPass);

            if(res == 0)
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    ErrorCode = k14_2023.COMMON.Enums.ErrorCode.APIParameterNullOrInvalid,
                    Message = Resource.InvalidData
                };
            }

            int numberOfAffectedRows = _customerDL.UpdatePassWord(id, newPass);
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
        protected internal override ServiceResult ValidateCustom(Customer? record)
        {
            int numberOfAffectedRows = _customerDL.CheckUserName(record.CustomerId, record.UserName);
            int numberOfAffectedRow = _customerDL.CheckEmailUnique(record.CustomerId, record.Email);
            if (numberOfAffectedRows > 0)
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    ErrorCode = k14_2023.COMMON.Enums.ErrorCode.Duplicate,
                    Data = new List<string>() { string.Format(Resource.Msg_DuplicateEmpCode, record.UserName) }
                };
            }
            if (numberOfAffectedRow > 0)
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    ErrorCode = k14_2023.COMMON.Enums.ErrorCode.Duplicate,
                    Data = new List<string>() { string.Format(Resource.Msg_DuplicateEmpCode, record.Email) }
                };
            }

            return new ServiceResult { IsSuccess = true };
        }

        public int FogotPassword(string password)
        {
            return _customerDL.CheckEmail(password);
        }

        public int UpdateToken(string email, string token)
        {
            return _customerDL.UpdateToken(email, token);
        }

        public int ConfirmToken(string email, string token, DateTime date)
        {
            return _customerDL.ConfirmToken(email, token, date);
        }

        public int resetPass(string email, string passWord)
        {
            return _customerDL.resetPass(email, passWord);
        }
        #endregion
    }
}
