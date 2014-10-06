using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic;
using Senai.SPE.Domain;
using Common;
using SPE.ViewModel;

namespace SPE.Controllers
{
    public class TipoAtividadeController : BaseController
    {

        public ViewResult Index()
        {
            var tipoAtividade = new List<TipoAtividade>();
            try
            {
                tipoAtividade = BL.TipoAtividade.Get(a => true, b => b.OrderBy(c => c.Nome)).ToList();
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";
                Logging.getInstance().Error("Erro ao carregar página de Index", ex);
            }
            return View(tipoAtividade);
        }

        public ViewResult DetalharTipoAtividade(int id)
        {
            TipoAtividade tipoAtividade;
            try
            {
                tipoAtividade = BL.TipoAtividade.GetById(id);

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";

                Logging.getInstance().Error("Erro ao carregar página Detalhar Tipo Atividade", ex);
                return View();
            }
            return View(tipoAtividade);
        }

        public ActionResult CadastrarTipoAtividade()
        {
            TipoAtividadeViewModel tipoAtividade = null;
            try
            {
                tipoAtividade = new TipoAtividadeViewModel();
                tipoAtividade.Interna = false;
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";

                Logging.getInstance().Error("Erro ao carregar página CadastrarTipoAtividade", ex);
            }
            return View(tipoAtividade);
        }

        [HttpPost]
        public ActionResult CadastrarTipoAtividade(TipoAtividadeViewModel tipoAtividade)
        {
            try
            {
                BL.TipoAtividade.Add(TipoAtividadeViewModel.MapToModel(tipoAtividade));
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Cadastro de Tipo de Atividade realizado com sucesso.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao cadastrar Tipo de Atividade";
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }

            return RedirectToAction("Index");
        }

        public ActionResult EditarTipoAtividade(int id)
        {
            TipoAtividadeViewModel tipoAtividade = null;
            try
            {
                tipoAtividade = TipoAtividadeViewModel.MapToViewModel(BL.TipoAtividade.GetById(id));

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";

                Logging.getInstance().Error("Erro ao carregar página EditarTipoAtividade", ex);
            }
            return View(tipoAtividade);
        }

        [HttpPost]
        public ActionResult EditarTipoAtividade(TipoAtividadeViewModel tipoAtividade)
        {
            try
            {
                BL.TipoAtividade.Update(TipoAtividadeViewModel.MapToModel(tipoAtividade));
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Edição de Tipo de Atividade realizada com sucesso.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao editar Tipo de Atividade";

                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }

            return RedirectToAction("Index");
        }

        public ActionResult ExcluirTipoAtividade(int id)
        {
            try
            {
                BL.TipoAtividade.Apagar(id);
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Tipo de Atividade excluído com sucesso.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao excluir este Tipo de Atividade";

                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }
            return RedirectToAction("Index");
        }

    }
}
