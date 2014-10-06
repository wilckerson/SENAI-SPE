using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Senai.SPE.Domain;

namespace Interfaces
{
    public interface IAgendaComponenteBL
    {
        List<AgendaComponente> GetComponenteAgendados(int IdModulo, int IdComponente, int idTurma);
        void AgendarComponente(AgendaComponente agenda);
        bool ProxyCreation { set; }
        bool LazyLoading { set; }
        void Add(AgendaComponente entity);
        bool AddUnique(Expression<Func<AgendaComponente, bool>> filter, AgendaComponente entity);
        void Update(AgendaComponente entity);
        void Delete(AgendaComponente entity);
        void Delete(IEnumerable<AgendaComponente> entitys);
        void Delete(Func<AgendaComponente, bool> predicate);
        AgendaComponente GetById(long Id);
        AgendaComponente GetOne(Func<AgendaComponente, bool> where);
        IEnumerable<AgendaComponente> Get();
        IEnumerable<AgendaComponente> Get(Func<AgendaComponente, bool> where);
        IEnumerable<AgendaComponente> Get(Expression<Func<AgendaComponente, bool>> filter = null, Func<IQueryable<AgendaComponente>, IOrderedQueryable<AgendaComponente>> orderBy = null, string includeProperties = "", int limit = 0);
        IEnumerable<AgendaComponente> Get(int page, int qtPage, out int total, Expression<Func<AgendaComponente, bool>> filter = null, Func<IQueryable<AgendaComponente>, IOrderedQueryable<AgendaComponente>> orderBy = null, string includeProperties = "");
        int GetCount(Expression<Func<AgendaComponente, bool>> filter = null);
        DbContext CreateContext();
        void Save();
    }
}