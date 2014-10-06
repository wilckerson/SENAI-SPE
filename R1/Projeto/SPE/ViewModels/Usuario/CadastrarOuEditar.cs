using Senai.SPE.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPE.ViewModel
{
    public class CadastrarOuEditar
    {
        
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public Nullable<int> IdPerfil { get; set; }

        public string Email { get; set; }

        public string Tel { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [RegularExpression(@"^([0-9]{3}\.){2}[0-9]{3}-[0-9]{2}$", ErrorMessage = "CPF Inválido!")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [DataType(DataType.Password)]
        [Compare("Senha", ErrorMessage="Senhas incompatíveis")]
        [Display(Name = "Confirmar Senha: ")]
        public string ConfSenha { get; set; }

        public Nullable<System.DateTime> UltimoAcesso { get; set; }

        public Nullable<int> Status { get; set; }

        public List<Perfil> ListaCatUsuario { get; set; }




        public static CadastrarOuEditar MapToCadastrarOuEditar(Usuario usuario)
        {
            CadastrarOuEditar cadastrarOuEditar = ( new CadastrarOuEditar()
                                                          {
                                                              IdUsuario = usuario.IdUsuario,
                                                              IdPerfil = usuario.IdPerfil,
                                                              Email = usuario.Email,
                                                              Tel = usuario.Tel,
                                                              CPF = usuario.CPF,
                                                              Nome = usuario.Nome,
                                                              Senha = usuario.Senha,
                                                              UltimoAcesso = usuario.UltimoAcesso,
                                                              Status = usuario.Status
                                                          });
            return cadastrarOuEditar;
        }
    }



}