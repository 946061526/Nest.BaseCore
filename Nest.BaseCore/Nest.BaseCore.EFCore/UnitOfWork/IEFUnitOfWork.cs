using Microsoft.EntityFrameworkCore;

namespace Nest.BaseCore.EFCore.UnitOfWork
{
    /// <summary>
    /// 表示EF的工作单元接口，因为DbContext是EF的对象
    /// </summary>
    public interface IEFUnitOfWork:IUnitOfWorkRepositoryContext
    {
        DbContext context { get; }
    }
}
