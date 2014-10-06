using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Senai.SPE.Domain;

namespace Interfaces
{
    public interface IAgendaDocenteBL
    {
        List<AgendaDocente> GetDocentesAgendados(int IdModulo, int IdComponente, int idTurma);
        void AgendarDocente(AgendaDocente agenda);
        bool ProxyCreation { set; }
        bool LazyLoading { set; }
        void Add(AgendaDocente entity);
        bool AddUnique(Expression<Func<AgendaDocente, bool>> filter, AgendaDocente entity);
        void Update(AgendaDocente entity);
        void Delete(AgendaDocente entity);
        void Delete(IEnumerable<AgendaDocente> entitys);
        void Delete(Func<AgendaDocente, bool> predicate);
        AgendaDocente GetById(long Id);
        AgendaDocente GetOne(Func<AgendaDocente, bool> where);
        IEnumerable<AgendaDocente> Get();
        IEnumerable<AgendaDocente> Get(Func<AgendaDocente, bool> where);
        IEnumerable<AgendaDocente> Get(Expression<Func<AgendaDocente, bool>> filter = null, Func<IQueryable<AgendaDocente>, IOrderedQueryable<AgendaDocente>> orderBy = null, string includeProperties = "", int limit = 0);
        IEnumerable<AgendaDocente> Get(int page, int qtPage, out int total, Expression<Func<AgendaDocente, bool>> filter = null, Func<IQueryable<AgendaDocente>, IOrderedQueryable<AgendaDocente>> orderBy = null, string includeProperties = "");
        int GetCount(Expression<Func<AgendaDocente, bool>> filter = null);
        DbContext CreateContext();
        void Save();
    }
}