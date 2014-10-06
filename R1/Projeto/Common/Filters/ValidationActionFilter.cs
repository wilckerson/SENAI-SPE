using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace Common.Filters
{
    /// <summary>
    /// This action filter works on by MVC 3 engine only
    /// </summary>
    public class ValidationActionFilter : ActionFilterAttribute
    {
        public virtual void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var viewData = filterContext.Controller.ViewData.ModelState;

            if (viewData.IsValid)
            {
                return;
            }

            var errors = new List<KeyValuePair<string, string>>();
            foreach (var key in viewData.Keys.Where(key => 
                viewData[key].Errors.Any()))
            {
                errors.AddRange(viewData[key].Errors
                    .Select(er => new KeyValuePair<string, string>(key, er.ErrorMessage)));
            }
        }

        //public override void OnActionExecuting(HttpActionContext actionContext)
        //{
        //    if (actionContext.ModelState.IsValid)
        //    {
        //        return;
        //    }

        //    var errors = new List<KeyValuePair<string, string>>();
        //    foreach (var key in actionContext.ModelState.Keys.Where(key =>
        //        actionContext.ModelState[key].Errors.Any()))
        //    {
        //        errors.AddRange(actionContext.ModelState[key].Errors
        //              .Select(er => new KeyValuePair<string, string>(key, er.ErrorMessage)));
        //    }

        //    actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest)
        //    {
        //        Content = new StringContent(errors.ToString())
        //    };
        //}
    }
}
