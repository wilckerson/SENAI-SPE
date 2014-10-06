using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPE.ViewModel
{
    public class FiltrosCR
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public int? IdModalidade { get; set; }

        public List<ModalidadeViewModel> Modalidade { get; set; }

        public List<CRViewModel> CR { get; set; }
    }
}