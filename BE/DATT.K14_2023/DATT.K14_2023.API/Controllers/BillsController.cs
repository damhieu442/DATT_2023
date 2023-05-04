using DATT.k14_2023.COMMON.Entities.DTO;
using DATT.k14_2023.COMMON;
using DATT.K14_2023.BL.BillBL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DATT.k14_2023.COMMON.ViewModel;
using DATT.k14_2023.COMMON.Enums;
using DATT.k14_2023.COMMON.Entities;

namespace DATT.K14_2023.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        private IBillBL _billBL;
        public BillsController(IBillBL billBL)
        {
            _billBL = billBL;
        }
        [HttpGet]
        public IActionResult GetRecordAll()
        {
            try
            {
                dynamic records = _billBL.GetRecordAll();

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
        
        [HttpPost("exportv2")]
        public async Task<IActionResult> ExportV2(CancellationToken cancellationToken, List<Guid>? listId)
        {
            await Task.Yield();
            var stream = _billBL.ExportExcel(listId);
            string excelName = "Bao_cao.xlsx";

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
        
        [HttpPost("PagingAndFilter")]
        public IActionResult GetRecordByFilterAndPaging(
            [FromQuery] string? id,
            [FromQuery] string? name,
            [FromQuery] string? phone,
            [FromQuery] int? status,
            [FromQuery] int pageSize = 20,
            [FromQuery] int pageNumber = 1
            )
        {
            try
            {
                dynamic records = _billBL.GetRecordByFilterAndPaging(pageSize, pageNumber, id, name, phone, status);

                if (records.Data.Count > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, records);
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, new ErrorResult
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

        [HttpGet("{id}")]
        public IActionResult GetRecordById(Guid id)
        {
            try
            {
                dynamic record = _billBL.GetRecordById(id);

                if (record != null)
                {
                    return StatusCode(StatusCodes.Status200OK, record);
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

        [HttpPost("CustomerId")]
        public IActionResult GetRecordByCustomerId(
            [FromQuery] Guid CustomerId, 
            [FromQuery] string? keyWord,
            [FromQuery] int? status,
            [FromQuery] int pageSize = 20,
            [FromQuery] int pageNumber = 1)
        {
            try
            {
                dynamic records = _billBL.GetRecordByCustomerId(CustomerId, pageSize, pageNumber, keyWord, status);

                if (records.Data.Count > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, records);
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, new ErrorResult
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
        public IActionResult InsertRecord([FromBody] Bill record, Guid customerId)
        {
            try
            {
                var result = _billBL.InsertRecord(customerId, record);

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

        [HttpPut("{id}")]
        public IActionResult UpdateRecord(Guid id, [FromBody] Bill record)
        {
            try
            {
                var result = _billBL.UpdateRecord(id, record);

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
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                    {
                        ErrorCode = result.ErrorCode,
                        DevMsg = Resource.DevMsg,
                        UserMsg = Resource.UserMsg,
                        MoreInfo = result.Message,
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

        [HttpDelete("{id}")]
        public IActionResult DeleteRecordOne(Guid id)
        {
            try
            {
                int numberOfAffectedRows = _billBL.DeleteRecordOne(id);

                if (numberOfAffectedRows > 0)
                {
                    return StatusCode(StatusCodes.Status200OK);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
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
                    DevMsg = Resource.DevMsg.ToString(),
                    UserMsg = Resource.UserMsg.ToString(),
                });
            }
        }

        [HttpDelete("deleteMany")]
        public IActionResult DeleteRecordMany(List<Guid> listId)
        {
            try
            {
                int numberOfAffectedRows = _billBL.DeleteRecordMany(listId);

                if (numberOfAffectedRows == listId.Count)
                {
                    return StatusCode(StatusCodes.Status200OK, numberOfAffectedRows);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = k14_2023.COMMON.Enums.ErrorCode.Exception,
                    DevMsg = numberOfAffectedRows.ToString(),
                    UserMsg = Resource.UserMsg.ToString(),
                });
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
    }
}
