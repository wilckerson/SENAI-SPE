using Senai.SPE.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPE.ViewModel
{
    public class CadastrarOuEditarPerfil
    {
        public Perfil Perfil { get; set; }
        public int idPerfil { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Habilitado")]
        public Nullable<int> Status { get; set; }

        public virtual ICollection<Funcionalidade> Funcionalidade { get; set; }
        public List<Funcionalidade> Funcionalidades { get; set; }       

        //public List<CatUsuario> ListaCatUsuario { get; set; }




        public static CadastrarOuEditarPerfil MapToCadastrarOuEditar(Perfil perfil)
        {
            CadastrarOuEditarPerfil cadastrarOuEditar = (new CadastrarOuEditarPerfil()
                                                          {
                                                              idPerfil = perfil.idPerfil,
                                                              Nome = perfil.Nome,
                                                              Descricao = perfil.Descricao,
                                                              Status = perfil.Status,
                                                              Funcionalidade =  perfil.Funcionalidade.ToList()
                                                              //IdUsuario = usuario.IdUsuario,
                                                              //IdCatUsuario = usuario.IdCatUsuario,
                                                              //Email = usuario.Email,
                                                              //Tel = usuario.Tel,
                                                              //CPF = usuario.CPF,
                                                              //Nome = usuario.Nome,
                                                              //Senha = usuario.Senha,
                                                              //UltimoAcesso = usuario.UltimoAcesso,
                                                              //Status = usuario.Status
                                                          });
            return cadastrarOuEditar;
        }
    }



}