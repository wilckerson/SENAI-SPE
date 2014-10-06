using BusinessLogic;
using Senai.SPE.Domain;
using Senai.SPE.Domain.Enum;
using SPE.ViewModels.Agendamento;
using SPE.ViewModels.Calendario;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Extensions.EnumEx;
using System.Data.Entity;
using System.Configuration;

namespace SPE.Controllers
{
    public class PlanejamentoEscolarController : BaseController
    {
        //
        // GET: /PlanejamentoEscolar/

        public ActionResult Index(int tipo, int item, int versao, int mes, string ano)
        {

            var data = new DateTime(int.Parse(ano), mes + 1, 1);

            var vm = new AgendamentoIndexVM();
            //var eventos = GetAgendamento(tipo, item, versao);
            ViewBag.Tipo = tipo;
            ViewBag.Item = item;
            ViewBag.Versao = versao;
            ViewBag.Data = (mes + 1) + "/" + "01" + "/" + ano;
            //vm.ItemTipo = tipo;
            //vm.ItemId = item;
            //vm.ItemVersao = versao;
            //vm.ItemData = (mes + 1) + "/" + "01" + "/" + ano;

            vm.listaAmbientes = new List<Ambiente>();
            vm.listaComponentes = new List<Componente>();
            vm.listaDocentes = new List<Docente>();
            vm.listaTurmas = new List<Turma>();
            vm.listaAreaAtuacao = new List<AreaAtuacao>();

            switch (tipo)
            {
                case 1:
                    var item1 = BL.Ambiente.GetById(item);
                    ViewBag.Nome = item1.Nome;

                    vm.listaAreaAtuacao.Distinct(new AreaClassFilter());
                    vm.listaDocentes = BL.Docente.Get(a => a.Componente.Where(b => b.TipoAmbiente.Where(c => c.Ambiente.Where(d => d.IdAmbiente == item1.IdAmbiente).Any()).Any()).Any()).ToList();
                    vm.listaTurmas = BL.Turma.Get(a => a.Matriz.Modulo.Where(w => w.Componente.Where(b => b.TipoAmbiente.Where(c => c.Ambiente.Where(d => d.IdAmbiente == item1.IdAmbiente).Any()).Any()).Any()).Any(), null, "Matriz").ToList();
                    vm.listaComponentes = BL.Componente.Get(a => a.TipoAmbiente.Where(b => b.Ambiente.Where(c => c.IdAmbiente == item1.IdAmbiente).Any()).Any()).ToList();


                    break;

                case 2:
                    var item2 = BL.Componente.GetById(item);
                    ViewBag.Nome = item2.Nome;

                    //vm.listaDocentes = BL.Docente.Get(null, null, "Agendamento").ToList();
                    vm.listaDocentes = BL.Docente.Get(a => a.Componente.Where(b => b.IdComponente == item2.IdComponente).Any()).ToList();
                    vm.listaTurmas = BL.Turma.Get(a => a.Matriz.Modulo.Where(b => b.Componente.Where(c => c.IdComponente == item2.IdComponente).Any()).Any(), null, "Matriz").ToList();
                    vm.listaAmbientes = BL.Ambiente.Get(a => a.TipoAmbiente.Componente.Where(b => b.IdComponente == item2.IdComponente).Any()).ToList();
                    break;

                case 3:
                    var item3 = BL.Docente.GetById(item);
                    ViewBag.Nome = item3.Nome;

                    vm.listaComponentes = BL.Componente.Get(a => a.Docente.Where(b => b.IdDocente == item3.IdDocente).Any()).ToList();
                    vm.listaTurmas = BL.Turma.Get(a => a.Matriz.Modulo.Where(b => b.Componente.Where(c => c.Docente.Where(d => d.IdDocente == item3.IdDocente).Any()).Any()).Any(), null, "Matriz").ToList();
                    vm.listaAmbientes = BL.Ambiente.Get().ToList();
                    break;
                case 4:
                    var item4 = BL.Turma.Get(a => a.IdTurma == item, null, "Matriz").FirstOrDefault();
                    ViewBag.Nome = item4.IdTurma.ToString("00000") + " - " + item4.Matriz.Nome;
                    var agendament = BL.Agendamento.Get(a => a.HoraFim >= DateTime.Now, null, "Docente").ToList();


                    vm.listaComponentes = BL.Componente.Get(a => a.Modulo.Where(b => b.Matriz.Turma.Where(c => c.IdTurma == item4.IdTurma).Any()).Any()).ToList();
                    vm.listaDocentes = BL.Docente.Get(a => a.Componente.Where(b => b.Modulo.Where(c => c.Matriz.Turma.Where(d => d.IdTurma == item4.IdTurma).Any()).Any()).Any()).ToList();

                    vm.listaAmbientes = BL.Ambiente.Get(a => a.TipoAmbiente.Componente.Where(b => b.Modulo.Where(c => c.Matriz.Turma.Where(d => d.IdTurma == item4.IdTurma).Any()).Any()).Any()).ToList();




                    break;
            }
            return View(vm);
        }

        class AreaClassFilter : EqualityComparer<AreaAtuacao>
        {
            public override bool Equals(AreaAtuacao x, AreaAtuacao y)
            {
                return x.IdAreaAtuacao == y.IdAreaAtuacao;
            }

