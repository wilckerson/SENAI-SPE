using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPE.ViewModel
{
    public class FiltrosAreaAtuacao
    {
        public string Nome { get; set; }

        public List<AreaAtuacaoViewModel> AreaAtuacao { get; set; }
    }
}