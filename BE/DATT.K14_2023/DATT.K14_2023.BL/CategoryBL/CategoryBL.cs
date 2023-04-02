using DATT.k14_2023.COMMON;
using DATT.k14_2023.COMMON.Entities;
using DATT.k14_2023.COMMON.Entities.DTO;
using DATT.k14_2023.DL.CategoryDL;
using DATT.K14_2023.BL.BaseBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.K14_2023.BL.CategoryBL
{
    public class CategoryBL : BaseBL<Category>, ICategoryBL
    {
        #region Field
        private ICategoryDL _categoryDL;
        #endregion

        #region Constructor
        public CategoryBL(ICategoryDL categoryDL) : base(categoryDL)
        {
            _categoryDL = categoryDL;
        }
        #endregion

        #region Method
        /// <summary>
        /// Hàm thực hiện validate riêng 
        /// </summary>
        /// <param name="category">đối tượng category</param>
        /// <returns>
        /// rỗng nếu không trùng mã nhân viên
        /// thông báo lỗi nếu trùng mã nhân viên
        /// </returns>
        /// CREATED BY: DVHIEU (02/04/2023)
        protected internal override ServiceResult ValidateCustom(Category? category)
        {
            int numberOfAffectedRows = _categoryDL.CheckEmployeeCode(category.CategoryCode, category.CategoryId);

            if (numberOfAffectedRows > 0)
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    ErrorCode = k14_2023.COMMON.Enums.ErrorCode.Duplicate,
                    Data = new List<string>() { string.Format(Resource.Msg_DuplicateEmpCode, category.CategoryCode) }
                };
            }

            return new ServiceResult { IsSuccess = true };
        }
        #endregion
    }
}
