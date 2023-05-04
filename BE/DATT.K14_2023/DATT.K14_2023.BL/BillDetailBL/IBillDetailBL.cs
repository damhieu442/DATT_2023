using DATT.k14_2023.COMMON.Entities.DTO;
using DATT.k14_2023.COMMON.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.K14_2023.BL.BillDetailBL
{
    public interface IBillDetailBL
    {
        dynamic GetRecordAll(Guid id);
        ServiceResult InsertRecord(Guid id, List<CartDetail> record);
    }
}
