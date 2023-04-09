using DATT.k14_2023.COMMON.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.k14_2023.DL.EvaluateDL
{
    public interface IEvaluateDL
    {
        dynamic GetRecordAll();
        dynamic GetRecordByFilterAndPaging(int pageSize, int pageNumber, string? keyword, string? queryString, string? type);
        dynamic GetRecordById(Guid id);
        int InsertRecord(Evaluate record);
        int DeleteRecordOne(Guid id);
    }
}
