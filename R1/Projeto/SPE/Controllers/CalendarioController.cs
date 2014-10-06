using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Senai.SPE.Domain;
using Common;
using SPE.ViewModel;
using SPE.ViewModels;
using BusinessLogic;

namespace SPE.Controllers
{
    public class CalendarioController : BaseController
    {
        public ActionResult Index()
        {
            return RedirectToAction("Feriados");
        }

        public ActionResult AgendaAmbienteDocente(int turmaId = 0, string start = "", string end = "")
        {
            DateTime startDate = UnixTimeStampToDateTime(double.Parse(start));
            DateTime endDate = UnixTimeStampToDateTime(double.Parse(end));
            endDate = endDate.AddHours(-endDate.Hour);
            startDate = startDate.AddHours(-startDate.Hour);
            SPEBL BL = new SPEBL();

            List<AgendaAmbiente> ambientes = BL.AgendaAmbiente.Get(a => a.IdTurma == turmaId
                    && ((a.DataIni >= startDate && a.DataIni <= endDate)
                    || (a.DataFim >= startDate && a.DataFim <= endDate))
            , null, "Ambiente,Componente,Turma.Matriz,Turma.Turno").ToList();
            List<CalendarioItem> itensCalendario = new List<CalendarioItem>();

            foreach (var item in ambientes)
            {
                DateTime StartDate = item.DataIni.Value;
                DateTime EndDate = item.DataFim.Value;
                int Interval = 1;

                while (StartDate.AddDays(Interval) <= EndDate)
                {
                    if (
                        (StartDate.DayOfWeek == DayOfWeek.Sunday && item.DiasSemana.Contains(((int)DayOfWeek.Sunday).ToString()))
                        || (StartDate.DayOfWeek == DayOfWeek.Monday && item.DiasSemana.Contains(((int)DayOfWeek.Monday).ToString()))
                        || (StartDate.DayOfWeek == DayOfWeek.Tuesday && item.DiasSemana.Contains(((int)DayOfWeek.Tuesday).ToString()))
                        || (StartDate.DayOfWeek == DayOfWeek.Wednesday && item.DiasSemana.Contains(((int)DayOfWeek.Wednesday).ToString()))
                        || (StartDate.DayOfWeek == DayOfWeek.Thursday && item.DiasSemana.Contains(((int)DayOfWeek.Thursday).ToString()))
                        || (StartDate.DayOfWeek == DayOfWeek.Friday && item.DiasSemana.Contains(((int)DayOfWeek.Friday).ToString()))
                        || (StartDate.DayOfWeek == DayOfWeek.Saturday && item.DiasSemana.Contains(((int)DayOfWeek.Saturday).ToString()))
                        )
                    {
                        itensCalendario.Add(new CalendarioItem
                        {
                            originalStart = StartDate.ToShortDateString(),
                            originalEnd = EndDate.ToShortDateString(),
                            start = StartDate.ToString("yyyy-MM-dd"),
                            title = item.Ambiente.Nome,
                            carga = item.Componente.CH,
                            matriz = item.Turma.Matriz.Nome,
                            turno = item.Turma.Turno.Descr,
                            dataInicioTurma = item.Turma.DataInicio.HasValue ? item.Turma.DataInicio.Value.ToString("dd/MM/yyyy") : "",
                            dataFinalTurma = item.Turma.DataFim.HasValue ? item.Turma.DataFim.Value.ToString("dd/MM/yyyy") : "",

                            description = "" + item.Componente.Nome + " " + item.HoraIni.Value.ToString("hh:mm") + " - " + item.HoraFim.Value.ToString("hh:mm") + " ",
                            color = "#e4bc43",
                            textColor = "black"

                        });
                    }

                    StartDate = StartDate.AddDays(Interval);
                }
            }


            List<AgendaDocente> docentes = BL.AgendaDocente.Get(a => a.IdTurma == turmaId
                    && ((a.DataIni >= startDate && a.DataIni <= endDate)
                    || (a.DataFim >= startDate && a.DataFim <= endDate))
                , null, "Docente,Componente,Turma.Matriz,Turma.Turno").ToList();

            foreach (var item in docentes)
            {
                DateTime StartDate = item.DataIni.Value;
                DateTime EndDate = item.DataFim.Value;
                int Interval = 1;

                while (StartDate.AddDays(Interval) <= EndDate)
                {
                    if (
                        (StartDate.DayOfWeek == DayOfWeek.Sunday && item.DiasSemana.Contains(((int)DayOfWeek.Sunday).ToString()))
                        || (StartDate.DayOfWeek == DayOfWeek.Monday && item.DiasSemana.Contains(((int)DayOfWeek.Monday).ToString()))
                        || (StartDate.DayOfWeek == DayOfWeek.Tuesday && item.DiasSemana.Contains(((int)DayOfWeek.Tuesday).ToString()))
                        || (StartDate.DayOfWeek == DayOfWeek.Wednesday && item.DiasSemana.Contains(((int)DayOfWeek.Wednesday).ToString()))
                        || (StartDate.DayOfWeek == DayOfWeek.Thursday && item.DiasSemana.Contains(((int)DayOfWeek.Thursday).ToString()))
                        || (StartDate.DayOfWeek == DayOfWeek.Friday && item.DiasSemana.Contains(((int)DayOfWeek.Friday).ToString()))
                        || (StartDate.DayOfWeek == DayOfWeek.Saturday && item.DiasSemana.Contains(((int)DayOfWeek.Saturday).ToString()))
                        )
                    {
                        itensCalendario.Add(new CalendarioItem
                        {
                            originalStart = StartDate.ToShortDateString(),
                            originalEnd = EndDate.ToShortDateString(),
                            carga = item.Componente.CH,
                            matriz = item.Turma.Matriz.Nome,
                            turno = item.Turma.Turno.Descr,
                            dataInicioTurma = item.Turma.DataInicio.HasValue ? item.Turma.DataInicio.Value.ToString("dd/MM/yyyy") : "",
                            dataFinalTurma = item.Turma.DataFim.HasValue ? item.Turma.DataFim.Value.ToString("dd/MM/yyyy") : "",
                            start = StartDate.ToString("yyyy-MM-dd"),
                            title = item.Docente.Nome,
                            description = "" + item.Componente.Nome + " " + item.HoraIni.Value.ToString("hh:mm") + " - " + item.HoraFim.Value.ToString("hh:mm"),
                            color = "#1abc9c",
                            textColor = "black"
                        });
                    }

                    StartDate = StartDate.AddDays(Interval);
                }
            }


            return Json(itensCalendario, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Feriados()
        {
            SPEBL BL = new SPEBL();

            CalendarioVM vm = new CalendarioVM();

            vm.Calendarios = BL.CalendarioCivil.Get().ToList();
            vm.Legendas = BL.CalendarioLegenda.Get().ToList();

            return View(vm);
        }

        [HttpPost]
        public ActionResult CadastrarFeriado(CalendarioCivil model)
        {

            CultureInfo brasil = new CultureInfo("pt-BR");

            var dataStr = Request.Form["DataInicial"];
            model.DataInicial = DateTime.Parse(dataStr, brasil);
            if (Request.Form["DataFim"] != null && Request.Form["DataFim"] != "")
            {
                var dataStr2 = Request.Form["DataFim"];
                model.DataFim = DateTime.Parse(dataStr2, brasil);
            }


            SPEBL BL = new SPEBL();
            if (model.DataInicial == model.DataFim)
            {
                model.DataFim = null;
            }
            BL.CalendarioCivil.Add(model);
            return null;
        }

        [HttpPost]
        public ActionResult EditarFeriado(CalendarioCivil model)
        {
            CultureInfo brasil = new CultureInfo("pt-BR");

            var dataStr = Request.Form["DataInicial"];
            model.DataInicial = DateTime.Parse(dataStr, brasil);
            if (Request.Form["DataFim"] != null && Request.Form["DataFim"] != "")
            {
                var dataStr2 = Request.Form["DataFim"];
                model.DataFim = DateTime.Parse(dataStr2, brasil);
            }


            SPEBL BL = new SPEBL();
            if (model.DataInicial == model.DataFim)
            {
                model.DataFim = null;
            }
            BL.CalendarioCivil.Update(model);
            return null;
        }

        public ActionResult DeletarFeriado(int id)
        {
            SPEBL BL = new SPEBL();
            BL.CalendarioCivil.Delete(id);
            return RedirectToAction("Feriados");
        }

        [HttpPost]
        public ActionResult Feriados(CalendarioVM vm)
        {


            return View(vm);
        }

        public ActionResult GetFeriados(int ano = -1)
        {
            if (ano == -1) { ano = DateTime.Now.Year; }

            DateTime startDate = new DateTime(ano, 1, 1);
            DateTime endDate = startDate.AddYears(1);

            SPEBL BL = new SPEBL();

            List<CalendarioCivil> datas = BL.CalendarioCivil.Get(a => a.DataInicial.Year == ano || (a.DataFim.HasValue && a.DataFim.Value.Year == ano), null, "CalendarioLegenda").ToList();
            List<CalendarioItem> itensCalendario = new List<CalendarioItem>();

            foreach (var item in datas)
            {
                DateTime StartDate = item.DataInicial;
                DateTime EndDate = item.DataFim.HasValue ? item.DataFim.Value : item.DataInicial;

                DateTime originalStart = item.DataInicial;
                DateTime? originalend = item.DataFim;

                int Interval = 1;

                StartDate = StartDate.AddDays(-1);
                while (StartDate.AddDays(Interval) <= EndDate)
                {
                    var feriado = new CalendarioItem
                        {
                            id = item.IdCalendarioCivil,
                            idLegenda = item.IdLegenda,
                            start = StartDate.AddDays(1).ToString("yyyy-MM-dd"),
                            originalStart = item.DataInicial.ToString("yyyy-MM-dd"),
                            originalEnd = item.DataFim.HasValue ? item.DataFim.Value.ToString("yyyy-MM-dd") : "",
                            description = item.Descricao,
                            color = "#" + item.CalendarioLegenda.Cor,
                        };

                    //var existeFeriado = itensCalendario.Any(a => a.originalStart == feriado.originalStart && a.originalEnd == feriado.originalEnd);

                    //if (!existeFeriado)
                    //{
                    itensCalendario.Add(feriado);
                    //}

                    StartDate = StartDate.AddDays(Interval);
                }

            }

            return Json(itensCalendario, JsonRequestBehavior.AllowGet);
        }



        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}