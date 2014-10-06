using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Senai.SPE.Domain;
using Common;
using SPE.ViewModel;

namespace SPE.Controllers
{
    public class TipoRecursoController : BaseController
    {

        //
        // GET: /TipoRecursoViewModels/
        public ViewResult Index()
        {
            List<TipoRecursoViewModel> tipoRecurso = null;

            try
            {
                //var listaTipoRecurso = this.speDominioService.GetTipoRecursoAll();
                var listaTipoRecurso = BL.TipoRecurso.Get(a=> true,b=> b.OrderBy(c=> c.Descr)).ToList();
                tipoRecurso = TipoRecursoViewModel.MapToListViewModel(listaTipoRecurso.ToList());

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";

                Logging.getInstance().Error("Erro ao carregar página de Index", ex);
            }
            return View(tipoRecurso);
        }

        //
        // GET: /TipoRecursoViewModels/Details/5
        public ViewResult DetalharTipoRecurso(int id)
        {
            TipoRecursoViewModel tipoRecursoViewModel = null;
            try
            {
                //var tipoRecurso = this.speDominioService.GetTipoRecursoById(id);
                var tipoRecurso = BL.TipoRecurso.GetById(id);
                tipoRecursoViewModel = TipoRecursoViewModel.MapToViewModel(tipoRecurso);

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";

                Logging.getInstance().Error("Erro ao carregar página DetalharTipoRecurso", ex);
                return View();
            }
            return View(tipoRecursoViewModel);
        }

        //
        // GET: /TipoRecursoViewModels/Create
        public ActionResult CadastrarTipoRecurso()
        {

            TipoRecursoViewModel viewModel = null;
            try
            {
                viewModel = new TipoRecursoViewModel();
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";

                Logging.getInstance().Error("Erro ao carregar página CadastrarTipoRecurso", ex);
            }
            return View(viewModel);
        }

        //
        // POST: /TipoRecursoViewModels/Create
        [HttpPost]
        public ActionResult CadastrarTipoRecurso(TipoRecursoViewModel tipoRecursoViewmodel)
        {
            TipoRecurso model = null;
            try
            {
                model = TipoRecursoViewModel.MapToModel(tipoRecursoViewmodel);

                //this.speDominioService.InserirTipoRecurso(model);
                BL.TipoRecurso.InserirTipoRecurso(model);
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Cadastro de Tipo de Recurso realizado com sucesso.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao cadastrar Tipo de Recurso";

                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }

            return RedirectToAction("Index");
        }

        //
        // GET: /TipoRecursoViewModels/Edit/5
        public ActionResult EditarTipoRecurso(int id)
        {
            TipoRecursoViewModel tipoRecursoViewModel = null;
            try
            {
                //var tipoRecurso = this.speDominioService.GetTipoRecursoById(id);
                var tipoRecurso = BL.TipoRecurso.GetById(id);
                tipoRecursoViewModel = TipoRecursoViewModel.MapToViewModel(tipoRecurso);

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";

                Logging.getInstance().Error("Erro ao carregar página EditarTipoRecurso", ex);
            }
            return View(tipoRecursoViewModel);
        }

        //
        // POST: /TipoRecursoViewModels/Edit/5
        [HttpPost]
        public ActionResult EditarTipoRecurso(TipoRecursoViewModel tipoRecursoViewModel)
        {
            try
            {
                var model = TipoRecursoViewModel.MapToModel(tipoRecursoViewModel);
                //this.speDominioService.AtualizarTipoRecurso(model);
                BL.TipoRecurso.AtualizarTipoRecurso(model);
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Edição de Tipo de Recurso realizada com sucesso.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao editar Tipo de Recurso";

                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }

            return RedirectToAction("Index");
        }

        //
        // GET: /TipoRecursoViewModels/Delete/5
        public ActionResult ExcluirTipoRecurso(int id)
        {
            try
            {
                //this.speDominioService.ApagarTipoRecurso(id);
                BL.TipoRecurso.DeletarTipoRecurso(id);
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Tipo de Recurso excluído com sucesso.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao excluir Tipo de Recurso";

                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }
            return RedirectToAction("Index");
        }

    }
}