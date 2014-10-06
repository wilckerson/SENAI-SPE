using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPE.ViewModel
{
    public class ModuloComponenteViewModel
    {
        public string Nome { get; set; }
        public int IdMatriz { get; set; }
        public List<ComponenteViewModel> Componentes { get; set; }
        public string NomeMatriz { get; set; }
        public decimal Preco { get; set; }
        public int IdModalidade { get; set; }
        public int IdAreaAtuacao { get; set; }
    }
}