using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Senai.SPE.Domain;

namespace Interfaces
{
    public interface ITipoAtividadeBL
    {
        void Apagar(int id);
        bool Delete(int id);
        bool ProxyCreation { set; }
        bool LazyLoading { set; }
        void Add(TipoAtividade entity);
        bool AddUnique(Expression<Func<TipoAtividade, bool>> filter, TipoAtividade entity);
        void Update(TipoAtividade entity);
        void Delete(TipoAtividade entity);
        void Delete(IEnumerable<TipoAtividade> entitys);
        void Delete(Func<TipoAtividade, bool> predicate);
        TipoAtividade GetById(long Id);
        TipoAtividade GetOne(Func<TipoAtividade, bool> where);
        IEnumerable<TipoAtividade> Get();
        IEnumerable<TipoAtividade> Get(Func<TipoAtividade, bool> where);
        IEnumerable<TipoAtividade> Get(Expression<Func<TipoAtividade, bool>> filter = null, Func<IQueryable<TipoAtividade>, IOrderedQueryable<TipoAtividade>> orderBy = null, string includeProperties = "", int limit = 0);
        IEnumerable<TipoAtividade> Get(int page, int qtPage, out int total, Expression<Func<TipoAtividade, bool>> filter = null, Func<IQueryable<TipoAtividade>, IOrderedQueryable<TipoAtividade>> orderBy = null, string includeProperties = "");
        int GetCount(Expression<Func<TipoAtividade, bool>> filter = null);
        DbContext CreateContext();
        void Save();
    }
}