using DATT.k14_2023.COMMON;
using DATT.k14_2023.COMMON.Entities;
using DATT.k14_2023.COMMON.Entities.DTO;
using DATT.k14_2023.COMMON.Enums;
using DATT.k14_2023.DL.EvaluateDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DATT.k14_2023.COMMON.Constants.BAttribute;

namespace DATT.K14_2023.BL.EvaluateBL
{
    public class EvaluateBL : IEvaluateBL
    {
        private IEvaluateDL _evaluateDL;

        public EvaluateBL(IEvaluateDL evaluateDL)
        {
            _evaluateDL = evaluateDL;
        }

        public dynamic GetRecordAll()
        {
            return _evaluateDL.GetRecordAll();
        }

        public PagingResult<Evaluate> GetRecordByFilterAndPaging(int pageSize, int pageNumber, string? keyword)
        {
            dynamic dataRecord = _evaluateDL.GetRecordByFilterAndPaging(pageSize, pageNumber, keyword, null, null);
            double totalPage = Convert.ToDouble(dataRecord[1]) / pageSize;

            return new PagingResult<Evaluate>
            {
                CurrentPage = pageNumber,
                CurrentPageRecords = pageSize,
                TotalPage = Convert.ToInt32(Math.Ceiling(totalPage)),
                TotalRecord = dataRecord[1],
                Data = dataRecord[0],
            };
        }

        public dynamic GetRecordById(Guid id)
        {
            return _evaluateDL.GetRecordById(id);
        }

        public ServiceResult InsertRecord(Evaluate record)
        {
            var validateResults = ValidateRequestData(record);
            if (validateResults.Count > 0)
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    ErrorCode = k14_2023.COMMON.Enums.ErrorCode.APIParameterNullOrInvalid,
                    Message = Resource.InvalidData,
                    Data = validateResults,
                };
            }

            var validateCustom = ValidateCustom(record);
            if (!validateCustom.IsSuccess)
            {
                return validateCustom;
            }

            int numberOfAffectedRows = _evaluateDL.InsertRecord(record);
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

        public int DeleteRecordOne(Guid id)
        {
            return _evaluateDL.DeleteRecordOne(id);
        }

        private List<String> ValidateRequestData(Evaluate? record)
        {
            var props = record.GetType().GetProperties();
            var validateFailures = new List<string>();

            foreach (var prop in props)
            {
                var propValue = prop.GetValue(record);
                var notEmptyAttribute = (NotEmpty)prop.GetCustomAttributes(typeof(NotEmpty), false).FirstOrDefault();
                if (notEmptyAttribute != null && propValue == null)
                {
                    validateFailures.Add(notEmptyAttribute.ErrorMessage);
                }
                else if (notEmptyAttribute != null && string.IsNullOrEmpty(propValue.ToString().Trim()))
                {
                    validateFailures.Add(notEmptyAttribute.ErrorMessage);
                }
            }

            if (validateFailures.Count > 0)
            {
                return validateFailures;
            }

            return new List<string>();
        }

        protected internal virtual ServiceResult ValidateCustom(Evaluate? record)
        {
            return new ServiceResult { IsSuccess = true };
        }
    }
}
