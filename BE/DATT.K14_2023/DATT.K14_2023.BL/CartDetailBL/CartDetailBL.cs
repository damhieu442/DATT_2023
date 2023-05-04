using DATT.k14_2023.COMMON;
using DATT.k14_2023.COMMON.Entities;
using DATT.k14_2023.COMMON.Entities.DTO;
using DATT.k14_2023.COMMON.Enums;
using DATT.k14_2023.DL.CartDetailDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATT.K14_2023.BL.CartDetailBL
{
    public class CartDetailBL : ICartDetailBL
    {
        private ICartDetailDL _cartDetailDL;

        public CartDetailBL(ICartDetailDL cartDetailDL)
        {
            _cartDetailDL = cartDetailDL;
        }

        public int DeleteRecordOne(Guid id)
        {
            return _cartDetailDL.DeleteRecordOne(id);
        }

        public dynamic GetRecordAll(Guid id)
        {
            return _cartDetailDL.GetRecordAll(id);
        }

        public ServiceResult InsertRecord(Guid id, CartDetail record)
        {
            return HandleEventInsertAndUpdate(Guid.Empty, record, id);
        }

        public ServiceResult UpdateRecord(Guid id, CartDetail record)
        {
            return HandleEventInsertAndUpdate(id, record, Guid.Empty);
        }

        private ServiceResult HandleEventInsertAndUpdate(Guid id, CartDetail record, Guid ids)
        {
            int numberOfAffectedRows;
            ErrorCode errorCode;
            string errorMessage;

            if (id == Guid.Empty)
            {
                numberOfAffectedRows = _cartDetailDL.InsertRecord(ids, record);
                errorCode = k14_2023.COMMON.Enums.ErrorCode.InsertFailed;
                errorMessage = Resource.Insert_Failed;
            }
            else
            {
                numberOfAffectedRows = _cartDetailDL.UpdateRecord(id, record);
                errorCode = k14_2023.COMMON.Enums.ErrorCode.UpdateFailed;
                errorMessage = Resource.Update_Failed;
            }

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
                    ErrorCode = errorCode,
                    Message = errorMessage,
                };
            }
        }
    }
}
