using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Senai.SPE.Domain;
using Senai.SPE.Domain.Enum;
using Extensions.EnumEx;
using System.ComponentModel.DataAnnotations;

namespace SPE.ViewModel
{

    public class AgendaEventoViewModel
    {
        public int IdAgendaEvento { get; set; }
        public string IdDocente { get; set; }

        [Required]
        public string DataIni { get; set; }

        [Required]
        public string DataFim { get; set; }

        [Required]
        public string Descricao { get; set; }

        public string HoraIni { get; set; }
        public string HoraFim { get; set; }
        public string DiaSemana { get; set; }

        public string TipoEvento { get; set; }
        public List<TipoAtividade> TipoEventos = new List<TipoAtividade>();

        public virtual Docente Docente { get; set; }
        public List<Docente> Docentes { get; set; }

        internal static AgendaEvento MapToModel(AgendaEventoViewModel item)
        {
            var docente = new AgendaEvento()
            {
                DataFim = DateTime.Parse(item.DataFim),
                DataIni = DateTime.Parse(item.DataIni),
                HoraIni = DateTime.Parse(item.HoraIni),
                HoraFim = DateTime.Parse(item.HoraFim),
                TipoEvento = byte.Parse(item.TipoEvento),
                IdDocente = int.Parse(item.IdDocente),
                DiaSemana = item.DiaSemana.Replace("Domingo", "0")
                    .Replace("Segunda", "1")
                    .Replace("Terca", "2")
                    .Replace("Quarta", "3")
                    .Replace("Quinta", "4")
                    .Replace("Sexta", "5")
                    .Replace("Sabado", "6").Replace(",", "").Replace(" ", "").Replace("/", ""),


            };
            return docente;
        }

        internal static List<AgendaEvento> MapToListViewModel(List<AgendaEvento> list)
        {

            List<AgendaEvento> listaAgendaEventosViewModel = new List<AgendaEvento>();

            Regex reg = new Regex("/$");

            if (list != null && list.Count != 0)
            {
                listaAgendaEventosViewModel = (from listaAgendaEventoViewModel in list
                                               select new AgendaEvento()
                                               {
                                                   DataFim = listaAgendaEventoViewModel.DataFim,
                                                   DataIni = listaAgendaEventoViewModel.DataIni,
                                                   HoraIni = listaAgendaEventoViewModel.HoraIni,
                                                   HoraFim = listaAgendaEventoViewModel.HoraFim,
                                                   TipoEvento = listaAgendaEventoViewModel.TipoEvento,
                                                   IdDocente = listaAgendaEventoViewModel.IdDocente,
                                                   DiaSemana = reg.Replace(listaAgendaEventoViewModel.DiaSemana.Replace("0", "Domingo/")
                                                       .Replace("1", "Segunda/")
                                                       .Replace("2", "Terca/")
                                                       .Replace("3", "Quarta/")
                                                       .Replace("4", "Quinta/")
                                                       .Replace("5", "Sexta/")
                                                       .Replace("6", "Sabado/")
                                                       .Replace(",", "").Replace(" ", ""), "")

                                               }).ToList();
            }

            return listaAgendaEventosViewModel;
        }
    }
}