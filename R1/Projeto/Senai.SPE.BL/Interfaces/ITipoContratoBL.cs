using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Senai.SPE.Domain;

namespace Interfaces
{
    public interface ITipoContratoBL
    {
        TipoContrato Inserir(TipoContrato model);
        void Atualizar(TipoContrato model);
        void Apagar(int id);
        bool Delete(int id);
        bool ProxyCreation { set; }
        bool LazyLoading { set; }
        void Add(TipoContrato entity);
        bool AddUnique(Expression<Func<TipoContrato, bool>> filter, TipoContrato entity);
        void Update(TipoContrato entity);
        void Delete(TipoContrato entity);
        void Delete(IEnumerable<TipoContrato> entitys);
        void Delete(Func<TipoContrato, bool> predicate);
        TipoContrato GetById(long Id);
        TipoContrato GetOne(Func<TipoContrato, bool> where);
        IEnumerable<TipoContrato> Get();
        IEnumerable<TipoContrato> Get(Func<TipoContrato, bool> where);
        IEnumerable<TipoContrato> Get(Expression<Func<TipoContrato, bool>> filter = null, Func<IQueryable<TipoContrato>, IOrderedQueryable<TipoContrato>> orderBy = null, string includeProperties = "", int limit = 0);
        IEnumerable<TipoContrato> Get(int page, int qtPage, out int total, Expression<Func<TipoContrato, bool>> filter = null, Func<IQueryable<TipoContrato>, IOrderedQueryable<TipoContrato>> orderBy = null, string includeProperties = "");
        int GetCount(Expression<Func<TipoContrato, bool>> filter = null);
        DbContext CreateContext();
        void Save();
    }
}