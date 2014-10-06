using Senai.SPE.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPE.ViewModel
{
    public class TipoRecursoViewModel
    {

        #region Átributos

        [Key]
        public int idTipoRecurso { get; set; }

        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Descr { get; set; }

        public virtual ICollection<Recurso> Recurso { get; set; }
        public virtual ICollection<Componente> Componente { get; set; }

        #endregion

        #region Métodos

        public static TipoRecurso MapToModel(TipoRecursoViewModel tipoRecursoViewModel)
        {
            TipoRecurso tipoRecurso = new TipoRecurso()
            {
                idTipoRecurso = tipoRecursoViewModel.idTipoRecurso,
                Descr = tipoRecursoViewModel.Descr

            };
            return tipoRecurso;
        }

        public static TipoRecursoViewModel MapToViewModel(TipoRecurso tipoRecurso)
        {
            TipoRecursoViewModel tipoRecursoViewModel = new TipoRecursoViewModel()
            {
                idTipoRecurso = tipoRecurso.idTipoRecurso,
                Descr = tipoRecurso.Descr
            };
            return tipoRecursoViewModel;
        }

        public static List<TipoRecurso> MapToListModel(List<TipoRecursoViewModel> tipoRecursoViewModel)
        {
            List<TipoRecurso> listaTipoRecurso = (from tipoRecurso in tipoRecursoViewModel
                                                    select new TipoRecurso()
                                                    {
                                                        idTipoRecurso = tipoRecurso.idTipoRecurso,
                                                        Descr = tipoRecurso.Descr
                                                    }).ToList();
            return listaTipoRecurso;
        }

        public static List<TipoRecursoViewModel> MapToListViewModel(List<TipoRecurso> tipoRecurso)
        {
            List<TipoRecursoViewModel> listaTipoRecursoViewModel = (from tipoRecursoViewModel in tipoRecurso
                                                                      select new TipoRecursoViewModel()
                                                                      {
                                                                          idTipoRecurso = tipoRecursoViewModel.idTipoRecurso,
                                                                          Descr = tipoRecursoViewModel.Descr
                                                                      }).ToList();
            return listaTipoRecursoViewModel;
        }

        #endregion
    }
}