using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Senai.SPE.Domain;

namespace Interfaces
{
    public interface IAmbienteBL
    {
        void InserirAmbienteRecurso(Ambiente model, string[] arrayRecurso);
        void AtualizarAmbienteRecurso(Ambiente model, string[] arrayRecurso);
        void DeletarAmbiente(int id);
        List<ufnListarAmbientes1_Result> RetornarAmbientesPor(int? idTipoAmbiente, int? pageIndex, int? pageNumber);
        bool ProxyCreation { set; }
        bool LazyLoading { set; }
        void Add(Ambiente entity);
        bool AddUnique(Expression<Func<Ambiente, bool>> filter, Ambiente entity);
        void Update(Ambiente entity);
        void Delete(Ambiente entity);
        void Delete(int i);
        void Delete(IEnumerable<Ambiente> entitys);
        void Delete(Func<Ambiente, bool> predicate);
        Ambiente GetById(long Id);
        Ambiente GetOne(Func<Ambiente, bool> where);
        IEnumerable<Ambiente> Get();
        IEnumerable<Ambiente> Get(Func<Ambiente, bool> where);
        IEnumerable<Ambiente> Get(Expression<Func<Ambiente, bool>> filter = null, Func<IQueryable<Ambiente>, IOrderedQueryable<Ambiente>> orderBy = null, string includeProperties = "", int limit = 0);
        IEnumerable<Ambiente> Get(int page, int qtPage, out int total, Expression<Func<Ambiente, bool>> filter = null, Func<IQueryable<Ambiente>, IOrderedQueryable<Ambiente>> orderBy = null, string includeProperties = "");
        int GetCount(Expression<Func<Ambiente, bool>> filter = null);
        DbContext CreateContext();
        void Save();
    }
}