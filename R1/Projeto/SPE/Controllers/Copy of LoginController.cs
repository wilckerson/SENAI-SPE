using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Security;
using BusinessLogic;
using System.DirectoryServices;
using System.Text;

namespace SPE.Controllers
{
    public class LoginController2 : BaseController
    {
        private string _path;
        private string _filterAttribute;

        public void LdapAuthentication(string path)
        {
            _path = path;
        }

        public bool IsAuthenticated(string username, string pwd)
        {

            string domainAndUsername = "SRV-ID-01.idevelop.com.br" + @"\" + username;

            var acesso = BL.Usuario.Get(a => a.Nome == username).FirstOrDefault();
            if (acesso != null)
            {
                if (acesso.Status != 1)
                {
                    return false;
                }
            }
             
            //DirectoryEntry entry = new DirectoryEntry("LDAP://idevelop.com.br", "idevelop\\" + username, pwd, AuthenticationTypes.Anonymous); 
            DirectoryEntry entry = new DirectoryEntry("LDAP://idevelop.com.br", domainAndUsername, pwd, AuthenticationTypes.Anonymous); 

            try
            {
               
                //Bind to the native AdsObject to force authentication.

                object obj = entry.NativeObject;

                DirectorySearcher search = new DirectorySearcher(entry);

                search.Filter = "(SAMAccountName=" + username + ")";
                search.PropertiesToLoad.Add("cn");
                SearchResult result = search.FindOne();

                var usuario = BL.Usuario.Get(a => a.Nome == username).FirstOrDefault();
                //if (null == result || usuario == null)
                if ( usuario == null  )
                {
                    return false;
                }

                //Update the new path to the user in the directory.
                _path = result.Path;
                _filterAttribute = (string)result.Properties["cn"][0];


                   LoggedUser loggeduser = new LoggedUser()
               {
                   Id = usuario.IdUsuario,
                  Name = _filterAttribute,
                  PerfilId = usuario.IdPerfil.Value,
                   
               };

                Session["usuario"] = loggeduser;
                Session["usuarioId"] = loggeduser.Id;
                Session["nome"] = loggeduser.Name;
                Session["PerfilId"] = loggeduser.PerfilId;
            }
            catch (Exception )
            {                
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

            return View();
        }


        public ActionResult Logout()
        {
            Session.Abandon();

            //baseController.InitializerLoggedUser();

            return View("Index");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult AuthenticateUser(string login, string senha)
        {
           // UsuarioViewModel usuarioViewModel = UsuarioViewModel.MapToViewModel(this.OdebrechtSegurancaService.AuthenticateUser(login, senha));

           
            if (IsAuthenticated(login, senha))
            {


                return RedirectToAction("index", "home");
            }

            ModelState.AddModelError(String.Empty, "Usuário e Senha inválidos !");

            return View("Index");
        }

    }

}

