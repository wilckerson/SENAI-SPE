using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Senai.SPE.Domain;

namespace Interfaces
{
    public interface IRecursoBL
    {
        void InserirRecurso(Recurso recurso);
        void AtualizarRecurso(Recurso recurso);
        void ApagarRecurso(int id);
        bool Delete(int id);
        bool ProxyCreation { set; }
        bool LazyLoading { set; }
        void Add(Recurso entity);
        bool AddUnique(Expression<Func<Recurso, bool>> filter, Recurso entity);
        void Update(Recurso entity);
        void Delete(Recurso entity);
        void Delete(IEnumerable<Recurso> entitys);
        void Delete(Func<Recurso, bool> predicate);
        Recurso GetById(long Id);
        Recurso GetOne(Func<Recurso, bool> where);
        IEnumerable<Recurso> Get();
        IEnumerable<Recurso> Get(Func<Recurso, bool> where);
        IEnumerable<Recurso> Get(Expression<Func<Recurso, bool>> filter = null, Func<IQueryable<Recurso>, IOrderedQueryable<Recurso>> orderBy = null, string includeProperties = "", int limit = 0);
        IEnumerable<Recurso> Get(int page, int qtPage, out int total, Expression<Func<Recurso, bool>> filter = null, Func<IQueryable<Recurso>, IOrderedQueryable<Recurso>> orderBy = null, string includeProperties = "");
        int GetCount(Expression<Func<Recurso, bool>> filter = null);
        DbContext CreateContext();
        void Save();
    }
}