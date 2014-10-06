using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Senai.SPE.Domain;

namespace Interfaces
{
    public interface IComponenteBL
    {
        List<ufnListarComponentesPorModulo1_Result> BuscarComponentesPor(string filtro = null, int pageIndex = 1, int pageNumber = 0);
        List<Componente> BuscarComponentesPorAreaAtuacao(string areaAtuacao);
        Componente InserirComponente(Componente componente, int[] idTipoAmbiente);
        Componente VincularTipoCompentes(Componente componente, int[] idTipoAmbiente);
        void AtualizarComponente(Componente componente, int[] idTipoAmbiente);
        void ApagarComponente(int id);
        void DeletarVinculoComponente(Componente componente);
        void AtualizarAreAtuacaoComponente(Componente componente, List<AreaAtuacao> areasAtuacao);
        bool Delete(int id);
        bool ProxyCreation { set; }
        bool LazyLoading { set; }
        void Add(Componente entity);
        bool AddUnique(Expression<Func<Componente, bool>> filter, Componente entity);
        void Update(Componente entity);
        void Delete(Componente entity);
        void Delete(IEnumerable<Componente> entitys);
        void Delete(Func<Componente, bool> predicate);
        Componente GetById(long Id);
        Componente GetOne(Func<Componente, bool> where);
        IEnumerable<Componente> Get();
        IEnumerable<Componente> Get(Func<Componente, bool> where);
        IEnumerable<Componente> Get(Expression<Func<Componente, bool>> filter = null, Func<IQueryable<Componente>, IOrderedQueryable<Componente>> orderBy = null, string includeProperties = "", int limit = 0);
        IEnumerable<Componente> Get(int page, int qtPage, out int total, Expression<Func<Componente, bool>> filter = null, Func<IQueryable<Componente>, IOrderedQueryable<Componente>> orderBy = null, string includeProperties = "");
        int GetCount(Expression<Func<Componente, bool>> filter = null);
        DbContext CreateContext();
        void Save();
    }
}