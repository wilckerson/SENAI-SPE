using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPE.ViewModel
{
    public class FiltrosUsuario
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public int? IdPerfil { get; set; }
        public bool? Status { get; set; }

        public List<PerfilVM> Perfil { get; set; }
        public List<UsuarioViewModel> Usuario { get; set; }
    }
}