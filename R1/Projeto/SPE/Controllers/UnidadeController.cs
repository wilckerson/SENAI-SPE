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
    public class UnidadeController : BaseController
    {

        public ViewResult Index()
        {
            List<UnidadeViewModel> unidade = null;

            try
            {
                var listaUnidade = BL.Unidade.Get(a=> true,b=> b.OrderBy(c=> c.Codigo));

                unidade = UnidadeViewModel.MapToListViewModel(listaUnidade.ToList());

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";

                Logging.getInstance().Error("Erro ao carregar página de Index", ex);
            }
            return View(unidade);
        }

        public ViewResult DetalharUnidade(int id)
        {
            UnidadeViewModel unidadeViewModel = null;
            try
            {
                var unidade = BL.Unidade.GetById(id);
                unidadeViewModel = UnidadeViewModel.MapToViewModel(unidade);

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";

                Logging.getInstance().Error("Erro ao carregar página DetalharUnidade", ex);
                return View();
            }
            return View(unidadeViewModel);
        }

        public ActionResult CadastrarUnidade()
        {

            UnidadeViewModel viewModel = null;
            try
            {
                viewModel = new UnidadeViewModel();

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";

                Logging.getInstance().Error("Erro ao carregar página CadastrarUnidade", ex);
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CadastrarUnidade(UnidadeViewModel unidadeViewModel)
        {
            Unidade model = null;
            try
            {
                model = UnidadeViewModel.MapToModel(unidadeViewModel);
                BL.Unidade.InserirUnidade(model);

                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Cadastro de Unidade realizada com sucesso.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao cadastrar Unidade";
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }

            return RedirectToAction("Index");
        }

        public ActionResult EditarUnidade(int id)
        {
            UnidadeViewModel unidadeViewModel = null;
            try
            {
                var unidade = BL.Unidade.GetById(id);
                unidadeViewModel = UnidadeViewModel.MapToViewModel(unidade);

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";

                Logging.getInstance().Error("Erro ao carregar página EditarUnidade", ex);
            }
            return View(unidadeViewModel);
        }

        [HttpPost]
        public ActionResult EditarUnidade(UnidadeViewModel unidadeViewModel)
        {
            try
            {
                var model = UnidadeViewModel.MapToModel(unidadeViewModel);
                BL.Unidade.AtualizarUnidade(model);

                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Edição de Unidade realizada com sucesso.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao editar Unidade";

                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }

            return RedirectToAction("Index");
        }

        public ActionResult ExcluirUnidade(int id)
        {
            try
            {
                BL.Unidade.ApagarUnidade(id);
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Unidade excluída com sucesso.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao excluir Unidade";
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }
            return RedirectToAction("Index");
        }

    }
}