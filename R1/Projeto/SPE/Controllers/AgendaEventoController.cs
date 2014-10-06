using System.Diagnostics;
using Common;
using Common.Extensions.EnumEx;
using Senai.SPE.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Senai.SPE.Domain.Enum;
using SPE.ViewModels;
using BusinessLogic;
using System.Globalization;
using SPE.ViewModel;
using SPE.ViewModels.Calendario;
using Senai.SPE.BL;

namespace SPE.Controllers
{
    public class AgendaEventoController : BaseController
    {       
        public ActionResult Index()
        {
            return RedirectToAction("Eventos", "AgendaEvento");
        }

        public ActionResult AgendaDocente(int docenteId = 0, string start = "", string end = "")
        {
            DateTime startDate = UnixTimeStampToDateTime(double.Parse(start));
            DateTime endDate = UnixTimeStampToDateTime(double.Parse(end));
            endDate = endDate.AddHours(-endDate.Hour);
            startDate = startDate.AddHours(-startDate.Hour);

            List<AgendaEvento> eventos = BL.AgendaEvento.Get(a => a.IdDocente == docenteId
                    && ((a.DataIni >= startDate && a.DataIni <= endDate)
                    || (a.DataFim >= startDate && a.DataFim <= endDate)), null, "Docente").ToList();

            var itensCalendario = new List<CalendarioItem>();

            foreach (var item in eventos)
            {
                DateTime StartDate = item.DataIni.Value;
                DateTime EndDate = item.DataFim.Value;
                int Interval = 1;

                while (StartDate.AddDays(Interval) <= EndDate)
                {
                    if (
                        (StartDate.DayOfWeek == DayOfWeek.Sunday && item.DiaSemana.Contains(((int)DayOfWeek.Sunday).ToString()))
                        || (StartDate.DayOfWeek == DayOfWeek.Monday && item.DiaSemana.Contains(((int)DayOfWeek.Monday).ToString()))
                        || (StartDate.DayOfWeek == DayOfWeek.Tuesday && item.DiaSemana.Contains(((int)DayOfWeek.Tuesday).ToString()))
                        || (StartDate.DayOfWeek == DayOfWeek.Wednesday && item.DiaSemana.Contains(((int)DayOfWeek.Wednesday).ToString()))
                        || (StartDate.DayOfWeek == DayOfWeek.Thursday && item.DiaSemana.Contains(((int)DayOfWeek.Thursday).ToString()))
                        || (StartDate.DayOfWeek == DayOfWeek.Friday && item.DiaSemana.Contains(((int)DayOfWeek.Friday).ToString()))
                        || (StartDate.DayOfWeek == DayOfWeek.Saturday && item.DiaSemana.Contains(((int)DayOfWeek.Saturday).ToString()))
                        )
                    {
                        itensCalendario.Add(new CalendarioItem
                        {
                            start = StartDate.ToString("yyyy-MM-dd"),
                            title = item.Descricao,

                            description = "" + item.Docente.Nome + " " + item.HoraIni.Value.ToString("hh:mm") + " - " + item.HoraFim.Value.ToString("hh:mm") + " ",
                            color = "#e4bc43",
                            textColor = "black"

                        });
                    }

                    StartDate = StartDate.AddDays(Interval);
                }
            }

            return Json(itensCalendario, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Eventos()
        {
            var agendaEventoVm = new AgendaEventoViewModel();
            agendaEventoVm.Docentes = BL.Docente.Get().ToList();
            agendaEventoVm.IdDocente = LoggedUser().Id.ToString();
            TipoAtividadeBL tipoAtividadeBL = new TipoAtividadeBL();

            //TODO: verificar se3 o perfil é o administrador LoggedUser().PerfilId != BL.Perfil.Get(a=>a.Nome == "Administrador");
            if (true)
            {
                agendaEventoVm.IdDocente = 0.ToString();
            }

            
            agendaEventoVm.TipoEventos = tipoAtividadeBL.Get().ToList();
            return View(agendaEventoVm);
        }

        [HttpPost]
        public ActionResult CadastrarEvento(AgendaEvento model)
        {

            CultureInfo brasil = new CultureInfo("pt-BR");
                       
            var dataStr = Request.Form["dataini"];
            model.DataIni = DateTime.Parse(dataStr, brasil);
            if (Request.Form["datafim"] != null && Request.Form["datafim"] != "")
            {
                var dataStr2 = Request.Form["datafim"];
                model.DataFim = DateTime.Parse(dataStr2, brasil);
            }            

            if (model.DataIni == model.DataFim)
            {
                model.DataFim = null;
            }

            var evDateIni = BL.AgendaEvento.Get(c => c.IdDocente == model.IdDocente && c.DataIni == model.DataIni);
            var evDateFim = BL.AgendaEvento.Get(c => c.IdDocente == model.IdDocente && c.DataFim == model.DataFim);

            var getDateIni = evDateIni.ToList().Select(c => c.DataIni).FirstOrDefault();
            var getDateFim = evDateFim.ToList().Select(c => c.DataFim).FirstOrDefault();
            var isConflict = false;

            if (getDateIni.HasValue || getDateFim.HasValue)
            {
                getDateIni = getDateIni.HasValue ? getDateIni : model.DataIni;
                getDateFim = getDateFim.HasValue ? getDateFim : model.DataFim;
                isConflict = TimePeriodOverlap(model.DataIni.Value, model.DataFim.Value, getDateIni.Value, getDateFim.Value);
                if (isConflict)
                {
                    var message = "Não foi possível cadastrar evento, pois há colisão de datas.";
                    return Json(message, JsonRequestBehavior.AllowGet);
                }
            }
            //model.TipoEvento = byte.Parse(Request.QueryString["TipoEvento"]);
            BL.AgendaEvento.Add(model);

            return null;
        }

        [HttpPost]
        public ActionResult EditarEvento(AgendaEvento model)
        {

           
            CultureInfo brasil = new CultureInfo("pt-BR");
            //[0]	"IdAgendaEvento"	
            //[1]	"idDocenteEdit"	
            //[2]	"Descricao"	
            //[3]	"DataIni"	
            //[4]	"TipoEventoEdit"
            //[5]	"DataFim"	

                var dataStr = Request.Form["dataini"];
                model.DataIni = DateTime.Parse(dataStr, brasil);
                if (Request.Form["datafim"] != null && Request.Form["datafim"] != "")
                {
                    var dataStr2 = Request.Form["datafim"];
                    model.DataFim = DateTime.Parse(dataStr2, brasil);
                }
           

            model.IdDocente = int.Parse(Request.Form["IdDocente"]);
            model.TipoEvento = byte.Parse(Request.Form["TipoEvento"]);


            if (model.DataIni == model.DataFim)
            {
                model.DataFim = null;
            }

            BL.AgendaEvento.Update(model);
            return null;
        }

        public ActionResult DeletarEvento(int id)
        {
            BL.AgendaEvento.Delete(id);
            return RedirectToAction("Eventos");
        }

        public ActionResult GetEventos(int idDocente, int ano = -1)
        {
            if (ano == -1) { ano = DateTime.Now.Year; }

            DateTime startDate = new DateTime(ano, 1, 1);
            DateTime endDate = startDate.AddYears(1);

            var baseColor = "";

            List<AgendaEvento> datas = BL.AgendaEvento.Get(a => a.DataIni.Value.Year == ano && a.IdDocente == idDocente || (a.DataFim.HasValue && a.DataFim.Value.Year == ano && a.IdDocente == idDocente)).ToList();
            List<CalendarioItem> itensCalendario = new List<CalendarioItem>();

            foreach (var item in datas)
            {
                DateTime StartDate = item.DataIni.Value;
                DateTime EndDate = item.DataFim.HasValue ? item.DataFim.Value : item.DataIni.Value;

                DateTime originalStart = item.DataIni.Value;
                DateTime? originalend = item.DataFim;

                baseColor = GetColorByType((TipoEventoEnum)item.TipoEvento.GetValueOrDefault());
                int Interval = 1;

                StartDate = StartDate.AddDays(-1);
                while (StartDate.AddDays(Interval) <= EndDate)
                {
                    StartDate = StartDate.AddDays(Interval);

                    itensCalendario.Add(new CalendarioItem
                        {
                            id = item.IdAgendaEvento,
                            idLegenda = (int)item.TipoEvento.GetValueOrDefault(),
                            start = StartDate.ToString("yyyy-MM-dd"),
                            originalStart = item.DataIni.Value.ToString("yyyy-MM-dd"),
                            originalEnd = item.DataFim.HasValue ? item.DataFim.Value.ToString("yyyy-MM-dd") : "",
                            description = item.Descricao,
                            color = "#BEBEBE",

                        });
                   
                }
            }

            //itensCalendario.AddRange(ItemsCalendarioDocente(idDocente));
            itensCalendario.AddRange(ItemsCalendarioCivil());

            return Json(itensCalendario, JsonRequestBehavior.AllowGet);
        }

        public static string GetColorByType(TipoEventoEnum tipo)
        {
            var baseColor = "";

            switch (tipo)
            {
                case TipoEventoEnum.Ferias:
                    baseColor = "00FF00";
                    break;
                case TipoEventoEnum.Licenca:
                    baseColor = "40E0D0";
                    break;
                case TipoEventoEnum.Evento:
                    baseColor = "FF8C00";
                    break;
                case TipoEventoEnum.Outros:
                    baseColor = "BEBEBE";
                    break;
            }
            return baseColor;
        }


        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        private List<CalendarioItem> ItemsCalendarioDocente(int idDocente)
        {
            List<CalendarioItem> itensCalendario = new List<CalendarioItem>();

            var calendarioDocente = BL.AgendaEvento.Get(d => d.IdDocente == idDocente);

            foreach (var item in calendarioDocente)
            {
                //Se não tem dataFim, atribu como sendo a data inicio
                item.DataFim = item.DataFim.GetValueOrDefault(item.DataIni.GetValueOrDefault());
                item.DataIni = item.DataIni.GetValueOrDefault();

                //Calculando quantos dias de diferença existe entre as duas datas
                var interval = (item.DataFim.Value - item.DataIni.Value).Days + 1;

                for (int i = 0; i < interval; i++)
                {
                    TipoEventoEnum tipoEvento = (TipoEventoEnum)item.TipoEvento.GetValueOrDefault();
                    var color = AgendaEventoController.GetColorByType(tipoEvento);

                    itensCalendario.Add(new CalendarioItem
                    {
                        tipo = TiposItemsAgendamento.AgendaDocente,
                        id = item.IdAgendaEvento,
                        start = item.DataIni.Value.AddDays(i).ToString("yyyy-MM-dd"),
                        originalStart = item.DataIni.Value.ToString("yyyy-MM-dd"),
                        originalEnd = item.DataFim.Value.ToString("yyyy-MM-dd"),
                        title = tipoEvento.ToDescription(),
                        description = item.Descricao,
                        //horaInicio = item1.HoraInicio.Value.ToString("HH:mm"),
                        //horaFim = item1.HoraFim.Value.ToString("HH:mm"),
                        //docente = item1.Docente.Nome,
                        //turma = item1.Turma.Matriz.Nome.ToString(),
                        //componente = item1.Componente.Nome,
                        //ambiente = item1.Ambiente.Nome,

                        color = "#" + color,
                    });
                }
            }

            return itensCalendario;
        }

        private List<CalendarioItem> ItemsCalendarioCivil()
        {
            List<CalendarioItem> itensCalendario = new List<CalendarioItem>();

            var calendarioCivil = BL.CalendarioCivil.Get(null, null, "CalendarioLegenda");

            foreach (var item in calendarioCivil)
            {
                //Se não tem dataFim, atribu como sendo a data inicio
                item.DataFim = item.DataFim.GetValueOrDefault(item.DataInicial);

                //Calculando quantos dias de diferença existe entre as duas datas
                var interval = (item.DataFim.Value - item.DataInicial).Days + 1;

                for (int i = 0; i < interval; i++)
                {
                    itensCalendario.Add(new CalendarioItem
                    {
                        tipo = TiposItemsAgendamento.CalendarioCivil,
                        id = item.IdCalendarioCivil,

                        start = item.DataInicial.AddDays(i).ToString("yyyy-MM-dd"),
                        originalStart = item.DataInicial.ToString("yyyy-MM-dd"),
                        originalEnd = item.DataFim.Value.ToString("yyyy-MM-dd"),
                        title = item.CalendarioLegenda.Descricao,
                        description = item.Descricao,
                        //horaInicio = item1.HoraInicio.Value.ToString("HH:mm"),
                        //horaFim = item1.HoraFim.Value.ToString("HH:mm"),
                        //docente = item1.Docente.Nome,
                        //turma = item1.Turma.Matriz.Nome.ToString(),
                        //componente = item1.Componente.Nome,
                        //ambiente = item1.Ambiente.Nome,

                        color = "#" + item.CalendarioLegenda.Cor,
                    });
                }
            }

            return itensCalendario;
        }
    }
}
