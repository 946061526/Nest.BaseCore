using Nest.BaseCore.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nest.BaseCore.Repository
{
    /// <summary>
    /// 库存仓储
    /// </summary>
    public interface IStockRepository : IBaseRepository<Stock>
    {

    }

    public class TStockRepository : BaseRepository<Stock>, IStockRepository
    {
        public TStockRepository(MainContext db) : base(db) { }
    }


    /// <summary>
    /// 库存盘点仓储
    /// </summary>
    public interface IStockCheckRepository : IBaseRepository<StockCheck>
    {

    }

    public class TStockCheckRepository : BaseRepository<StockCheck>, IStockCheckRepository
    {
        public TStockCheckRepository(MainContext db) : base(db) { }
    }
}
