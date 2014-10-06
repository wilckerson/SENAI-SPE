using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Common.ErrorHandling
{
    public class ErrorHandlingController : Controller
    {
        public ActionResult GeneralErrorHandling()
        {
            return View();
        }

        public ActionResult ErrorHandling404()
        {
            return View();
        }
    }
}
