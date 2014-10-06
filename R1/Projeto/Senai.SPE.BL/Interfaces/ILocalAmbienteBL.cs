using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Senai.SPE.Domain;

namespace Interfaces
{
    public interface ILocalAmbienteBL
    {
        void InserirLocalAmbiente(LocalAmbiente localAmbiente);
        void AtualizarLocalAmbiente(LocalAmbiente localAmbiente);
        void DeletarLocalAmbiente(int id);
        bool Delete(int id);
        bool ProxyCreation { set; }
        bool LazyLoading { set; }
        void Add(LocalAmbiente entity);
        bool AddUnique(Expression<Func<LocalAmbiente, bool>> filter, LocalAmbiente entity);
        void Update(LocalAmbiente entity);
        void Delete(LocalAmbiente entity);
        void Delete(IEnumerable<LocalAmbiente> entitys);
        void Delete(Func<LocalAmbiente, bool> predicate);
        LocalAmbiente GetById(long Id);
        LocalAmbiente GetOne(Func<LocalAmbiente, bool> where);
        IEnumerable<LocalAmbiente> Get();
        IEnumerable<LocalAmbiente> Get(Func<LocalAmbiente, bool> where);
        IEnumerable<LocalAmbiente> Get(Expression<Func<LocalAmbiente, bool>> filter = null, Func<IQueryable<LocalAmbiente>, IOrderedQueryable<LocalAmbiente>> orderBy = null, string includeProperties = "", int limit = 0);
        IEnumerable<LocalAmbiente> Get(int page, int qtPage, out int total, Expression<Func<LocalAmbiente, bool>> filter = null, Func<IQueryable<LocalAmbiente>, IOrderedQueryable<LocalAmbiente>> orderBy = null, string includeProperties = "");
        int GetCount(Expression<Func<LocalAmbiente, bool>> filter = null);
        DbContext CreateContext();
        void Save();
    }
}