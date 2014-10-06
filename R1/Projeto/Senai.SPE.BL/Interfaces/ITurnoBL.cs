using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Senai.SPE.Domain;

namespace Interfaces
{
    public interface ITurnoBL
    {
        bool Delete(int id);
        bool ProxyCreation { set; }
        bool LazyLoading { set; }
        void Add(Turno entity);
        bool AddUnique(Expression<Func<Turno, bool>> filter, Turno entity);
        void Update(Turno entity);
        void Delete(Turno entity);
        void Delete(IEnumerable<Turno> entitys);
        void Delete(Func<Turno, bool> predicate);
        Turno GetById(long Id);
        Turno GetOne(Func<Turno, bool> where);
        IEnumerable<Turno> Get();
        IEnumerable<Turno> Get(Func<Turno, bool> where);
        IEnumerable<Turno> Get(Expression<Func<Turno, bool>> filter = null, Func<IQueryable<Turno>, IOrderedQueryable<Turno>> orderBy = null, string includeProperties = "", int limit = 0);
        IEnumerable<Turno> Get(int page, int qtPage, out int total, Expression<Func<Turno, bool>> filter = null, Func<IQueryable<Turno>, IOrderedQueryable<Turno>> orderBy = null, string includeProperties = "");
        int GetCount(Expression<Func<Turno, bool>> filter = null);
        DbContext CreateContext();
        void Save();
    }
}