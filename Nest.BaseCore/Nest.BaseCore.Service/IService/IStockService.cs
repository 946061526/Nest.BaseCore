using Nest.BaseCore.Common;
using Nest.BaseCore.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nest.BaseCore.Service
{
    public interface IStockService
    {

        ApiResultModel<int> AddStock(Stock model);
        ApiResultModel<int> AddStockCheck(StockCheck model);
    }
}
