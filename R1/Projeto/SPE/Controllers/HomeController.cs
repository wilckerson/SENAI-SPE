using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPE.Controllers
{
    public class HomeController : BaseController
    {
        //[Inject]
        //public IUoWSPE UoWSPE { get; set; }

        public ActionResult Index()
        {
            //this.UoWSPE.UnidadeRepository.All();
          
            return View();
        }
        public ActionResult Cadastros()
        {
            //this.UoWSPE.UnidadeRepository.All();

            return View();
        }
    }
}
