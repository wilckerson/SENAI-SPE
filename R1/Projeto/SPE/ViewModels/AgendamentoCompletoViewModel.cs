using Senai.SPE.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPE.ViewModel
{
    public class AgendamentoCompletoViewModel
    {
        public string IdTurma { get; set; }
        public string IdModulo { get; set; }
        public string IdComponente { get; set; }
     
        public string DataIni { get; set; }

        public string DataFim { get; set; }

         [Required(ErrorMessage = "Campo Obrigatório")]
        public string DataIniView { get; set; }

        public string DataFimView { get; set; }

        public string DiasSemanaComponente { get; set; }


       
        public string HoraIniView { get; set; }

        public string HoraFimView { get; set; }

        public List<AgendaAmbienteViewModel> AgendaAmbientes { get; set; }
        public List<AgendaDocenteViewModel> AgendaDocentes { get; set; }

        internal static Senai.SPE.Domain.AgendaComponente MapToModelAgendaComponente(AgendamentoCompletoViewModel agenda)
        {
            AgendaComponente componente = new AgendaComponente()
            {
              IdComponente = int.Parse(agenda.IdComponente),
              IdModulo =  int.Parse(agenda.IdModulo),
              DataFim = DateTime.Parse(agenda.DataFimView),
              DataIni =  DateTime.Parse(agenda.DataIniView),
              HoraIni =  DateTime.Parse(agenda.HoraIniView),
              HoraFim = DateTime.Parse(agenda.HoraFimView),
              IdTurma = int.Parse(agenda.IdTurma),
              DiasSemana = agenda.DiasSemanaComponente.Replace("Domingo","0")
              .Replace("Segunda", "1")
              .Replace("Terca", "2")
              .Replace("Quarta", "3")
              .Replace("Quinta", "4")
              .Replace("Sexta", "5")
              .Replace("Sabado", "6").Replace(",", "").Replace(" ", "").Replace("/", ""),
              

            };
            return componente;
        }
    }
}