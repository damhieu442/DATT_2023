using DATT.k14_2023.COMMON;
using DATT.k14_2023.COMMON.Entities;
using DATT.k14_2023.COMMON.Entities.DTO;
using DATT.k14_2023.COMMON.Enums;
using DATT.K14_2023.BL.BaseBL;
using DATT.K14_2023.BL.CustomerBL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

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
        #endregion

    }
}
