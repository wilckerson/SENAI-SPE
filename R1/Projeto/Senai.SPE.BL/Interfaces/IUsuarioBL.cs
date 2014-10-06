using Senai.SPE.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;


namespace Interfaces
{
    public interface IUsuarioBL
    {
        Usuario Inserir(Usuario model);
        void Atualizar(Usuario model);
        void Modificar(Usuario model);
        bool ProxyCreation { set; }
        bool LazyLoading { set; }
        void Add(Usuario entity);
        bool AddUnique(Expression<Func<Usuario, bool>> filter, Usuario entity);
        void Update(Usuario entity);
        void Delete(Usuario entity);
        void Delete(IEnumerable<Usuario> entitys);
        void Delete(Func<Usuario, bool> predicate);
        Usuario GetById(long Id);
        Usuario GetOne(Func<Usuario, bool> where);
        IEnumerable<Usuario> Get();
        IEnumerable<Usuario> Get(Func<Usuario, bool> where);
        IEnumerable<Usuario> Get(Expression<Func<Usuario, bool>> filter = null, Func<IQueryable<Usuario>, IOrderedQueryable<Usuario>> orderBy = null, string includeProperties = "", int limit = 0);
        IEnumerable<Usuario> Get(int page, int qtPage, out int total, Expression<Func<Usuario, bool>> filter = null, Func<IQueryable<Usuario>, IOrderedQueryable<Usuario>> orderBy = null, string includeProperties = "");
        int GetCount(Expression<Func<Usuario, bool>> filter = null);
        DbContext CreateContext();
        void Save();
    }
}