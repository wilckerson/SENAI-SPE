using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Senai.SPE.Domain;

namespace Interfaces
{
    public interface ITipoRecursoBL
    {
        void InserirTipoRecurso(TipoRecurso tipoRecurso);
        void AtualizarTipoRecurso(TipoRecurso tipoRecurso);
        void DeletarTipoRecurso(int id);
        bool Delete(int id);
        bool ProxyCreation { set; }
        bool LazyLoading { set; }
        void Add(TipoRecurso entity);
        bool AddUnique(Expression<Func<TipoRecurso, bool>> filter, TipoRecurso entity);
        void Update(TipoRecurso entity);
        void Delete(TipoRecurso entity);
        void Delete(IEnumerable<TipoRecurso> entitys);
        void Delete(Func<TipoRecurso, bool> predicate);
        TipoRecurso GetById(long Id);
        TipoRecurso GetOne(Func<TipoRecurso, bool> where);
        IEnumerable<TipoRecurso> Get();
        IEnumerable<TipoRecurso> Get(Func<TipoRecurso, bool> where);
        IEnumerable<TipoRecurso> Get(Expression<Func<TipoRecurso, bool>> filter = null, Func<IQueryable<TipoRecurso>, IOrderedQueryable<TipoRecurso>> orderBy = null, string includeProperties = "", int limit = 0);
        IEnumerable<TipoRecurso> Get(int page, int qtPage, out int total, Expression<Func<TipoRecurso, bool>> filter = null, Func<IQueryable<TipoRecurso>, IOrderedQueryable<TipoRecurso>> orderBy = null, string includeProperties = "");
        int GetCount(Expression<Func<TipoRecurso, bool>> filter = null);
        DbContext CreateContext();
        void Save();
    }
}