using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPE.ViewModel
{
    public class FiltrosComponente
    {
        public int? Codigo { get; set; }
        public string Nome { get; set; }
        public int? CargaHoraria { get; set; }
        public int? TipoAmbienteId { get; set; }

        public List<ComponenteViewModel> Componentes;
        public List<TipoAmbienteViewModel> TipoAmbientes;
        
    }
}