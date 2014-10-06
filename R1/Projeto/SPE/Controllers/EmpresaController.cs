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
    public class EmpresaController : BaseController
    {
        public ActionResult Index()
        {
            List<EmpresaViewModel> empresaViewModel = null;
            try
            {
                var listaEmpresa = BL.Empresa.Get(a => true, b => b.OrderBy(c => c.NomeFantasia)).ToList();

                empresaViewModel = EmpresaViewModel.MapToListViewModel(listaEmpresa);

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";
                Logging.getInstance().Error("Erro ao carregar página de Index de Empresa.", ex);
            }

            return View(empresaViewModel);
        }

        public ActionResult Cadastrar()
        {
            EmpresaViewModel model = new EmpresaViewModel();
            try
            {

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";
                Logging.getInstance().Error("Erro ao carregar página CadastrarEmrpesa", ex);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Cadastrar(EmpresaViewModel model)
        {
            try
            {
                model.CNPJ = Extensions.StringEx.StringExtension.ToOnlyNumber(model.CNPJ);
                BL.Empresa.Inserir(EmpresaViewModel.MapToModel(model));

                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Cadastro de Empresa realizado com sucesso.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao cadastrar Empresa.";
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            EmpresaViewModel model = null;
            try
            {
                model = EmpresaViewModel.MapToViewModel(BL.Empresa.GetById(id));

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";
                Logging.getInstance().Error("Erro ao carregar página EditarEmpresa", ex);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(EmpresaViewModel model)
        {
            try
            {
                model.CNPJ = Extensions.StringEx.StringExtension.ToOnlyNumber(model.CNPJ);

                BL.Empresa.Atualizar(EmpresaViewModel.MapToModel(model));
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Edição de Empresa realizada com sucesso.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao editar Empresa";
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Excluir(int id)
        {
            try
            {
                BL.Empresa.Apagar(id);
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Empresa excluída com sucesso.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao excluir Empresa.";
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }
            return RedirectToAction("Index");
        }
    }
}