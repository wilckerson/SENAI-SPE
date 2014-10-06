using Senai.SPE.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPE.ViewModel
{
    public class TipoAmbienteViewModel
    {

        #region Átributos

        [Key]
        public int IdTipoAmbiente { get; set; }

        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Descr { get; set; }

        public virtual ICollection<Ambiente> Ambiente { get; set; }
        public virtual ICollection<Componente> Componente { get; set; }

        #endregion

        #region Métodos

        public static TipoAmbiente MapToModel(TipoAmbienteViewModel tipoAmbienteViewModel)
        {
            TipoAmbiente tipoAmbiente = new TipoAmbiente()
            {
                IdTipoAmbiente = tipoAmbienteViewModel.IdTipoAmbiente,
                Descr = tipoAmbienteViewModel.Descr

            };
            return tipoAmbiente;
        }

        public static TipoAmbienteViewModel MapToViewModel(TipoAmbiente tipoAmbiente)
        {
            TipoAmbienteViewModel tipoAmbienteViewModel = new TipoAmbienteViewModel()
            {
                IdTipoAmbiente = tipoAmbiente.IdTipoAmbiente,
                Descr = tipoAmbiente.Descr
            };
            return tipoAmbienteViewModel;
        }

        public static List<TipoAmbiente> MapToListModel(List<TipoAmbienteViewModel> tipoAmbienteViewModel)
        {
            List<TipoAmbiente> listaTipoAmbiente = (from tipoAmbiente in tipoAmbienteViewModel
                                  select new TipoAmbiente()
                                  {
                                      IdTipoAmbiente = tipoAmbiente.IdTipoAmbiente,
                                      Descr = tipoAmbiente.Descr
                                  }).ToList();
            return listaTipoAmbiente;
        }

        public static List<TipoAmbienteViewModel> MapToListViewModel(List<TipoAmbiente> tipoAmbiente)
        {
            List<TipoAmbienteViewModel> listaTipoAmbienteViewModel = (from tipoAmbienteViewModel in tipoAmbiente
                                                    select new TipoAmbienteViewModel()
                                                    {
                                                        IdTipoAmbiente = tipoAmbienteViewModel.IdTipoAmbiente,
                                                        Descr = tipoAmbienteViewModel.Descr
                                                    }).ToList();
            return listaTipoAmbienteViewModel;
        }

        #endregion
    }
}