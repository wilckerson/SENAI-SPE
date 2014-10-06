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
    public class TurnoController : BaseController
    {

        public ViewResult Index()
        {
            List<TurnoViewModel> turno = null;

            try
            {
                var listaTurno = BL.Turno.Get(a => true, b => b.OrderBy(c => c.Descr)).ToList();

                turno = TurnoViewModel.MapToListViewModel(listaTurno.ToList());

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao carregar Turnos";

                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }
            return View(turno);
        }

        public ActionResult CadastrarTurno()
        {

            TurnoViewModel viewModel = null;
            try
            {
                viewModel = new TurnoViewModel();

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao carregar página de cadastro de Turno";

                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CadastrarTurno(TurnoViewModel turnoViewModel)
        {

            Turno model = null;
            try
            {
                model = TurnoViewModel.MapToModel(turnoViewModel);
                BL.Turno.Inserir(model);

                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Cadastro de Turno realizado com sucesso.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao cadastrar Turno";

                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }

            return RedirectToAction("Index");
        }

        public ActionResult EditarTurno(int id)
        {
            TurnoViewModel turnoViewModel = null;
            try
            {
                var turno = BL.Turno.GetById(id);
                turnoViewModel = TurnoViewModel.MapToViewModel(turno);

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao carregar página de edição de Turno";

                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }
            return View(turnoViewModel);
        }

        [HttpPost]
        public ActionResult EditarTurno(TurnoViewModel turnoViewModel)
        {
            try
            {
                var model = TurnoViewModel.MapToModel(turnoViewModel);
                BL.Turno.Atualizar(model);
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Edição de Turno realizado com sucesso.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao editar Turno";

                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }

            return RedirectToAction("Index");
        }

        public ActionResult ExcluirTurno(int id)
        {
            try
            {
                BL.Turno.Deletar(id);
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Turno excluído com sucesso.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao excluir Turno";

                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }
            return RedirectToAction("Index");
        }

    }
}