using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SPERepository;
using Senai.SPE.Domain;

namespace SPE.ViewModels.Agendamento
{
    public class AgendamentoIndexVM
    {
         public int ItemId { get; set; }

         public int ItemTipo { get; set; }

         public int ItemVersao { get; set; }
         
         public string Docente { get; set; }

         public List<Senai.SPE.Domain.Docente> listaDocentes { get; set; }

         public List<Ambiente> listaAmbientes { get; set; }

         public List<Turma> listaTurmas { get; set; }

         public List<Componente> listaComponentes { get; set; }

         public List<AreaAtuacao> listaAreaAtuacao { get; set; }
         public int areaAtuacao { get; set; }
         public string ItemData { get; set; }
         public string NomeItem { get; set; }
         
    }
}