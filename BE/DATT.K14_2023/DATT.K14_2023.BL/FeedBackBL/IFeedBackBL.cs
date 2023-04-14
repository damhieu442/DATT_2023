using DATT.k14_2023.COMMON.Entities;
using DATT.K14_2023.BL.IBaseBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.K14_2023.BL.FeedBackBL
{
    public interface IFeedBackBL : IBaseBL<FeedBack>
    {
        dynamic ExportExcel(List<Guid>? listId);
    }
}
