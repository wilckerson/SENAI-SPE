using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Repository
{
    public interface IRepositoryBase<TEntity, TContext>
        where TEntity : class
        where TContext : DbContext, new()
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(Func<TEntity, bool> predicate);
        TEntity GetById(long Id);
        TEntity GetOne(Func<TEntity, bool> where);
        IEnumerable<TEntity> Get();
        IEnumerable<TEntity> Get(Func<TEntity, bool> where);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", int limit = 0);
        IEnumerable<TEntity> Get(int page, int qtPage, out int total, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        int GetCount(Expression<Func<TEntity, bool>> filter = null);
    }
}
