using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Senai.SPE.Domain;
using Common;
using Senai.SPE.Domain.Enum;
using Common.Extensions.EnumEx;
using SPE.ViewModel;

namespace SPE.Controllers
{
    public class ModalidadeController : BaseController
    {
        public ViewResult Index(FiltrosModalidade filtro = null)
        {
            List<ModalidadeViewModel> modalidades = null;

            try
            {
                List<Modalidade> listaModalidade = BL.Modalidade.Get(a => true, b => b.OrderBy(c => c.Nome)).ToList();
                modalidades = ModalidadeViewModel.MapToListViewModel(listaModalidade);

                filtro.Modalidade = modalidades;
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";

                Logging.getInstance().Error("Erro ao carregar página de modalidades", ex);
            }
            return View(filtro);
        }

        public ViewResult DetalharModalidade(int id)
        {
            ModalidadeViewModel modalidadeViewModel = null;
            try
            {
                var modalidade = BL.Modalidade.GetById(id);
                modalidadeViewModel = ModalidadeViewModel.MapToViewModel(modalidade);

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";

                Logging.getInstance().Error("Erro ao carregar página DetalharModalidade", ex);
                return View();
            }
            return View(modalidadeViewModel);
        }

        public ActionResult CadastrarModalidade()
        {
            ModalidadeViewModel viewModel = null;
            try
            {
                viewModel = new ModalidadeViewModel();
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";

                Logging.getInstance().Error("Erro ao carregar página CadastrarModalidade", ex);
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CadastrarModalidade(ModalidadeViewModel modalidadeviewmodel)
        {
            Modalidade model = null;
            try
            {
                model = ModalidadeViewModel.MapToModel(modalidadeviewmodel);
                BL.Modalidade.InserirModalidade(model);
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Cadastro de Modalidade realizado com sucesso.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao cadastrar Modalidade";
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }

            return RedirectToAction("Index");
        }

        public ActionResult EditarModalidade(int id)
        {
            ModalidadeViewModel modalidadeViewModel = null;
            try
            {
                var modalidade = BL.Modalidade.GetById(id);
                modalidadeViewModel = ModalidadeViewModel.MapToViewModel(modalidade);

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";

                Logging.getInstance().Error("Erro ao carregar página EditarModalidade", ex);
            }
            return View(modalidadeViewModel);
        }

        //
        // POST: /ModalidadeViewModels/Edit/5
        [HttpPost]
        public ActionResult EditarModalidade(ModalidadeViewModel modalidadeviewmodel)
        {
            try
            {
                var model = ModalidadeViewModel.MapToModel(modalidadeviewmodel);
                BL.Modalidade.AtualizarModalidade(model);
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Edição de Modalidade realizada com sucesso.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao editar Modalidade";

                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }

            return RedirectToAction("Index");
        }

        //
        // GET: /ModalidadeViewModels/Delete/5
        public ActionResult ExcluirModalidade(int id)
        {
            try
            {
                BL.Modalidade.ApagarModalidade(id);
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Modalidade excluída com sucesso.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao excluir Modalidade";
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }
            return RedirectToAction("Index");
        }

    }
}