using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Senai.SPE.Domain;

namespace Interfaces
{
    public interface IMatrizBL
    {
        Matriz InserirMatriz(Matriz matriz);
        void AtualizarMatriz(Matriz matriz);
        void ApagarMatriz(int id);
        void DeleteVinculoMatriz(Matriz matriz);
        bool Delete(int id);
        bool ProxyCreation { set; }
        bool LazyLoading { set; }
        void Add(Matriz entity);
        bool AddUnique(Expression<Func<Matriz, bool>> filter, Matriz entity);
        void Update(Matriz entity);
        void Delete(Matriz entity);
        void Delete(IEnumerable<Matriz> entitys);
        void Delete(Func<Matriz, bool> predicate);
        Matriz GetById(long Id);
        Matriz GetOne(Func<Matriz, bool> where);
        IEnumerable<Matriz> Get();
        IEnumerable<Matriz> Get(Func<Matriz, bool> where);
        IEnumerable<Matriz> Get(Expression<Func<Matriz, bool>> filter = null, Func<IQueryable<Matriz>, IOrderedQueryable<Matriz>> orderBy = null, string includeProperties = "", int limit = 0);
        IEnumerable<Matriz> Get(int page, int qtPage, out int total, Expression<Func<Matriz, bool>> filter = null, Func<IQueryable<Matriz>, IOrderedQueryable<Matriz>> orderBy = null, string includeProperties = "");
        int GetCount(Expression<Func<Matriz, bool>> filter = null);
        DbContext CreateContext();
        void Save();
    }
}