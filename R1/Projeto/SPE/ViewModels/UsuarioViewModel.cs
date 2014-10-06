using Senai.SPE.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPE.ViewModel
{
    public class UsuarioViewModel
    {
        #region Atributos
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public Nullable<int> IdPerfil { get; set; }

        public string Email { get; set; }

        public string Tel { get; set; }

        [Required(ErrorMessage="Campo obrigatório")]
        public string CPF { get; set; }

        public string Nome { get; set; }

        public string Senha { get; set; }

        public Nullable<System.DateTime> UltimoAcesso { get; set; }

        public int Status { get; set; }

        public virtual Perfil Perfil { get; set; }
        public virtual ICollection<AreaAtuacao> AreaAtuacao { get; set; }
        public virtual ICollection<Turma> Turma { get; set; }

        #endregion

        #region Métodos
        public static Usuario MapToModel(UsuarioViewModel usuarioViewModel)
        {
            Usuario usuario = new Usuario()
            {
                IdUsuario = usuarioViewModel.IdUsuario,
                IdPerfil = usuarioViewModel.IdPerfil,
                Email = usuarioViewModel.Email,
                Tel = usuarioViewModel.Tel,
                CPF = usuarioViewModel.CPF,
                Nome = usuarioViewModel.Nome,
                Senha = usuarioViewModel.Senha,
                Perfil = usuarioViewModel.Perfil,
                Status = usuarioViewModel.Status,
                UltimoAcesso = usuarioViewModel.UltimoAcesso
            };

            return usuario;
        }

        public static UsuarioViewModel MapToViewModel(Usuario usuario)
        {
            UsuarioViewModel usuarioViewModel = new UsuarioViewModel()
            {
                IdUsuario = usuario.IdUsuario,
                IdPerfil = usuario.IdPerfil,
                Email = usuario.Email,
                Tel = usuario.Tel,
                CPF = usuario.CPF,
                Nome = usuario.Nome,
                Perfil = usuario.Perfil,
                AreaAtuacao = usuario.AreaAtuacao,
                Turma = usuario.Turma,
                Senha = usuario.Senha,
                Status = usuario.Status.HasValue ? usuario.Status.Value : 0,
                UltimoAcesso = usuario.UltimoAcesso

            };
            return usuarioViewModel;
        }

        public static List<Usuario> MapToListModel(List<UsuarioViewModel> usuariosViewModel)
        {
            List<Usuario> listaUsuario = (from usuario in usuariosViewModel
                                          select new Usuario()
                                          {
                                              IdUsuario = usuario.IdUsuario,
                                              IdPerfil = usuario.IdPerfil,
                                              Email = usuario.Email,
                                              Tel = usuario.Tel,
                                              CPF = usuario.CPF,
                                              Nome = usuario.Nome,
                                              Senha = usuario.Senha,
                                              Perfil = usuario.Perfil,
                                              Status = usuario.Status,
                                              UltimoAcesso = usuario.UltimoAcesso

                                          }).ToList();
            return listaUsuario;
        }

        public static List<UsuarioViewModel> MapToListViewModel(List<Usuario> usuarios)
        {
            List<UsuarioViewModel> listaUsuarioViewModel = (from usuarioViewModel in usuarios
                                                            select new UsuarioViewModel()
                                                            {
                                                                IdUsuario = usuarioViewModel.IdUsuario,
                                                                IdPerfil = usuarioViewModel.IdPerfil,
                                                                Email = usuarioViewModel.Email,
                                                                Tel = usuarioViewModel.Tel,
                                                                CPF = usuarioViewModel.CPF,
                                                                Nome = usuarioViewModel.Nome,
                                                                Perfil = usuarioViewModel.Perfil,
                                                                AreaAtuacao = usuarioViewModel.AreaAtuacao,
                                                                Turma = usuarioViewModel.Turma,
                                                                Senha = usuarioViewModel.Senha,
                                                                Status = usuarioViewModel.Status.HasValue ? usuarioViewModel.Status.Value : 0,
                                                                UltimoAcesso = usuarioViewModel.UltimoAcesso
                                                            }).ToList();
            return listaUsuarioViewModel;
        }

        #endregion



    }
}