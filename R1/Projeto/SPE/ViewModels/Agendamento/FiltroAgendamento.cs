using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPE.ViewModel
{
    public class FiltroAgendamento
    {
        public int? LocalId { get; set; }
        public int? TipoId { get; set; }
        public int IdComponente { get; set; }

        public List<LocalAmbienteViewModel> Locais;
        public List<TipoAmbienteViewModel> TiposAmbientes;
    }
}