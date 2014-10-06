using Senai.SPE.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace SPE.ViewModel
{
    public class AreaAtuacaoDocenteViewModel
    {
        public int IdDocente { get; set; }
        public int IdAreaAtuacao { get; set; }

        //internal static List<AgendaDocenteViewModel> MapToListViewModel(List<AreaAtuacaoDocente> list)
        //{

        //    List<AgendaDocenteViewModel> listaAgendaDocenteViewModel = new List<AgendaDocenteViewModel>();

        //    Regex reg = new Regex("/$");

        //     if (list != null && list.Count != 0)
        //     { 
        //         listaAgendaDocenteViewModel = (from agendaDocenteViewModel in list
        //                                                                     select new AgendaDocenteViewModel()
        //                                                                     {
        //                                                                         IdAgendaDocente = agendaDocenteViewModel.IdAgendaDocente,
        //                                                                         IdDocente = agendaDocenteViewModel.IdDocente.ToString(),
        //                                                                         IdTurma = agendaDocenteViewModel.IdTurma.ToString(),
               
        //                                                                         DataIni = agendaDocenteViewModel.DataIni.Value.ToString("dd/MM/yyyy"),
        //                                                                         DataFim = agendaDocenteViewModel.DataFim.Value.ToString("HH:mm"),
        //                                                                         HoraIni = agendaDocenteViewModel.HoraIni.Value.ToString("HH:mm"),
        //                                                                         HoraFim = agendaDocenteViewModel.HoraFim.Value.ToString("HH:mm"),
        //                                                                         Docente = agendaDocenteViewModel.Docente,
        //                                                                         DiasSemanaDocente = reg.Replace(agendaDocenteViewModel.DiasSemana
        //                                                                            .Replace("0", "Domingo/")
        //                                                                            .Replace("1", "Segunda/")
        //                                                                            .Replace("2", "Terca/")
        //                                                                            .Replace("3", "Quarta/")
        //                                                                            .Replace("4", "Quinta/")
        //                                                                            .Replace("5", "Sexta/")
        //                                                                            .Replace("6", "Sabado/")
        //                                                                            .Replace(",", "").Replace(" ", ""), "")
        //                                                                     }).ToList();

        //     }
        //     return listaAgendaDocenteViewModel;
        //}
    }
}