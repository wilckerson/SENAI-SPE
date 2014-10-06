using Senai.SPE.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPE.ViewModel
{
    public class LocalAmbienteViewModel
    {

        #region Atributos

        [Key]
        public int IdLoc { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Descr { get; set; }

        public virtual ICollection<Ambiente> Ambiente { get; set; }

        #endregion

        #region Métodos

        public static LocalAmbiente MapToModel(LocalAmbienteViewModel localAmbienteViewModel)
        {
            LocalAmbiente localAmbiente = new LocalAmbiente()
            {
                IdLoc = localAmbienteViewModel.IdLoc,
                Descr = localAmbienteViewModel.Descr

            };
            return localAmbiente;
        }

        public static LocalAmbienteViewModel MapToViewModel(LocalAmbiente localAmbiente)
        {
            LocalAmbienteViewModel localAmbienteViewModel = new LocalAmbienteViewModel()
            {
                IdLoc = localAmbiente.IdLoc,
                Descr = localAmbiente.Descr
            };
            return localAmbienteViewModel;
        }

        public static List<LocalAmbiente> MapToListModel(List<LocalAmbienteViewModel> localAmbienteViewModel)
        {
            List<LocalAmbiente> listaLocalAmbiente = (from localAmbiente in localAmbienteViewModel
                                                    select new LocalAmbiente()
                                                    {
                                                        IdLoc = localAmbiente.IdLoc,
                                                        Descr = localAmbiente.Descr
                                                    }).ToList();
            return listaLocalAmbiente;
        }

        public static List<LocalAmbienteViewModel> MapToListViewModel(List<LocalAmbiente> localAmbiente)
        {
            List<LocalAmbienteViewModel> listaLocalAmbienteViewModel = (from localAmbienteViewModel in localAmbiente
                                                                      select new LocalAmbienteViewModel()
                                                                      {
                                                                          IdLoc = localAmbienteViewModel.IdLoc,
                                                                          Descr = localAmbienteViewModel.Descr
                                                                      }).ToList();
            return listaLocalAmbienteViewModel;
        }

        #endregion
    }
}