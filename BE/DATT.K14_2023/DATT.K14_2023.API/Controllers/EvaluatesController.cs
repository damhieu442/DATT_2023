using DATT.k14_2023.COMMON.Entities.DTO;
using DATT.k14_2023.COMMON;
using DATT.K14_2023.BL.EvaluateBL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DATT.k14_2023.COMMON.ViewModel;
using DATT.k14_2023.COMMON.Entities;
using DATT.k14_2023.COMMON.Enums;

namespace DATT.K14_2023.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluatesController : ControllerBase
    {
        private IEvaluateBL _evaluateBL;

        public EvaluatesController(IEvaluateBL evaluateBL)
        {
            _evaluateBL = evaluateBL;
        }

        [HttpGet]
        public IActionResult GetRecordAll()
        {
            try
            {
                dynamic records = _evaluateBL.GetRecordAll();

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

        [HttpPost("PagingAndFilter")]
        public IActionResult GetRecordByFilterAndPaging(
            [FromQuery] string? keyWord,
            [FromQuery] int pageSize = 20,
            [FromQuery] int pageNumber = 1
            )
        {
            try
            {
                dynamic records = _evaluateBL.GetRecordByFilterAndPaging(pageSize, pageNumber, keyWord);

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
                dynamic record = _evaluateBL.GetRecordById(id);

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

        [HttpPost]
        public IActionResult InsertRecord([FromForm] Evaluate record)
        {
            try
            {
                var result = _evaluateBL.InsertRecord(record);

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
                int numberOfAffectedRows = _evaluateBL.DeleteRecordOne(id);

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
                int numberOfAffectedRows = _evaluateBL.DeleteRecordMany(listId);

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

        [HttpPost("exportv2")]
        public async Task<IActionResult> ExportV2(CancellationToken cancellationToken, List<Guid>? listId)
        {
            await Task.Yield();
            var stream = _evaluateBL.ExportExcel(listId);
            string excelName = "Bao_cao.xlsx";

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
    }
}
