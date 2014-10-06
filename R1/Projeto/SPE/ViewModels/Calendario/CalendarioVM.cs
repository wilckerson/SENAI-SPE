using Senai.SPE.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPE.ViewModels
{
    public class CalendarioVM
    {
        public CalendarioCivil Calendario { get; set; }

        public List<CalendarioCivil> Calendarios { get; set; }
        public List<CalendarioLegenda> Legendas { get; set; }
    }
}