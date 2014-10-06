using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Senai.SPE.Domain;

namespace Interfaces
{
    public interface IModalidadeBL
    {
        void InserirModalidade(Modalidade modalidade);
        void AtualizarModalidade(Modalidade modalidade);
        void ApagarModalidade(int id);
        bool Delete(int id);
        bool ProxyCreation { set; }
        bool LazyLoading { set; }
        void Add(Modalidade entity);
        bool AddUnique(Expression<Func<Modalidade, bool>> filter, Modalidade entity);
        void Update(Modalidade entity);
        void Delete(Modalidade entity);
        void Delete(IEnumerable<Modalidade> entitys);
        void Delete(Func<Modalidade, bool> predicate);
        Modalidade GetById(long Id);
        Modalidade GetOne(Func<Modalidade, bool> where);
        IEnumerable<Modalidade> Get();
        IEnumerable<Modalidade> Get(Func<Modalidade, bool> where);
        IEnumerable<Modalidade> Get(Expression<Func<Modalidade, bool>> filter = null, Func<IQueryable<Modalidade>, IOrderedQueryable<Modalidade>> orderBy = null, string includeProperties = "", int limit = 0);
        IEnumerable<Modalidade> Get(int page, int qtPage, out int total, Expression<Func<Modalidade, bool>> filter = null, Func<IQueryable<Modalidade>, IOrderedQueryable<Modalidade>> orderBy = null, string includeProperties = "");
        int GetCount(Expression<Func<Modalidade, bool>> filter = null);
        DbContext CreateContext();
        void Save();
    }
}