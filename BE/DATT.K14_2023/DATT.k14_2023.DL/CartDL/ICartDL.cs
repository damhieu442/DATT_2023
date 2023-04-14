using DATT.k14_2023.COMMON.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.k14_2023.DL.CartDL
{
    public interface ICartDL
    {
        int InsertRecord(Cart record);

        int UpdateRecord(Guid id, Cart record);

        int DeleteRecordOne(Guid id);
    }
}
