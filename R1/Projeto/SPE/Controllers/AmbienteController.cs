using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Senai.SPE.Domain;
using Senai.SPE.Domain.Enum;
using SPE.ViewModel;
using Common;

namespace SPE.Controllers
{
    public class AmbienteController : BaseController
    {
        public ViewResult Index()
        {
            List<AmbienteViewModel> ambientes = null;
            try
            {
                //var listaAmbiente = this.speDominioService.GetFilteredAmbiente(a => true, b => b.OrderBy(x => x.Nome), "LocalAmbiente,TipoAmbiente");
                var listaAmbiente = BL.Ambiente.Get(a => true, b => b.OrderBy(x => x.Nome), "LocalAmbiente,TipoAmbiente");
                ambientes = AmbienteViewModel.MapToListViewModel(listaAmbiente.ToList());

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página.";

                Logging.getInstance().Error("Erro ao carregar a página.", ex);
            }
            return View(ambientes);
        }

        public ViewResult DetalharAmbiente(int id)
        {
            AmbienteViewModel AmbienteViewModel = null;
            try
            {
                //var Ambiente = this.speDominioService.GetAmbienteById(id);
                var Ambiente = BL.Ambiente.GetById(id);
                AmbienteViewModel = AmbienteViewModel.MapToViewModel(Ambiente);
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página.";
                Logging.getInstance().Error("Erro ao carregar página DetalharAmbiente", ex);
                return View();
            }
            return View(AmbienteViewModel);
        }

