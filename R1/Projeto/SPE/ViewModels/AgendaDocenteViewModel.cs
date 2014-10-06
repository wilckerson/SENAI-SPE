using Senai.SPE.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace SPE.ViewModel
{
    public class AgendaDocenteViewModel
    {
        public int IdAgendaDocente { get; set; }
        public string IdModulo { get; set; }
        public string IdTurma { get; set; }
       
        public string IdComponente { get; set; }
        public string IdDocente { get; set; }
        public string DataIni { get; set; }


        public string DataFim { get; set; }

        public string DiasSemanaDocente { get; set; }



        public string HoraIni { get; set; }

        public string HoraFim { get; set; }

        public virtual Docente Docente { get; set; }
        public virtual Componente Componente { get; set; }
        public virtual Modulo Modulo { get; set; }
        internal static AgendaDocente MapToModel(AgendaDocenteViewModel item)
        {
            AgendaDocente docente = new AgendaDocente()
            {
                IdComponente = int.Parse(item.IdComponente),
                IdModulo = int.Parse(item.IdModulo),
                DataFim = DateTime.Parse(item.DataFim),
                DataIni = DateTime.Parse(item.DataIni),
                HoraIni = DateTime.Parse(item.HoraIni),
                HoraFim = DateTime.Parse(item.HoraFim),
                IdTurma = int.Parse(item.IdTurma),
                IdDocente = int.Parse(item.IdDocente),
                DiasSemana = item.DiasSemanaDocente.Replace("Domingo", "0")
                .Replace("Segunda", "1")
                .Replace("Terca", "2")
                .Replace("Quarta", "3")
                .Replace("Quinta", "4")
                .Replace("Sexta", "5")
                .Replace("Sabado", "6").Replace(",", "").Replace(" ","").Replace("/",""),


            };
            return docente;
        }

        internal static List<AgendaDocenteViewModel> MapToListViewModel(List<AgendaDocente> list)
        {

            List<AgendaDocenteViewModel> listaAgendaDocenteViewModel = new List<AgendaDocenteViewModel>();

            Regex reg = new Regex("/$");

             if (list != null && list.Count != 0)
             { 
                 listaAgendaDocenteViewModel = (from agendaDocenteViewModel in list
                                                                             select new AgendaDocenteViewModel()
                                                                             {
                                                                                 IdAgendaDocente = agendaDocenteViewModel.IdAgendaDocente,
                                                                                 IdDocente = agendaDocenteViewModel.IdDocente.ToString(),
                                                                                 IdTurma = agendaDocenteViewModel.IdTurma.ToString(),
               
                                                                                 DataIni = agendaDocenteViewModel.DataIni.Value.ToString("dd/MM/yyyy"),
                                                                                 DataFim = agendaDocenteViewModel.DataFim.Value.ToString("HH:mm"),
                                                                                 HoraIni = agendaDocenteViewModel.HoraIni.Value.ToString("HH:mm"),
                                                                                 HoraFim = agendaDocenteViewModel.HoraFim.Value.ToString("HH:mm"),
                                                                                 Docente = agendaDocenteViewModel.Docente,
                                                                                 DiasSemanaDocente = reg.Replace(agendaDocenteViewModel.DiasSemana
                                                                                    .Replace("0", "Domingo/")
                                                                                    .Replace("1", "Segunda/")
                                                                                    .Replace("2", "Terca/")
                                                                                    .Replace("3", "Quarta/")
                                                                                    .Replace("4", "Quinta/")
                                                                                    .Replace("5", "Sexta/")
                                                                                    .Replace("6", "Sabado/")
                                                                                    .Replace(",", "").Replace(" ", ""), "")
                                                                             }).ToList();

             }
             return listaAgendaDocenteViewModel;
        }
    }
}