using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Senai.SPE.Domain;

namespace Interfaces
{
    public interface ITurmaBL
    {
        void AtualizarTurmaPorAgendamento(string idTurma);
        IQueryable<ufnPegarMinMaxDatas1_Result> GetDatasMinMax(int id);
        void AtualizarTurma(Turma turma);
        List<ufnListarTurmas1_Result> RetornarTurmas(int pageIndex = 1, int pageNumber = 10, int? idCR = null, string nome = "", int? codigo = null, int? modalidade = null);
        bool Delete(int id);
        bool ProxyCreation { set; }
        bool LazyLoading { set; }
        void Add(Turma entity);
        bool AddUnique(Expression<Func<Turma, bool>> filter, Turma entity);
        void Update(Turma entity);
        void Delete(Turma entity);
        void Delete(IEnumerable<Turma> entitys);
        void Delete(Func<Turma, bool> predicate);
        Turma GetById(long Id);
        Turma GetOne(Func<Turma, bool> where);
        IEnumerable<Turma> Get();
        IEnumerable<Turma> Get(Func<Turma, bool> where);
        IEnumerable<Turma> Get(Expression<Func<Turma, bool>> filter = null, Func<IQueryable<Turma>, IOrderedQueryable<Turma>> orderBy = null, string includeProperties = "", int limit = 0);
        IEnumerable<Turma> Get(int page, int qtPage, out int total, Expression<Func<Turma, bool>> filter = null, Func<IQueryable<Turma>, IOrderedQueryable<Turma>> orderBy = null, string includeProperties = "");
        int GetCount(Expression<Func<Turma, bool>> filter = null);
        DbContext CreateContext();
        void Save();
    }
}