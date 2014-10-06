using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPE.ViewModel
{
    public class FiltrosMatriz
    {
        public int? Codigo { get; set; }
        public string Nome { get; set; }
        public int? IdModalidade { get; set; }
        public int? IdAreaAtuacao { get; set; }

        public List<ModalidadeViewModel> Modalidades { get; set; }
        public List<AreaAtuacaoViewModel> AreasAtucao { get; set; }
        public List<MatrizViewModel> Matriz { get; set; }
    }
}