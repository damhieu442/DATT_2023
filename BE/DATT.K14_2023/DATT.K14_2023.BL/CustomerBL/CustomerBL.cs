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
            var res = _customerDL.isUserName(model.Username, model.Password, model.Role)[0];

            // return null if user not found
            if (res == null) 
            { 
                return null; 
            } 

            // authentication successful so generate jwt token
            var token = generateJwtToken((Customer)res);

            return new AuthenticateResponse((Customer)res, token);
        }

        public async Task<object> CustomerInfo()
        {
            Dictionary<string, object> customerInfo = new Dictionary<string, object>();
            var customer = (Customer)_httpContextAccessor.HttpContext.Items["Customer"];
            customerInfo.Add("CustomerId", customer.CustomerId);
            
            return customerInfo;
        }

        protected override dynamic AfterSave(Guid? id, Customer record)
        {
            Cart cart = new Cart();
            cart.CartId = record.CustomerId;
            cart.CustomerId = record.CustomerId;

            return _cartDL.InsertRecord(cart);
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
        #endregion
    }
}
