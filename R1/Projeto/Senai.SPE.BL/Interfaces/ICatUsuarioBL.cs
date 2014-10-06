using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Senai.SPE.Domain;

namespace Interfaces
{
    public interface ICatUsuarioBL
    {
        CatUsuario InserirCatUsuario(CatUsuario model);
        void AtualizarCatUsuario(CatUsuario model);
        void ApagarCatUsuario(int id);
        bool Delete(int id);
        bool ProxyCreation { set; }
        bool LazyLoading { set; }
        void Add(CatUsuario entity);
        bool AddUnique(Expression<Func<CatUsuario, bool>> filter, CatUsuario entity);
        void Update(CatUsuario entity);
        void Delete(CatUsuario entity);
        void Delete(IEnumerable<CatUsuario> entitys);
        void Delete(Func<CatUsuario, bool> predicate);
        CatUsuario GetById(long Id);
        CatUsuario GetOne(Func<CatUsuario, bool> where);
        IEnumerable<CatUsuario> Get();
        IEnumerable<CatUsuario> Get(Func<CatUsuario, bool> where);
        IEnumerable<CatUsuario> Get(Expression<Func<CatUsuario, bool>> filter = null, Func<IQueryable<CatUsuario>, IOrderedQueryable<CatUsuario>> orderBy = null, string includeProperties = "", int limit = 0);
        IEnumerable<CatUsuario> Get(int page, int qtPage, out int total, Expression<Func<CatUsuario, bool>> filter = null, Func<IQueryable<CatUsuario>, IOrderedQueryable<CatUsuario>> orderBy = null, string includeProperties = "");
        int GetCount(Expression<Func<CatUsuario, bool>> filter = null);
        DbContext CreateContext();
        void Save();
    }
}