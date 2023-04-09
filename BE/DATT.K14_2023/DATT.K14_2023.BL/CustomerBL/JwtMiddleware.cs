using Dapper;
using DATT.k14_2023.COMMON.Entities;
using DATT.k14_2023.DL;
using DATT.k14_2023.DL.BaseDL;
using DATT.K14_2023.BL.CustomerBL;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.k14_2023.COMMON.Constants
{
    public class JwtMiddleware
    {
        #region Field
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;
        private ICustomerBL _customerBL;
        #endregion

        #region Constructor
        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings, ICustomerBL customerBL)
        {
            _next = next;
            _appSettings = appSettings.Value;
            _customerBL = customerBL;
        }
        #endregion

        #region Method
        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                await attachUserToContext(context, token);
            }

            await _next(context);
        }

        private async Task attachUserToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var cutomerId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "CustomerId").Value);

                // attach user to context on successful jwt validation
                //var customer = await _customerBL.GetRecordById(cutomerId);
                var sql = "SELECT * FROM customer WHERE CustomerId = @CustomerId";
                var parameters = new DynamicParameters();
                parameters.Add($"@CustomerId", cutomerId);
                using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
                {
                    var customers = await mySqlConnection.QueryAsync<Customer>(sql, parameters, commandType: CommandType.Text);
                    if(customers != null || customers.Count() > 0)
                    {
                        context.Items["Customer"] = customers.FirstOrDefault();
                    }
                    else
                    {
                        context.Items["Customer"] = null;
                    }
                }
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
        #endregion
    }
}
