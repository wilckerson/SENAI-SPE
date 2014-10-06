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

namespace SPE.Controllers
{
    public class CRController : BaseController
    {

        public ViewResult Index(FiltrosCR filtro = null)
        {
            List<CRViewModel> crs = new List<CRViewModel>();
            try
            {
                var listaCrs = BL.CR.Get(a=> true, b=> b.OrderBy(c=> c.Nome),"Modalidade").ToList();
                crs = CRViewModel.MapToListViewModel(listaCrs.ToList());

                filtro.CR = crs;
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";
                Logging.getInstance().Error("Erro ao carregar página de CR", ex);
            }
            return View(filtro);
        }

        public ViewResult DetalharCR(int id)
        {
            CRViewModel crViewModel = null;
            try
            {
                var cr = BL.CR.Get(a => a.IdCR == id, null, "Componente").FirstOrDefault();
                crViewModel = CRViewModel.MapToViewModel(cr);
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";
                Logging.getInstance().Error("Erro ao carregar página DetalharCR", ex);
                return View();
            }

            return View(crViewModel);
        }

        public ActionResult CadastrarCR()
        {
            CRViewModel viewModel = new CRViewModel();
            try
            {
                List<Modalidade> listaModalidade = BL.Modalidade.Get().ToList();
                viewModel.ListaModalidades = ModalidadeViewModel.MapToListViewModel(listaModalidade);

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";

                Logging.getInstance().Error("Erro ao carregar página CadastrarCR", ex);
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CadastrarCR(CRViewModel crviewmodel)
        {
            CR model = null;
            try
            {
                model = CRViewModel.MapToModel(crviewmodel);
                BL.CR.InserirCR(model);
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Cadatro de CR realizado com sucesso.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao Cadastrar CR.";
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }

            return RedirectToAction("Index");
        }

        public ActionResult EditarCR(int id)
        {
            CRViewModel crViewModel = new CRViewModel();
            try
            {
                var cr = BL.CR.GetById(id);
                crViewModel = CRViewModel.MapToViewModel(cr);
                crViewModel.ListaModalidades = ModalidadeViewModel.MapToListViewModel(BL.Modalidade.Get().ToList());
               
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";
                Logging.getInstance().Error("Erro ao carregar página EditarCR", ex);
            }
            return View(crViewModel);
        }

        //
        // POST: /CRViewModels/Edit/5
        [HttpPost]
        public ActionResult EditarCR(CRViewModel crviewmodel)
        {
            try
            {
                var model = CRViewModel.MapToModel(crviewmodel);
                BL.CR.AtualizarCR(model);
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Edição de CR realizado com sucesso";
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao Editar CR.";
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }

            return RedirectToAction("Index");
        }

        //
        // GET: /CRViewModels/Delete/5
        public ActionResult ExcluirCR(int id)
        {
            try
            {
                BL.CR.DeletarCR(id);
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "CR excluído com sucesso ";
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao excluir CR.";
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }
            return RedirectToAction("Index");
        }
    }
}