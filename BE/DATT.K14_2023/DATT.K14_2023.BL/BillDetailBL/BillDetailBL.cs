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
using DATT.k14_2023.DL.ShoeDL;
using Newtonsoft.Json;

namespace DATT.K14_2023.BL.BillDetailBL
{
    public class BillDetailBL : IBillDetailBL
    {
        private IBillDetailDL _billDetailDL;
        private IShoeDL _shoeDL;
        public BillDetailBL(IBillDetailDL billDetailDL, IShoeDL shoeDL)
        {
            _billDetailDL = billDetailDL;
            _shoeDL = shoeDL;
        }
        public dynamic GetRecordAll(Guid id)
        {
            return _billDetailDL.GetRecordAll(id);
        }
        public ServiceResult InsertRecord(Guid id, List<CartDetail> record)
        {
            var total = new List<dynamic>();
            foreach(CartDetail cart in record){
                var shoe = _shoeDL.GetRecordById(cart.ShoeId);
                var model = JsonConvert.DeserializeObject<List<SizeDetail>>(shoe.Size);
                foreach(var size in model)
                {
                    if(cart.Size == size.SizeName && cart.Amount > size.Amount)
                    {
                        total.Add(cart);
                    }
                }
            }
            if(total.Count > 0)
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    ErrorCode = (ErrorCode?)6,
                    Data = total,
                    Message = Resource.Insert_Failed,
                };
            }

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
