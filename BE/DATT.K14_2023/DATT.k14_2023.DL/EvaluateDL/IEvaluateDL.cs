﻿using DATT.k14_2023.COMMON.Entities;
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
        dynamic GetRecordByFilterAndPaging(int pageSize, int pageNumber, string? keyword, int? status);
        dynamic GetRecordById(Guid id);
        int InsertRecord(Evaluate record);
        int UpdateRecord(Guid id, Evaluate record);
        int DeleteRecordOne(Guid id);
        int DeleteRecordMany(List<Guid> listId);
        List<Evaluate> ExportExcel(List<Guid>? listId);
    }
}
