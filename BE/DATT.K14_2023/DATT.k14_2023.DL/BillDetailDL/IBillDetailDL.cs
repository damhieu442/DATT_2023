using DATT.k14_2023.COMMON.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.k14_2023.DL.BillDetailDL
{
    public interface IBillDetailDL
    {
        dynamic GetRecordAll(Guid id);
        int InsertRecord(Guid id, List<CartDetail> record);
    }
}
