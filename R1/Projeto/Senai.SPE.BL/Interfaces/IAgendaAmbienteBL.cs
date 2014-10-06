using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Senai.SPE.Domain;

namespace Interfaces
{
    public interface IAgendaAmbienteBL
    {
        List<AgendaAmbiente> GetAmbientesAgendados(int IdModulo, int IdComponente, int idTurma);
        void AgendarAmbiente(AgendaAmbiente agenda);
        bool ProxyCreation { set; }
        bool LazyLoading { set; }
        void Add(AgendaAmbiente entity);
        bool AddUnique(Expression<Func<AgendaAmbiente, bool>> filter, AgendaAmbiente entity);
        void Update(AgendaAmbiente entity);
        void Delete(AgendaAmbiente entity);
        void Delete(IEnumerable<AgendaAmbiente> entitys);
        void Delete(Func<AgendaAmbiente, bool> predicate);
        AgendaAmbiente GetById(long Id);
        AgendaAmbiente GetOne(Func<AgendaAmbiente, bool> where);
        IEnumerable<AgendaAmbiente> Get();
        IEnumerable<AgendaAmbiente> Get(Func<AgendaAmbiente, bool> where);
        IEnumerable<AgendaAmbiente> Get(Expression<Func<AgendaAmbiente, bool>> filter = null, Func<IQueryable<AgendaAmbiente>, IOrderedQueryable<AgendaAmbiente>> orderBy = null, string includeProperties = "", int limit = 0);
        IEnumerable<AgendaAmbiente> Get(int page, int qtPage, out int total, Expression<Func<AgendaAmbiente, bool>> filter = null, Func<IQueryable<AgendaAmbiente>, IOrderedQueryable<AgendaAmbiente>> orderBy = null, string includeProperties = "");
        int GetCount(Expression<Func<AgendaAmbiente, bool>> filter = null);
        DbContext CreateContext();
        void Save();
    }
}