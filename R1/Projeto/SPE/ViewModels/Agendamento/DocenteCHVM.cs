using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SPERepository;
using Senai.SPE.Domain;

namespace SPE.ViewModels.Agendamento
{
    public class DocenteCHVM
    {

       public Senai.SPE.Domain.Docente docente { get; set; }
       public  int CHUsada { get; set; }
       public int CHTotal { get; set; }
    }
}