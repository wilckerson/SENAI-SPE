using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Senai.SPE.Domain;

namespace Interfaces
{
    public interface ICBOBL
    {
        void InserirCBO(CBO CBO);
        void AtualizarCBO(CBO CBO);
        void DeletarCBO(int id);
        bool Delete(int id);
        bool ProxyCreation { set; }
        bool LazyLoading { set; }
        void Add(CBO entity);
        bool AddUnique(Expression<Func<CBO, bool>> filter, CBO entity);
        void Update(CBO entity);
        void Delete(CBO entity);
        void Delete(IEnumerable<CBO> entitys);
        void Delete(Func<CBO, bool> predicate);
        CBO GetById(long Id);
        CBO GetOne(Func<CBO, bool> where);
        IEnumerable<CBO> Get();
        IEnumerable<CBO> Get(Func<CBO, bool> where);
        IEnumerable<CBO> Get(Expression<Func<CBO, bool>> filter = null, Func<IQueryable<CBO>, IOrderedQueryable<CBO>> orderBy = null, string includeProperties = "", int limit = 0);
        IEnumerable<CBO> Get(int page, int qtPage, out int total, Expression<Func<CBO, bool>> filter = null, Func<IQueryable<CBO>, IOrderedQueryable<CBO>> orderBy = null, string includeProperties = "");
        int GetCount(Expression<Func<CBO, bool>> filter = null);
        DbContext CreateContext();
        void Save();
    }
}