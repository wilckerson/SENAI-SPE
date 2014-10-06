using Common;
using Repository;
using Senai.SPE.Domain;
using SPERepository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BusinessLogic
{
    public class ComponenteBL : SPERepository<Componente>
    {
        public ComponenteBL(TransactionRepository transaction = null)
            : base(transaction)
        {

        }

        public List<ufnListarComponentesPorModulo1_Result> BuscarComponentesPor(string filtro = null, int pageIndex = 1, int pageNumber = 0)
        {
            var componentes = (from u in Context.ufnListarComponentesPorModulo1("0", filtro, pageIndex, pageNumber)
                               select u).ToList();
            return componentes;
        }

        public List<Componente> BuscarComponentesPorAreaAtuacao(string areaAtuacao)
        {
            List<int> listaidAreaAtuacao = new List<int>();

            var listaId = areaAtuacao.Split(',');
            foreach (var s in listaId)
            {
                listaidAreaAtuacao.Add(int.Parse(s));
            }

            var ctx = new SPEContext();
            ctx.Configuration.LazyLoadingEnabled = false;
            ctx.Configuration.ProxyCreationEnabled = false;
            var componentes = (from u in ctx.Componente.Include("AreaAtuacao")
                               where u.AreaAtuacao.Count(v => listaidAreaAtuacao.Contains(v.IdAreaAtuacao)) > 0
                             
                               select u).ToList();
            foreach (Componente componente in componentes)
            {
                foreach ( var z in componente.AreaAtuacao)
                {
                    z.Componente = null;
                }
            }
            return componentes;

        }

        public Componente InserirComponente(Componente componente, int[] idTipoAmbiente)
        {
            var existe = Get(e => string.Compare(e.Nome.ToLower(), componente.Nome.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0 && e.CH == componente.CH);

            if (existe.Any())
                throw new CustomException("Erro ao cadastrar Componente. Já existe um Componente cadastrado com esse Nome e CH.");


            var vinculoComponente = Add(componente);
            return VincularTipoCompentes(vinculoComponente, idTipoAmbiente);
        }

        public Componente VincularTipoCompentes(Componente componente, int[] idTipoAmbiente)
        {
            var ctx = new SPEContext();
            ctx.Configuration.LazyLoadingEnabled = false;
            ctx.Configuration.ProxyCreationEnabled = false;
            var componenteTipoAmbiente = (from u in ctx.Componente.Include("TipoAmbiente")
                                          where u.IdComponente == componente.IdComponente
                                          select u).SingleOrDefault();

            if (componenteTipoAmbiente != null && componenteTipoAmbiente.TipoAmbiente.Count > 0)
            {
                foreach (var item in componenteTipoAmbiente.TipoAmbiente.ToList())
                {
                    componenteTipoAmbiente.TipoAmbiente.Remove(item);
                }

            }

            var tiposAmbiente = (from u in ctx.TipoAmbiente
                                 where idTipoAmbiente.Contains(u.IdTipoAmbiente)
                                 select u).ToList();

            foreach (var item in tiposAmbiente)
            {
                componenteTipoAmbiente.TipoAmbiente.Add(item);
            }

            ctx.SaveChanges();
            ctx.Dispose();

            return componente;
        }

        public void AtualizarComponente(Componente componente, int[] idTipoAmbiente)
        {
            var existe = Get(e => string.Compare(e.Nome.ToLower(), componente.Nome.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0
                                                    && e.CH == componente.CH
                                                    && e.IdComponente != componente.IdComponente);

            if (existe.Any())
                throw new CustomException("Erro ao cadastrar Componente. Já existe um Componente cadastrado com esse Nome e CH.");


            Update(componente);
            VincularTipoCompentes(componente, idTipoAmbiente);
        }

        public void ApagarComponente(int id)
        {
              var  componente = this.Context.Componente.Include("Docente")
                .Include("AgendaDocente")
                .Include("AgendaComponente")
                .Include("AgendaAmbiente")
                .Include("TipoAmbiente")
                .Include("AreaAtuacao")
                .Where(a => a.IdComponente == id).FirstOrDefault();
         
            if (componente.AgendaAmbiente.Any() || componente.AgendaComponente.Any() || componente.AgendaDocente.Any())
                throw new CustomException("Erro ao excluir Componente. Este componente já encontra-se agendado.");

            if (componente.Docente.Any())
            {
                throw new CustomException("Erro ao excluir Componente. Este componente está vinculado a algum docente.");
            }
          
            if (componente.TipoAmbiente.Any())
            {
                foreach (var item in componente.TipoAmbiente.ToList())
                {
                    componente.TipoAmbiente.Remove(item);
                }
            }

            if (componente.AreaAtuacao.Any())
            {
                foreach (var item in componente.AreaAtuacao.ToList())
                {
                    componente.AreaAtuacao.Remove(item);
                }
            }
            this.Context.SaveChanges();
            this.Context.Dispose();
            Delete(componente);
        }

        public void DeletarVinculoComponente(Componente componente)
        {

            if (componente != null)
            {
                if (componente.Modulo.Any())
                {
                    this.Delete(componente);
                }
                else if (componente.TipoAmbiente.Any())
                {
                    foreach (var item in componente.TipoAmbiente.ToList())
                    {
                        componente.TipoAmbiente.Remove(item);
                        this.Save();
                    }
                    this.Delete(componente);
                }
                else
                {
                    this.Delete(componente);
                }
            }
        }

        public void AtualizarAreAtuacaoComponente(Componente componente, List<AreaAtuacao> areasAtuacao)
        {
            this.Update(componente);
            this.Save();
            var ctx = new SPEContext();
            ctx.Configuration.LazyLoadingEnabled = false;
            ctx.Configuration.ProxyCreationEnabled = false;
            List<AreaAtuacao> lista = new List<AreaAtuacao>();
            foreach (var item in areasAtuacao)
            {
                lista.Add(ctx.AreaAtuacao.Where(a => a.IdAreaAtuacao == item.IdAreaAtuacao).FirstOrDefault());
            }
            var ComponenteAreaAtuacao = (from u in ctx.Componente.Include("AreaAtuacao")
                                      where u.IdComponente == componente.IdComponente
                                     select u).SingleOrDefault();

            if (ComponenteAreaAtuacao != null && ComponenteAreaAtuacao.AreaAtuacao.Count > 0)
            {
                foreach (var item in ComponenteAreaAtuacao.AreaAtuacao.ToList())
                {
                    ComponenteAreaAtuacao.AreaAtuacao.Remove(item);
                }

            }
            ComponenteAreaAtuacao.AreaAtuacao = lista;
            ctx.SaveChanges();
            ctx.Dispose();
            Save();
        }

    }
}
