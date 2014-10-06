using Senai.SPE.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPE.ViewModel
{
    public class TipoContratoViewModel
    {

        #region Atributos

        public int IdTipoContrato { get; set; }
        [Required]
        [Display(Name = "Tipo de Contrato")]
        public string Descr { get; set; }

        #endregion

        #region Métodos

        public static TipoContrato MapToModel(TipoContratoViewModel tipoContratoViewModel)
        {
            TipoContrato tipoContrato = new TipoContrato()
            {
                IdTipoContrato = tipoContratoViewModel.IdTipoContrato,
                Descr = tipoContratoViewModel.Descr
            };
            return tipoContrato;
        }

        public static TipoContratoViewModel MapToViewModel(TipoContrato tipoContrato)
        {
            TipoContratoViewModel tipoContratoViewModel = new TipoContratoViewModel()
            {
                IdTipoContrato = -1,
                Descr = "N/A"
            };

            if (tipoContrato != null)
            {
                tipoContratoViewModel = new TipoContratoViewModel()
                {
                    IdTipoContrato = tipoContrato.IdTipoContrato,
                    Descr = tipoContrato.Descr
                };
            }
           
            return tipoContratoViewModel;
        }


        public static List<TipoContrato> MapToListModel(List<TipoContratoViewModel> tipoContratosViewModel)
        {
            List<TipoContrato> listaTipoContrato = (from tipoContrato in tipoContratosViewModel
                                          select new TipoContrato()
                                          {
                                              IdTipoContrato = tipoContrato.IdTipoContrato,
                                              Descr = tipoContrato.Descr
                                          }).ToList();
            return listaTipoContrato;
        }

        public static List<TipoContratoViewModel> MapToListViewModel(List<TipoContrato> tipoContratos)
        {
            List<TipoContratoViewModel> listaTipoContratoViewModel = (from tipoContratoViewModel in tipoContratos
                                                            select new TipoContratoViewModel()
                                                            {
                                                                IdTipoContrato = tipoContratoViewModel.IdTipoContrato,
                                                                Descr = tipoContratoViewModel.Descr
                                                            }).ToList();
            return listaTipoContratoViewModel;
        }

        #endregion
    }
}