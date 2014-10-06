using Common;
using Repository;
using Senai.SPE.Domain;
using SPERepository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BusinessLogic
{

    public class DocenteBL : SPERepository<Docente>
    {
        public DocenteBL(TransactionRepository transaction)
            : base(transaction)
        {

        }

        public List<ufnListarDocentes1_Result> RetornarDocentesPor(int? idTipoContrato, int? pageIndex, int? pageNumber)
        {
            var lista = (from u in this.Context.ufnListarDocentes1(idTipoContrato, pageIndex, pageNumber) select u).ToList();
            return lista;
        }

        public Docente InserirDocente(Docente docente, List<Componente> componentes)
        {
            var doc = Context.Docente.Add(docente);

            var array = componentes.Select(a => a.IdComponente).ToList();
            var componentesAdd = (from u in this.Context.Componente where array.Contains(u.IdComponente) select u).ToList();

            foreach (var item in componentesAdd.ToList())
            {
                docente.Componente.Add(item);
            }

            this.Context.SaveChanges();
            this.Context.Dispose();
            Save();
            return doc;
        }

        public void AtualizarAgendaDocente(Docente docente, List<Componente> componentes)
        {
            this.Update(docente);
            this.Save();
            var ctx = new SPEContext();
            ctx.Configuration.LazyLoadingEnabled = false;
            ctx.Configuration.ProxyCreationEnabled = false;
          List<Componente> lista= new List<Componente>();
            foreach (var item in componentes)
            {
                lista.Add(ctx.Componente.Where(a=>a.IdComponente == item.IdComponente).FirstOrDefault());
            }
            var DocenteComponente = (from u in ctx.Docente.Include("Componente")
                                          where u.IdDocente == docente.IdDocente
                                          select u).SingleOrDefault();

            if (DocenteComponente != null && DocenteComponente.Componente.Count > 0)
            {
                foreach (var item in DocenteComponente.Componente.ToList())
                {
                    DocenteComponente.Componente.Remove(item);
                }

            }
            DocenteComponente.Componente = lista;
            ctx.SaveChanges();
            ctx.Dispose();
            Save();
        }

        public void ApagarDocente(int id)
        {
            Docente docente = Get(e => e.IdDocente == id, null, "AgendaComponente,AgendaDocente,Componente,AreaAtuacao").SingleOrDefault();

            if (docente.AgendaDocente.Any())
                throw new CustomException("Erro ao excluir Docente. Este Docente já se encontra agendado.");

            if (docente.Componente.Any())
            {
                foreach (var item in docente.Componente.ToList())
                {
                    docente.Componente.Remove(item);
                }
            }
            if (docente.AreaAtuacao.Any()) {
                foreach (var item in docente.AreaAtuacao.ToList())
                {
                    docente.AreaAtuacao.Remove(item);
                }
            }
            AtualizarAgendaDocente(docente, docente.Componente.ToList());
            AtualizarAreAtuacaoDocente(docente, docente.AreaAtuacao.ToList());

            Delete(docente);
        }

        public void AtualizarAreAtuacaoDocente(Docente docente, List<AreaAtuacao> areaAtuacao)
        {
            this.Update(docente);
            this.Save();
            var ctx = new SPEContext();
            ctx.Configuration.LazyLoadingEnabled = false;
            ctx.Configuration.ProxyCreationEnabled = false;
            List<AreaAtuacao> lista = new List<AreaAtuacao>();
            foreach (var item in areaAtuacao)
            {
                lista.Add(ctx.AreaAtuacao.Where(a => a.IdAreaAtuacao == item.IdAreaAtuacao).FirstOrDefault());
            }
            var DocenteAreaAtuacao = (from u in ctx.Docente.Include("AreaAtuacao")
                                     where u.IdDocente == docente.IdDocente
                                     select u).SingleOrDefault();

            if (DocenteAreaAtuacao != null && DocenteAreaAtuacao.AreaAtuacao.Count > 0)
            {
                foreach (var item in DocenteAreaAtuacao.AreaAtuacao.ToList())
                {
                    DocenteAreaAtuacao.AreaAtuacao.Remove(item);
                }

            }
            DocenteAreaAtuacao.AreaAtuacao = lista;
            ctx.SaveChanges();
            ctx.Dispose();
            Save();
        }
    }
}
