using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace Common.ActionResults
{
    /// <summary>
    /// An action result that returns a View for a specific HTTP status code.
    /// </summary>
    public class HttpStatusCodeViewResult : ViewResult
    {
        private readonly HttpStatusCode statusCode;
        private readonly string description;

        public HttpStatusCodeViewResult(HttpStatusCode statusCode, string description = null) :
            this(null, statusCode, description) { }

        public HttpStatusCodeViewResult(string viewName, HttpStatusCode statusCode, string description = null)
        {
            this.statusCode = statusCode;
            this.description = description;
            this.ViewName = viewName;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var httpContext = context.HttpContext;
            var response = httpContext.Response;

            response.TrySkipIisCustomErrors = true;
            response.StatusCode = (int)statusCode;
            response.StatusDescription = description;

            base.ExecuteResult(context);
        }
    }
}
