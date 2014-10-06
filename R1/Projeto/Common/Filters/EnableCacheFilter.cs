using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Common.Filters
{
    public class EnableCacheFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // With caching of one day
            filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(1));
            filterContext.HttpContext.Response.Cache.SetValidUntilExpires(true);
            filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.Public);
        }
    }
}
