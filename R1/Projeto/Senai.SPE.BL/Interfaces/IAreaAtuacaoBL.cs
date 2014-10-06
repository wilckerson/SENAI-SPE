using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Senai.SPE.Domain;

namespace Interfaces
{
    public interface IAreaAtuacaoBL
    {
        void InserirAreaAtuacao(AreaAtuacao areaAtuacao);
        void AtualizarAreaAtuacao(AreaAtuacao areaAtuacao);
        void DeletarAreaAtuacao(int id);
        bool ProxyCreation { set; }
        bool LazyLoading { set; }
        void Add(AreaAtuacao entity);
        bool AddUnique(Expression<Func<AreaAtuacao, bool>> filter, AreaAtuacao entity);
        void Update(AreaAtuacao entity);
        void Delete(AreaAtuacao entity);
        void Delete(IEnumerable<AreaAtuacao> entitys);
        void Delete(Func<AreaAtuacao, bool> predicate);
        AreaAtuacao GetById(long Id);
        AreaAtuacao GetOne(Func<AreaAtuacao, bool> where);
        IEnumerable<AreaAtuacao> Get();
        IEnumerable<AreaAtuacao> Get(Func<AreaAtuacao, bool> where);
        IEnumerable<AreaAtuacao> Get(Expression<Func<AreaAtuacao, bool>> filter = null, Func<IQueryable<AreaAtuacao>, IOrderedQueryable<AreaAtuacao>> orderBy = null, string includeProperties = "", int limit = 0);
        IEnumerable<AreaAtuacao> Get(int page, int qtPage, out int total, Expression<Func<AreaAtuacao, bool>> filter = null, Func<IQueryable<AreaAtuacao>, IOrderedQueryable<AreaAtuacao>> orderBy = null, string includeProperties = "");
        int GetCount(Expression<Func<AreaAtuacao, bool>> filter = null);
        DbContext CreateContext();
        void Save();
    }
}