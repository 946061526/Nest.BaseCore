using Nest.BaseCore.Common;
using Nest.BaseCore.Domain;
using Nest.BaseCore.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nest.BaseCore.Service
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;
        private readonly IStockCheckRepository _stockCheckRepository;

        public StockService(IStockRepository stockRepository, IStockCheckRepository stockCheckRepository)
        {
            _stockRepository = stockRepository;
            _stockCheckRepository = stockCheckRepository;
        }

        public ApiResultModel<int> AddStock(Stock model)
        {
            var result = new ApiResultModel<int>();

            _stockRepository.Add(model);
            _stockRepository.SaveChanges();

            result.Code = ApiResultCode.Success;
            return result;
        }

        public ApiResultModel<int> AddStockCheck(StockCheck model)
        {
            var result = new ApiResultModel<int>();

            _stockCheckRepository.Add(model);
            _stockCheckRepository.SaveChanges();

            result.Code = ApiResultCode.Success;
            return result;
        }
    }
}
