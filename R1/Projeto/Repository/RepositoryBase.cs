using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;

using System.Transactions;
using System.Collections;

namespace Repository
{
    public class RepositoryBase<TEntity, TContext> : IRepositoryBase<TEntity, TContext>
        where TEntity : class
        where TContext : DbContext, new()
    {
        protected TransactionRepository transactionScope;

        private bool proxyCreationEnabled = false;
        private bool lazyLoadingEnabled = false;

        public TContext Context = new TContext();

        public bool ProxyCreation
        {
            set
            {
                this.proxyCreationEnabled = value;
            }
        }

        public bool LazyLoading
        {
            set
            {
                this.lazyLoadingEnabled = value;
            }
        }

        protected ArrayList dbSets = new ArrayList();
        protected ArrayList addList = new ArrayList();
        protected ArrayList updateList = new ArrayList();
        protected ArrayList removeList = new ArrayList();

        public RepositoryBase()
        {

        }

        public virtual void Add(TEntity entity)
        {
            addList.Add(entity);
        }

        public bool AddUnique(Expression<Func<TEntity, bool>> filter, TEntity entity)
        {
            if (this.Get(filter).Count() == 0)
            {
                using (DbContext ctx = CreateContext())
                {
                    DbSet<TEntity> DbSet = ctx.Set<TEntity>();
                    dbSets.Add(entity);
                }

                return true;
            }
            return false;
        }

        public void Update(TEntity entity)
        {
            updateList.Add(entity);
        }
               
        public void Delete(TEntity entity)
        {
            removeList.Add(entity);
        }
               
        public void Delete(IEnumerable<TEntity> entitys)
        {
            removeList.AddRange(entitys.ToArray());
        }
               
        public void Delete(Func<TEntity, bool> predicate)
        {
            using (DbContext ctx = CreateContext())
            {
                DbSet<TEntity> DbSet = ctx.Set<TEntity>();
                removeList.AddRange(DbSet.Where<TEntity>(predicate).ToArray());
            }
        }

        public TEntity GetById(long Id)
        {
            using (DbContext ctx = CreateContext())
            {
                DbSet<TEntity> DbSet = ctx.Set<TEntity>();
                return DbSet.Find(Id);
            }
        }

        public TEntity GetOne(Func<TEntity, bool> where)
        {
            using (DbContext ctx = CreateContext())
            {
                DbSet<TEntity> DbSet = ctx.Set<TEntity>();

                return DbSet.Where(where).FirstOrDefault<TEntity>();
            }
        }

        public IEnumerable<TEntity> Get()
        {
            using (DbContext ctx = CreateContext())
            {
                DbSet<TEntity> DbSet = ctx.Set<TEntity>();
                return DbSet.ToList();
            }
        }

        public IEnumerable<TEntity> Get(Func<TEntity, bool> where)
        {
            using (DbContext ctx = CreateContext())
            {
                DbSet<TEntity> DbSet = ctx.Set<TEntity>();
                return DbSet.Where(where).ToList();
            }
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", int limit = 0)
        {
            IEnumerable<TEntity> list;

            try
            {
                DbContext ctx = CreateContext();
                DbSet<TEntity> DbSet = ctx.Set<TEntity>();

                IQueryable<TEntity> query = DbSet;

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                if (orderBy != null)
                {
                    query = orderBy(query);
                }

                if (limit != 0)
                {
                    query.Take(limit);
                }

                list = query.ToList();
                ctx.Dispose();

                return list;
            }
            catch (Exception ex)
            {
                //EmgeaLog.HandlerDBErrorLog(ex);

                throw ex;
            }

        }

        public IEnumerable<TEntity> Get(int page, int qtPage, out int total, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            using (DbContext ctx = CreateContext())
            {
                ctx.Configuration.LazyLoadingEnabled = lazyLoadingEnabled;
                ctx.Configuration.ProxyCreationEnabled = proxyCreationEnabled;
                DbSet<TEntity> DbSet = ctx.Set<TEntity>();

                try
                {
                    IQueryable<TEntity> query = DbSet;

                    if (filter != null)
                    {
                        query = query.Where(filter);
                    }

                    foreach (var includeProperty in includeProperties.Split
                        (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProperty);
                    }

                    if (orderBy != null)
                    {
                        query = orderBy(query);
                    }

                    total = query.Count();

                    if (qtPage != 0)
                    {
                        query = query.Skip((page == 0 ? 0 : page - 1) * qtPage).Take(qtPage);
                    }

                    return query.ToList();

                }
                catch (Exception ex)
                {
                    //EmgeaLog.HandlerDBErrorLog(ex);

                    throw ex;
                }
            }
        }

        public int GetCount(Expression<Func<TEntity, bool>> filter = null)
        {
            using (DbContext ctx = CreateContext())
            {
                ctx.Configuration.LazyLoadingEnabled = lazyLoadingEnabled;
                ctx.Configuration.ProxyCreationEnabled = proxyCreationEnabled;
                DbSet<TEntity> DbSet = ctx.Set<TEntity>();
                try
                {
                    IQueryable<TEntity> query = DbSet;

                    if (filter != null)
                    {
                        return query.Count(filter);
                    }
                    else
                    {
                        return query.Count();
                    }
                }
                catch (Exception ex)
                {
                    //EmgeaLog.HandlerDBErrorLog(ex);

                    throw ex;
                }
            }
        }

        private DbSet<TEntity> AddDbSets(DbSet<TEntity> DbSet)
        {
            dbSets = new ArrayList();
            foreach (TEntity item in dbSets)
            {
                DbSet.Add(item);
            }

            return DbSet;
        }

        public DbContext CreateContext()
        {
            DbContext ctx = new TContext();
            ctx.Configuration.LazyLoadingEnabled = lazyLoadingEnabled;
            ctx.Configuration.ProxyCreationEnabled = proxyCreationEnabled;
            return ctx;
        }

        public void Save()
        {
            DbContext ctx = CreateContext();

            DbSet<TEntity> DbSet = ctx.Set<TEntity>();

            foreach (TEntity entity in addList)
            {
                DbSet.Add(entity);
            }

            foreach (TEntity entity in updateList)
            {
                DbSet.Attach(entity);
                ctx.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            }

            foreach (TEntity entity in removeList)
            {
                if (ctx.Entry(entity).State == System.Data.Entity.EntityState.Detached)
                {
                    DbSet.Attach(entity);
                }

                DbSet.Remove(entity);
            }

            addList = new ArrayList();
            updateList = new ArrayList();
            removeList = new ArrayList();

            if (transactionScope != null)
            {
                transactionScope.AddContext(ctx);
            }
            else
            {
                try
                {
                    ctx.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    DbEntityEntry entry = ex.Entries.Single<DbEntityEntry>();
                    entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                }
                catch (DbEntityValidationException ex2)
                {
                    foreach (var entity in ex2.EntityValidationErrors)
                    {
                        var entityName = entity.Entry.Entity.GetType().Name;

                        foreach (var error in entity.ValidationErrors)
                        {
                            Console.WriteLine(String.Format("Erro ao salvar o domínio: {0} - Mensagem de Erro: {1} - Propriedade com Erro: {2}",
                                entityName, error.ErrorMessage, error.PropertyName));
                        }
                    }
                }
                finally
                {
                    ctx.Dispose();
                }

            }
        }
    }
}