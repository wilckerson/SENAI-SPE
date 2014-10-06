using System;
using Senai.SPE.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SPERepository;

namespace SPE.ViewModel
{
    public class PerfilVM
    {
        public Perfil Perfil = new Perfil();

        [Required(ErrorMessage = "Campo Obrigatório.")]
        public string Nome
        {
            get { return Perfil.Nome; }
            set { Perfil.Nome = value; }
        }

        [Required(ErrorMessage = "Campo Obrigatório.")]
        public string Descricao
        {
            get { return Perfil.Descricao; }
            set { Perfil.Descricao = value; }
        }

        public List<int> IdFuncionalidade { get; set; }
        public List<Funcionalidade> Funcionalidades { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório.")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório.")]
        public string AprovarMatriz { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório.")]
        public string AprovarTurma { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório.")]
        public string ExpirarMatriz { get; set; }
    }


}