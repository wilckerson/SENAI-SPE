using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Senai.SPE.Domain;

namespace Interfaces
{
    public interface ICRBL
    {
        IQueryable<ufnListarCR1_Result> ListarCR(int pageIndex, int numberRows = 1);
        void InserirCR(CR cr);
        void AtualizarCR(CR cr);
        void DeletarCR(int id);
        bool Delete(int id);
        bool ProxyCreation { set; }
        bool LazyLoading { set; }
        void Add(CR entity);
        bool AddUnique(Expression<Func<CR, bool>> filter, CR entity);
        void Update(CR entity);
        void Delete(CR entity);
        void Delete(IEnumerable<CR> entitys);
        void Delete(Func<CR, bool> predicate);
        CR GetById(long Id);
        CR GetOne(Func<CR, bool> where);
        IEnumerable<CR> Get();
        IEnumerable<CR> Get(Func<CR, bool> where);
        IEnumerable<CR> Get(Expression<Func<CR, bool>> filter = null, Func<IQueryable<CR>, IOrderedQueryable<CR>> orderBy = null, string includeProperties = "", int limit = 0);
        IEnumerable<CR> Get(int page, int qtPage, out int total, Expression<Func<CR, bool>> filter = null, Func<IQueryable<CR>, IOrderedQueryable<CR>> orderBy = null, string includeProperties = "");
        int GetCount(Expression<Func<CR, bool>> filter = null);
        DbContext CreateContext();
        void Save();
    }
}