            public override int GetHashCode(AreaAtuacao obj)
            {
                return obj == null ? 0 : obj.IdAreaAtuacao;
            }
        }

        //public ActionResult ListarAgendamentos(int itemTipo, int itemId, int itemVersao, string itemData, string start = "", string end = "")
        //{
        //    var vm = new AgendamentoIndexVM();
        //    vm.listaAmbientes = new List<Ambiente>();
        //    vm.listaComponentes = new List<Componente>();
        //    vm.listaDocentes = new List<Docente>();
        //    vm.listaTurmas = new List<Turma>();
        //    switch (itemTipo)
        //    {
        //        case 1:
        //            var ambiente = BL.Ambiente.Get(a => a.IdAmbiente == itemId, null, "TipoAmbiente.Componente.Docente,TipoAmbiente.Componente.Modulo.Matriz").FirstOrDefault();

        //            foreach (var componente in ambiente.TipoAmbiente.Componente)
        //            {
        //                vm.listaDocentes.AddRange(componente.Docente);
        //                vm.listaComponentes.Add(componente);
        //                foreach (var modulo in componente.Modulo)
        //                {
        //                    vm.listaTurmas.AddRange(modulo.Matriz.Turma.ToList());
        //                }

        //            }
        //            break;

        //        case 2:

        //            var componente1 = BL.Componente.Get(a => a.IdComponente == itemId, null, "Docente,Modulo.Matriz.Turma,TipoAmbiente.Ambiente").FirstOrDefault();

        //            vm.listaDocentes = componente1.Docente.ToList();

        //            foreach (var tipoambiente1 in componente1.TipoAmbiente)
        //{
        //              vm.listaAmbientes.AddRange(tipoambiente1.Ambiente);
        //                //vm.listaAmbientes.Add(componente1.TipoAmbiente.ToList());

        //            }
        //            foreach (var mod in componente1.Modulo)
        //    {
        //                vm.listaTurmas.AddRange(mod.Matriz.Turma);
        //            }
        //            break;               


        //        case 3:

        //            var docente = BL.Docente.Get(a => a.IdDocente == itemId, null, "Componente.TipoAmbiente.Ambiente,Componente.Modulo.Matriz").FirstOrDefault();

        //            vm.listaComponentes = docente.Componente.ToList();
        //            foreach (var comp in docente.Componente)
        //            {
        //                foreach (var mod in comp.Modulo)
        //                {
        //                    vm.listaTurmas.AddRange(mod.Matriz.Turma);
        //                }
        //                foreach (var x1 in comp.TipoAmbiente)
        //                {
        //                    vm.listaAmbientes.AddRange(x1.Ambiente);

        //                }   
        //            }
        //            break;

        //        case 4:
        //            //Componente
        //            //  Ambiente - Matriz.Modulo.Componente.TipoAmbiente.Ambiente 
        //            //Docente - Matriz.Modulo.Componente.Docente
        //            var turma1 = BL.Turma.Get(a => a.IdTurma == itemId, null, "Matriz.Modulo.Componente.TipoAmbiente.Ambiente,Matriz.Modulo.Componente.Docente,,Matriz.Modulo.Componente.Agendamento").FirstOrDefault();
        //            foreach (var mod in turma1.Matriz.Modulo)
        //            {
        //                vm.listaComponentes.AddRange(mod.Componente);
        //                foreach (var comp in mod.Componente)
        //                {
        //                    var chAgendamento = BL.Agendamento.Get(a => a.IdTurma ==itemId && a.IdComponente == comp.IdComponente).Sum(a=>a.HoraInicio.Value.CompareTo(a.HoraFim));
        //                    vm.listaDocentes.AddRange(comp.Docente);
        //                    foreach (var amb in comp.TipoAmbiente)
        //                    {
        //                        vm.listaAmbientes.AddRange(amb.Ambiente);
        //                    }                            
        //                }
        //            }
        //            break;

        //    }
        //    return View(vm);
        //}

