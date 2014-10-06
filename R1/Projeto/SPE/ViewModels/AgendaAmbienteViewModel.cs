using Senai.SPE.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace SPE.ViewModel
{
    public class AgendaAmbienteViewModel
    {
        public int IdAgendaAmbiente { get; set; }
        public string IdModulo { get; set; }
        public string IdTurma { get; set; }

        public string IdComponente { get; set; }
        public string IdAmbiente { get; set; }
        public string DataIni { get; set; }

        public string DataFim { get; set; }

        public string DiasSemanaAmbiente { get; set; }



        public string HoraIni { get; set; }

        public string HoraFim { get; set; }

        public virtual Ambiente Ambiente { get; set; }
        public virtual Componente Componente { get; set; }
        public virtual Modulo Modulo { get; set; }

        internal static AgendaAmbiente MapToModel(AgendaAmbienteViewModel item)
        {
            AgendaAmbiente ambiente = new AgendaAmbiente()
            {
                IdComponente = int.Parse(item.IdComponente),
                IdModulo = int.Parse(item.IdModulo),
                DataFim = DateTime.Parse(item.DataFim),
                DataIni = DateTime.Parse(item.DataIni),
                HoraIni = DateTime.Parse(item.HoraIni),
                HoraFim = DateTime.Parse(item.HoraFim),
                IdAmbiente = int.Parse(item.IdAmbiente),
                IdTurma = int.Parse(item.IdTurma),
                DiasSemana = item.DiasSemanaAmbiente.Replace("Domingo", "0")
                .Replace("Segunda", "1")
                .Replace("Terca", "2")
                .Replace("Quarta", "3")
                .Replace("Quinta", "4")
                .Replace("Sexta", "5")
                .Replace("Sabado", "6").Replace(",", "").Replace(" ", "").Replace("/", ""),



            };
            return ambiente;
        }

        internal static List<AgendaAmbienteViewModel> MapToListViewModel(List<AgendaAmbiente> list)
        {
            List<AgendaAmbienteViewModel> listaAgendaAmbienteViewModel = new List<AgendaAmbienteViewModel>();

            Regex reg = new Regex("/$");

            if (list != null && list.Count != 0)
            {
                listaAgendaAmbienteViewModel = (from agendaAmbienteViewModel in list
                                                select new AgendaAmbienteViewModel()
                                                {
                                                    IdAgendaAmbiente = agendaAmbienteViewModel.IdAgendaAmbiente,
                                                    IdAmbiente = agendaAmbienteViewModel.IdAmbiente.ToString(),
                                                    IdTurma = agendaAmbienteViewModel.IdTurma.Value.ToString(),
                                                    DataIni = agendaAmbienteViewModel.DataIni.Value.ToString("dd/MM/yyyy"),
                                                    DataFim = agendaAmbienteViewModel.DataFim.Value.ToString("HH:mm"),
                                                    HoraIni = agendaAmbienteViewModel.HoraIni.Value.ToString("HH:mm"),
                                                    HoraFim = agendaAmbienteViewModel.HoraFim.Value.ToString("HH:mm"),

                                                    DiasSemanaAmbiente = reg.Replace(agendaAmbienteViewModel.DiasSemana.Replace("0", "Domingo/")
                                                        .Replace("1", "Segunda/")
                                                        .Replace("2", "Terca/")
                                                        .Replace("3", "Quarta/")
                                                        .Replace("4", "Quinta/")
                                                        .Replace("5", "Sexta/")
                                                        .Replace("6", "Sabado/").Replace(",", "").Replace(" ", ""), ""),
                                                    Ambiente = agendaAmbienteViewModel.Ambiente,
                                                }).ToList();
            }
            return listaAgendaAmbienteViewModel;
        }

    }
}
