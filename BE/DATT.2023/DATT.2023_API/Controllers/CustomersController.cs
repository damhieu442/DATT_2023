using DATT._2023.COMMOM.Enities;
using DATT._2023_BL.CustomerBL;
using DATT._2023_COMMOM;
using DATT._2023_COMMOM.Enities.DTO;
using DATT._2023_COMMOM.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATT._2023_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        #region Field
        private ICustomerBL _customerBL;
        public static IWebHostEnvironment _webHostEnvironment;
        #endregion

        #region Constructor
        public CustomersController(ICustomerBL customerBL, IWebHostEnvironment webHostEnvironment)
        {
            _customerBL = customerBL;
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion

        #region Method
        /// <summary>
        /// Hàm lấy dữ liệu người dùng theo id
        /// </summary>
        /// <param name="id">id người dùng</param>
        /// <returns>
        /// Thông tin người dùng: nếu get thành công
        /// rỗng : nếu get thất bại
        /// </returns>
        /// CREATED BY: DVHIEU - 22/03/2023
        [HttpGet("{id}")]
        public dynamic GetCustomerById(Guid id)
        {
            try
            {
                dynamic customer = _customerBL.GetCustomerById(id);

                if (customer != null)
                {
                    return StatusCode(StatusCodes.Status200OK, customer);
                }
                else
                {
                    return StatusCode(StatusCodes.Status204NoContent);
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
        /// API Thêm mới người dùng
        /// </summary>
        /// <param name="customer">Người dùng cần thêm mới</param>
        /// <returns>
        /// status 201: nếu insert thành công
        /// status 400, 500: nếu insert thất bại
        /// </returns>
        [HttpPost]
        public IActionResult InsertCustomer([FromForm] Customer customer )
        {
            try
            {
                string imgName = CreatedImg(customer);
                var result = _customerBL.InsertCustomer(customer, imgName);
                return StatusCode(StatusCodes.Status201Created);
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

        private static string CreatedImg(Customer customer)
        {
            string imgName = "";
            if (customer.ImgName != null)
            {
                string path = _webHostEnvironment.WebRootPath + "\\customer\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                using (FileStream fileStream = System.IO.File.Create(path + customer.ImgName.FileName))
                {
                    customer.ImgName.CopyTo(fileStream);
                    imgName += customer.ImgName.FileName;
                    fileStream.Flush();
                }
            }

            return imgName;
        }
        #endregion
    }
}
