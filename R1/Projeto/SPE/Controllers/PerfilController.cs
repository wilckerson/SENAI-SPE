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
    public class PerfilController : BaseController
    {
        public ActionResult Index()
        {
            List<Perfil> listPerfil = null;
            try
            {
                listPerfil = BL.Perfil.Get().ToList();
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";
                Logging.getInstance().Error("Erro ao carregar página de Index de Perfil.", ex);
            }

            return View(listPerfil);
        }

        public ActionResult Cadastrar()
        {
            PerfilVM model = new PerfilVM();
            try
            {
               
                model.Funcionalidades = BL.Funcionalidade.Get().ToList();
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";
                Logging.getInstance().Error("Erro ao carregar página CadastrarPerfilUsuario", ex);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Cadastrar(PerfilVM model, FormCollection collection)
        {
            try
            {
                model.Perfil.Status = Convert.ToInt32(model.Status);
                model.Perfil.AprovarMatriz = Convert.ToInt32(model.AprovarMatriz);
                model.Perfil.AprovarTurma = Convert.ToInt32(model.AprovarTurma);
                model.Perfil.ExpirarMatriz = Convert.ToInt32(model.ExpirarMatriz);
                var arrayId = Array.ConvertAll(collection["idFuncionalidades"].Split(';'), t => int.Parse(t));
                BL.Perfil.Inserir(model.Perfil, arrayId.ToList());

                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Cadastro de Perfil realizado com sucesso.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao cadastrar Perfil";
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            PerfilVM model = new PerfilVM();
            try
            {
                model.Perfil = BL.Perfil.Get(a => a.idPerfil == id, null, "Funcionalidade").SingleOrDefault();
                //model.Perfil = BL.Perfil.Get();
                var funcDisponiveis = BL.Funcionalidade.Get().ToList();
                funcDisponiveis = funcDisponiveis.Where(a => !model.Perfil.Funcionalidade.Any(b => a.idFuncionalidade == b.idFuncionalidade)).ToList();
                model.Funcionalidades = funcDisponiveis;

            }
            //try
            //{
            //    model = CadastrarOuEditar.MapToCadastrarOuEditar(BL.Usuario.GetById(id));
            //    model.ListaCatUsuario = BL.CatUsuario.Get().ToList();
            //}
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";
                Logging.getInstance().Error("Erro ao carregar página EditarPerfilUsuario", ex);
            }
            return View(model);
        }

        // POST: /AreaAtuacaoViewModels/Edit/5
        [HttpPost]
        public ActionResult Editar(PerfilVM model, FormCollection collection)
        {
            try
            {
                //this.speDominioService.AtualizarTipoUsuario(model);
                model.Perfil.idPerfil = Convert.ToInt32(collection["Perfil.idPerfil"]);
                model.Perfil.Status = Convert.ToInt32(model.Status);
                model.Perfil.AprovarMatriz = Convert.ToInt32(model.AprovarMatriz);
                model.Perfil.AprovarTurma = Convert.ToInt32(model.AprovarTurma);
                model.Perfil.ExpirarMatriz = Convert.ToInt32(model.ExpirarMatriz);
                var arrayId = Array.ConvertAll(collection["idFuncionalidades"].Split(';'), t => int.Parse(t));
                BL.Perfil.Atualizar(model.Perfil, arrayId.ToList());
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Edição de Perfil de Usuário realizado com sucesso.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao editar Perfil de Usuário";
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }

            return RedirectToAction("Index");
        }

        // GET: /AreaAtuacaoViewModels/Delete/5
        public ActionResult Excluir(int id)
        {
            try
            {
                //this.speDominioService.DeletarTipoUsuario(id);
                BL.Perfil.Apagar(id);
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Perfil excluído com sucesso.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao excluir Perfil.";
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }
            return RedirectToAction("Index");
        }

    }
}