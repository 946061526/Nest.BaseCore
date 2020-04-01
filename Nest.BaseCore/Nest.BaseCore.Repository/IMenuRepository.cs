using Nest.BaseCore.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nest.BaseCore.Repository
{
    /// <summary>
    /// 菜单仓储
    /// </summary>
    public interface IMenuRepository : IBaseRepository<Menu>
    {

    }

    public class TMenuRepository : BaseRepository<Menu>, IMenuRepository
    {
        public TMenuRepository(MainContext db) : base(db) { }
    }
}