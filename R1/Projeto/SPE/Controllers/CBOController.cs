using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Senai.SPE.Domain;
using Common;
using SPE.ViewModel;
using SPERepository;

namespace SPE.Controllers
{
    public class CBOController : BaseController
    {
        public ViewResult Index()
        {
            List<CBOViewModel> cbos = new List<CBOViewModel>();
            try
            {
                //var listaCbos = this.speDominioService.GetCBOAll();
                var listaCbos = BL.CBO.Get();
                cbos = CBOViewModel.MapToListViewModel(listaCbos.ToList());
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";
                Logging.getInstance().Error("Erro ao carregar página de CBO", ex);
            }
            return View(cbos);
        }

        public ViewResult DetalharCBO(int id)
        {
            CBOViewModel cboViewModel = null;
            try
            {
                //var cbo = this.speDominioService.GetCBOById(id);
                var cbo = BL.CBO.GetById(id);

                cboViewModel = CBOViewModel.MapToViewModel(cbo);
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";
                Logging.getInstance().Error("Erro ao carregar página DetalharCBO", ex);
                return View();
            }

            return View(cboViewModel);
        }

        public ActionResult CadastrarCBO()
        {
            CBOViewModel viewModel = null;
            try
            {
                viewModel = new CBOViewModel();

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";

                Logging.getInstance().Error("Erro ao carregar página CadastrarCBO", ex);
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CadastrarCBO(CBOViewModel cboviewmodel)
        {
            CBO model = null;
            try
            {
                model = CBOViewModel.MapToModel(cboviewmodel);
                //this.speDominioService.InserirCBO(model);
                BL.CBO.InserirCBO(model);
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Cadastro de CBO realizado com sucesso.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao Cadastrar CBO.";
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }

            return RedirectToAction("Index");
        }

        public ActionResult EditarCBO(int id)
        {
            CBOViewModel cboViewModel = new CBOViewModel();
            try
            {
                //var cbo = this.speDominioService.GetCBOById(id);
                var cbo = BL.CBO.GetById(id);
                cboViewModel = CBOViewModel.MapToViewModel(cbo);
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";
                Logging.getInstance().Error("Erro ao carregar página EditarCBO", ex);
            }
            return View(cboViewModel);
        }

        //
        // POST: /CBOViewModels/Edit/5
        [HttpPost]
        public ActionResult EditarCBO(CBOViewModel cboviewmodel)
        {
            try
            {
                var model = CBOViewModel.MapToModel(cboviewmodel);
                //this.speDominioService.AtualizarCBO(model);
                BL.CBO.AtualizarCBO(model);
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Edição de CBO realizada com sucesso";
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao Editar CBO.";
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }

            return RedirectToAction("Index");
        }

        //
        // GET: /CBOViewModels/Delete/5
        public ActionResult ExcluirCBO(int id)
        {
            try
            {
                //this.speDominioService.ApagarCBO(id);
                BL.CBO.DeletarCBO(id);
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "CBO excluído com sucesso ";
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao excluir CBO.";
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }
            return RedirectToAction("Index");
        }
    }
}