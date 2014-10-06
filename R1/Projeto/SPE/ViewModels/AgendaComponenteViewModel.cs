using Senai.SPE.Domain;
using Senai.SPE.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPE.ViewModel
{
    public class AgendaComponenteViewModel
    {

        #region Atributos

        public int IdAgendaComponente { get; set; }
        public Nullable<int> IdComponente { get; set; }
        public Nullable<int> IdModulo { get; set; }
        public Nullable<int> IdUnidade { get; set; }

         [Display(Name = "Data Inicial")]
         [Required(ErrorMessage = "Campo Obrigatório")]
        public Nullable<System.DateTime> DataIni { get; set; }

        [Display(Name = "Data Fim")]
        public Nullable<System.DateTime> DataFim { get; set; }

        [Display(Name = "Hora Inicial")]
        public Nullable<System.DateTime> HoraIni { get; set; }

           [Display(Name = "Hora Fim")]
        public Nullable<System.DateTime> HoraFim { get; set; }
        public Nullable<bool> STDefinir { get; set; }
        public string DiasSemana { get; set; }
        public Nullable<int> IdTurma { get; set; }

        public Componente Componente { get; set; }

        public List<Ambiente> Ambientes { get; set; }
        public List<Docente> Docentes { get; set; }
        public List<string> ListaDias { get; set; }
        public List<TipoAmbienteViewModel> ListaTipoAmbiente{ get; set; }

        public AgendaComponenteViewModel ComponentesAgendados { get; set; }
        public List<AgendaAmbienteViewModel> AmbienteAgendados { get; set; }
        public List<AgendaDocenteViewModel> DocentesAgendados { get; set; }

        public List<LocalAmbienteViewModel> Locais;
        public List<TipoAmbienteViewModel> TiposAmbientes;

        public string HoraIniView { get; set; }

        public string HoraFimView { get; set; }

        public string DataFimView { get; set; }
         [Required(ErrorMessage = "Campo Obrigatório")]
        public string DataIniView { get; set; }
         public virtual Turma Turma { get; set; }
        //public List<DiasSemanaEnum> ListDiasSemana { get{
        //    List<DiasSemanaEnum> dias = new List<DiasSemanaEnum>();
        //    dias.Add(DiasSemanaEnum.Domingo);
        //    dias.Add(DiasSemanaEnum.Segunda);
        //    dias.Add(DiasSemanaEnum.Terca);
        //    dias.Add(DiasSemanaEnum.Quarta);
        //    dias.Add(DiasSemanaEnum.Quinta);
        //    dias.Add(DiasSemanaEnum.Sexta);
        //    dias.Add(DiasSemanaEnum.Sabado);
        // return dias ;
        //} set; }

        #endregion

        #region Métodos

        public static AgendaComponente MapToModel(AgendaComponenteViewModel agendaComponenteViewModel)
        {
            AgendaComponente agendaComponente = new AgendaComponente()
            {
                IdAgendaComponente = agendaComponenteViewModel.IdAgendaComponente,
                IdComponente = agendaComponenteViewModel.IdComponente,
                IdUnidade = agendaComponenteViewModel.IdUnidade,
                DataIni = DateTime.Parse(agendaComponenteViewModel.DataIniView),
                DataFim = DateTime.Parse(agendaComponenteViewModel.DataFimView),
                HoraIni = DateTime.Parse(agendaComponenteViewModel.HoraIniView),
                HoraFim = DateTime.Parse(agendaComponenteViewModel.HoraFimView),
                STDefinir = agendaComponenteViewModel.STDefinir,
                DiasSemana = agendaComponenteViewModel.DiasSemana,
               
            };
            return agendaComponente;
        }

        public static AgendaComponenteViewModel MapToViewModel(AgendaComponente agendaComponente)
        {
            AgendaComponenteViewModel agendaComponenteViewModel = new AgendaComponenteViewModel();
            if (agendaComponente != null && agendaComponente.IdAgendaComponente != 0)
            { 
             agendaComponenteViewModel = new AgendaComponenteViewModel()
            {
                IdAgendaComponente = agendaComponente.IdAgendaComponente,
                IdComponente = agendaComponente.IdComponente,
                IdUnidade = agendaComponente.IdUnidade,
                DataIniView = agendaComponente.DataIni == null ? "" : agendaComponente.DataIni.Value.ToShortDateString(),
                DataFimView = agendaComponente.DataFim== null ? "" : agendaComponente.DataFim.Value.ToShortDateString(),
                HoraIniView = agendaComponente.HoraIni== null ? "" : agendaComponente.HoraIni.Value.ToShortTimeString(),
                HoraFimView = agendaComponente.HoraFim== null ? "" : agendaComponente.HoraFim.Value.ToShortTimeString(),
                STDefinir = agendaComponente.STDefinir,
                DiasSemana = agendaComponente.DiasSemana.Replace("0", "Domingo")
                                 .Replace("1", "Segunda")
                                 .Replace("2", "Terca")
                                 .Replace("3", "Quarta")
                                 .Replace("4", "Quinta")
                                 .Replace("5", "Sexta")
                                 .Replace("6", "Sabado").Replace(",", "").Replace(" ", "").Replace("/", ""),
                Turma = agendaComponente.Turma
            };
            }
            return agendaComponenteViewModel;
        }


        public static List<AgendaComponente> MapToListModel(List<AgendaComponenteViewModel> agendaComponentesViewModel)
        {
            List<AgendaComponente> listaAgendaComponente = (from agendaComponente in agendaComponentesViewModel
                                          select new AgendaComponente()
                                          {
                                              IdAgendaComponente = agendaComponente.IdAgendaComponente,
                                              IdComponente = agendaComponente.IdComponente,
                                              IdUnidade = agendaComponente.IdUnidade,
                                              DataIni = agendaComponente.DataIni,
                                              DataFim = agendaComponente.DataFim,
                                              HoraIni = agendaComponente.HoraIni,
                                              HoraFim = agendaComponente.HoraFim,
                                              STDefinir = agendaComponente.STDefinir,
                                              DiasSemana = agendaComponente.DiasSemana
                                          }).ToList();
            return listaAgendaComponente;
        }

        public static List<AgendaComponenteViewModel> MapToListViewModel(List<AgendaComponente> agendaComponentes)
        {
            List<AgendaComponenteViewModel> listaAgendaComponenteViewModel = (from agendaComponenteViewModel in agendaComponentes
                                                            select new AgendaComponenteViewModel()
                                                            {
                                                                IdAgendaComponente = agendaComponenteViewModel.IdAgendaComponente,
                                                                IdComponente = agendaComponenteViewModel.IdComponente,
                                                                IdUnidade = agendaComponenteViewModel.IdUnidade,
                                                                DataIni = agendaComponenteViewModel.DataIni,
                                                                DataFim = agendaComponenteViewModel.DataFim,
                                                                HoraIni = agendaComponenteViewModel.HoraIni,
                                                                HoraFim = agendaComponenteViewModel.HoraFim,
                                                                STDefinir = agendaComponenteViewModel.STDefinir,
                                                                DiasSemana = agendaComponenteViewModel.DiasSemana,
                                                                Turma = agendaComponenteViewModel.Turma
                                                            }).ToList();
            return listaAgendaComponenteViewModel;
        }



        #endregion

        public static AgendaComponenteViewModel MapToListViewModel(AgendaComponente agendaComponente)
        {
            AgendaComponenteViewModel agendaComponenteViewModel = new AgendaComponenteViewModel();
            if (agendaComponente != null && agendaComponente.IdAgendaComponente != 0)
            {
                agendaComponenteViewModel = new AgendaComponenteViewModel()
                {
                    IdAgendaComponente = agendaComponente.IdAgendaComponente,
                    IdComponente = agendaComponente.IdComponente,
                    IdUnidade = agendaComponente.IdUnidade,
                    DataIniView = agendaComponente.DataIni == null ? "" : agendaComponente.DataIni.Value.ToShortDateString(),
                    DataFimView = agendaComponente.DataFim == null ? "" : agendaComponente.DataFim.Value.ToShortDateString(),
                    HoraIniView = agendaComponente.HoraIni == null ? "" : agendaComponente.HoraIni.Value.ToShortTimeString(),
                    HoraFimView = agendaComponente.HoraFim == null ? "" : agendaComponente.HoraFim.Value.ToShortTimeString(),
                    STDefinir = agendaComponente.STDefinir,
                    DiasSemana = agendaComponente.DiasSemana.Replace("0", "Domingo")
                                     .Replace("1", "Segunda")
                                     .Replace("2", "Terca")
                                     .Replace("3", "Quarta")
                                     .Replace("4", "Quinta")
                                     .Replace("5", "Sexta")
                                     .Replace("6", "Sabado").Replace(",", "").Replace(" ", "").Replace("/", ""),
                };
            }
            return agendaComponenteViewModel;
        }
    }
}