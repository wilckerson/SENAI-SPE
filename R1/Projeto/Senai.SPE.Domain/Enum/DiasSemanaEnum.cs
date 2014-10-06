using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senai.SPE.Domain.Enum
{
    public enum DiasSemanaEnum
    {
          [Description("Domingo")]
         Domingo = 0,
        [Description("Segunda-Feira")]
        Segunda = 1,
        [Description("Terça-Feira")]
        Terca = 2,
        [Description("Quarta-Feira")]
        Quarta = 3,
        [Description("Quinta-Feira")]
        Quinta = 4,
        [Description("Sexta-Feira")]
        Sexta = 5,
        [Description("Sábado")]
        Sabado = 6,
      
    }
}
