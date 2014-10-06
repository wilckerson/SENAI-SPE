using Senai.SPE.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPE.ViewModel
{
    public class RecursoViewModel
    {

        #region Atributos

        [Key]
        public int idRecurso { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        [DataType(DataType.MultilineText)]
        public string Descr { get; set; }

        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public int idTipoRecurso { get; set; }
        
        public virtual TipoRecurso TipoRecurso { get; set; }
        public virtual ICollection<RecursoAmbiente> RecursoAmbiente { get; set; }

        //Lista para popular Dropdown de Tipo de Recurso
        public List<TipoRecursoViewModel> ListaTipoRecurso { get; set; }

        #endregion

        #region Métodos

        public static Recurso MapToModel(RecursoViewModel recursoViewModel)
        {
            Recurso recurso = new Recurso()
            {
                idRecurso = recursoViewModel.idRecurso,
                Descr = recursoViewModel.Descr,
                idTipoRecurso = recursoViewModel.idTipoRecurso

            };
            return recurso;
        }

        public static RecursoViewModel MapToViewModel(Recurso recurso)
        {
            RecursoViewModel recursoViewModel = new RecursoViewModel()
            {
                idRecurso = recurso.idRecurso,
                Descr = recurso.Descr,
                idTipoRecurso = recurso.idTipoRecurso,
                TipoRecurso = recurso.TipoRecurso
            };
            return recursoViewModel;
        }

        public static List<Recurso> MapToListModel(List<RecursoViewModel> recursoViewModel)
        {
            List<Recurso> listaRecurso = (from recurso in recursoViewModel
                                                      select new Recurso()
                                                      {
                                                          idRecurso = recurso.idRecurso,
                                                          Descr = recurso.Descr,
                                                          idTipoRecurso = recurso.idTipoRecurso
                                                      }).ToList();
            return listaRecurso;
        }

        public static List<RecursoViewModel> MapToListViewModel(List<Recurso> recurso)
        {
            List<RecursoViewModel> listaRecursoViewModel = (from recursoViewModel in recurso
                                                                        select new RecursoViewModel()
                                                                        {
                                                                            idRecurso = recursoViewModel.idRecurso,
                                                                            Descr = recursoViewModel.Descr,
                                                                            idTipoRecurso = recursoViewModel.idTipoRecurso,
                                                                            TipoRecurso = recursoViewModel.TipoRecurso
                                                                        }).ToList();
            return listaRecursoViewModel;
        }

        #endregion
    }
}