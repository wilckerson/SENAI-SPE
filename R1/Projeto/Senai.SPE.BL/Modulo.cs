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

    public class ModuloBL : SPERepository<Modulo>
    {
        public ModuloBL(TransactionRepository transaction = null)
            : base(transaction)
        {

        }

        public void AddList(List<Modulo> lista)
        {
            
            foreach (var item in lista)
            {
                var ctx = new SPEContext();
                ctx.Configuration.LazyLoadingEnabled = false;
                ctx.Configuration.ProxyCreationEnabled = false;

                Modulo modulo = new Modulo();
                modulo.Nome = item.Nome;
                modulo.IdMatriz = item.IdMatriz;
                var matriz = ctx.Matriz.Include("Modulo").Where(a => a.IdMatriz == item.IdMatriz).FirstOrDefault();


                foreach (var componente in item.Componente)
                {
                    Componente newComponente = (from u in ctx.Componente where u.IdComponente == componente.IdComponente select u).FirstOrDefault();
                    modulo.Componente.Add(newComponente);
                
                }
                ctx.Modulo.Add(modulo);
                ctx.SaveChanges();
                ctx.Dispose();
              
                this.Save();
            }
        }

        public void RemoverModulos(int p, string Nome, string CH)
        {
            MatrizBL matrizBL = new MatrizBL();
            var CH2 = int.Parse(CH);
            var existe = matrizBL.Get(e => string.Compare(e.Nome.ToLower(), Nome.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0
                                                   && e.CH == CH2
                                                   && e.IdMatriz != p);

            if (existe.ToList().Count > 0)
            {
                throw new CustomException("Erro ao cadastrar Matriz. Já existe uma Matriz cadastrado com esse Nome e CH.");
            }
            else
            {


                var matriz = matrizBL.Get(e => e.IdMatriz == p, null, "Modulo,Modulo.AgendaDocente,Modulo.AgendaComponente,Modulo.AgendaAmbiente");
                var modulos = matriz.FirstOrDefault().Modulo;

                var teste = modulos.ToList();
                foreach (var item in teste)
                {
                    if (item.AgendaComponente.Count > 0 || item.AgendaAmbiente.Count > 0 || item.AgendaDocente.Count > 0)
                        throw new CustomException("Erro ao editar Matriz. Esse Matriz encontra-se com agendamentos, não podendo ser editada.");
                }


                RemoverModulos(p);
            }
        }

        public void RemoverModulos(int p = 0)
        {
            var ctx = new SPEContext();
            ctx.Configuration.LazyLoadingEnabled = false;
            ctx.Configuration.ProxyCreationEnabled = false;

            MatrizBL matrizBL = new MatrizBL();
            var matriz =
                ctx.Matriz.Include("Modulo")
                    .Include("Modulo.Componente")
                    .Include("Modulo.AgendaDocente")
                    .Include("Modulo.AgendaComponente")
                    .Include("Modulo.AgendaAmbiente")
                    .Where(e => e.IdMatriz == p);
            var modulos = matriz.FirstOrDefault().Modulo;

            var teste = modulos.ToList();
            foreach (var item in teste)
            {
                if (item.AgendaComponente.Count > 0 || item.AgendaAmbiente.Count > 0 || item.AgendaDocente.Count > 0)
                    throw new CustomException("Erro ao editar Matriz. Esse Matriz está encontra-se com agendamentos, não podendo ser editada.");
            }

          
            if (teste.Count > 0  ) {

                foreach (Modulo modulo in teste.ToList())
                {
                    if ( modulo.Componente.Any())
                    {
                        modulo.Componente = null;
                    }

                }

                matriz.FirstOrDefault().Modulo = modulos;
                ctx.SaveChanges();
                
                Delete(teste);
                this.Save();
            }
        }

        public void VinculoModuloComponentes(List<Modulo> modulo, string Nome, string CH)
        {
            RemoverModulos(modulo.FirstOrDefault().IdMatriz, Nome, CH);
            AddList(modulo);
        }


    }
}
