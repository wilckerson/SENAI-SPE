using Senai.SPE.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPE.ViewModel
{
    public class CalendarioLegendaViewModel
    {
        #region Atributos

        [Key]
        public int? IdCalendario { get; set; }

        [Required]
        [MaxLength(250, ErrorMessage = "Máximo 250 caracteres")]
        [Display(Name = "Descrição: ")]
        public string Descricao { get; set; }

        [Required]
        [Display(Name = "Cor: ")]
        public string Cor { get; set; }

        #endregion

        #region Métodos

        public static CalendarioLegenda MapToModel(CalendarioLegendaViewModel CalendarioLegendaViewModel)
        {
            CalendarioLegenda CalendarioLegenda = new CalendarioLegenda()
            {
                IdCalendario = CalendarioLegendaViewModel.IdCalendario.GetValueOrDefault(0),
                Descricao = CalendarioLegendaViewModel.Descricao,
                Cor = CalendarioLegendaViewModel.Cor
            };

            return CalendarioLegenda;
        }

        public static CalendarioLegendaViewModel MapToViewModel(CalendarioLegenda CalendarioLegenda)
        {
            CalendarioLegendaViewModel CalendarioLegendaViewModel = new CalendarioLegendaViewModel()
            {
                IdCalendario = CalendarioLegenda.IdCalendario,
                Descricao = CalendarioLegenda.Descricao,
                Cor = CalendarioLegenda.Cor
            };
            return CalendarioLegendaViewModel;
        }


        public static List<CalendarioLegenda> MapToListModel(List<CalendarioLegendaViewModel> CalendarioLegendaViewModel)
        {
            List<CalendarioLegenda> listaCalendarioLegenda = (from CalendarioLegenda in CalendarioLegendaViewModel
                                                              select new CalendarioLegenda()
                                                              {
                                                                  IdCalendario = CalendarioLegenda.IdCalendario.GetValueOrDefault(0),
                                                                  Descricao = CalendarioLegenda.Descricao,
                                                                  Cor = CalendarioLegenda.Cor
                                                              }).ToList();
            return listaCalendarioLegenda;
        }

        public static List<CalendarioLegendaViewModel> MapToListViewModel(List<CalendarioLegenda> CalendarioLegenda)
        {
            List<CalendarioLegendaViewModel> listaCalendarioLegendaViewModel = (from CalendarioLegendaViewModel in CalendarioLegenda
                                                                                select new CalendarioLegendaViewModel()
                                                                                {
                                                                                    IdCalendario = CalendarioLegendaViewModel.IdCalendario,
                                                                                    Descricao = CalendarioLegendaViewModel.Descricao,
                                                                                    Cor = CalendarioLegendaViewModel.Cor
                                                                                }).ToList();
            return listaCalendarioLegendaViewModel;
        }



        #endregion
    }
}
