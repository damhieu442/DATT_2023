using DATT.k14_2023.COMMON.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.k14_2023.DL.BillDL
{
    public interface IBillDL
    {
        dynamic GetRecordAll();
        dynamic GetRecordByFilterAndPaging(int pageSize, int pageNumber, string? id, string? name, string? phone, int? status);
        dynamic GetRecordByCustomerId(Guid id, int pageSize, int pageNumber, string? keyword, int? status);
        dynamic GetRecordById(Guid id);
        int InsertRecord(Guid id, Bill record);
        int UpdateRecord(Guid id, Bill record);
        int DeleteRecordOne(Guid id);
        int DeleteRecordMany(List<Guid> listId);
        List<Bill> ExportExcel(List<Guid>? listId);
    }
}
