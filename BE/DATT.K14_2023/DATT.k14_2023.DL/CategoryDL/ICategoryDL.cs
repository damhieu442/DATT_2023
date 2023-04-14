using DATT.k14_2023.COMMON.Entities;
using DATT.k14_2023.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.k14_2023.DL.CategoryDL
{
    public interface ICategoryDL : IBaseDL<Category>
    {
        #region Method
        /// <summary>
        /// Check trùng mã nhân viên
        /// </summary>
        /// <param name="CategoryCode">Mã danh mục</param>
        /// <param name="CategoryId">Id danh mục</param>
        /// <returns>
        /// 1 - trùng mã
        /// 0 - không trùng mã
        /// </returns>
        /// Created By: DVHIEU (02/04/2023)
        int CheckEmployeeCode(int? CategoryCode, Guid? CategoryId);
        List<Category> ExportExcel(List<Guid>? listId);
        #endregion
    }
}
