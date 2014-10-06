using BusinessLogic;
using Common;
using Senai.SPE.Domain;
using Senai.SPE.Domain.Enum;
using SPE.ViewModel;
using SPE.ViewModels;
using SPE.ViewModels.Calendario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Extensions.EnumEx;

namespace SPE.Controllers
{
    public class AgendamentoController : BaseController
    {

        public ActionResult Index()
        {
            return View();

        }
        [HttpPost]
        public JsonResult GetListItem(int idNumero)
        {
            object lista = null;
            switch (idNumero)
            {
                case 1:
                    lista = BL.Ambiente.Get().OrderBy(a=>a.Nome).ToList();
                    break;
                case 2:
                    lista = BL.Componente.Get().OrderBy(a => a.Nome).ToList();
                    break;
                case 3:
                    lista = BL.Docente.Get().OrderBy(a => a.Nome).ToList();
                    break;
                case 4:
                    lista = BL.Turma.Get(a => true, null, "Matriz").Select(a => new { IdTurmaZero = a.IdTurma.ToString("00000"), NomeMatriz = a.Matriz.Nome, a.IdTurma }).OrderBy(a=>a.IdTurmaZero).ToList();

                    //foreach (Turma turma in lista as List<Turma>)
                    //{
                    //    turma.Matriz.Turma = null;
                    //}

                    break;


            }
            return Json(lista);
        }

        [HttpPost]
        public JsonResult GetListVersao(int id, int tipo)
        {
            List<string> lista = new List<string>();

            switch (tipo)
            {
                case 1:
                    var agendamento = BL.Agendamento.Get(a => a.IdAmbiente == id).GroupBy(a => a.Versao).ToList();

                    foreach (var item in agendamento)
                    {
                        lista.Add(item.Key.ToString());
                    }

                    break;
                case 2:
                    var agendamento1 = BL.Agendamento.Get(a => a.IdComponente == id).GroupBy(a => a.Versao).ToList();

                    foreach (var item in agendamento1)
                    {
                        lista.Add(item.Key.ToString());
                    }
                    break;
                case 3:
                    var agendamento2 = BL.Agendamento.Get(a => a.IdDocente == id).GroupBy(a => a.Versao).ToList();

                    foreach (var item in agendamento2)
                    {
                        lista.Add(item.Key.ToString());
                    }
                    break;
                case 4:
                    var agendamento3 = BL.Agendamento.Get(a => a.IdTurma == id).GroupBy(a => a.Versao).ToList();

                    foreach (var item in agendamento3)
                    {
                        lista.Add(item.Key.ToString());
                    }
                    break;


            }

            return Json(lista);
        }

        public ActionResult FiltrarAmbientes(FiltroAgendamento filtro = null)
        {
            filtro = filtro == null ? new FiltroAgendamento() : filtro;

            var TipoAmbientes = BL.Componente.Get(a => a.IdComponente == filtro.IdComponente
                    && (filtro.LocalId.HasValue ? a.TipoAmbiente.Where(c => c.Ambiente.Where(b => b.IdLoc == filtro.LocalId.Value).Count() > 0).Count() > 0 : true)
                    && (filtro.TipoId.HasValue ? a.TipoAmbiente.Where(b => b.IdTipoAmbiente == filtro.TipoId.Value).Count() > 0 : true)
                , null, "TipoAmbiente,TipoAmbiente.Ambiente,TipoAmbiente.Ambiente.localambiente").FirstOrDefault();

            //var TipoAmbientes = this.speDominioService.GetFilteredComponente(
            //    a => a.IdComponente == filtro.IdComponente
            //        && (filtro.LocalId.HasValue ? a.TipoAmbiente.Where(c => c.Ambiente.Where(b => b.IdLoc == filtro.LocalId.Value).Count() > 0).Count() > 0 : true)
            //        && (filtro.TipoId.HasValue ? a.TipoAmbiente.Where(b => b.IdTipoAmbiente == filtro.TipoId.Value).Count() > 0 : true)
            //    , null, "TipoAmbiente,TipoAmbiente.Ambiente,TipoAmbiente.Ambiente.localambiente").FirstOrDefault();

            List<TipoAmbiente> tipos = new List<TipoAmbiente>();

            if (TipoAmbientes != null)
            {
                foreach (var item in TipoAmbientes.TipoAmbiente)
                {
                    tipos.Add(item);
                }
            }

            return View("_FiltroAmbiente", tipos);
        }

