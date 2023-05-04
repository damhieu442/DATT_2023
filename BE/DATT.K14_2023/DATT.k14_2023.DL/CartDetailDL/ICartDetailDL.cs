using DATT.k14_2023.COMMON.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.k14_2023.DL.CartDetailDL
{
    public interface ICartDetailDL
    {
        dynamic GetRecordAll(Guid id);
        int InsertRecord(Guid id, CartDetail record);
        int UpdateRecord(Guid id, CartDetail record);
        int DeleteRecordOne(Guid id);
    }
}
