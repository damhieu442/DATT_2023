using DATT.k14_2023.COMMON.Entities.DTO;
using DATT.k14_2023.COMMON;
using DATT.K14_2023.BL.IBaseBL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DATT.k14_2023.COMMON.Enums;
using System.IO;
using DATT.k14_2023.COMMON.ViewModel;

namespace DATT.K14_2023.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasesController<T> : ControllerBase
    {
        #region Filed
        private IBaseBL<T> _baseBL;
        #endregion

        #region Constructor
        public BasesController(IBaseBL<T> baseBL)
        {
            _baseBL = baseBL;
        }
        #endregion

        #region Method
        /// <summary>
        /// API lấy toàn bộ dữ liệu 
        /// </summary>
        /// <returns>
        /// Danh sách bản ghi nếu thành công
        /// Status500 nếu thất bại
        /// </returns>
        /// Created By: DVHIEU (23/03/2023)
        [HttpGet]
        public IActionResult GetRecordAll()
        {
            try
            {
                dynamic records = _baseBL.GetRecordAll();

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

        /// <summary>
        /// API lấy dữ liệu theo bộ lọc và phân trang
        /// </summary>
        /// <param name="pageSize">Số bản ghi trên một trang</param>
        /// <param name="pageNumber">Trang hiện tại</param>
        /// <param name="keyWord">Từ khóa để search</param>
        /// <param name="minPrice">Giá bé nhất</param>
        /// <param name="maxPrice">Giá lớn nhất</param>
        /// <returns>
        /// Danh sách bản ghi nếu thành công
        /// Status500 nếu thất bại
        /// </returns>
        /// Created By: DVHIEU (23/03/2023)
        [HttpPost("PagingAndFilter")]
        public IActionResult GetRecordByFilterAndPaging(
            [FromQuery] string? keyWord,
            [FromBody] List<CustomParams>? customParams,
            [FromQuery] long? minPrice,
            [FromQuery] long? maxPrice,
            [FromQuery] long? CategoryCode,
            [FromQuery] int pageSize = 20,
            [FromQuery] int pageNumber = 1
            )
        {
            try
            {
                dynamic records = _baseBL.GetRecordByFilterAndPaging(pageSize, pageNumber, keyWord , minPrice, maxPrice, CategoryCode, customParams);
                var empty = new PagingResult<dynamic>()
                {
                    Data = new List<dynamic>()
                };
                if (records.Data.Count > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, records);
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, empty);
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

        /// <summary>
        /// API lấy dữ liệu 1 bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns>
        /// Trả về bản ghi nếu thành công
        /// Trả về lỗi nếu thất bại
        /// </returns>
        /// Created By: DVHIEU (23/03/2023)
        [HttpGet("{id}")]
        public IActionResult GetRecordById(Guid id)
        {
            try
            {
                dynamic record = _baseBL.GetRecordById(id);

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

        /// <summary>
        /// API thêm mới 1 bản ghi
        /// </summary>
        /// <param name="record">Bản ghi cần thêm</param>
        /// <returns>
        /// Trả về status201 nếu thành công
        /// Trả về lỗi nếu thất bại
        /// </returns>
        /// Created By: DVHIEU (23/03/2023)
        [HttpPost]
        public IActionResult InsertRecord([FromForm] T record)
        {
            try
            {
                string imgName = CreatedAndUpdateImg(record);
                var result = _baseBL.InsertRecord(record, imgName);

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

        [HttpPost("v2")]
        public IActionResult InsertRecordv2([FromBody] T record)
        {
            try
            {
                string imgName = CreatedAndUpdateImg(record);
                var result = _baseBL.InsertRecord(record, imgName);

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

        /// <summary>
        /// API sửa 1 bản ghi
        /// </summary>
        /// <param name="id">Id bản ghi cần sửa</param>
        /// <param name="record">đối tượng muốn sửa thành</param>
        /// <returns>
        /// Trả về status201 nếu thành công
        /// Trả về lỗi nếu thất bại
        /// </returns>
        /// Created By: DVHIEU (23/03/2023)
        [HttpPut("{id}")]
        public IActionResult UpdateRecord(Guid id, [FromForm] T record)
        {
            try
            {
                string imgName = CreatedAndUpdateImg(record);
                var result = _baseBL.UpdateRecord(id, record, imgName);

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

        [HttpPut("v2/{id}")]
        public IActionResult UpdateRecordv2(Guid id, [FromBody] T record)
        {
            try
            {
                string imgName = CreatedAndUpdateImg(record);
                var result = _baseBL.UpdateRecord(id, record, imgName);

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

        /// <summary>
        /// Hàm xử lý ảnh
        /// </summary>
        /// <param name="record">đối tượng muốn xử lý</param>
        /// <returns></returns>
        /// Created By: DVHIEU (23/03/2023)
        protected internal virtual string CreatedAndUpdateImg(T? record)
        {
            return "";
        }

        /// <summary>
        /// API xóa 1 bản ghi
        /// </summary>
        /// <param name="id">Id bản ghi cần xóa</param>
        /// <returns>
        /// Trả về status200 nếu thành công
        /// Trả về lỗi nếu thất bại
        /// </returns>
        /// Created By: DVHIEU (23/03/2023)
        [HttpDelete("{id}")]
        public IActionResult DeleteRecordOne(Guid id)
        {
            try
            {
                int numberOfAffectedRows = _baseBL.DeleteRecordOne(id);

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

        /// <summary>
        /// API xóa nhiều bản ghi
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        [HttpDelete("deleteMany")]
        public IActionResult DeleteRecordMany(List<Guid> listId)
        {
            try
            {
                int numberOfAffectedRows = _baseBL.DeleteRecordMany(listId);

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
        #endregion
    }
}
