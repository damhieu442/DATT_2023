using System;
using System.Collections.Generic;
using DATT.k14_2023.COMMON.Entities.DTO;
using DATT.k14_2023.COMMON.Enums;
using DATT.k14_2023.COMMON;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Stripe;

namespace DATT.K14_2023.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        [HttpPost]
        public IActionResult Create(PaymentIntentCreateRequest request)
        {
            try
            {
                if(request.amount != null)
                {
                    var paymentIntentService = new PaymentIntentService();
                    var paymentIntent = paymentIntentService.Create(new PaymentIntentCreateOptions
                    {
                        Amount = CalculateOrderAmount(request.amount),
                        Currency = "vnd",
                        AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                        {
                            Enabled = true,
                        },
                    });
                    return StatusCode(StatusCodes.Status200OK, paymentIntent.ClientSecret);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, false);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = ErrorCode.Exception,
                    DevMsg = ex.Message,
                    UserMsg = Resource.UserMsg,
                    TraceId = HttpContext.TraceIdentifier
                });
            }
        }
        private long CalculateOrderAmount(long amount)
        {
            // Replace this constant with a calculation of the order's amount
            // Calculate the order total on the server to prevent
            // people from directly manipulating the amount on the client
            return amount;
        }

        public class PaymentIntentCreateRequest
        {
            public long amount { get; set; }
        }
    }
}
