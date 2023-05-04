using DATT.k14_2023.COMMON.Entities;
using DATT.k14_2023.COMMON.Entities.DTO;
using DATT.k14_2023.COMMON.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.K14_2023.BL.BillBL
{
    public interface IBillBL
    {
        dynamic GetRecordAll();
        PagingResult<Bill> GetRecordByFilterAndPaging(int pageSize, int pageNumber, string? id, string? name, string? phone, int? status);
        PagingResult<Bill> GetRecordByCustomerId(Guid id, int pageSize, int pageNumber, string? keyWord, int? status);
        dynamic GetRecordById(Guid id);
        ServiceResult InsertRecord(Guid customerId, Bill record);
        ServiceResult UpdateRecord(Guid id, Bill record);
        int DeleteRecordOne(Guid id);
        int DeleteRecordMany(List<Guid> listId);
        dynamic ExportExcel(List<Guid>? listId);
    }
}
