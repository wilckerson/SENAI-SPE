using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPE.ViewModel
{
    public class MatrizListarComponentesViewModel
    {
        public List<ComponenteViewModel> ListaViewModel = new List<ComponenteViewModel>();
        public string Filtro { get; set; }
        public int Matriz { get; set; }
    }
}