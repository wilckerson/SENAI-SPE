using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Senai.SPE.Domain;

namespace Interfaces
{
    public interface IDocenteBL
    {
        List<ufnListarDocentes1_Result> RetornarDocentesPor(int? idTipoContrato, int? pageIndex, int? pageNumber);
        Docente InserirDocente(Docente docente, List<Componente> componentes);
        void AtualizarAgendaDocente(Docente docente, List<Componente> componentes);
        void ApagarDocente(int id);
        void AtualizarAreAtuacaoDocente(Docente docente, List<AreaAtuacao> areaAtuacao);
        bool Delete(int id);
        bool ProxyCreation { set; }
        bool LazyLoading { set; }
        void Add(Docente entity);
        bool AddUnique(Expression<Func<Docente, bool>> filter, Docente entity);
        void Update(Docente entity);
        void Delete(Docente entity);
        void Delete(IEnumerable<Docente> entitys);
        void Delete(Func<Docente, bool> predicate);
        Docente GetById(long Id);
        Docente GetOne(Func<Docente, bool> where);
        IEnumerable<Docente> Get();
        IEnumerable<Docente> Get(Func<Docente, bool> where);
        IEnumerable<Docente> Get(Expression<Func<Docente, bool>> filter = null, Func<IQueryable<Docente>, IOrderedQueryable<Docente>> orderBy = null, string includeProperties = "", int limit = 0);
        IEnumerable<Docente> Get(int page, int qtPage, out int total, Expression<Func<Docente, bool>> filter = null, Func<IQueryable<Docente>, IOrderedQueryable<Docente>> orderBy = null, string includeProperties = "");
        int GetCount(Expression<Func<Docente, bool>> filter = null);
        DbContext CreateContext();
        void Save();
    }
}