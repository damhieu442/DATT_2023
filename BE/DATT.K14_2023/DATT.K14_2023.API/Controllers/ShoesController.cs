using DATT.k14_2023.COMMON.Entities;
using DATT.K14_2023.BL.ShoeBL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace DATT.K14_2023.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoesController : BasesController<Shoe>
    {
        #region Field
        private IShoeBL _shoeBL;
        public static IWebHostEnvironment _webHostEnvironment;
        #endregion

        #region Constructor
        public ShoesController(IWebHostEnvironment webHostEnvironment, IShoeBL shoeBL) : base(shoeBL)
        {
            _webHostEnvironment = webHostEnvironment;
            _shoeBL = shoeBL;
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
        protected internal override string CreatedAndUpdateImg(Shoe shoe)
        {
            string imgName = "";
            if (shoe.Img.Count > 0)
            {
                foreach (var file in shoe.Img)
                {
                    int count = _shoeBL.CheckImgName(file.FileName);
                    if (count == 0)
                    {
                        string path = _webHostEnvironment.WebRootPath + "\\shoe\\";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        using (FileStream fileStream = System.IO.File.Create(path + file.FileName))
                        {
                            file.CopyTo(fileStream);
                            imgName += file.FileName + ",";
                            fileStream.Flush();
                        }
                    }
                    else
                    {
                        imgName = file.FileName;
                    }
                }
            }
            return imgName;
        }

        /// <summary>
        /// Hàm lấy path ảnh
        /// </summary>
        /// <returns></returns>
        /// Created By: DVHIEU (23/03/2023)
        protected internal override dynamic urlImg()
        {
            return _webHostEnvironment.WebRootPath + "\\shoe\\";
        }
        #endregion
    }
}
