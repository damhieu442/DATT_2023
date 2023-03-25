using DATT.k14_2023.COMMON.Entities;
using DATT.K14_2023.BL.BaseBL;
using DATT.K14_2023.BL.CustomerBL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            if (customer.ImgName != null)
            {
                int count = _customerBL.CheckImgName(customer.ImgName.FileName);
                if (count == 0)
                {
                    string path = _webHostEnvironment.WebRootPath + "\\customer\\";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream fileStream = System.IO.File.Create(path + customer.ImgName.FileName))
                    {
                        customer.ImgName.CopyTo(fileStream);
                        imgName = customer.ImgName.FileName;
                        fileStream.Flush();
                    }
                }
                else
                {
                    imgName = customer.ImgName.FileName;
                }
            }
            return imgName;
        }
        #endregion

    }
}
