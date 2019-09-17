using Nest.BaseCore.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nest.BaseCore.Repository
{
    public class TRoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public TRoleRepository(MainContext db) : base(db) { }
    }
    public interface IRoleRepository : IRepository<Role> { }
}
