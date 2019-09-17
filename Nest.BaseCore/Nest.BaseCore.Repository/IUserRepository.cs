using Nest.BaseCore.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nest.BaseCore.Repository
{
    public class TUserRepository : BaseRepository<User>, IUserRepository
    {
        public TUserRepository(MainContext db) : base(db) { }
    }
    public interface IUserRepository : IRepository<User> { }
}
