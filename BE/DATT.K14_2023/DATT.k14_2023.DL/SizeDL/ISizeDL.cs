﻿using DATT.k14_2023.COMMON.Entities;
using DATT.k14_2023.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.k14_2023.DL.SizeDL
{
    public interface ISizeDL : IBaseDL<Size>
    {
        #region Method
        List<Size> ExportExcel(List<Guid>? listId);
        #endregion
    }
}
