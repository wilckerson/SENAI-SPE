using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPE.ViewModels
{
    public class CalendarioItem
    {
        public int id { get; set; }
        public string title { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string originalStart { get; set; }
        public string originalEnd { get; set; }
        public int idLegenda{ get; set; }
        public string color { get; set; }
        public string textColor { get; set; }
        public bool allDay { get; set; }
        public string description { get; set; }

        public string tipo { get; set; }

        public short? carga { get; set; }

        public string matriz { get; set; }

        public string turno { get; set; }

        public string dataInicioTurma { get; set; }

        public string dataFinalTurma { get; set; }
    }
}