        public ActionResult ListarAgendamentos(int itemTipo, int itemId, int itemVersao, string start = "", string end = "")
        {

            var baseColor = "";
            SPEBL BL = new SPEBL();
            List<Agendamento> eventos = GetAgendamento(itemTipo, itemId, itemVersao, start, end);

            List<CalendarioItemAgendamento> itensCalendario = new List<CalendarioItemAgendamento>();

            foreach (var item1 in eventos)
            {
                DateTime StartDate = item1.HoraInicio.Value;
                DateTime EndDate = item1.HoraFim.GetValueOrDefault();

                DateTime originalStart = item1.HoraInicio.GetValueOrDefault();
                DateTime? originalend = item1.HoraFim;

                switch (itemTipo)
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
                        turma = item1.Turma.Matriz.Nome.ToString(),
                        componente = item1.Componente.Nome,
                        ambiente = item1.Ambiente.Nome,
                        color = "#" + baseColor,
                    });
                    StartDate = StartDate.AddDays(Interval);
                }
            }

            //Carregando os eventos do calendario civíl
            itensCalendario.AddRange(ItemsCalendarioCivil());

            //Carregar os eventos do docente se a tela estiver no contexto de docente(itemTipo == 3)
            if (itemTipo == 3)
            {
                itensCalendario.AddRange(ItemsCalendarioDocente(itemId));
            }

            return Json(itensCalendario, JsonRequestBehavior.AllowGet);
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

        public JsonResult CadastrarAgendamento(string dtInicio, string dtFim, int idDocente, int idComponente, int versao, int idTurma, int idAmbiente, string dias, string horaInicio, string horaFinal, bool pularValidacao = false, bool pularConflito = false)
        {
            var timeInicio = TimeSpan.Parse(horaInicio);

            var timeFim = TimeSpan.Parse(horaFinal);
            var booTemChoque = false;
            var booValidacaoTurno = false;

            string detalheConflito = "";

            //verificacao de validade de turno
            var turma = BL.Turma.Get(a => a.IdTurma == idTurma, null, "Turno").FirstOrDefault();
            if ((turma.Turno.HoraFim != TimeSpan.Zero && timeFim > turma.Turno.HoraFim) ||
                 (turma.Turno.HoraFim != TimeSpan.Zero && timeInicio < turma.Turno.HoraIni))
            {
                booValidacaoTurno = true;
                string timeFormat = "{0:00}:{1:00}h";
                string strHoraInicio = string.Format(timeFormat, turma.Turno.HoraIni.GetValueOrDefault().Hours, turma.Turno.HoraIni.GetValueOrDefault().Minutes);
                string strHoraFim = string.Format(timeFormat, turma.Turno.HoraFim.GetValueOrDefault().Hours, turma.Turno.HoraFim.GetValueOrDefault().Minutes);

                detalheConflito = string.Format(ConfigurationManager.AppSettings["MsgConflitoTurno"], strHoraInicio, strHoraFim);
            }

            if (!booValidacaoTurno || pularValidacao)
            {
                CultureInfo brasil = new CultureInfo("pt-BR");
                DateTime inicioMes = DateTime.Parse(dtInicio, brasil);

                var fimMes = DateTime.Parse(dtFim, brasil);

                List<DateTime> listaDias = new List<DateTime>();

                while (inicioMes.Date <= fimMes.Date)
                {
                    if (dias.Contains(((int)inicioMes.DayOfWeek).ToString()))
                    {
                        listaDias.Add(inicioMes);
                    }
                    inicioMes = inicioMes.AddDays(1);
                }

                var listaHorario = new List<Agendamento>();
                //foreach (var dataEscolhida in listaDias)
                for (int i = 0; i < listaDias.Count; i++)
                {
                    var dataEscolhida = listaDias[i];

                    listaHorario.AddRange(BL.Agendamento.Get(a => a.IdAmbiente == idAmbiente && DbFunctions.TruncateTime(a.HoraInicio.Value) == DbFunctions.TruncateTime(dataEscolhida), null, "Ambiente").ToList());
                    listaHorario.AddRange(BL.Agendamento.Get(a => a.IdComponente == idComponente && DbFunctions.TruncateTime(a.HoraInicio.Value) == DbFunctions.TruncateTime(dataEscolhida), null, "Turma.Matriz").ToList());
                    listaHorario.AddRange(BL.Agendamento.Get(a => a.IdTurma == idTurma && DbFunctions.TruncateTime(a.HoraInicio.Value) == DbFunctions.TruncateTime(dataEscolhida), null).ToList());
                    listaHorario.AddRange(BL.Agendamento.Get(a => a.IdDocente == idDocente && DbFunctions.TruncateTime(a.HoraInicio.Value) == DbFunctions.TruncateTime(dataEscolhida), null, "Docente").ToList());


                    var calCivil = BL.CalendarioCivil.Get(a =>

                        a.DataFim == null ?
                        (
                            DbFunctions.TruncateTime(dataEscolhida) == DbFunctions.TruncateTime(a.DataInicial)
                        )
                        :
                        (
                            DbFunctions.TruncateTime(dataEscolhida) >= DbFunctions.TruncateTime(a.DataInicial) &&
                            DbFunctions.TruncateTime(dataEscolhida) <= DbFunctions.TruncateTime(a.DataFim)
                         ));


                    if (calCivil.Count() > 0)
                    {
                        if (pularConflito == false)
                        {
                            booTemChoque = true;
                            detalheConflito = string.Format(ConfigurationManager.AppSettings["MsgConflitoCalCivil"],
                                calCivil.First().Descricao,
                                dataEscolhida.ToShortDateString());

                            listaHorario.AddRange(calCivil.Select(s => new Agendamento() { HoraInicio = s.DataInicial, HoraFim = s.DataFim }));
                        }
                        else
                        {
                            listaDias.RemoveAt(i);
                            i--;
                            continue;
                        }
                    }

                    var calDocente = BL.AgendaEvento.Get(a =>

                        a.DataFim == null ?
                        (
                            DbFunctions.TruncateTime(dataEscolhida) == DbFunctions.TruncateTime(a.DataIni)
                        )
                        :
                        (
                            DbFunctions.TruncateTime(dataEscolhida) >= DbFunctions.TruncateTime(a.DataIni) &&
                            DbFunctions.TruncateTime(dataEscolhida) <= DbFunctions.TruncateTime(a.DataFim)
                         )

                       , null, "Docente");

                    if (calDocente.Count() > 0)
                    {
                        if (pularConflito == false)
                        {
                            var evDocente = calDocente.First();
                            var tipoAtividade = BL.TipoAtividade.GetById(evDocente.TipoEvento.GetValueOrDefault());

                            booTemChoque = true;
                            detalheConflito = string.Format(ConfigurationManager.AppSettings["MsgConflitoCalDocente"],
                                evDocente.Docente.Nome,
                                tipoAtividade.Nome,
                                evDocente.Descricao,
                                dataEscolhida.ToShortDateString());


                            listaHorario.AddRange(calDocente.Select(s => new Agendamento() { HoraInicio = s.DataIni, HoraFim = s.DataFim }));
                        }
                        else
                        {
                            listaDias.RemoveAt(i);
                            i--;
                            continue;
                        }
                    }


                }

                List<DateTime> horariosChoque = new List<DateTime>();

                if (!booTemChoque)
                {
                    var atribuiuMsg = false;
                    for (int i = 0; i < listaHorario.Count; i++)
                    {
                        var dataExistente = listaHorario[i];
                        dataExistente.HoraFim = dataExistente.HoraFim.GetValueOrDefault(dataExistente.HoraInicio.GetValueOrDefault());

                        booTemChoque = this.TimePeriodOverlap(
                            dataExistente.HoraInicio.Value,
                            dataExistente.HoraFim.Value,
                            DateTime.Parse(dataExistente.HoraInicio.Value.Date.ToShortDateString() + " " + timeInicio),
                            DateTime.Parse(dataExistente.HoraInicio.Value.Date.ToShortDateString() + " " + timeFim));

                        if (booTemChoque)
                        {
                            
                            if (dataExistente.IdAmbiente == idAmbiente && dataExistente.Ambiente != null)
                            {
                                detalheConflito = string.Format(ConfigurationManager.AppSettings["MsgConflitoAmbiente"],
                                    dataExistente.Ambiente.Nome,
                                    dataExistente.HoraInicio.GetValueOrDefault().ToShortDateString());
                                //atribuiuMsg = true;
                            }
                            else if (dataExistente.IdComponente == idComponente && dataExistente.Componente != null)
                            {
                                detalheConflito = string.Format(ConfigurationManager.AppSettings["MsgConflitoComponente"],
                                    dataExistente.Componente.Nome,
                                    dataExistente.HoraInicio.GetValueOrDefault().ToShortDateString());
                                //atribuiuMsg = true;
                            }
                            else if (dataExistente.IdTurma == idTurma && dataExistente.Turma != null && dataExistente.Turma.Matriz != null)
                            {
                                detalheConflito = string.Format(ConfigurationManager.AppSettings["MsgConflitoTurma"],
                                    dataExistente.Turma.IdTurma.ToString("00000"),
                                    dataExistente.Turma.Matriz.Nome,
                                    dataExistente.HoraInicio.GetValueOrDefault().ToShortDateString());
                               // atribuiuMsg = true;
                            }
                            else if (dataExistente.IdDocente == idDocente && dataExistente.Docente != null)
                            {
                                detalheConflito = string.Format(ConfigurationManager.AppSettings["MsgConflitoDocente"],
                                    dataExistente.Docente.Nome,
                                    dataExistente.HoraInicio.GetValueOrDefault().ToShortDateString());
                                atribuiuMsg = true;
                            }
                            else //if(atribuiuMsg == false)
                            {
                                detalheConflito = string.Format("Houve conflitos no agendamento.");
                            }

                            listaDias.RemoveAll(a => a.Date >= dataExistente.HoraInicio.GetValueOrDefault().Date && a.Date <= dataExistente.HoraFim.GetValueOrDefault().Date);
                            //i--;
                            break;
                        }
                    }
                }



                if (!booTemChoque || pularValidacao)
                {
                    if (listaDias.Count == 0)
                    {
                        detalheConflito = ConfigurationManager.AppSettings["MsgNenhumDiaAgendamento"];
                        return Json(new
                        {
                            status = false,
                            msg = detalheConflito,
                            temOpcao = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    foreach (var item in listaDias)
                    {
                        Agendamento agen = new Agendamento();

                        agen.IdComponente = idComponente;
                        agen.IdAmbiente = idAmbiente;
                        agen.IdDocente = idDocente;
                        agen.IdTurma = idTurma;
                        agen.Versao = versao;

                        agen.HoraInicio = DateTime.Parse(item.Date.ToShortDateString() + " " + timeInicio.ToString());

                        agen.HoraFim = DateTime.Parse(item.Date.ToShortDateString() + " " + timeFim.ToString());
                        BL.Agendamento.Add(agen);
                    }
                }
                else
                {
                    return Json(new
                    {
                        status = false,
                        msg = detalheConflito,
                        temOpcao = !booValidacaoTurno
                    }, JsonRequestBehavior.AllowGet);

                }



                //verifica Inconsistencia de Horario de Docente
                //verifica Inconsistencia de Horario de Ambiente
                //verifica Inconsistencia de Horario de 
                //verifica Inconsistencia de Horario de Docente
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    status = false,
                    msg = detalheConflito,
                    temOpcao = !booValidacaoTurno
                }, JsonRequestBehavior.AllowGet);

            }
        }

        private List<Agendamento> GetAgendamento(int itemTipo, int itemId, int itemVersao, string start = "", string end = "")
        {
            List<Agendamento> eventos = new List<Agendamento>();
            switch (itemTipo)
            {
                case 1:
                    eventos = BL.Agendamento.Get(a => a.IdAmbiente == itemId && a.Versao == itemVersao, null, "turma.matriz,Docente,Componente,Ambiente").ToList();

                    break;
                case 2:
                    eventos = BL.Agendamento.Get(a => a.IdComponente == itemId && a.Versao == itemVersao, null, "turma.matriz,Docente,Componente,Ambiente").ToList();

                    break;
                case 3:
                    eventos = BL.Agendamento.Get(a => a.IdDocente == itemId && a.Versao == itemVersao, null, "turma.matriz,Docente,Componente,Ambiente").ToList();

                    break;
                case 4:
                    eventos = BL.Agendamento.Get(a => a.IdTurma == itemId && a.Versao == itemVersao, null, "turma.matriz,Docente,Componente,Ambiente").ToList();

                    break;
            }

            return eventos;
        }


        public JsonResult _OptionsDocente(int itemId, int tipoItemContext, int itemIdContext)
        {

            optionsVM option = new optionsVM();
            var docente = BL.Docente.Get(a => a.IdDocente == itemId, null, "Componente.Modulo.Matriz.Turma").FirstOrDefault();
            List<Ambiente> listaAmbiente = new List<Ambiente>();
            List<Turma> listaTurmas = new List<Turma>();
            List<Componente> listaComponente = new List<Componente>();
            switch (tipoItemContext)
            {
                case 1:
                    var ambiente = BL.Ambiente.Get(a => a.IdAmbiente == itemIdContext, null, "TipoAmbiente.Componente").FirstOrDefault();

                    foreach (var item in ambiente.TipoAmbiente.Componente)
                    {
                        var comp = docente.Componente.Where(a => a.IdComponente == item.IdComponente).FirstOrDefault();
                        if (comp != null)
                        {
                            if (!listaComponente.Any(a => a.IdComponente == comp.IdComponente))
                            {
                                listaComponente.Add(comp);
                            }
                        }
                    }


                    foreach (var componente in listaComponente)
                    {
                        if (componente != null)
                        {
                            foreach (var modulo in componente.Modulo)
                            {
                                foreach (var turma8 in modulo.Matriz.Turma)
                                {
                                    if (!listaTurmas.Any(a => a.IdTurma == turma8.IdTurma))
                                    {
                                        listaTurmas.Add(turma8);
                                    }
                                }
                            }
                        }
                    }

                    option.listaAmbientes = new List<Ambiente>();
                    option.listaComponente = new List<Componente>();
                    option.listaDocentes = new List<Docente>();
                    option.listaTurma = new List<Turma>();

                    ambiente.TipoAmbiente = null;
                    docente.Componente = null;

                    option.listaAmbientes.Add(ambiente);
                    option.listaDocentes.Add(docente);
                    option.listaComponente.AddRange(listaComponente);
                    option.listaTurma.AddRange(listaTurmas);


                    break;
                case 2:


                    var componente1 = BL.Componente.Get(a => a.IdComponente == itemIdContext, null, "TipoAmbiente.Ambiente,Modulo.Matriz.Turma").FirstOrDefault();



                    foreach (var item in componente1.TipoAmbiente)
                    {
                        foreach (var ambiente1 in item.Ambiente)
                        {
                            ambiente1.TipoAmbiente = null;
                            listaAmbiente.Add(ambiente1);
                        }

                    }


                    foreach (var modulo in componente1.Modulo)
                    {
                        modulo.Matriz.Modulo = null;
                        foreach (var turma232 in modulo.Matriz.Turma)
                        {
                            turma232.Matriz.Turma = null;
                            listaTurmas.Add(turma232);
                        }

                    }

                    //removendo Rendundancias Ciclicaas

                    componente1.Modulo = null;
                    componente1.TipoAmbiente = null;
                    //Fim redundancias Ciclicas
                    option.listaAmbientes = new List<Ambiente>();
                    option.listaComponente = new List<Componente>();
                    option.listaDocentes = new List<Docente>();
                    option.listaTurma = new List<Turma>();

                    docente.Componente = null;

                    option.listaAmbientes.AddRange(listaAmbiente);
                    option.listaDocentes.Add(docente);
                    option.listaComponente.Add(componente1);
                    option.listaTurma.AddRange(listaTurmas);


                    break;
                case 3:

                    var ambiente2 = BL.Ambiente.Get(a => a.IdAmbiente == itemIdContext, null, "TipoAmbiente.Componente").FirstOrDefault();

                    foreach (var item in ambiente2.TipoAmbiente.Componente)
                    {
                        Componente comp = docente.Componente.Where(a => a.IdComponente == item.IdComponente).FirstOrDefault();
                        if (comp != null)
                        {
                            listaComponente.Add(comp);
                        }
                    }

                    foreach (var componente in listaComponente)
                    {
                        foreach (var modulo in componente.Modulo)
                        {
                            listaTurmas.AddRange(modulo.Matriz.Turma);
                        }

                    }

                    option.listaAmbientes = new List<Ambiente>();
                    option.listaComponente = new List<Componente>();
                    option.listaDocentes = new List<Docente>();
                    option.listaTurma = new List<Turma>();


                    option.listaAmbientes.Add(ambiente2);
                    option.listaDocentes.Add(docente);
                    option.listaComponente.AddRange(listaComponente);
                    option.listaTurma.AddRange(listaTurmas);


                    break;
                case 4:
                    var turma1 = BL.Turma.Get(a => a.IdTurma == itemIdContext, null, "Matriz.Modulo.Componente.TipoAmbiente").FirstOrDefault();



                    foreach (var modulo in turma1.Matriz.Modulo)
                    {

                        foreach (var comp2 in modulo.Componente)
                        {
                            listaComponente.Add(comp2);
                            foreach (var tipoAmbiente21 in comp2.TipoAmbiente)
                            {
                                listaAmbiente.AddRange(tipoAmbiente21.Ambiente);
                            }

                        }
                    }


                    option.listaAmbientes = new List<Ambiente>();
                    option.listaComponente = new List<Componente>();
                    option.listaDocentes = new List<Docente>();
                    option.listaTurma = new List<Turma>();

                    turma1.Matriz.Turma = null;

                    foreach (var item in listaComponente)
                    {
                        item.Modulo = null;
                    }
                    foreach (var item in listaAmbiente)
                    {
                        item.TipoAmbiente = null;
                    }

                    option.listaAmbientes.AddRange(listaAmbiente);
                    option.listaDocentes.Add(docente);
                    option.listaComponente.AddRange(listaComponente);
                    option.listaTurma.Add(turma1);


                    break;
            }


            var jsonResult = new
            {
                listaComponente = option.listaComponente.Select(s => new { IdComponente = s.IdComponente, Nome = s.Nome }),
                listaAmbientes = option.listaAmbientes.Select(a => new { IdAmbiente = a.IdAmbiente, Nome = a.Nome }),
                listaTurma = option.listaTurma.Select(t => new { IdTurma = t.IdTurma, Nome = t.Matriz.Nome }),
                listaDocentes = option.listaDocentes.Select(d => new { IdDocente = d.IdDocente, Nome = d.Nome }),
            };


            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }
        public JsonResult _OptionsComponente(int itemId, int tipoItemContext, int itemIdContext)
        {

            optionsVM option = new optionsVM();
            var componente = BL.Componente.Get(a => a.IdComponente == itemId, null, "Docente,Modulo.Matriz.Turma,TipoAmbiente.Ambiente").FirstOrDefault();
            List<Ambiente> listaAmbiente = new List<Ambiente>();
            List<Turma> listaTurmas = new List<Turma>();
            List<Docente> listaDocente = new List<Docente>();
            switch (tipoItemContext)
            {
                case 1:
                    var ambiente = BL.Ambiente.Get(a => a.IdAmbiente == itemIdContext, null, "").FirstOrDefault();



                    option.listaAmbientes = new List<Ambiente>();
                    option.listaComponente = new List<Componente>();
                    option.listaDocentes = new List<Docente>();
                    option.listaTurma = new List<Turma>();

                    foreach (var docente in componente.Docente)
                    {
                        docente.Componente = null;
                        docente.AgendaComponente = null;
                    }

                    foreach (var modulo in componente.Modulo)
                    {
                        modulo.Componente = null;

                        foreach (var turma3 in modulo.Matriz.Turma)
                        {
                            listaTurmas.Add(turma3);
                        }

                    }

                    option.listaAmbientes.Add(ambiente);
                    option.listaDocentes.AddRange(componente.Docente);
                    option.listaComponente.Add(componente);
                    option.listaTurma.AddRange(listaTurmas);


                    break;
                case 2:





                    break;
                case 3:

                    var docente1 = BL.Docente.Get(a => a.IdDocente == itemIdContext, null, "").FirstOrDefault();

                    option.listaAmbientes = new List<Ambiente>();
                    option.listaComponente = new List<Componente>();
                    option.listaDocentes = new List<Docente>();
                    option.listaTurma = new List<Turma>();

                    docente1.Componente = null;


                    docente1.AgendaComponente = null;

                    foreach (var modulo in componente.Modulo)
                    {
                        modulo.Componente = null;

                        foreach (var turma2 in modulo.Matriz.Turma)
                        {
                            if (!listaTurmas.Any(a => a.IdTurma == turma2.IdTurma))
                            {
                                listaTurmas.Add(turma2);
                            }
                        }

                    }

                    foreach (var tipoambiente1 in componente.TipoAmbiente)
                    {
                        foreach (var ambiente1 in tipoambiente1.Ambiente)
                        {
                            ambiente1.TipoAmbiente = null;
                            listaAmbiente.Add(ambiente1);
                        }

                    }

                    option.listaAmbientes.AddRange(listaAmbiente);
                    option.listaDocentes.Add(docente1);
                    option.listaComponente.Add(componente);
                    option.listaTurma.AddRange(listaTurmas);

                    break;
                case 4:
                    var turma1 = BL.Turma.Get(a => a.IdTurma == itemIdContext, null, "Matriz").FirstOrDefault();

                    option.listaAmbientes = new List<Ambiente>();
                    option.listaComponente = new List<Componente>();
                    option.listaDocentes = new List<Docente>();
                    option.listaTurma = new List<Turma>();

                    foreach (var tipoambiente1 in componente.TipoAmbiente)
                    {
                        foreach (var ambiente1 in tipoambiente1.Ambiente)
                        {
                            ambiente1.TipoAmbiente = null;
                            listaAmbiente.Add(ambiente1);
                        }

                    }
                    option.listaAmbientes.AddRange(listaAmbiente);
                    option.listaDocentes.AddRange(componente.Docente);
                    option.listaComponente.Add(componente);
                    option.listaTurma.Add(turma1);

                    break;
            }

            var jsonResult = new
            {
                listaComponente = option.listaComponente.Select(s => new { IdComponente = s.IdComponente, Nome = s.Nome }),
                listaAmbientes = option.listaAmbientes.Select(a => new { IdAmbiente = a.IdAmbiente, Nome = a.Nome }),
                listaTurma = option.listaTurma.Select(t => new { IdTurma = t.IdTurma, Nome = t.Matriz.Nome }),
                listaDocentes = option.listaDocentes.Select(d => new { IdDocente = d.IdDocente, Nome = d.Nome }),
            };

            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }
        public JsonResult _OptionsTurma(int itemId, int tipoItemContext, int itemIdContext)
        {

            optionsVM option = new optionsVM();
            var turma = BL.Turma.Get(a => a.IdTurma == itemId, null, "Matriz.Modulo.Componente.Docente").FirstOrDefault();
            List<Ambiente> listaAmbiente = new List<Ambiente>();
            List<Componente> listaComponente = new List<Componente>();
            List<Docente> listaDocente = new List<Docente>();
            switch (tipoItemContext)
            {
                case 1:
                    var ambiente = BL.Ambiente.Get(a => a.IdAmbiente == itemIdContext, null, "").FirstOrDefault();
                    option.listaAmbientes = new List<Ambiente>();
                    option.listaComponente = new List<Componente>();
                    option.listaDocentes = new List<Docente>();
                    option.listaTurma = new List<Turma>();

                    foreach (var modulo in turma.Matriz.Modulo)
                    {
                        foreach (var componente55 in modulo.Componente)
                        {
                            componente55.Modulo = null;
                            componente55.TipoAmbiente = null;
                            listaComponente.Add(componente55);
                            listaDocente.AddRange(componente55.Docente);
                        }
                    }

                    option.listaAmbientes.Add(ambiente);
                    option.listaDocentes.AddRange(listaDocente);
                    option.listaComponente.AddRange(listaComponente);
                    option.listaTurma.Add(turma);


                    break;
                case 2:


                    var componente1 = BL.Componente.Get(a => a.IdComponente == itemIdContext, null, "TipoAmbiente.Ambiente,Docente").FirstOrDefault();


                    foreach (var docente in componente1.Docente)
                    {
                        listaDocente.Add(docente);
                    }

                    foreach (var tipoambiente23 in componente1.TipoAmbiente)
                    {
                        listaAmbiente.AddRange(tipoambiente23.Ambiente);
                    }



                    option.listaAmbientes.AddRange(listaAmbiente);
                    option.listaDocentes.AddRange(listaDocente);
                    option.listaComponente.Add(componente1);
                    option.listaTurma.Add(turma);


                    break;
                case 3:

                    var docente1 = BL.Docente.Get(a => a.IdDocente == itemIdContext, null, "Componente.TipoAmbiente.Ambiente").FirstOrDefault();

                    option.listaAmbientes = new List<Ambiente>();
                    option.listaComponente = new List<Componente>();
                    option.listaDocentes = new List<Docente>();
                    option.listaTurma = new List<Turma>();

                    var listComp = new List<Componente>();
                    // ambiente.TipoAmbiente = null;
                    // docente1.Componente = null;



                    foreach (var comp in docente1.Componente)
                    {
                        if (!listaComponente.Any(a => a.IdComponente == comp.IdComponente))
                        {
                            listaComponente.Add(comp);
                        }
                        //O Metodo acima Evitar bug de lazyLoading do entity
                        //listaComponente.AddRange(listComp.Where(a => a.IdComponente == comp.IdComponente));
                    }

                    foreach (var xomp in listaComponente)
                    {
                        foreach (var tipoAmb in xomp.TipoAmbiente)
                        {
                            foreach (var ambienteRec in tipoAmb.Ambiente)
                            {

                                if (!listaAmbiente.Where(a => a.IdAmbiente == ambienteRec.IdAmbiente).Any())
                                {
                                    listaAmbiente.AddRange(tipoAmb.Ambiente);
                                }

                            }
                        }
                    }


                    option.listaAmbientes.AddRange(listaAmbiente);
                    option.listaDocentes.Add(docente1);
                    option.listaComponente.AddRange(listaComponente);
                    option.listaTurma.Add(turma);

                    break;
                case 4:


                    break;
            }

            var jsonResult = new
            {
                listaComponente = option.listaComponente.Select(s => new { IdComponente = s.IdComponente, Nome = s.Nome }),
                listaAmbientes = option.listaAmbientes.Select(a => new { IdAmbiente = a.IdAmbiente, Nome = a.Nome }),
                listaTurma = option.listaTurma.Select(t => new { IdTurma = t.IdTurma, Nome = t.Matriz.Nome }),
                listaDocentes = option.listaDocentes.Select(d => new { IdDocente = d.IdDocente, Nome = d.Nome }),
            };


            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }
        public JsonResult _OptionsAmbiente(int itemId, int tipoItemContext, int itemIdContext)
        {

            optionsVM option = new optionsVM();
            var ambiente = BL.Ambiente.Get(a => a.IdAmbiente == itemId, null, "TipoAmbiente.Componente.Docente,TipoAmbiente.Componente.Modulo.Matriz.Turma").FirstOrDefault();
            List<Componente> listaComponente = new List<Componente>();
            List<Turma> listaTurmas = new List<Turma>();
            List<Docente> listaDocente = new List<Docente>();
            switch (tipoItemContext)
            {
                case 1:


                    break;
                case 2:


                    var componente1 = BL.Componente.Get(a => a.IdComponente == itemIdContext, null, "TipoAmbiente.Ambiente,Docente,Modulo.Matriz.Turma").FirstOrDefault();
                    foreach (var modulo in componente1.Modulo)
                    {
                        foreach (var turma in modulo.Matriz.Turma)
                        {
                            if (!listaTurmas.Any(a => a.IdTurma == turma.IdTurma))
                            {
                                listaTurmas.AddRange(modulo.Matriz.Turma);
                            }
                        }
                    }

                    option.listaAmbientes = new List<Ambiente>();
                    option.listaComponente = new List<Componente>();
                    option.listaDocentes = new List<Docente>();
                    option.listaTurma = new List<Turma>();


                    option.listaAmbientes.Add(ambiente);
                    option.listaDocentes.AddRange(componente1.Docente);
                    option.listaComponente.Add(componente1);
                    option.listaTurma.AddRange(listaTurmas);


                    break;
                case 3:

                    var docente1 = BL.Docente.Get(a => a.IdDocente == itemIdContext, null, "Componente.TipoAmbiente.Ambiente").FirstOrDefault();

                    option.listaAmbientes = new List<Ambiente>();
                    option.listaComponente = new List<Componente>();
                    option.listaDocentes = new List<Docente>();
                    option.listaTurma = new List<Turma>();

                    foreach (var item in docente1.Componente)
                    {
                        if (ambiente.TipoAmbiente.Componente.Where(a => a.IdComponente == item.IdComponente).Count() > 0)
                        {
                            if (!listaComponente.Any(a => a.IdComponente == item.IdComponente))
                            {
                                listaComponente.Add(ambiente.TipoAmbiente.Componente.Where(a => a.IdComponente == item.IdComponente).First());
                            }
                        }
                    }


                    foreach (var componente13 in listaComponente)
                    {
                        foreach (var modulo in componente13.Modulo)
                        {
                            foreach (var turma15 in modulo.Matriz.Turma)
                            {

                                if (!listaTurmas.Any(a => a.IdTurma == turma15.IdTurma))
                                {
                                    listaTurmas.Add(turma15);
                                }
                            }
                        }

                    }
                    //docente1.Componente = null;

                    //ambiente.TipoAmbiente = null;
                    option.listaAmbientes.Add(ambiente);
                    option.listaDocentes.Add(docente1);
                    option.listaComponente.AddRange(listaComponente);
                    option.listaTurma.AddRange(listaTurmas);

                    break;
                case 4:
                    var turma1 = BL.Turma.Get(a => a.IdTurma == itemIdContext, null, "Matriz").FirstOrDefault();

                    option.listaAmbientes = new List<Ambiente>();
                    option.listaComponente = new List<Componente>();
                    option.listaDocentes = new List<Docente>();
                    option.listaTurma = new List<Turma>();

                    foreach (var componente in ambiente.TipoAmbiente.Componente)
                    {
                        foreach (var docente123 in componente.Docente)
                        {
                            docente123.Componente = null;
                            if (!listaDocente.Where(a => a.IdDocente == docente123.IdDocente).Any())
                            {
                                listaDocente.Add(docente123);
                            }
                        }


                    }

                    foreach (var componente24 in ambiente.TipoAmbiente.Componente)
                    {
                        componente24.TipoAmbiente = null;
                        if (!listaComponente.Where(a => a.IdComponente == componente24.IdComponente).Any())
                        {
                            listaComponente.Add(componente24);
                        }
                    }

                    ambiente.TipoAmbiente = null;
                    option.listaAmbientes.Add(ambiente);
                    option.listaDocentes.AddRange(listaDocente);
                    option.listaComponente.AddRange(listaComponente);
                    option.listaTurma.Add(turma1);


                    break;
            }


            var jsonResult = new
            {
                listaComponente = option.listaComponente.Select(s => new { IdComponente = s.IdComponente, Nome = s.Nome }),
                listaAmbientes = option.listaAmbientes.Select(a => new { IdAmbiente = a.IdAmbiente, Nome = a.Nome }),
                listaTurma = option.listaTurma.Select(t => new { IdTurma = t.IdTurma, Nome = t.Matriz.Nome }),
                listaDocentes = option.listaDocentes.Select(d => new { IdDocente = d.IdDocente, Nome = d.Nome }),
            };



            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }


    }
}
