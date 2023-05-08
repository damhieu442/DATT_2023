using DATT.k14_2023.COMMON.Entities;
using DATT.k14_2023.COMMON.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.K14_2023.BL.EvaluateBL
{
    public interface IEvaluateBL
    {
        dynamic GetRecordAll();
        PagingResult<Evaluate> GetRecordByFilterAndPaging(int pageSize, int pageNumber, string? keyword, int? status);
        dynamic GetRecordById(Guid id);
        ServiceResult InsertRecord(Evaluate record);
        ServiceResult UpdateRecord(Guid id, Evaluate record);
        int DeleteRecordOne(Guid id);
        int DeleteRecordMany(List<Guid> listId);
        dynamic ExportExcel(List<Guid>? listId);
    }
}
