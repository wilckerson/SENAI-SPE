using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Senai.SPE.Domain;

namespace Interfaces
{
    public interface IModuloBL
    {
        void AddList(List<Modulo> lista);
        void RemoverModulos(int p, string Nome, string CH);
        void RemoverModulos(int p = 0);
        void VinculoModuloComponentes(List<Modulo> modulo, string Nome, string CH);
        bool Delete(int id);
        bool ProxyCreation { set; }
        bool LazyLoading { set; }
        void Add(Modulo entity);
        bool AddUnique(Expression<Func<Modulo, bool>> filter, Modulo entity);
        void Update(Modulo entity);
        void Delete(Modulo entity);
        void Delete(IEnumerable<Modulo> entitys);
        void Delete(Func<Modulo, bool> predicate);
        Modulo GetById(long Id);
        Modulo GetOne(Func<Modulo, bool> where);
        IEnumerable<Modulo> Get();
        IEnumerable<Modulo> Get(Func<Modulo, bool> where);
        IEnumerable<Modulo> Get(Expression<Func<Modulo, bool>> filter = null, Func<IQueryable<Modulo>, IOrderedQueryable<Modulo>> orderBy = null, string includeProperties = "", int limit = 0);
        IEnumerable<Modulo> Get(int page, int qtPage, out int total, Expression<Func<Modulo, bool>> filter = null, Func<IQueryable<Modulo>, IOrderedQueryable<Modulo>> orderBy = null, string includeProperties = "");
        int GetCount(Expression<Func<Modulo, bool>> filter = null);
        DbContext CreateContext();
        void Save();
    }
}