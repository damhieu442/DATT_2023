using DATT.k14_2023.COMMON.Entities;
using DATT.k14_2023.DL.FeedBackDL;
using DATT.K14_2023.BL.FeedBackBL;
using DATT.K14_2023.BL.IBaseBL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATT.K14_2023.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedBacksController : BasesController<FeedBack>
    {
        private IFeedBackBL _feedBackBL;
        public FeedBacksController(IFeedBackBL feedBackBL) : base(feedBackBL)
        {
            _feedBackBL = feedBackBL;
        }

        [HttpPost("exportv2")]
        public async Task<IActionResult> ExportV2(CancellationToken cancellationToken, List<Guid>? listId)
        {
            await Task.Yield();
            var stream = _feedBackBL.ExportExcel(listId);
            string excelName = "Bao_cao.xlsx";

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
    }
}
