using Senai.SPE.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPE.ViewModels.Agendamento
{
    public class optionsVM
    {
        public List<Senai.SPE.Domain.Docente> listaDocentes{get;set;}
        public List<Ambiente> listaAmbientes { get; set; }
        public List<Turma> listaTurma { get; set; }
        public List<Componente> listaComponente { get; set; }
    }
}