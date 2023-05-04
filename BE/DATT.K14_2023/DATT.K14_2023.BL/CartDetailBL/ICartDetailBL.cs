using DATT.k14_2023.COMMON.Entities;
using DATT.k14_2023.COMMON.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.K14_2023.BL.CartDetailBL
{
    public interface ICartDetailBL
    {
        dynamic GetRecordAll(Guid id);
        ServiceResult InsertRecord(Guid id, CartDetail record);
        ServiceResult UpdateRecord(Guid id, CartDetail record);
        int DeleteRecordOne(Guid id);
    }
}
