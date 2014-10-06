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
    public class CalendarioLegendaController : BaseController
    {

        public ActionResult Index()
        {
            List<CalendarioLegenda> CalendarioLegenda = null;

            try
            {
                CalendarioLegenda = BL.CalendarioLegenda.Get().ToList();

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";

                Logging.getInstance().Error("Erro ao carregar página de Calendario Legenda", ex);
            }
            return View(CalendarioLegenda);
        }

        public ActionResult Cadastrar()
        {
            CalendarioLegendaViewModel viewModel = null;

            try
            {
                viewModel = new CalendarioLegendaViewModel();
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";

                Logging.getInstance().Error("Erro ao carregar página Cadastrar Calendario Legenda", ex);
            }


            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Cadastrar(CalendarioLegendaViewModel CalendarioLegenda)
        {
            try
            {
                BL.CalendarioLegenda.Inserir(CalendarioLegendaViewModel.MapToModel(CalendarioLegenda));
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Cadastro de Calendario Legenda realizado com sucesso.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao cadastrar Calendario Legenda";
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }


            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            CalendarioLegendaViewModel CalendarioLegenda = null;
            try
            {
                CalendarioLegenda = CalendarioLegendaViewModel.MapToViewModel(BL.CalendarioLegenda.GetById(id));
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";

                Logging.getInstance().Error("Erro ao carregar página Editar Calendario Legenda", ex);
            }


            return View(CalendarioLegenda);
        }

        [HttpPost]
        public ActionResult Editar(CalendarioLegendaViewModel CalendarioLegenda)
        {
            try
            {
                BL.CalendarioLegenda.Atualizar(CalendarioLegendaViewModel.MapToModel(CalendarioLegenda));
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Edição de Calendario Legenda realizada com sucesso.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao editar Calendario Legenda";

                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }


            return RedirectToAction("Index");
        }

        public ActionResult Excluir(int id)
        {
            try
            {
                BL.CalendarioLegenda.Apagar(id);
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Calendario Legenda excluído com sucesso.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao excluir Calendario Legenda";
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }
            return RedirectToAction("Index");
        }
    }
}
