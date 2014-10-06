using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Senai.SPE.Domain;

namespace Interfaces
{
    public interface ICalendarioLegendaBL
    {
        bool Delete(int id);
        bool ProxyCreation { set; }
        bool LazyLoading { set; }
        void Add(CalendarioLegenda entity);
        bool AddUnique(Expression<Func<CalendarioLegenda, bool>> filter, CalendarioLegenda entity);
        void Update(CalendarioLegenda entity);
        void Delete(CalendarioLegenda entity);
        void Delete(IEnumerable<CalendarioLegenda> entitys);
        void Delete(Func<CalendarioLegenda, bool> predicate);
        CalendarioLegenda GetById(long Id);
        CalendarioLegenda GetOne(Func<CalendarioLegenda, bool> where);
        IEnumerable<CalendarioLegenda> Get();
        IEnumerable<CalendarioLegenda> Get(Func<CalendarioLegenda, bool> where);
        IEnumerable<CalendarioLegenda> Get(Expression<Func<CalendarioLegenda, bool>> filter = null, Func<IQueryable<CalendarioLegenda>, IOrderedQueryable<CalendarioLegenda>> orderBy = null, string includeProperties = "", int limit = 0);
        IEnumerable<CalendarioLegenda> Get(int page, int qtPage, out int total, Expression<Func<CalendarioLegenda, bool>> filter = null, Func<IQueryable<CalendarioLegenda>, IOrderedQueryable<CalendarioLegenda>> orderBy = null, string includeProperties = "");
        int GetCount(Expression<Func<CalendarioLegenda, bool>> filter = null);
        DbContext CreateContext();
        void Save();
    }
}