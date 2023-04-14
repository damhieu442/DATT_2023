using DATT.k14_2023.COMMON.Entities;
using DATT.K14_2023.BL.IBaseBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.K14_2023.BL.SizeBL
{
    public interface ISizeBL : IBaseBL<Size>
    {
        dynamic ExportExcel(List<Guid>? listId);
    }
}
