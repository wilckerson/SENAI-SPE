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
    public class TipoContratoController : BaseController
    {
        public ActionResult Index()
        {
            List<TipoContrato> tipoContrato = null;

            try
            {
                tipoContrato = BL.TipoContrato.Get(a => true, b => b.OrderBy(c => c.Descr)).ToList();

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";
                Logging.getInstance().Error("Erro ao carregar página de Index de Tipo de Contrato.", ex);
            }

            return View(tipoContrato);
        }

        public ActionResult Cadastrar()
        {
            TipoContratoViewModel model = new TipoContratoViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Cadastrar(TipoContratoViewModel model)
        {
            try
            {
                BL.TipoContrato.Inserir(TipoContratoViewModel.MapToModel(model));

                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Cadastro de Tipo de Contrato realizado com sucesso.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao cadastrar Tipo de Contrato.";
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            TipoContratoViewModel model = null;
            try
            {
                model = TipoContratoViewModel.MapToViewModel(BL.TipoContrato.GetById(id));

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";
                Logging.getInstance().Error("Erro ao carregar página EditarTipoContrato", ex);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(TipoContratoViewModel model)
        {
            try
            {
                BL.TipoContrato.Atualizar(TipoContratoViewModel.MapToModel(model));
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Edição de Tipo de Contrato realizada com sucesso.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao editar Tipo de Contrato";
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Excluir(int id)
        {
            try
            {
                BL.TipoContrato.Apagar(id);
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Tipo de Contrato excluído com sucesso.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao excluir Tipo de Contrato";
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }
            return RedirectToAction("Index");
        }
    }
}