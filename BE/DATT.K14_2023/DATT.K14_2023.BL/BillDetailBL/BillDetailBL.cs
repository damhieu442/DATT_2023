using DATT.k14_2023.COMMON.Entities.DTO;
using DATT.k14_2023.COMMON.Entities;
using DATT.k14_2023.DL.BillDetailDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATT.k14_2023.COMMON.Enums;
using DATT.k14_2023.COMMON;

namespace DATT.K14_2023.BL.BillDetailBL
{
    public class BillDetailBL : IBillDetailBL
    {
        private IBillDetailDL _billDetailDL;
        public BillDetailBL(IBillDetailDL billDetailDL)
        {
            _billDetailDL = billDetailDL;
        }
        public dynamic GetRecordAll(Guid id)
        {
            return _billDetailDL.GetRecordAll(id);
        }
        public ServiceResult InsertRecord(Guid id, List<CartDetail> record)
        {
            int numberOfAffectedRows = _billDetailDL.InsertRecord(id, record);
            if (numberOfAffectedRows > 0)
            {
                return new ServiceResult
                {
                    IsSuccess = true,
                };
            }
            else
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    ErrorCode = k14_2023.COMMON.Enums.ErrorCode.InsertFailed,
                    Message = Resource.Insert_Failed,
                };
            }
        }
    }
}
