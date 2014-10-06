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
    public class RecursoController : BaseController
    {

        public ViewResult Index()
        {
            List<RecursoViewModel> recurso = null;

            try
            {

                var listaRecurso = BL.Recurso.Get(a => true, b => b.OrderBy(x => x.Descr), "TipoRecurso");
                recurso = RecursoViewModel.MapToListViewModel(listaRecurso.ToList());

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";

                Logging.getInstance().Error("Erro ao carregar página de Index", ex);
            }
            return View(recurso);
        }

        public ViewResult DetalharRecurso(int id)
        {
            RecursoViewModel recursoViewModel = null;
            try
            {
                var recurso = BL.Recurso.GetById(id);
                recursoViewModel = RecursoViewModel.MapToViewModel(recurso);

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";

                Logging.getInstance().Error("Erro ao carregar página DetalharRecurso", ex);
                return View();
            }
            return View(recursoViewModel);
        }

        public ActionResult CadastrarRecurso()
        {

            RecursoViewModel viewModel = null;
            try
            {
                viewModel = new RecursoViewModel();
                viewModel.ListaTipoRecurso = TipoRecursoViewModel.MapToListViewModel(BL.TipoRecurso.Get(a=> true, b=> b.OrderBy(c=> c.Descr)).ToList());
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";

                Logging.getInstance().Error("Erro ao carregar página CadastrarRecurso", ex);
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CadastrarRecurso(RecursoViewModel recursoViewModel)
        {
            Recurso model = null;
            try
            {
                model = RecursoViewModel.MapToModel(recursoViewModel);
                BL.Recurso.InserirRecurso(model);
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Cadastro de Recurso realizado com sucesso.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao cadastrar Recurso";
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }

            return RedirectToAction("Index");
        }

        public ActionResult EditarRecurso(int id)
        {
            RecursoViewModel recursoViewModel = null;
            try
            {
                var recurso = BL.Recurso.GetById(id);
                recursoViewModel = RecursoViewModel.MapToViewModel(recurso);
                recursoViewModel.ListaTipoRecurso = TipoRecursoViewModel.MapToListViewModel(BL.TipoRecurso.Get(a=> true, b=> b.OrderBy(c=> c.Descr)).ToList());

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";

                Logging.getInstance().Error("Erro ao carregar página EditarRecurso", ex);
            }
            return View(recursoViewModel);
        }

        [HttpPost]
        public ActionResult EditarRecurso(RecursoViewModel recursoViewModel)
        {
            try
            {
                var model = RecursoViewModel.MapToModel(recursoViewModel);
                BL.Recurso.AtualizarRecurso(model);
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Edição de Recurso realizado com sucesso.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao editar Recurso";

                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }

            return RedirectToAction("Index");
        }

        public ActionResult ExcluirRecurso(int id)
        {
            try
            {
                BL.Recurso.ApagarRecurso(id);
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Recurso excluído com sucesso.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao excluir Recurso";
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }
            return RedirectToAction("Index");
        }

    }
}