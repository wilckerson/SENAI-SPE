using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPE.ViewModel
{
    public class FiltrosTurma
    {
        public int? Codigo { get; set; }
        public string Nome { get; set; }
        public int? ModalidadeId { get; set; }
        public int? idCR { get; set; }

        public List<ModalidadeViewModel> Modalidades { get; set; }
        public List<CRViewModel> CRs { get; set; }
        public List<TurmaViewModel> Turmas { get; set; }
    }
}