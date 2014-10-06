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
    public class LocalAmbienteController : BaseController
    {

        public ViewResult Index()
        {
            List<LocalAmbienteViewModel> localAmbiente = null;

            try
            {
                var listaLocalAmbiente = BL.LocalAmbiente.Get(a => true, b => b.OrderBy(c => c.Descr));
                localAmbiente = LocalAmbienteViewModel.MapToListViewModel(listaLocalAmbiente.ToList());

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";

                Logging.getInstance().Error("Erro ao carregar página de Index", ex);
            }
            return View(localAmbiente);
        }

        public ViewResult DetalharLocalAmbiente(int id)
        {
            LocalAmbienteViewModel localAmbienteViewModel = null;
            try
            {
                var localAmbiente = BL.LocalAmbiente.GetById(id);
                localAmbienteViewModel = LocalAmbienteViewModel.MapToViewModel(localAmbiente);

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";

                Logging.getInstance().Error("Erro ao carregar página DetalharLocalAmbiente", ex);
                return View();
            }
            return View(localAmbienteViewModel);
        }

        public ActionResult CadastrarLocalAmbiente()
        {

            LocalAmbienteViewModel viewModel = null;
            try
            {
                viewModel = new LocalAmbienteViewModel();
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";

                Logging.getInstance().Error("Erro ao carregar página CadastrarLocalAmbiente", ex);
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CadastrarLocalAmbiente(LocalAmbienteViewModel localAmbienteViewModel)
        {
            LocalAmbiente model = null;
            try
            {
                model = LocalAmbienteViewModel.MapToModel(localAmbienteViewModel);
                BL.LocalAmbiente.InserirLocalAmbiente(model);
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Cadastro de Localização realizado com sucesso.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao cadastrar Localização";
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }

            return RedirectToAction("Index");
        }

        public ActionResult EditarLocalAmbiente(int id)
        {
            LocalAmbienteViewModel localAmbienteViewModel = null;
            try
            {
                var localAmbiente = BL.LocalAmbiente.GetById(id);
                localAmbienteViewModel = LocalAmbienteViewModel.MapToViewModel(localAmbiente);

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";

                Logging.getInstance().Error("Erro ao carregar página EditarLocalAmbiente", ex);
            }
            return View(localAmbienteViewModel);
        }

        [HttpPost]
        public ActionResult EditarLocalAmbiente(LocalAmbienteViewModel localAmbienteViewModel)
        {
            try
            {
                var model = LocalAmbienteViewModel.MapToModel(localAmbienteViewModel);
                BL.LocalAmbiente.AtualizarLocalAmbiente(model);
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Edição de Localização realizada com sucesso.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao editar Localização";

                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }

            return RedirectToAction("Index");
        }

        public ActionResult ExcluirLocalAmbiente(int id)
        {
            try
            {
                //this.speDominioService.ApagarLocalAmbiente(id);
                BL.LocalAmbiente.DeletarLocalAmbiente(id);
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Localização excluída com sucesso.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao excluir esta Localidade de Ambiente";

                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }
            return RedirectToAction("Index");
        }

    }
}