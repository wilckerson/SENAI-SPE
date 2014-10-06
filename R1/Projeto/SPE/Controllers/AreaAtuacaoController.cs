using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Senai.SPE.Domain;
using SPE.ViewModel;
using Common;

namespace SPE.Controllers
{   
    public class AreaAtuacaoController : BaseController
    {
        public ViewResult Index(FiltrosAreaAtuacao filtro = null)
        {
            List<AreaAtuacaoViewModel> areasAtuacao = null;
            try
            {
                List<AreaAtuacao> listaAreaAtuacao = BL.AreaAtuacao.Get().ToList();
                areasAtuacao = AreaAtuacaoViewModel.MapToListViewModel(listaAreaAtuacao);

                filtro.AreaAtuacao = areasAtuacao;

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a p�gina";
                Logging.getInstance().Error("Erro ao carregar p�gina de Index da �rea de Atua��o.", ex);
            }
            return View(filtro);
        }

        public ViewResult DetalharAreaAtuacao(int id)
        {
            AreaAtuacaoViewModel areaAtuacaoViewModel = null;
            try
            {

                var areaAtuacao = BL.AreaAtuacao.GetById(id);
                areaAtuacaoViewModel = AreaAtuacaoViewModel.MapToViewModel(areaAtuacao);
              
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a p�gina";

                Logging.getInstance().Error("Erro ao carregar p�gina DetalharAreaAtuacao", ex);
                return View();
            }
            return View(areaAtuacaoViewModel);
        }

        public ActionResult CadastrarAreaAtuacao()
        {
            AreaAtuacaoViewModel viewModel = null;
            try
            {
                viewModel = new AreaAtuacaoViewModel();
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a p�gina";
                Logging.getInstance().Error("Erro ao carregar p�gina CadastrarAreaAtuacao", ex);
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CadastrarAreaAtuacao(AreaAtuacaoViewModel areaAtuacaoviewmodel)
        {
            AreaAtuacao model = null;
            try
            {
                model = AreaAtuacaoViewModel.MapToModel(areaAtuacaoviewmodel);
                BL.AreaAtuacao.InserirAreaAtuacao(model);
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Cadastro de �rea de Atua��o realizado com sucesso.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao cadastrar �rea de Atuaca��o";
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }

            return RedirectToAction("Index");
        }

        public ActionResult EditarAreaAtuacao(int id)
        {
            AreaAtuacaoViewModel areaAtuacaoViewModel = null;
            try
            {
                var areaAtuacao = BL.AreaAtuacao.GetById(id);
                areaAtuacaoViewModel = AreaAtuacaoViewModel.MapToViewModel(areaAtuacao);

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a p�gina";
                Logging.getInstance().Error("Erro ao carregar p�gina EditarAreaAtuacao", ex);
            }
            return View(areaAtuacaoViewModel);
        }

        //
        // POST: /AreaAtuacaoViewModels/Edit/5
        [HttpPost]
        public ActionResult EditarAreaAtuacao(AreaAtuacaoViewModel areaAtuacaoviewmodel)
        {
            try
            {
                var model = AreaAtuacaoViewModel.MapToModel(areaAtuacaoviewmodel);
                BL.AreaAtuacao.AtualizarAreaAtuacao(model);
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Edi��o de �rea de Atua��o realizada com sucesso.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao editar �rea de Atuaca��o";
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }

            return RedirectToAction("Index");
        }

        //
        // GET: /AreaAtuacaoViewModels/Delete/5
        public ActionResult ExcluirAreaAtuacao(int id)
        {
            try
            {
                BL.AreaAtuacao.DeletarAreaAtuacao(id);
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "�rea de atua��o exclu�da com sucesso.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao excluir �rea de Atua��o";
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }
            return RedirectToAction("Index");
        }
    }
}