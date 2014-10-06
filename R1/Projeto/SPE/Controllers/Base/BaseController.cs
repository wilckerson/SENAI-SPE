using Common;
using Common.Extensions.EnumEx;
using Senai.SPE.Domain;
using Senai.SPE.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Security;
using System.Web.Routing;
using BusinessLogic;
using System.Globalization;
using System.Threading;

namespace SPE.Controllers
{
    public class BaseController : Controller
    {

        protected readonly SPEBL BL = new SPEBL();

        private LoggedUser _loggeduser = null;
        public void initializerloggeduser()
        {
            _loggeduser = null;
        }

        public LoggedUser LoggedUser()
        {
            return _loggeduser ?? (_loggeduser = new LoggedUser());
        }

        public BaseController()
        {

        }
        private Boolean ValidateAndBuildMenu(String url, bool skiValidation)
        {
            try
            {
                //return true;
               

                // Valida os menus do perfil que o usuário tentou acessar
                Perfil perfil = BL.Perfil.Get(x => x.idPerfil == _loggeduser.PerfilId, null, "Funcionalidade").FirstOrDefault();

                // Guarda os menus do perfil do usuario acessado
               // Verifica se a url acessada existe dentro das permissões do perfil do usuário
                var flag = url.IndexOf("/", 1);

                if (flag >= 1)
                {
                    url = url.Substring(0, flag);
                }

                ViewBag.Funcionalidades = perfil.Funcionalidade;
                ViewBag.Count = perfil.Funcionalidade.Count;
                ViewBag.AprovacaoMatriz = perfil.AprovarMatriz;
                ViewBag.AprovacaoTurma = perfil.AprovarTurma;
                
                return perfil.Funcionalidade.Count > 0 ? perfil.Funcionalidade.Any(x => x.Pagina.ToLower().Equals(url))  : false ;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool skipValidation = filterContext.ActionDescriptor.IsDefined(typeof(System.Web.Mvc.AllowAnonymousAttribute), true);

            String controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower();
            String actionName = filterContext.ActionDescriptor.ActionName.ToLower();
            String url = "/" + controllerName + (actionName.Equals("index") ? "" : "/" + actionName);
            if (this.LoggedUser().Id == 0 && !skipValidation)
            {

                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Login" }, { "Action", "Index" } });
                return;
            }
            if (!controllerName.Equals("login"))
            {
                if (!ValidateAndBuildMenu(url.ToLower(), skipValidation))
                {
                    //TODO: Redirecionar o usuário para uma página informando que não tem acesso ao recurso acessado no sistema, sem a necessidade de sair do mesmo.
                    //      Está redirecionando para o Login, temporariamente.
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Login" }, { "Action", "Index" } });
                    return;
                }
            }
                
            
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");


       
            TempData["Url"] = actionName;


            if (TempData["Url"].ToString().Contains(MensagemEnum.Index.ToString().ToLower()))
            {
                TempData["MsgErro"] = MensagemEnum.Index.ToDescription();
            }
            else if (TempData["Url"].ToString().Contains(MensagemEnum.Excluir.ToString().ToLower()))
            {
                TempData["MsgErro"] = MensagemEnum.Excluir.ToDescription();
            }
            else if (TempData["Url"].ToString().Contains(MensagemEnum.Editar.ToString().ToLower()))
            {
                TempData["MsgErro"] = MensagemEnum.Editar.ToDescription();
            }
            else if (TempData["Url"].ToString().Contains(MensagemEnum.Salvar.ToString().ToLower()))
            {
                TempData["MsgErro"] = MensagemEnum.Salvar.ToDescription();
            }
            else if (TempData["Url"].ToString().Contains(MensagemEnum.Detalhar.ToString().ToLower()))
            {
                TempData["MsgErro"] = MensagemEnum.Detalhar.ToDescription();
            }
            if (this.LoggedUser().Id == 0 && !skipValidation)
            {

                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Login" }, { "Action", "Index" } });
                return;
            }
        }
              
        protected override void OnException(ExceptionContext filterContext)
        {
            Exception ex = filterContext.Exception;

            if (!(ex is HttpException)) //ignore "file not found"
            {
                //TempData["Error"] = true; 
                Logging.getInstance().Error("Unhandled exception error", ex);
            }
        }

        /// <summary>
        /// Tests if two given periods overlap each other.
        /// </summary>
        /// <param name="BS">Base period start</param>
        /// <param name="BE">Base period end</param>
        /// <param name="TS">Test period start</param>
        /// <param name="TE">Test period end</param>
        /// <returns>
        /// 	<c>true</c> if the periods overlap; otherwise, <c>false</c>.
        /// </returns>
        public bool TimePeriodOverlap(DateTime BS, DateTime BE, DateTime TS, DateTime TE)
        {
            // More simple?
            // return !((TS < BS && TE < BS) || (TS > BE && TE > BE));

            // The version below, without comments 
            /*
            return (
                (TS >= BS && TS < BE) || (TE <= BE && TE > BS) || (TS <= BS && TE >= BE)
            );
            */

            return (
                // 1. Case:
                //
                //       TS-------TE
                //    BS------BE 
                //
                // TS is after BS but before BE
                (TS >= BS && TS < BE)
                || // or

                // 2. Case
                //
                //    TS-------TE
                //        BS---------BE
                //
                // TE is before BE but after BS
                (TE <= BE && TE > BS)
                || // or

                // 3. Case
                //
                //  TS----------TE
                //     BS----BE
                //
                // TS is before BS and TE is after BE
                (TS <= BS && TE >= BE)
            );
        }
    }
}
