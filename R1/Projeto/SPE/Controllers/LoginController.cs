//Comantar a linha abaixo caso não queira utilizar a conexão com o AD (Active Directory)
#define USE_AD

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Security;
using BusinessLogic;
using System.DirectoryServices;
using System.Text;
using System.Configuration;

namespace SPE.Controllers
{
    public class LoginController : BaseController
    {
        private string _path;
        private string _filterAttribute = null;

        public void LdapAuthentication(string path)
        {
            _path = path;
        }

        public bool IsAuthenticated(string username, string pwd)
        {
           
            var usuarioAdmin = System.Configuration.ConfigurationManager.AppSettings["AdminUser"];
            
            
            var ldap = System.Configuration.ConfigurationManager.AppSettings["LDAP"];
            var domain = System.Configuration.ConfigurationManager.AppSettings["Domain"];

            try
            {
                var usuario = BL.Usuario.Get(a => a.Nome == username,null,"Perfil").FirstOrDefault();

                if (usuario == null)
                {
                    TempData["LoginMsg"] = "Usuário e Senha estão inválidos";
                    return false;
                }
                else if(usuario.Status != 1 || usuario.Perfil.Status != 1)
                {
                    TempData["LoginMsg"] = "Seu usuário está desabilitado e não possui mais acesso ao sistema. Se necessário, contate o administrador.";
                    return false;
                }

                bool isAdmin = !String.IsNullOrEmpty(usuarioAdmin) && username == usuarioAdmin;

                string nomeUsuario = usuario.Nome;

#if USE_AD
                if (!isAdmin)
                {
                    DirectoryEntry entry = new DirectoryEntry(ldap, domain + username, pwd, AuthenticationTypes.Secure);

                    //Bind to the native AdsObject to force authentication.
                    object obj = entry.NativeObject;

                    nomeUsuario = entry.Username;
                }
#endif

                LoggedUser loggeduser = new LoggedUser()
                {
                    Id = usuario.IdUsuario,
                    Name = nomeUsuario,
                    PerfilId = usuario.IdPerfil.Value,

                };

                Session["usuario"] = loggeduser;
                Session["usuarioId"] = loggeduser.Id;
                Session["nome"] = loggeduser.Name;
                Session["PerfilId"] = loggeduser.PerfilId;
            }
            catch (Exception)
            {
                TempData["LoginMsg"] = "Erro ao fazer login, tente novamente mais tarde.";
                return false;
            }

            return true;
        }

        public string GetGroups()
        {
            DirectorySearcher search = new DirectorySearcher(_path);
            search.Filter = "(cn=" + _filterAttribute + ")";
            search.PropertiesToLoad.Add("memberOf");
            StringBuilder groupNames = new StringBuilder();

            try
            {
                SearchResult result = search.FindOne();
                int propertyCount = result.Properties["memberOf"].Count;
                string dn;
                int equalsIndex, commaIndex;

                for (int propertyCounter = 0; propertyCounter < propertyCount; propertyCounter++)
                {
                    dn = (string)result.Properties["memberOf"][propertyCounter];
                    equalsIndex = dn.IndexOf("=", 1);
                    commaIndex = dn.IndexOf(",", 1);
                    if (-1 == equalsIndex)
                    {
                        return null;
                    }
                    groupNames.Append(dn.Substring((equalsIndex + 1), (commaIndex - equalsIndex) - 1));
                    groupNames.Append("|");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error obtaining group names. " + ex.Message);
            }
            return groupNames.ToString();
        }
        // protected readonly OdebrechtSegurancaService OdebrechtSegurancaService = new OdebrechtSegurancaService();
        private readonly BaseController baseController = new BaseController();
        [AllowAnonymous]
        public ViewResult Index()
        {
            //Se tem o cookie de lembrar senha
            if (this.Request.Cookies.AllKeys.Contains("Auth"))
            {
                HttpCookie coockie = this.Request.Cookies["Auth"];
                //Passa os dados para a view
                ViewBag.ckLogin = coockie.Values["login"];
                ViewBag.ckSenha = coockie.Values["senha"];
                ViewBag.ckLembrar = true;
            }

            return View();
        }


        public ActionResult Logout()
        {
            Session.Abandon();
            Session["nome"] = null;
            //baseController.InitializerLoggedUser();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult AuthenticateUser(string login, string senha, FormCollection collection)
        {
            //Se tem o cookie de lembrar senha
            if (this.Request.Cookies.AllKeys.Contains("Auth"))
            {
                HttpCookie coockie = this.Request.Cookies["Auth"];
                //Passa os dados para a view
                ViewBag.ckLogin = coockie.Values["login"];
                ViewBag.ckSenha = coockie.Values["senha"];
                ViewBag.ckLembrar = true;
            }


            if (IsAuthenticated(login, senha))
            {
                var lembrar = this.Request["lembrar"];

                if (lembrar == "on")
                {
                    HttpCookie coockie = new HttpCookie("Auth");

                    coockie.Values["login"] = login;
                    coockie.Values["senha"] = senha;

                    //Atualiza a data de expiração somente se não tiver o cookie
                    if (!this.Request.Cookies.AllKeys.Contains("Auth"))
                    {
                        TimeSpan timespan = new TimeSpan(15, 0, 0, 0);
                        coockie.Expires = DateTime.Now + timespan;
                    }

                    //Adding coockie
                    Response.Cookies.Add(coockie);
                }

                return RedirectToAction("index", "home");
            }

            ModelState.AddModelError("LoginMsg", TempData["LoginMsg"].ToString());

            return View("Index");
        }

    }

}

