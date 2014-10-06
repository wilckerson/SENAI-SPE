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
using System.DirectoryServices;
using SPE.ViewModels.Usuario;

namespace SPE.Controllers
{
    public class UsuarioController : BaseController
    {

        public ActionResult Index(FiltrosUsuario filtro = null)
        {
            List<Usuario> listUsuario = null;
            try
            {
                listUsuario = BL.Usuario.Get(a => true, a=> a.OrderBy(d=> d.Nome), "Perfil").ToList();

                //listUsuario = BL.Usuario.Get(null, null, "Perfil").ToList();
                filtro.Usuario = UsuarioViewModel.MapToListViewModel(listUsuario);
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";
                Logging.getInstance().Error("Erro ao carregar página de Index da Categoria de Usuário.", ex);
            }

            return View(filtro);
        }

        public ActionResult Cadastrar()
        {
            CadastrarOuEditar model = new CadastrarOuEditar();
            try
            {
                model.ListaCatUsuario = BL.Perfil.Get(p => p.Status == 1).OrderBy(o => o.Nome).ToList();
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao cadastrar Usuário";
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Cadastrar(Usuario model)
        {
            try
            {
                model.Status = 1;
                BL.Usuario.Inserir(model);

                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Cadastro de Usuário realizado com sucesso.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao cadastrar Usuário";
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            CadastrarOuEditar model = null;
            try
            {
                model = CadastrarOuEditar.MapToCadastrarOuEditar(BL.Usuario.GetById(id));
                model.ListaCatUsuario = BL.Perfil.Get(p => p.Status == 1).OrderBy(o => o.Nome).ToList();
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao cadastrar Usuário";
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(Usuario model)
        {
            try
            {
                BL.Usuario.Atualizar(model);
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Edição de Usuário realizado com sucesso.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao editar Usuário";
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int id)
        {
            try
            {
                Usuario model = null;
                model = BL.Usuario.GetById(id);
                if (model.Status == 1)
                {
                    model.Status = 0;
                    TempData["Sucesso"] = true;
                    TempData["SucessoMessage"] = "Usuário Desativado com sucesso.";
                }
                else
                {
                    model.Status = 1;
                    TempData["Sucesso"] = true;
                    TempData["SucessoMessage"] = "Usuário Ativado com sucesso.";
                }
                BL.Usuario.Modificar(model);


            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao editar Categoria de Usuário";
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public PartialViewResult BuscarUsuario(string nome)
        {

            try
            {
                //Configura Usuario e senha padrão pra busca no diretorio.
                var username = System.Configuration.ConfigurationManager.AppSettings["LoginBaseAD"];
                var pwd = System.Configuration.ConfigurationManager.AppSettings["SenhaBaseAD"];
                var ldap = System.Configuration.ConfigurationManager.AppSettings["LDAP"];
                var domain = System.Configuration.ConfigurationManager.AppSettings["Domain"];

                //Criando o Diretorio com as informações passadas em cima
                DirectoryEntry entry = new DirectoryEntry(ldap, domain + username, pwd);
                
                //Biind o NativeObject para forçar a conexão e validação da senha.
                object obj = entry.NativeObject;
                //Iniciar a busca com as informação passadas
                DirectorySearcher search = new DirectorySearcher(entry);
                
                //search.Filter = "(GivenName=*" + nome + "*)";
                search.Filter = string.Format("(&(objectCategory=Person)(|(SAMAccountName=*{0}*)(sn=*{0}*)))", nome);
                                
                search.PropertiesToLoad.Add("cn");
                search.PropertiesToLoad.Add("SAMAccountName");
                search.PropertiesToLoad.Add("mail");
                SearchResultCollection results = search.FindAll();
                List<NomeLogin> usuarios = new List<NomeLogin>();


                foreach (SearchResult result in results)
                {
                    var nomeLogin = new NomeLogin();

                    // Display each of the values for the property identified by
                    // the property name.

                    nomeLogin.Nome = result.Properties["cn"][0].ToString();
                    nomeLogin.Login = result.Properties["SAMAccountName"][0].ToString();
                    nomeLogin.Email = result.Properties["mail"][0].ToString();
                    
                    usuarios.Add(nomeLogin);
                }
                return PartialView("_ListaUsuarioLogin", usuarios);
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao cadastrar Usuário";
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);

                return PartialView("_ListaUsuarioLogin", new List<NomeLogin>());
            }


        }
    }
}