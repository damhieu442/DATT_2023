using DATT.k14_2023.COMMON;
using DATT.k14_2023.COMMON.Entities;
using DATT.k14_2023.COMMON.Entities.DTO;
using DATT.k14_2023.COMMON.Enums;
using DATT.K14_2023.BL.IBaseBL;
using DATT.K14_2023.BL.CustomerBL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Reflection;

namespace DATT.K14_2023.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : BasesController<Customer>
    {
        #region Field
        private ICustomerBL _customerBL;
        public static IWebHostEnvironment _webHostEnvironment;
        #endregion

        #region Constructor
        public CustomersController(ICustomerBL customerBL, IWebHostEnvironment webHostEnvironment) : base(customerBL)
        {
            _customerBL = customerBL;
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion

        #region Method
        [HttpPost("authenticate")]
        public async Task<object> Authenticate([FromBody] AuthenticateRequest model)
        {
            var response = await _customerBL.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpGet("user-info")]
        [k14_2023.COMMON.Constants.Authorize]
        public async Task<IActionResult> CustomerInfo()
        {
            try
            {
                dynamic res = await _customerBL.CustomerInfo();
                return StatusCode(StatusCodes.Status200OK, res);
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
        /// API lấy ảnh từ server
        /// </summary>
        /// <param name="imgName">Tên ảnh</param>
        /// <returns>
        /// Trả về ảnh nếu thành công
        /// Trả về lỗi nếu thất bại
        /// </returns>
        /// Created By: DVHIEU (29/03/2023)
        [HttpGet("imgName/{imgName}")]
        public IActionResult GetImg([FromRoute] string imgName)
        {
            try
            {
                string path = _webHostEnvironment.WebRootPath + "\\customer\\";
                var filePath = path + imgName + ".jpg";
                if (System.IO.File.Exists(filePath))
                {
                    byte[] b = System.IO.File.ReadAllBytes(filePath);
                    return File(b, "image/jpg");
                }
                return null;
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
        /// <param name="customer">đối tượng muốn xử lý</param>
        /// <returns>
        /// "": nếu ko có ảnh
        /// Tên ảnh: nếu có ảnh
        /// </returns>
        /// Created By: DVHIEU (23/03/2023)
        protected internal override string CreatedAndUpdateImg(Customer customer)
        {
            string imgName = "";
            if (customer.Img != null)
            {
                int count = _customerBL.CheckImgName(customer.Img.FileName);
                if (count == 0)
                {
                    string path = _webHostEnvironment.WebRootPath + "\\customer\\";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream fileStream = System.IO.File.Create(path + customer.Img.FileName))
                    {
                        customer.Img.CopyTo(fileStream);
                        imgName = customer.Img.FileName;
                        fileStream.Flush();
                    }
                }
                else
                {
                    imgName = customer.Img.FileName;
                }
            }
            return imgName;
        }

        [HttpPost("exportv2")]
        public async Task<IActionResult> ExportV2(CancellationToken cancellationToken, List<Guid>? listId)
        {
            await Task.Yield();
            var stream = _customerBL.ExportExcel(listId);
            string excelName = "Bao_cao.xlsx";

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

        [HttpPut("update-password/{id}")]
        public IActionResult UpdateRecord(Guid id, [FromBody] string passWord)
        {
            try
            {
                var result = _customerBL.UpdatePassWord(id, passWord);

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
        #endregion

    }
}
