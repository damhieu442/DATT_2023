using DATT.k14_2023.COMMON.Entities.DTO;
using DATT.k14_2023.COMMON;
using DATT.K14_2023.BL.DashBoardBL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATT.K14_2023.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardsController : ControllerBase
    {
        private IDashBoardBL _dashBoardBL;
        public DashboardsController(IDashBoardBL dashBoardBL)
        {
            _dashBoardBL = dashBoardBL;
        }
        [HttpGet]
        public IActionResult getDashBoard()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK,_dashBoardBL.getDashBoard());
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
        [HttpGet("chart")]
        public IActionResult getDashBoardChart()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _dashBoardBL.getDashBoardChart());
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
