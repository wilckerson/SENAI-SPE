using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Senai.SPE.Domain;

namespace Interfaces
{
    public interface IUnidadeBL
    {
        void InserirUnidade(Unidade unidade);
        void AtualizarUnidade(Unidade unidade);
        void ApagarUnidade(int id);
        bool Delete(int id);
        bool ProxyCreation { set; }
        bool LazyLoading { set; }
        void Add(Unidade entity);
        bool AddUnique(Expression<Func<Unidade, bool>> filter, Unidade entity);
        void Update(Unidade entity);
        void Delete(Unidade entity);
        void Delete(IEnumerable<Unidade> entitys);
        void Delete(Func<Unidade, bool> predicate);
        Unidade GetById(long Id);
        Unidade GetOne(Func<Unidade, bool> where);
        IEnumerable<Unidade> Get();
        IEnumerable<Unidade> Get(Func<Unidade, bool> where);
        IEnumerable<Unidade> Get(Expression<Func<Unidade, bool>> filter = null, Func<IQueryable<Unidade>, IOrderedQueryable<Unidade>> orderBy = null, string includeProperties = "", int limit = 0);
        IEnumerable<Unidade> Get(int page, int qtPage, out int total, Expression<Func<Unidade, bool>> filter = null, Func<IQueryable<Unidade>, IOrderedQueryable<Unidade>> orderBy = null, string includeProperties = "");
        int GetCount(Expression<Func<Unidade, bool>> filter = null);
        DbContext CreateContext();
        void Save();
    }
}