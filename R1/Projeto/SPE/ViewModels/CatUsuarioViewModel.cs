using Senai.SPE.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPE.ViewModel
{
    public class CatUsuarioViewModel
    {
        public int IdCatUsuario { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Categoria de Usuário", ShortName = "Categoria de Usuário")]
        public string NomeCatUsuario { get; set; }


        #region Métodos
        public static CatUsuario MapToModel(CatUsuarioViewModel catUsuarioViewModel)
        {
            CatUsuario catUsuario = new CatUsuario()
            {
                IdCatUsuario = catUsuarioViewModel.IdCatUsuario,
                NomeCatUsuario = catUsuarioViewModel.NomeCatUsuario,
            };
            return catUsuario;
        }

        public static CatUsuarioViewModel MapToViewModel(CatUsuario catUsuario)
        {
            CatUsuarioViewModel catUsuarioViewModel = new CatUsuarioViewModel()
            {
                IdCatUsuario = catUsuario.IdCatUsuario,
                NomeCatUsuario = catUsuario.NomeCatUsuario,

            };
            return catUsuarioViewModel;
        }


        public static List<CatUsuario> MapToListModel(List<CatUsuarioViewModel> catUsuarioViewModel)
        {
            List<CatUsuario> listaCatUsuario = (from turno in catUsuarioViewModel
                                                select new CatUsuario()
                                      {
                                          IdCatUsuario = turno.IdCatUsuario,
                                          NomeCatUsuario = turno.NomeCatUsuario,
                                      }).ToList();
            return listaCatUsuario;
        }

        public static List<CatUsuarioViewModel> MapToListViewModel(List<CatUsuario> catUsuario)
        {
            List<CatUsuarioViewModel> listaCatUsuarioViewModel = (from turnoViewModel in catUsuario
                                                             select new CatUsuarioViewModel()
                                                        {
                                                            IdCatUsuario = turnoViewModel.IdCatUsuario,
                                                            NomeCatUsuario = turnoViewModel.NomeCatUsuario,
                                                        }).ToList();
            return listaCatUsuarioViewModel;
        }

        #endregion
    }
}