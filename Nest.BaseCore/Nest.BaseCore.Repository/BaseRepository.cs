using Microsoft.EntityFrameworkCore;
using Nest.BaseCore.Common;
using Nest.BaseCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Transactions;

namespace Nest.BaseCore.Repository
{
    //public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    //{
    //    public MainContext UnitOfWork { get; set; }

    //    public BaseRepository(MainContext db)
    //    {
    //        UnitOfWork = db;
    //    }

    //    IQueryable<TEntity> IBaseRepository<TEntity>.Entities
    //    {
    //        get
    //        {
    //            return UnitOfWork.Set<TEntity>();
    //        }
    //    }

    //    public virtual TEntity GetByKey(object key)
    //    {
    //        return UnitOfWork.Set<TEntity>().Find(key);
    //    }

    //    public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> express)
    //    {
    //        return Set().AsQueryable().Where(express);
    //    }


    //    public virtual int Insert(TEntity entity)
    //    {
    //        //UnitOfWork.RegisterNew(entity);
    //        return 1;
    //    }

    //    public virtual int Insert(IEnumerable<TEntity> entities)
    //    {
    //        foreach (var obj in entities)
    //        {
    //            //UnitOfWork.RegisterNew(obj);
    //        }
    //        return 1;
    //    }

    //    public virtual int Delete(object id)
    //    {
    //        //var obj = UnitOfWork.context.Set<TEntity>().Find(id);
    //        //if (obj == null)
    //        //{
    //        //    return 0;
    //        //}
    //        //UnitOfWork.RegisterDeleted(obj);
    //        return 1;
    //    }

    //    public virtual int Delete(TEntity entity)
    //    {
    //        //UnitOfWork.RegisterDeleted(entity);
    //        return 1;
    //    }

    //    public virtual int Delete(IEnumerable<TEntity> entities)
    //    {
    //        foreach (var entity in entities)
    //        {
    //            //UnitOfWork.RegisterDeleted(entity);
    //        }
    //        return 1;
    //    }

    //    public virtual int Delete(Expression<Func<TEntity, bool>> express)
    //    {
    //        var lstEntity = Set().AsQueryable().Where(express);
    //        //foreach (var entity in lstEntity)
    //        //{
    //        //    UnitOfWork.RegisterDeleted(entity);
    //        //}
    //        return 1;
    //    }

    //    public virtual int Update(TEntity entity, params string[] fields)
    //    {
    //        // UnitOfWork.RegisterModified(entity, fields);
    //        return 1;
    //    }

    //    /// <summary>
    //    ///     更新实体记录
    //    /// </summary>
    //    /// <param name="entity"> 实体对象集合 </param>
    //    /// <param name="fields"> 修改字段名集合</param>
    //    /// <returns> 操作影响的行数 </returns>
    //    public int Update(IEnumerable<TEntity> entitys, params string[] fields)
    //    {
    //        foreach (var entity in entitys)
    //        {
    //            // UnitOfWork.RegisterModified(entity, fields);
    //        }
    //        return entitys.Count();
    //    }
    //    /// <summary>
    //    /// 保存修改
    //    /// </summary>
    //    /// <returns></returns>
    //    public int Commit()
    //    {
    //        //return UnitOfWork.Commit();
    //        return 1;
    //    }

    //    /// <summary>
    //    /// 分布式事务
    //    /// </summary>
    //    /// <returns></returns>
    //    public TransactionScope BeginTrans()
    //    {
    //        var transaction = new TransactionScope(TransactionScopeOption.Required);
    //        return transaction;
    //    }

    //    /// <summary>
    //    /// 放弃保存
    //    /// </summary>
    //    /// <returns></returns>
    //    public void RollBack()
    //    {
    //        //UnitOfWork.RollBack();
    //    }
    //    #region 基础设置
    //    /// <summary>
    //    /// 获取当前DbSet
    //    /// </summary>
    //    /// <returns></returns>
    //    public DbSet<TEntity> Set()
    //    {
    //        return UnitOfWork.Set<TEntity>();
    //    }
    //    #endregion
    //    #region 增强CURD
    //    /// <summary>
    //    /// 根据过滤条件，获取记录
    //    /// </summary>
    //    /// <param name="whereLambda">过滤条件</param>
    //    /// <param name="orderLambda">排序条件</param>
    //    /// <returns>IQueryable查询对象</returns>
    //    public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> whereLambda = null, params IOrderByBuilder<TEntity>[] orderLambda)
    //    {
    //        var query = Set().AsQueryable();
    //        if (whereLambda != null)
    //        {
    //            query = query.Where(whereLambda);
    //        }

    //        if (orderLambda != null)
    //        {
    //            query = query.OrderBy(orderLambda);
    //        }
    //        return query.AsNoTracking().AsQueryable();
    //    }

