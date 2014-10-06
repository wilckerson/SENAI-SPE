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
using Common;
using System.Data.Entity;

namespace BusinessLogic
{

    public class AmbienteBL : SPERepository<Ambiente>
    {
        public AmbienteBL(TransactionRepository transaction)
            : base(transaction)
        {

        }

        public AmbienteBL()
        {
            // TODO: Complete member initialization
        }

   

        public void InserirAmbienteRecurso(Ambiente model, string[] arrayRecurso)
        {
            var existe = this.Get(e => string.Compare(e.Nome.ToLower(), model.Nome.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0
                                                                        && e.IdLoc == model.IdLoc);

            if (existe.Any())
                throw new CustomException("Erro ao cadastrar Ambiente. Já existe um Ambiente cadastrado com esse Nome e Localidade.");

            List<Recurso> lstRecurosSelecionados = new List<Recurso>();
            List<Recurso> lstRecursosAll = (from u in this.Context.Recurso.Include("TipoRecurso") select u).ToList();

            foreach (var item in arrayRecurso)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    var teste = item.Split('-').First();
                }
            }

            foreach (var item in arrayRecurso)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    lstRecurosSelecionados.Add(lstRecursosAll.FirstOrDefault(element => element.idRecurso == Int32.Parse(item.Split('-').First())));
                }
            }

            if (lstRecurosSelecionados.Count > 0)
            {
                List<RecursoAmbiente> listaRecursoAmbiente = (from item in lstRecurosSelecionados
                                                              select new RecursoAmbiente()
                                                              {
                                                                  IdRecursoAmbiente = 0,
                                                                  IdRecurso = item.idRecurso,
                                                                  IdAmbiente = model.IdAmbiente,
                                                                  Status = 1
                                                              }).ToList();

                model.RecursoAmbiente = listaRecursoAmbiente;

            }

            Context.Ambiente.Add(model);
            Context.SaveChanges();
            Context.Dispose();
        }

        public void AtualizarAmbienteRecurso(Ambiente model, string[] arrayRecurso)
        {
            var existe = this.Get(e => string.Compare(e.Nome.ToLower(), model.Nome.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0
                                                                        && e.IdLoc == model.IdLoc
                                                                        && e.IdAmbiente != model.IdAmbiente);
            if (existe.Any())
                throw new CustomException("Erro ao editar Ambiente. Já existe um Ambiente cadastrado com esse Nome e Localidade.");

            var ambienteOriginal = this.Get(e => e.IdAmbiente == model.IdAmbiente).SingleOrDefault();
            if (ambienteOriginal.AgendaAmbiente.Any())
                throw new CustomException("Erro ao editar Ambiente. Este Ambiente encontra-se agendado.");


            ambienteOriginal = (from u in this.Context.Ambiente.Include("RecursoAmbiente") where u.IdAmbiente == model.IdAmbiente select u).FirstOrDefault();

            List<Recurso> lstRecursosSelecionados = new List<Recurso>();
            List<Recurso> lstRecursosAll = (from u in this.Context.Recurso select u).ToList();
            //var list =  ambienteOriginal.Recurso.ToList();

            //foreach (var item in list)
            //{
            //   ambienteOriginal.Recurso.Remove(item);
            //}
            List<RecursoAmbiente> listaRecursoAmbiente = new List<RecursoAmbiente>();


            foreach (var item in arrayRecurso)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    var achou = lstRecursosAll.FirstOrDefault(element => element.idRecurso == Int32.Parse(item.Split('-').First()));
                    if (achou != null)
                    {
                        lstRecursosSelecionados.Add(achou);
                        listaRecursoAmbiente.Add(new RecursoAmbiente()
                        {
                            IdRecursoAmbiente = 0,
                            IdRecurso = achou.idRecurso,
                            IdAmbiente = model.IdAmbiente,
                            Status = (short)Int32.Parse(item.Split('-').Last())
                        });
                    }
                }
            }

            //Aqui eu posso deletar todos os que não estão marcados e 
            //this.Context.Ambiente.Attach(model);
            //this.Context.Entry(model).State = EntityState.Modified;

            //if (lstRecursosSelecionados.Count > 0)
            //{
            //    listaRecursoAmbiente = (from item in lstRecursosSelecionados
            //                                select new RecursoAmbiente()
            //                                {
            //                                    IdRecursoAmbiente = 0,
            //                                    IdRecurso = item.idRecurso,
            //                                    IdAmbiente = model.IdAmbiente,
            //                                    Status = 1
            //                                }).ToList();

            //}

            ambienteOriginal.RecursoAmbiente = listaRecursoAmbiente;
            this.Context.Entry(ambienteOriginal).CurrentValues.SetValues(model);
            this.Context.Entry(ambienteOriginal).State = EntityState.Modified;

            //if (ambienteOriginal.Recurso != null)
            //{
            //    foreach (var item in lstRecurosSelecionados)
            //    {
            //        ambienteOriginal.Recurso.Add(item);
            //    }
            //}
            //else
            //{
            //    ambienteOriginal.Recurso = lstRecurosSelecionados;
            //}


            this.Context.SaveChanges();

            var recursosAmbienteNulos = (from u in this.Context.RecursoAmbiente where u.IdAmbiente == null select u);

            foreach (var item in recursosAmbienteNulos.ToList())
            {
                this.Context.RecursoAmbiente.Remove(item);
                this.Save();
            }
            Context.SaveChanges();
            Context.Dispose();
        }
       
        public void DeletarAmbiente(int id)
        {
            
            Ambiente ambiente = Get(a=>a.IdAmbiente == id, null,"RecursoAmbiente,AgendaAmbiente").FirstOrDefault();
            if (ambiente.AgendaAmbiente.Any())
            {
                throw new CustomException("Erro ao excluir Ambiente. Já existem turmas criadas com este Ambiente.");
            }
            if (ambiente.RecursoAmbiente.Any())
            {
              SPERepository<RecursoAmbiente> recursoAmbiente = new SPERepository<RecursoAmbiente>();

                recursoAmbiente.Delete(ambiente.RecursoAmbiente);
              recursoAmbiente.Save();

            }
            Delete(id);
        }

        public List<ufnListarAmbientes1_Result> RetornarAmbientesPor(int? idTipoAmbiente, int? pageIndex, int? pageNumber)
        {
            var lista = (from u in this.Context.ufnListarAmbientes1(idTipoAmbiente, pageIndex, pageNumber) select u).ToList();
            return lista;
        }
    }
}
