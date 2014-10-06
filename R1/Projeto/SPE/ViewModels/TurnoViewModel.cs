using Senai.SPE.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPE.ViewModel
{
    public class TurnoViewModel
    {
        public int IdTurno { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Descrição")]
        public string Descr { get; set; }

        public int TimeInicialHoraView { get; set; }
        public int TimeInicialMinutoView { get; set; }
        public int TimeFinalHoraView { get; set; }
        public int TimeFinalMinutoView { get; set; }


        [DisplayFormat(DataFormatString = "{0:hh\\:mm}")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Hora Inicial")]
        public Nullable<System.TimeSpan> HoraIni { get; set; }

        [DisplayFormat(DataFormatString = "{0:hh\\:mm}")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Hora Final")]
        public Nullable<System.TimeSpan> HoraFim { get; set; }


        public virtual ICollection<Turma> Turma { get; set; }
        

        #region Métodos
       
        public static Turno MapToModel(TurnoViewModel turnoViewModel)
        {
            Turno turno = new Turno()
            {
                IdTurno = turnoViewModel.IdTurno,
                Descr = turnoViewModel.Descr,
                HoraIni = turnoViewModel.HoraIni,
                HoraFim = turnoViewModel.HoraFim
                //HoraIni = new TimeSpan(turnoViewModel.TimeInicialHoraView, turnoViewModel.TimeInicialMinutoView, 0),
                //HoraFim = new TimeSpan(turnoViewModel.TimeFinalHoraView, turnoViewModel.TimeFinalMinutoView, 0)

            };
            return turno;
        }

        public static TurnoViewModel MapToViewModel(Turno turno)
        {
            TurnoViewModel turnoViewModel = new TurnoViewModel()
            {
                IdTurno = turno.IdTurno,
                Descr = turno.Descr,
                Turma = turno.Turma,
                HoraIni = turno.HoraIni,
                HoraFim = turno.HoraFim,
                TimeInicialHoraView = (turno.HoraIni != null) ? turno.HoraIni.Value.Hours : 6,
                TimeInicialMinutoView = (turno.HoraIni != null) ? turno.HoraIni.Value.Minutes : 0,
                TimeFinalHoraView = (turno.HoraFim != null) ? turno.HoraFim.Value.Hours : 6,
                TimeFinalMinutoView = (turno.HoraFim != null) ? turno.HoraFim.Value.Minutes : 0
            };
            return turnoViewModel;
        }


        public static List<Turno> MapToListModel(List<TurnoViewModel> turnoViewModel)
        {
            List<Turno> listaTurno = (from turno in turnoViewModel
                                      select new Turno()
                                      {
                                          IdTurno = turno.IdTurno,
                                          Descr = turno.Descr,
                                          HoraIni = turno.HoraIni,
                                          HoraFim = turno.HoraFim
                                          //HoraIni = new TimeSpan(turno.TimeInicialHoraView, turno.TimeInicialMinutoView, 0),
                                          //HoraFim = new TimeSpan(turno.TimeFinalHoraView, turno.TimeFinalMinutoView, 0)
                                      }).ToList();
            return listaTurno;
        }

        public static List<TurnoViewModel> MapToListViewModel(List<Turno> turno)
        {
            List<TurnoViewModel> listaTurnoViewModel = (from turnoViewModel in turno
                                                        select new TurnoViewModel()
                                                        {
                                                            IdTurno = turnoViewModel.IdTurno,
                                                            Descr = turnoViewModel.Descr,
                                                            Turma = turnoViewModel.Turma,
                                                            HoraIni = turnoViewModel.HoraIni,
                                                            HoraFim = turnoViewModel.HoraFim,
                                                            TimeInicialHoraView = (turnoViewModel.HoraIni != null) ? turnoViewModel.HoraIni.Value.Hours : 6,
                                                            TimeInicialMinutoView = (turnoViewModel.HoraIni != null) ? turnoViewModel.HoraIni.Value.Minutes : 0,
                                                            TimeFinalHoraView = (turnoViewModel.HoraFim != null) ? turnoViewModel.HoraFim.Value.Hours : 6,
                                                            TimeFinalMinutoView = (turnoViewModel.HoraFim != null) ? turnoViewModel.HoraFim.Value.Minutes : 0
                                                        }).ToList();
            return listaTurnoViewModel;
        }

    
        #endregion
    }
}