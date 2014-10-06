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
    public class CatUsuarioController : BaseController
    {
        public ActionResult Index()
        {
            List<CatUsuario> catUsuario = null;
            
            try
            {
                catUsuario = BL.CatUsuario.Get(a => true, b => b.OrderBy(c => c.NomeCatUsuario)).ToList();
                
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a p�gina";
                Logging.getInstance().Error("Erro ao carregar p�gina de Index da Categoria de Usu�rio.", ex);
            }

            return View(catUsuario);
        }

        public ActionResult Cadastrar()
        {
            CatUsuarioViewModel model = new CatUsuarioViewModel();
            try
            {

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a p�gina";
                Logging.getInstance().Error("Erro ao carregar p�gina CadastrarCategoriaUsuario", ex);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Cadastrar(CatUsuarioViewModel model)
        {
            try
            {
                BL.CatUsuario.InserirCatUsuario(CatUsuarioViewModel.MapToModel(model));

                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Cadastro de Categoria de Usu�rio realizado com sucesso.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao cadastrar Categoria de Usu�rio";
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            CatUsuarioViewModel model = null;
            try
            {
                model = CatUsuarioViewModel.MapToViewModel(BL.CatUsuario.GetById(id));

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a p�gina";
                Logging.getInstance().Error("Erro ao carregar p�gina EditarCategoriaUsuario", ex);
            }
            return View(model);
        }

        //
        // POST: /AreaAtuacaoViewModels/Edit/5
        [HttpPost]
        public ActionResult Editar(CatUsuarioViewModel model)
        {
            try
            {
                //this.speDominioService.AtualizarTipoUsuario(model);
                BL.CatUsuario.AtualizarCatUsuario(CatUsuarioViewModel.MapToModel(model));
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Edi��o de Categoria de Usu�rio realizada com sucesso.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao editar Categoria de Usu�rio";
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }

            return RedirectToAction("Index");
        }

        //
        // GET: /AreaAtuacaoViewModels/Delete/5
        public ActionResult Excluir(int id)
        {
            try
            {
                //BL.CatUsuario.ApagarCatUsuario(id);
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Categoria de Usu�rio exclu�da com sucesso.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao excluir Categoria de Usu�rio";
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }
            return RedirectToAction("Index");
        }
    }
}