    //    /// <summary>
    //    /// 根据过滤条件，获取单个实体
    //    /// </summary>
    //    /// <param name="whereLambda">过滤条件</param>
    //    /// <returns>IQueryable查询对象</returns>
    //    public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> whereLambda = null)
    //    {
    //        var query = Set().AsQueryable();
    //        return query.AsNoTracking().FirstOrDefault(whereLambda);
    //    }
    //    /// <summary>
    //    /// 根据过滤条件，判断是否存在数据
    //    /// </summary>
    //    /// <param name="whereLambda">过滤条件</param>
    //    /// <returns>IQueryable查询对象</returns>
    //    public bool Any(Expression<Func<TEntity, bool>> whereLambda = null)
    //    {
    //        var query = Set().AsQueryable();
    //        return query.Any(whereLambda);
    //    }
    //    /// <summary>
    //    /// 根据过滤条件，获取数量
    //    /// </summary>
    //    /// <param name="whereLambda">过滤条件</param>
    //    /// <returns>IQueryable查询对象</returns>
    //    public int Count(Expression<Func<TEntity, bool>> whereLambda = null)
    //    {
    //        var query = Set().AsQueryable();
    //        return query.Count(whereLambda);
    //    }


    //    /// <summary>
    //    /// 批量更新数据，实现按需要只更新部分更新
    //    /// <para>如：Update(u =>u.Id==1,u =>new User{Name="ok"});</para>
    //    /// </summary>
    //    /// <param name="whereLambda">条件表达式：u =>u.Id==1</param>
    //    /// <param name="updateLambda">更新表达式：u =>new User{Name="ok"}</param>
    //    /// <returns>是否添加成功：true是，false否</returns>
    //    public bool BatchUpdate(Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TEntity>> updateLambda)
    //    {
    //        //return Set().AsQueryable().Where(whereLambda).Update(updateLambda) > -1;
    //        return false;
    //    }
    //    /// <summary>
    //    /// 批量删除数据
    //    /// </summary>
    //    /// <param name="whereLambda"></param>
    //    /// <returns>是否添加成功：true是，false否</returns>
    //    public bool BatchDelete(Expression<Func<TEntity, bool>> whereLambda)
    //    {
    //        //return Set().AsQueryable().Where(whereLambda).Delete() > -1;
    //        return false;
    //    }
    //    #endregion
    //    #region 分页
    //    /// <summary>
    //    /// 获取分页数据
    //    /// </summary>
    //    /// <param name="totalCount"></param>
    //    /// <param name="pageIndex"></param>
    //    /// <param name="pageSize">页数大小</param>
    //    /// <param name="whereLambda"></param>
    //    /// <param name="orderLambda">排序方式：new OrderByBuilder<TEntity, string>(a => a.UserName[,true])，true=倒序，默认false正序</param>
    //    /// <returns></returns>
    //    public IQueryable<TEntity> Find(out int totalCount, int pageIndex = 1, int pageSize = 10, Expression<Func<TEntity, bool>> whereLambda = null, params IOrderByBuilder<TEntity>[] orderLambda)
    //    {
    //        if (pageIndex < 1) pageIndex = 1;

    //        var query = Set().AsQueryable();
    //        if (whereLambda != null)
    //        {
    //            query = query.Where(whereLambda);
    //        }
    //        if (orderLambda != null)
    //        {
    //            query = query.OrderBy(orderLambda);
    //        }

    //        totalCount = query.Count();

    //        return query.Skip(pageSize * (pageIndex - 1)).Take(pageSize);
    //    }

    //    /// <summary>
    //    /// 获取分页数据
    //    /// </summary>
    //    /// <param name="totalCount"></param>
    //    /// <param name="pageIndex"></param>
    //    /// <param name="pageSize">页数大小</param>
    //    /// <param name="query">linq表达式</param>
    //    /// <returns></returns>
    //    public IQueryable<TEntity> Find(out int totalCount, int pageIndex = 1, int pageSize = 10, IQueryable<TEntity> query = null)
    //    {
    //        if (query == null)
    //        {
    //            totalCount = 0;
    //            return null;
    //        }

    //        totalCount = query.Count();

    //        return query.Skip(pageSize * (pageIndex - 1)).Take(pageSize);
    //    }

    //    #endregion

    //}

    public abstract class BaseRepository<T> where T : class
    {
        private MainContext db;//数据库上下文

        public BaseRepository(MainContext _db)
        {
            db = _db;
        }

        public virtual void Save()
        {
            db.SaveChanges();
        }

        public virtual void Add(T entity)
        {
            db.Set<T>().Add(entity);
        }

        public virtual void CloseProxy()
        {
            db.Database.CommitTransaction();
        }

        public virtual void Delete(T entity)
        {
            db.Set<T>().Remove(entity);
        }

        public virtual void Delete(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            var dataList = db.Set<T>().Where(where).AsEnumerable();
            db.Set<T>().RemoveRange(dataList);
        }

        public virtual T Get(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            return db.Set<T>().FirstOrDefault(where);
        }

        public virtual System.Linq.IQueryable<T> GetAll()
        {
            return db.Set<T>();
        }

        public virtual T GetById(long Id)
        {
            return db.Set<T>().Find(Id);
        }

        public virtual T GetById(string Id)
        {
            return db.Set<T>().Find(Id);
        }

        public virtual int GetCount(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            return db.Set<T>().Count(where);
        }

        public virtual System.Linq.IQueryable<T> GetMany(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            return db.Set<T>().Where(where);
        }

        public virtual bool IsHasValue(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            return db.Set<T>().Any(where);
        }

        public virtual void OpenProxy()
        {
            db.Database.BeginTransaction();
        }

        public virtual void Update(T entity)
        {
            db.Set<T>().Attach(entity);
            db.Entry<T>(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