        public ActionResult CadastrarAmbiente()
        {
            AmbienteViewModel viewModel = null;
            try
            {
                viewModel = new AmbienteViewModel();

                //List<TipoAmbienteViewModel> tipoAmbiente = TipoAmbienteViewModel.MapToListViewModel(speDominioService.GetTipoAmbienteAll().ToList());
                List<TipoAmbienteViewModel> tipoAmbiente = TipoAmbienteViewModel.MapToListViewModel(BL.TipoAmbiente.Get().ToList());
                viewModel.ListaTipoAmbiente = tipoAmbiente;
                //List<LocalAmbienteViewModel> localAmbiente = LocalAmbienteViewModel.MapToListViewModel(speDominioService.GetLocalAmbienteAll().ToList());
                List<LocalAmbienteViewModel> localAmbiente = LocalAmbienteViewModel.MapToListViewModel(BL.LocalAmbiente.Get().ToList());
                viewModel.ListaLocalidade = localAmbiente;
                //List<RecursoViewModel> recursos = RecursoViewModel.MapToListViewModel(speDominioService.GetFilteredRecurso(a => true, b => b.OrderBy(x => x.Descr), "TipoRecurso").ToList());
                List<RecursoViewModel> recursos = RecursoViewModel.MapToListViewModel(BL.Recurso.Get(a => true, b => b.OrderBy(x => x.Descr), "TipoRecurso").ToList());
                viewModel.ListaRecurso = recursos;



            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página.";

                Logging.getInstance().Error("Erro ao carregar página CadastrarAmbiente", ex);
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CadastrarAmbiente(AmbienteViewModel Ambienteviewmodel)
        {
            Ambiente model = null;

            try
            {
                //Tratar a lista de recursos selecionados
                string[] arrayRecurso = new string[1] { "" };
                if (Ambienteviewmodel.HiddenListaRecurso != null)
                {
                    arrayRecurso = Ambienteviewmodel.HiddenListaRecurso.Split(';');
                    //var teste = arrayRecurso.ToList();
                }

                if (Ambienteviewmodel.IdAmbiente != null)
                    Ambienteviewmodel.IdAmbiente = null;
                model = AmbienteViewModel.MapToModel(Ambienteviewmodel);

                //this.speDominioService.InserirAmbienteRecurso(model, arrayRecurso);
                BL.Ambiente.InserirAmbienteRecurso(model, arrayRecurso);

                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Cadastro de Ambiente realizado com sucesso.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao cadastrar Ambiente.";

                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }

            return RedirectToAction("Index");
        }

        public ActionResult EditarAmbiente(int id)
        {
            AmbienteViewModel ambienteViewModel = null;
            try
            {
                //var Ambiente = this.speDominioService.GetFilteredAmbiente(e => e.IdAmbiente == id, null, "RecursoAmbiente,RecursoAmbiente.Recurso,RecursoAmbiente.Recurso.TipoRecurso").FirstOrDefault();
                var Ambiente = BL.Ambiente.Get(e => e.IdAmbiente == id, null, "RecursoAmbiente,RecursoAmbiente.Recurso,RecursoAmbiente.Recurso.TipoRecurso").FirstOrDefault();
                ambienteViewModel = AmbienteViewModel.MapToViewModel(Ambiente);

                //List<TipoAmbienteViewModel> tipoAmbiente = TipoAmbienteViewModel.MapToListViewModel(speDominioService.GetTipoAmbienteAll().ToList());
                List<TipoAmbienteViewModel> tipoAmbiente = TipoAmbienteViewModel.MapToListViewModel(BL.TipoAmbiente.Get().ToList());
                ambienteViewModel.ListaTipoAmbiente = tipoAmbiente;
                //List<LocalAmbienteViewModel> localAmbiente = LocalAmbienteViewModel.MapToListViewModel(speDominioService.GetLocalAmbienteAll().ToList());
                List<LocalAmbienteViewModel> localAmbiente = LocalAmbienteViewModel.MapToListViewModel(BL.LocalAmbiente.Get().ToList());
                ambienteViewModel.ListaLocalidade = localAmbiente;
                //List<Recurso> recursos = speDominioService.GetFilteredRecurso(a => true, b => b.OrderBy(x => x.Descr), "TipoRecurso").ToList();
                List<Recurso> recursos = BL.Recurso.Get(a => true, b => b.OrderBy(x => x.Descr), "TipoRecurso").ToList();

                foreach (var item in ambienteViewModel.RecursoAmbiente)
                {
                    var remover = recursos.FirstOrDefault(e => e.idRecurso == item.IdRecurso);
                    if (remover != null)
                    {
                        recursos.Remove(remover);
                    }

                }

                ambienteViewModel.ListaRecurso = RecursoViewModel.MapToListViewModel(recursos);

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página.";

                Logging.getInstance().Error("Erro ao carregar página EditarAmbiente", ex);
            }
            return View(ambienteViewModel);
        }

        //
        // POST: /AmbienteViewModels/Edit/5
        [HttpPost]
        public ActionResult EditarAmbiente(AmbienteViewModel Ambienteviewmodel)
        {
            try
            {
                //Tratar a lista de recursos selecionados
                string[] arrayRecurso = new string[1] { "" };
                if (Ambienteviewmodel.HiddenListaRecurso != null)
                {
                    arrayRecurso = Ambienteviewmodel.HiddenListaRecurso.Split(';');
                }

                var model = AmbienteViewModel.MapToModel(Ambienteviewmodel);
                //this.speDominioService.AtualizarAmbienteRecurso(model, arrayRecurso);
                BL.Ambiente.AtualizarAmbienteRecurso(model, arrayRecurso);
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Edição de Ambiente realizada com sucesso.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao editar Ambiente.";

                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }

            return RedirectToAction("Index");
        }

        //
        // GET: /AmbienteViewModels/Delete/5
        public ActionResult ExcluirAmbiente(int id)
        {
            try
            {
                //this.speDominioService.ApagarAmbiente(id);
                BL.Ambiente.DeletarAmbiente(id);
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Ambiente excluído com sucesso.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao excluir Ambiente.";

                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }
            return RedirectToAction("Index");
        }

        public ActionResult RetornarAmbientesPor(int? idTipoAmbiente)
        {
            //var model = this.speDominioService.RetornarAmbientesPor(idTipoAmbiente, 1, (int)PaginacaoEnum.NumeroElementos);
            var model = BL.Ambiente.RetornarAmbientesPor(idTipoAmbiente, 1, (int)PaginacaoEnum.NumeroElementos);
            List<AmbienteViewModel> viewModel = AmbienteViewModel.MapToListViewModel(model);

            return View();
        }

        public ActionResult RetornarDocentesPor(int? idTipoContrato)
        {
            //var model = this.speDominioService.RetornarDocentesPor(idTipoContrato, 1, (int)PaginacaoEnum.NumeroElementos);
            var model = BL.Docente.RetornarDocentesPor(idTipoContrato, 1, (int)PaginacaoEnum.NumeroElementos);
            List<DocenteViewModel> viewModel = DocenteViewModel.MapToListViewModel(model);
            return View();
        }

    }
}