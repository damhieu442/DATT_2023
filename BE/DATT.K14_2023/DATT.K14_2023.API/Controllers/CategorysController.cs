using DATT.k14_2023.COMMON.Entities;
using DATT.K14_2023.BL.BaseBL;
using DATT.K14_2023.BL.CategoryBL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATT.K14_2023.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorysController : BasesController<Category>
    {
        #region Field
        private ICategoryBL _categoryBL;
        #endregion

        #region Constructor
        public CategorysController(ICategoryBL categoryBL) : base(categoryBL)
        {
            _categoryBL = categoryBL;
        }
        #endregion

        #region Method
        #endregion
    }
}