        public ActionResult Componente(int IdModulo, int IdComponente, int IdTurma)
        {
            AgendaComponenteViewModel agendaComponente = new AgendaComponenteViewModel();

            try
            {
                List<string> listaDias = new List<string>();
                //agendaComponente = AgendaComponenteViewModel.MapToViewModel(this.speDominioService.getComponentesAgendados(IdModulo, IdComponente, IdTurma).FirstOrDefault());
                agendaComponente = AgendaComponenteViewModel.MapToViewModel(BL.AgendaComponente.GetComponenteAgendados(IdModulo, IdComponente, IdTurma).FirstOrDefault());

                var lista = Enum.GetNames(typeof(DiasSemanaEnum)).ToList();
                agendaComponente.ListaDias = lista;
                agendaComponente.IdTurma = IdTurma;
                if (agendaComponente.Turma == null)
                {
                    //agendaComponente.Turma = this.speDominioService.GetFilteredTurma(a => a.IdTurma == IdTurma, null, "Turno").FirstOrDefault();
                    agendaComponente.Turma = BL.Turma.Get(a => a.IdTurma == IdTurma, null, "Turno").FirstOrDefault();
                }

                //agendaComponente.Componente = this.speDominioService.GetFilteredComponente(a => a.IdComponente == IdComponente, null, "TipoAmbiente,TipoAmbiente.Ambiente,TipoAmbiente.Ambiente.localambiente").FirstOrDefault();
                agendaComponente.Componente = BL.Componente.Get(a => a.IdComponente == IdComponente, null, "TipoAmbiente,TipoAmbiente.Ambiente,TipoAmbiente.Ambiente.localambiente").FirstOrDefault();

                //agendaComponente.Docentes = this.speDominioService.GetDocenteAll().ToList();
                agendaComponente.Docentes = BL.Docente.Get().ToList();

                agendaComponente.ListaTipoAmbiente = new List<TipoAmbienteViewModel>();

                //agendaComponente.ComponentesAgendados = AgendaComponenteViewModel.MapToViewModel(this.speDominioService.getComponentesAgendados(IdModulo, IdComponente, IdTurma).FirstOrDefault());
                agendaComponente.ComponentesAgendados = AgendaComponenteViewModel.MapToViewModel(BL.AgendaComponente.GetComponenteAgendados(IdModulo, IdComponente, IdTurma).FirstOrDefault());

                //agendaComponente.AmbienteAgendados = AgendaAmbienteViewModel.MapToListViewModel(this.speDominioService.getAmbienteAgendados(IdModulo, IdComponente, IdTurma));
                agendaComponente.AmbienteAgendados = AgendaAmbienteViewModel.MapToListViewModel(BL.AgendaAmbiente.GetAmbientesAgendados(IdModulo, IdComponente, IdTurma));

                //agendaComponente.DocentesAgendados = AgendaDocenteViewModel.MapToListViewModel(this.speDominioService.getDocenteAgendados(IdModulo, IdComponente, IdTurma));
                agendaComponente.DocentesAgendados = AgendaDocenteViewModel.MapToListViewModel(BL.AgendaDocente.GetDocentesAgendados(IdModulo, IdComponente, IdTurma));

                agendaComponente.HoraIniView = agendaComponente.Turma.Turno.HoraIni.Value.ToString().Substring(0, 5);
                agendaComponente.HoraFimView = agendaComponente.Turma.Turno.HoraFim.Value.ToString().Substring(0, 5);

                //agendaComponente.Locais = LocalAmbienteViewModel.MapToListViewModel(this.speDominioService.GetLocalAmbienteAll().ToList());
                agendaComponente.Locais = LocalAmbienteViewModel.MapToListViewModel(BL.LocalAmbiente.Get().ToList());

                //agendaComponente.TiposAmbientes = TipoAmbienteViewModel.MapToListViewModel(this.speDominioService.GetTipoAmbienteAll().ToList());
                agendaComponente.TiposAmbientes = TipoAmbienteViewModel.MapToListViewModel(BL.TipoAmbiente.Get().ToList());

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }
            agendaComponente.IdModulo = IdModulo;
            return View(agendaComponente);
        }
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public ActionResult BuscaAgenda(int tipo, int item, int versao, string start = "", string end = "")
        {
            DateTime startDate = UnixTimeStampToDateTime(double.Parse(start));
            DateTime endDate = UnixTimeStampToDateTime(double.Parse(end));
            endDate = endDate.AddHours(-endDate.Hour);
            startDate = startDate.AddHours(-startDate.Hour);
            //SPEBL BL = new SPEBL();
            List<Agendamento> eventos = new List<Agendamento>();
            switch (tipo)
            {
                case 1:
                    eventos = BL.Agendamento.Get(a => a.IdTurma == item
              && ((a.HoraInicio >= startDate && a.HoraInicio <= endDate)
              || (a.HoraFim >= startDate && a.HoraFim <= endDate)), null, "Matriz,Docente,Componente").ToList();

                    break;
                case 2:
                    eventos = BL.Agendamento.Get(a => a.IdTurma == item
              && ((a.HoraInicio >= startDate && a.HoraInicio <= endDate)
              || (a.HoraFim >= startDate && a.HoraFim <= endDate)), null, "Matriz,Docente,Componente").ToList();

                    break;
                case 3:
                    eventos = BL.Agendamento.Get(a => a.IdTurma == item
               && ((a.HoraInicio >= startDate && a.HoraInicio <= endDate)
               || (a.HoraFim >= startDate && a.HoraFim <= endDate)), null, "Matriz,Docente,Componente").ToList();

                    break;
                case 4:
                    eventos = BL.Agendamento.Get(a => a.IdTurma == item
              && ((a.HoraInicio >= startDate && a.HoraInicio <= endDate)
              || (a.HoraFim >= startDate && a.HoraFim <= endDate)), null, "Matriz,Docente,Componente").ToList();

                    break;
            }

            var itensCalendario = new List<CalendarioItem>();

            foreach (var caaa in eventos)
            {

                DateTime StartDate = caaa.HoraInicio.Value;
                DateTime EndDate = caaa.HoraFim.Value;
                int Interval = 1;

                while (StartDate.AddDays(Interval) <= EndDate)
                {
                    if (
                        (StartDate.DayOfWeek == DayOfWeek.Sunday && DayOfWeek.Sunday == caaa.HoraInicio.Value.DayOfWeek)
                        || (StartDate.DayOfWeek == DayOfWeek.Monday && DayOfWeek.Monday == caaa.HoraInicio.Value.DayOfWeek)
                        || (StartDate.DayOfWeek == DayOfWeek.Tuesday && DayOfWeek.Tuesday == caaa.HoraInicio.Value.DayOfWeek)
                        || (StartDate.DayOfWeek == DayOfWeek.Wednesday && DayOfWeek.Wednesday == caaa.HoraInicio.Value.DayOfWeek)
                        || (StartDate.DayOfWeek == DayOfWeek.Thursday && DayOfWeek.Thursday == caaa.HoraInicio.Value.DayOfWeek)
                        || (StartDate.DayOfWeek == DayOfWeek.Friday && DayOfWeek.Friday == caaa.HoraInicio.Value.DayOfWeek)
                        || (StartDate.DayOfWeek == DayOfWeek.Saturday && DayOfWeek.Saturday == caaa.HoraInicio.Value.DayOfWeek)
                        )
                    {
                        itensCalendario.Add(new CalendarioItem
                        {
                            start = StartDate.ToString("yyyy-MM-dd"),


                            color = "#e4bc43",
                            textColor = "black"

                        });
                    }

                    StartDate = StartDate.AddDays(Interval);
                }
            }

            return Json(itensCalendario, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetEventos(int tipo, int item, int versao, int ano = 0)
        {
            if (ano == 0)
            {
                ano = DateTime.Now.Year;
            }

            DateTime startDate = new DateTime(ano, 1, 1);
            DateTime endDate = startDate.AddYears(1);


            var baseColor = "";
            SPEBL BL = new SPEBL();
            List<Agendamento> eventos = new List<Agendamento>();
            switch (tipo)
            {
                case 1:
                    eventos = BL.Agendamento.Get(a => a.IdAmbiente == item && a.Versao == versao
              && ((a.HoraInicio >= startDate && a.HoraInicio <= endDate)
              || (a.HoraFim >= startDate && a.HoraFim <= endDate)), null, "turma.matriz,Docente,Componente,Ambiente").ToList();

                    break;
                case 2:
                    eventos = BL.Agendamento.Get(a => a.IdComponente == item && a.Versao == versao
              && ((a.HoraInicio >= startDate && a.HoraInicio <= endDate)
              || (a.HoraFim >= startDate && a.HoraFim <= endDate)), null, "turma.matriz,Docente,Componente,Ambiente").ToList();

                    break;
                case 3:
                    eventos = BL.Agendamento.Get(a => a.IdDocente == item && a.Versao == versao
               && ((a.HoraInicio >= startDate && a.HoraInicio <= endDate)
               || (a.HoraFim >= startDate && a.HoraFim <= endDate)), null, "turma.matriz,Docente,Componente,Ambiente").ToList();

                    break;
                case 4:
                    eventos = BL.Agendamento.Get(a => a.IdTurma == item && a.Versao == versao
              && ((a.HoraInicio >= startDate && a.HoraInicio <= endDate)
              || (a.HoraFim >= startDate && a.HoraFim <= endDate)), null, "turma.matriz,Docente,Componente,Ambiente").ToList();

                    break;
            }
            List<CalendarioItemAgendamento> itensCalendario = new List<CalendarioItemAgendamento>();

            foreach (var item1 in eventos)
            {
                DateTime StartDate = item1.HoraInicio.Value;
                DateTime EndDate = item1.HoraFim.HasValue ? item1.HoraFim.Value : item1.HoraFim.Value;

                DateTime originalStart = item1.HoraInicio.Value;
                DateTime? originalend = item1.HoraFim;

                switch (tipo)
                {
                    case 0:
                        baseColor = "00FF00";
                        break;
                    case 1:
                        baseColor = "40E0D0";
                        break;
                    case 2:
                        baseColor = "FF8C00";
                        break;
                    case 3:
                        baseColor = "BEBEBE";
                        break;
                    case 4:
                        baseColor = "CCCC22";
                        break;
                }
                int Interval = 1;

                StartDate = StartDate.AddDays(-1);
                while (StartDate.AddDays(Interval) <= EndDate)
                {
                    itensCalendario.Add(new CalendarioItemAgendamento
                    {
                        id = item1.IdAgendamento,
                        //idLegenda = item.IdDocente,
                        start = StartDate.AddDays(1).ToString("yyyy-MM-dd"),
                        originalStart = item1.HoraInicio.Value.ToString("yyyy-MM-dd"),
                        originalEnd = item1.HoraFim.HasValue ? item1.HoraFim.Value.ToString("yyyy-MM-dd") : "",
                        horaInicio = item1.HoraInicio.Value.ToString("HH:mm"),
                        horaFim = item1.HoraFim.Value.ToString("HH:mm"),
                        docente = item1.Docente.Nome,
                        turma = item1.Turma.IdTurma.ToString(),
                        componente = item1.Componente.Nome,
                        ambiente = item1.Ambiente.Nome,
                        color = "#" + baseColor,
                    });
                    StartDate = StartDate.AddDays(Interval);
                }
            }

            itensCalendario.AddRange(ItemsCalendarioCivil());

            if (tipo == 3)
            {
                itensCalendario.AddRange(ItemsCalendarioDocente(item));
            }

            //Lógica para marcar os itens com conflito de data
            var listRetorno = LogicaConflitos(itensCalendario);

            return Json(listRetorno, JsonRequestBehavior.AllowGet);
        }

        public List<CalendarioItemAgendamento> LogicaConflitos(List<CalendarioItemAgendamento> itensCalendario)
        {
            var lstRetorno = new List<CalendarioItemAgendamento>();

            //Lógica para marcar os itens com conflito de data
            var grupos = itensCalendario.GroupBy(g => g.start);

            foreach (var grupo in grupos)
            {
                var itemCalendario = grupo.FirstOrDefault();

                //Se tem mais de um elemento no grupo por DIA
                if (grupo.Count() > 1)
                {
                    //Agrupo os elementos por TIPO
                    var subGrupo = grupo.GroupBy(g => g.tipo);

                    //Se possui itens no subGrupo de tipos diferentes
                    if (subGrupo.Count() > 1)
                    {
                        //É conflito
                        itemCalendario.conflito = true;
                       
                    }
                    else
                    {
                        //Se for do tipo aula(agendamento simples == NULL)
                        if (subGrupo.FirstOrDefault().Key == null)
                        {
                            var itemsSubGrupo = subGrupo.FirstOrDefault().ToList();

                            //Deve verificar os horários
                            for (int i = 0; i < itemsSubGrupo.Count; i++)
                            {
                                var atual = itemsSubGrupo[i];

                                var dtInicioAtual = DateTime.Parse(atual.originalStart + " " + atual.horaInicio);
                                var dtFinalAtual = DateTime.Parse(atual.originalEnd + " " + atual.horaFim);
                               
                                for (int j = 0; j < itemsSubGrupo.Count; j++)
                                {
                                    if (i != j)
                                    {
                                        var outro = itemsSubGrupo[j];

                                        var dtInicioOutro = DateTime.Parse(outro.originalStart + " " + outro.horaInicio);
                                        var dtFinalOutro = DateTime.Parse(outro.originalEnd + " " + outro.horaFim);

                                        var temChoque = this.TimePeriodOverlap(dtInicioAtual, dtFinalAtual, dtInicioOutro, dtFinalOutro);
                                        if (temChoque)
                                        {
                                            itemCalendario.conflito = true;

                                            //Se teve algum conflito pode parar de verificar
                                            break; 
                                        }
                                    }
                                }

                                //Se teve algum conflito pode parar de verificar
                                if (atual.conflito)
                                {
                                    break;
                                }
                            }
                        }
                    }                   
                }
                lstRetorno.Add(itemCalendario);
            }

            return lstRetorno;
        }

        private List<CalendarioItemAgendamento> ItemsCalendarioDocente(int idDocente)
        {
            List<CalendarioItemAgendamento> itensCalendario = new List<CalendarioItemAgendamento>();

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

                    itensCalendario.Add(new CalendarioItemAgendamento
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

        private List<CalendarioItemAgendamento> ItemsCalendarioCivil()
        {
            List<CalendarioItemAgendamento> itensCalendario = new List<CalendarioItemAgendamento>();

            var calendarioCivil = BL.CalendarioCivil.Get(null, null, "CalendarioLegenda");

            foreach (var item in calendarioCivil)
            {
                //Se não tem dataFim, atribu como sendo a data inicio
                item.DataFim = item.DataFim.GetValueOrDefault(item.DataInicial);

                //Calculando quantos dias de diferença existe entre as duas datas
                var interval = (item.DataFim.Value - item.DataInicial).Days + 1;

                for (int i = 0; i < interval; i++)
                {
                    itensCalendario.Add(new CalendarioItemAgendamento
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

        [HttpPost]
        public JsonResult AgendamentoCompleto(AgendamentoCompletoViewModel agenda)
        {
            //var AmbienteAgendados = AgendaAmbienteViewModel.MapToListViewModel(this.speDominioService.getAmbienteAgendados(int.Parse(agenda.IdModulo), int.Parse(agenda.IdComponente), int.Parse(agenda.IdTurma)));
            var AmbienteAgendados = AgendaAmbienteViewModel.MapToListViewModel(BL.AgendaAmbiente.GetAmbientesAgendados(int.Parse(agenda.IdModulo), int.Parse(agenda.IdComponente), int.Parse(agenda.IdTurma)));

            //var DocentesAgendados = AgendaDocenteViewModel.MapToListViewModel(this.speDominioService.getDocenteAgendados(int.Parse(agenda.IdModulo), int.Parse(agenda.IdComponente), int.Parse(agenda.IdTurma)));
            var DocentesAgendados = AgendaDocenteViewModel.MapToListViewModel(BL.AgendaDocente.GetDocentesAgendados(int.Parse(agenda.IdModulo), int.Parse(agenda.IdComponente), int.Parse(agenda.IdTurma)));

            //var agendaComponente = AgendaComponenteViewModel.MapToListViewModel(this.speDominioService.getComponentesAgendados(int.Parse(agenda.IdModulo), int.Parse(agenda.IdComponente), int.Parse(agenda.IdTurma)));
            var agendaComponente = AgendaComponenteViewModel.MapToListViewModel(BL.AgendaComponente.GetComponenteAgendados(int.Parse(agenda.IdModulo), int.Parse(agenda.IdComponente), int.Parse(agenda.IdTurma)));

            foreach (var Comp in agendaComponente)
            {
                //this.speDominioService.ApagarAgendaComponente(Comp.IdAgendaComponente);
                BL.AgendaComponente.Delete(Comp.IdAgendaComponente);
            }

            //this.speDominioService.AgendarComponente(AgendamentoCompletoViewModel.MapToModelAgendaComponente(agenda));
            BL.AgendaComponente.AgendarComponente(AgendamentoCompletoViewModel.MapToModelAgendaComponente(agenda));

            foreach (var Ambiente in AmbienteAgendados.ToList())
            {
                //this.speDominioService.ApagarAgendaAmbiente(Ambiente.IdAgendaAmbiente);
                BL.AgendaAmbiente.Delete(Ambiente.IdAgendaAmbiente);
            }
            foreach (var Docente in DocentesAgendados.ToList())
            {
                //this.speDominioService.ApagarAgendaDocente(Docente.IdAgendaDocente);
                BL.AgendaDocente.Delete(Docente.IdAgendaDocente);
            }

            if (agenda.AgendaAmbientes != null)
            {
                foreach (var item in agenda.AgendaAmbientes)
                {
                    //this.speDominioService.AgendarAmbiente(AgendaAmbienteViewModel.MapToModel(item));
                    BL.AgendaAmbiente.AgendarAmbiente(AgendaAmbienteViewModel.MapToModel(item));
                }
            }
            if (agenda.AgendaDocentes != null)
            {
                foreach (var item in agenda.AgendaDocentes)
                {
                    //this.speDominioService.AgendarDocente(AgendaDocenteViewModel.MapToModel(item));
                    BL.AgendaDocente.AgendarDocente(AgendaDocenteViewModel.MapToModel(item));
                }
            }


            //this.speDominioService.AtualizarTurmaPorAgendamento(agenda.IdTurma);
            BL.Turma.AtualizarTurmaPorAgendamento(agenda.IdTurma);


            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ExcluirAgendamento(int id)
        {
            try
            {
                BL.Agendamento.Deletar(id);
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Agendamento excluído com sucesso.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao excluir Agendamento.";
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
                return Json(false);
            }

            return Json(true);

        }
    }
}
