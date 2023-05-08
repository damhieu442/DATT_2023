using DATT.k14_2023.COMMON.Entities.DTO;
using DATT.k14_2023.COMMON;
using DATT.K14_2023.BL.BillDetailBL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DATT.k14_2023.COMMON.Entities;
using DATT.k14_2023.COMMON.Enums;

namespace DATT.K14_2023.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillDetailsController : ControllerBase
    {
        private IBillDetailBL _BillDetailBL;

        public BillDetailsController(IBillDetailBL billDetailBL)
        {
            _BillDetailBL = billDetailBL;
        }

        [HttpGet("{billId}")]
        public IActionResult GetRecordAll(Guid billId)
        {
            try
            {
                dynamic records = _BillDetailBL.GetRecordAll(billId);

                if (records != null)
                {
                    return StatusCode(StatusCodes.Status200OK, records);
                }
                else
                {
                    return StatusCode(StatusCodes.Status204NoContent, new ErrorResult
                    {
                        ErrorCode = k14_2023.COMMON.Enums.ErrorCode.None,
                        DevMsg = Resource.DevMsg_CallAPI.ToString(),
                        UserMsg = Resource.UserMsg_CallAPI.ToString(),
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = k14_2023.COMMON.Enums.ErrorCode.Exception,
                    DevMsg = ex.Message,
                    UserMsg = Resource.UserMsg.ToString(),
                });
            }
        }

        [HttpPost("{customerId}")]
        public IActionResult InsertRecord([FromBody] List<CartDetail> record, Guid customerId)
        {
            try
            {
                var result = _BillDetailBL.InsertRecord(customerId, record);

                if (result.IsSuccess)
                {
                    return StatusCode(StatusCodes.Status201Created);
                }
                else if (!result.IsSuccess && result.ErrorCode == k14_2023.COMMON.Enums.ErrorCode.APIParameterNullOrInvalid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = result.ErrorCode,
                        DevMsg = Resource.Validate,
                        UserMsg = Resource.UserMsg,
                        MoreInfo = result.Data,
                        TraceId = HttpContext.TraceIdentifier
                    });
                }
                else if (!result.IsSuccess && result.ErrorCode == (ErrorCode?)6)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = result.ErrorCode,
                        DevMsg = "Sản phẩm đã hết hàng",
                        UserMsg = "Sản phẩm đã hết hàng",
                        MoreInfo = result.Data,
                        TraceId = HttpContext.TraceIdentifier
                    });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                    {
                        ErrorCode = result.ErrorCode,
                        DevMsg = Resource.DevMsg,
                        UserMsg = Resource.UserMsg,
                        MoreInfo = result.Data,
                        TraceId = HttpContext.TraceIdentifier
                    });
                }
            }
            catch (Exception ex)
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
    }
}
