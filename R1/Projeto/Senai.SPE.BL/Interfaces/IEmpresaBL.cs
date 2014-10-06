using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Senai.SPE.Domain;

namespace Interfaces
{
    public interface IEmpresaBL
    {
        Empresa Inserir(Empresa model);
        void Atualizar(Empresa model);
        void Apagar(int id);
        bool Delete(int id);
        bool ProxyCreation { set; }
        bool LazyLoading { set; }
        void Add(Empresa entity);
        bool AddUnique(Expression<Func<Empresa, bool>> filter, Empresa entity);
        void Update(Empresa entity);
        void Delete(Empresa entity);
        void Delete(IEnumerable<Empresa> entitys);
        void Delete(Func<Empresa, bool> predicate);
        Empresa GetById(long Id);
        Empresa GetOne(Func<Empresa, bool> where);
        IEnumerable<Empresa> Get();
        IEnumerable<Empresa> Get(Func<Empresa, bool> where);
        IEnumerable<Empresa> Get(Expression<Func<Empresa, bool>> filter = null, Func<IQueryable<Empresa>, IOrderedQueryable<Empresa>> orderBy = null, string includeProperties = "", int limit = 0);
        IEnumerable<Empresa> Get(int page, int qtPage, out int total, Expression<Func<Empresa, bool>> filter = null, Func<IQueryable<Empresa>, IOrderedQueryable<Empresa>> orderBy = null, string includeProperties = "");
        int GetCount(Expression<Func<Empresa, bool>> filter = null);
        DbContext CreateContext();
        void Save();
    }
}