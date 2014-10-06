using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPE.ViewModels.Calendario
{

    public static class TiposItemsAgendamento
    {
        public static string CalendarioCivil = "CalendarioCivil";
        public static string AgendaDocente = "AgendaDocente";
        public static string Evento = "Evento";
    }

    public class CalendarioItemAgendamento
    {

        public string tipo { get; set; }

        public int id { get; set; }
        public string title { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string originalStart { get; set; }
        public string originalEnd { get; set; }
        public int idLegenda { get; set; }
        public string color { get; set; }
        public string textColor { get; set; }
        public bool allDay { get; set; }
        public string description { get; set; }

        public bool conflito { get; set; }

        public short? carga { get; set; }

        public string matriz { get; set; }

        public string turno { get; set; }

        public string dataInicioTurma { get; set; }

        public string dataFinalTurma { get; set; }

        public string horaInicio { get; set; }
        public string horaFim { get; set; }
        public string docente { get; set; }
        public string ambiente { get; set; }
        public string componente { get; set; }
        public string turma { get; set; }
    }
}