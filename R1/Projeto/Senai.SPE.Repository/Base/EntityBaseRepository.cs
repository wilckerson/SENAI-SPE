using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Transactions;
using System.Data.Entity;

using Repository;
using Senai.SPE.Domain;

namespace SPERepository
{
    public class EntityRepository<TEntity,TContext> : RepositoryBase<TEntity,TContext>
        where TEntity : class, new()
        where TContext : DbContext, new()
    {

        public TEntity Add(TEntity entity)
        {
            base.Add(entity);
            base.Save();

            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            base.Update(entity);
            base.Save();

            return entity;
        }

        public bool Delete(int id)
        {
            TEntity entity = this.GetById(id);
            base.Delete(entity);
            base.Save();

            return true;
        }

        public bool Delete(TEntity entity)
        {
            base.Delete(entity);
            base.Save();

            return true;
        }

        public IEnumerable<TEntity> Get()
        {
            return base.Get().ToList();
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", int limit = 0)
        {
            return base.Get(filter, orderBy, includeProperties, limit).ToList();
        }

        public IEnumerable<TEntity> Get(int page, int qtPage, out int total, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            return base.Get(page, qtPage, out total, filter, orderBy, includeProperties).ToList();
        }

        public int GetCount(Expression<Func<TEntity, bool>> filter = null)
        {
            return base.GetCount(filter);
        }
    }
}
