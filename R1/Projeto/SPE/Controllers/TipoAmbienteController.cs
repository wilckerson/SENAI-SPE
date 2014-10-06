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
    public class TipoAmbienteController : BaseController
    {

        public ViewResult Index()
        {
            List<TipoAmbienteViewModel> tipoAmbiente = null;

            try
            {
                var listaTipoAmbiente = BL.TipoAmbiente.Get(a => true, b => b.OrderBy(c => c.Descr)).ToList();
                tipoAmbiente = TipoAmbienteViewModel.MapToListViewModel(listaTipoAmbiente.ToList());

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";

                Logging.getInstance().Error("Erro ao carregar página de Index", ex);
            }
            return View(tipoAmbiente);
        }

        public ViewResult DetalharTipoAmbiente(int id)
        {
            TipoAmbienteViewModel tipoAmbienteViewModel = null;
            try
            {
                var tipoAmbiente = BL.TipoAmbiente.GetById(id);
                tipoAmbienteViewModel = TipoAmbienteViewModel.MapToViewModel(tipoAmbiente);

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";

                Logging.getInstance().Error("Erro ao carregar página DetalharTipoAmbiente", ex);
                return View();
            }
            return View(tipoAmbienteViewModel);
        }

        public ActionResult CadastrarTipoAmbiente()
        {

            TipoAmbienteViewModel viewModel = null;
            try
            {
                viewModel = new TipoAmbienteViewModel();
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";

                Logging.getInstance().Error("Erro ao carregar página CadastrarTipoAmbiente", ex);
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CadastrarTipoAmbiente(TipoAmbienteViewModel tipoAmbienteViewmodel)
        {
            TipoAmbiente model = null;
            try
            {
                model = TipoAmbienteViewModel.MapToModel(tipoAmbienteViewmodel);
                BL.TipoAmbiente.InserirTipoAmbiente(model);
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Cadastro de Tipo de Ambiente realizado com sucesso.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao cadastrar Tipo de Ambiente";
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }

            return RedirectToAction("Index");
        }

        public ActionResult EditarTipoAmbiente(int id)
        {
            TipoAmbienteViewModel tipoAmbienteViewModel = null;
            try
            {
                var tipoAmbiente = BL.TipoAmbiente.GetById(id);
                tipoAmbienteViewModel = TipoAmbienteViewModel.MapToViewModel(tipoAmbiente);

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";

                Logging.getInstance().Error("Erro ao carregar página EditarTipoAmbiente", ex);
            }
            return View(tipoAmbienteViewModel);
        }

        [HttpPost]
        public ActionResult EditarTipoAmbiente(TipoAmbienteViewModel tipoAmbienteViewModel)
        {
            try
            {
                var model = TipoAmbienteViewModel.MapToModel(tipoAmbienteViewModel);
                BL.TipoAmbiente.AtualizarTipoAmbiente(model);
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Edição de Tipo de Ambiente realizada com sucesso.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao editar Tipo de Ambiente";

                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }

            return RedirectToAction("Index");
        }

        public ActionResult ExcluirTipoAmbiente(int id)
        {
            try
            {
                BL.TipoAmbiente.ApagarTipoAmbiente(id);
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Tipo de Ambiente excluído com sucesso.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao excluir este Tipo de Ambiente";

                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }
            return RedirectToAction("Index");
        }

    }
}