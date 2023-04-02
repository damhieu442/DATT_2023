using DATT.k14_2023.COMMON.Entities;
using DATT.K14_2023.BL.BaseBL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATT.K14_2023.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizesController : BasesController<Size>
    {
        public SizesController(IBaseBL<Size> baseBL) : base(baseBL)
        {
        }
    }
}
