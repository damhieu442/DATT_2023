using DATT.k14_2023.COMMON.Entities;
using DATT.K14_2023.BL.IBaseBL;
using DATT.K14_2023.BL.SizeBL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATT.K14_2023.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizesController : BasesController<Size>
    {
        private ISizeBL _sizeBL;
        public SizesController(ISizeBL sizeBL) : base(sizeBL)
        {
            _sizeBL = sizeBL;
        }

        [HttpPost("exportv2")]
        public async Task<IActionResult> ExportV2(CancellationToken cancellationToken, List<Guid>? listId)
        {
            await Task.Yield();
            var stream = _sizeBL.ExportExcel(listId);
            string excelName = "Bao_cao.xlsx";

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
    }
}
