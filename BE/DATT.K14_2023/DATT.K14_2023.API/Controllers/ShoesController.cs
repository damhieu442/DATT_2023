﻿using DATT.k14_2023.COMMON;
using DATT.k14_2023.COMMON.Entities;
using DATT.k14_2023.COMMON.Entities.DTO;
using DATT.k14_2023.COMMON.Enums;
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
                // string path local
                string path = _webHostEnvironment.WebRootPath + "\\shoe\\";
                // string path deploy azure
                // string path = "/home/site/wwwroot/wwwroot/shoe/";
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
        protected internal override string CreatedAndUpdateImg(Shoe shoe)
        {
            string imgName = "";
            if(shoe.Img == null)
            {
                return "";
            };
            if (shoe.Img.Count > 0)
            {
                foreach (var file in shoe.Img)
                {
                    int count = _shoeBL.CheckImgName(file.FileName);
                    if (count == 0)
                    {
                        // string path local
                        string path = _webHostEnvironment.WebRootPath + "\\shoe\\";
                        // string path deploy azure
                        // string path = "/home/site/wwwroot/wwwroot/shoe/";
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

        [HttpPost("exportv2")]
        public async Task<IActionResult> ExportV2(CancellationToken cancellationToken, List<Guid>? listId)
        {
            await Task.Yield();
            var stream = _shoeBL.ExportExcel(listId);
            string excelName = "Bao_cao.xlsx";

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

        [HttpPut("update-sold-number/{id}")]
        public IActionResult UpdateSoldNumber(Guid id, int soldNumber)
        {
            try
            {
                int res = _shoeBL.UpdateSoldNumber(id, soldNumber);
                if (res > 0) 
                {
                    return StatusCode(StatusCodes.Status201Created);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
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
