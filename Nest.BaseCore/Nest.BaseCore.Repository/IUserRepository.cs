using Nest.BaseCore.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nest.BaseCore.Repository
{
    /// <summary>
    /// 用户仓储
    /// </summary>
    public interface IUserRepository : IBaseRepository<User>
    {

    }

    public class TUserRepository : BaseRepository<User>, IUserRepository
    {
        public TUserRepository(MainContext db) : base(db) { }
    }
}
