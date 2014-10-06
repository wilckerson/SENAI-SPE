using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Senai.SPE.Domain;

namespace Interfaces
{
    public interface ITipoAmbienteBL
    {
        void InserirTipoAmbiente(TipoAmbiente tipoAmbiente);
        void AtualizarTipoAmbiente(TipoAmbiente tipoAmbiente);
        void ApagarTipoAmbiente(int id);
        bool Delete(int id);
        bool ProxyCreation { set; }
        bool LazyLoading { set; }
        void Add(TipoAmbiente entity);
        bool AddUnique(Expression<Func<TipoAmbiente, bool>> filter, TipoAmbiente entity);
        void Update(TipoAmbiente entity);
        void Delete(TipoAmbiente entity);
        void Delete(IEnumerable<TipoAmbiente> entitys);
        void Delete(Func<TipoAmbiente, bool> predicate);
        TipoAmbiente GetById(long Id);
        TipoAmbiente GetOne(Func<TipoAmbiente, bool> where);
        IEnumerable<TipoAmbiente> Get();
        IEnumerable<TipoAmbiente> Get(Func<TipoAmbiente, bool> where);
        IEnumerable<TipoAmbiente> Get(Expression<Func<TipoAmbiente, bool>> filter = null, Func<IQueryable<TipoAmbiente>, IOrderedQueryable<TipoAmbiente>> orderBy = null, string includeProperties = "", int limit = 0);
        IEnumerable<TipoAmbiente> Get(int page, int qtPage, out int total, Expression<Func<TipoAmbiente, bool>> filter = null, Func<IQueryable<TipoAmbiente>, IOrderedQueryable<TipoAmbiente>> orderBy = null, string includeProperties = "");
        int GetCount(Expression<Func<TipoAmbiente, bool>> filter = null);
        DbContext CreateContext();
        void Save();
    }
}