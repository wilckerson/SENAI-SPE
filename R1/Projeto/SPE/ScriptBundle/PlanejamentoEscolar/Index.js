$(document).ready(function () {

    prepararIntervaloDatas($("#dtInicio"), $("#dtFim"));

    $("input[name='diasemana']").change(function () {
        $("#msgValidDiasSemana").text("");
    });

    $('#taba a').click(function (e) {
        e.preventDefault()
        $(this).tab('show')
    });

    //setTimeout(function () {
    $("#planejamento").delegate(".deleteComponente", "click", function (e) {

        var idAgendamento = $(this).parents().find(".idAgendamento").val();

        $("body").append("<div id='modalExcluir' class='modal hide fade' tabindex='-1' role='dialog' aria-labelledby='Informação' aria-hidden='true'>" +
"<div class='modal-header'>" +
" <button type='button' class='close' data-dismiss='modal' aria-hidden='true'>×</button>" +
"  <h3 id='myModalLabel'>Excluir Agendamento</h3>" +
"</div>" +
" <div class='modal-body'>" +
" <p>Deseja excluir este agendamento?</p>" +
"</div>" +
" <div class='modal-footer'>" +
" <button class='btn' data-dismiss='modal' aria-hidden='true'>Fechar</button>" +
"<button class='btn btn-primary' data-dismiss='modal' aria-hidden='true' onClick ='deletarAgendamento(" + idAgendamento + ")' >Excluir</button>" +
" </div>" +
" </div>");

        $("#modalExcluir").ready(function () {
            $("#modalExcluir").modal({ backdrop: 'static', keyboard: true });
        });

    });


    $("#slt1").change(hideValidation);
    $("#slt2").change(hideValidation);
    $("#slt3").change(hideValidation);
    $("#slt4").change(hideValidation);
    $("#dtInicio").change(hideValidation);
    $("#DataFim").change(hideValidation);

    function hideValidation() {
        $("#msgValidacao").text("");
    }

    var diasSemana = "";
    $("input[name='diasemana']").change(function () {
        diasSemana += $(this).val();
        hideValidation();
    });

    $("#btnSaveAgendamento").click(function (e) {
        e.stopPropagation();

        var Docente = $("#slt1").val(),
			Componente = $("#slt2").val(),
			Ambiente = $("#slt3").val(),
			Turma = $("#slt4").val(),
			versaourl = window.location.valueOf().search.split("&")[2].substr(7),
			horarioInicio = $("#horaInicio").val(),
        horarioFim = $("#horaFim").val(),

        dataInicio = $("#dtInicio").val(),
        DataFim = $("#dtFim").val();

        $("#msgValidacao").text("");

        var submit = true;//Boolean(Docente && Ambiente && Componente && Turma && dataInicio && DataFim && diasSemana);

        //Validando os campos
        if (!Docente) {
            $("#msgValidacao").text("Selecione um docente.");
            submit = false;
        }
        else if (!Ambiente) {
            $("#msgValidacao").text("Selecione um ambiente.");
            submit = false;
        }
        else if (!Componente) {
            $("#msgValidacao").text("Selecione um componente.");
            submit = false;
        }
        else if (!Turma) {
            $("#msgValidacao").text("Selecione uma turma.");
            submit = false;
        }
        else if (!dataInicio) {
            $("#msgValidacao").text("Informe a data início.");
            submit = false;
        }
        else if (!DataFim) {
            $("#msgValidacao").text("Informe a data fim.");
            submit = false;
        }
        else if (!diasSemana) {
            $("#msgValidacao").text("Selecione pelo menos um dia da semana.");
            submit = false;
        }
        else if (!horarioInicio || horarioInicio == "0:00") {
            $("#msgValidacao").text("Informe a hora início.");
            submit = false;
        }
        else if (!horarioFim || horarioFim == "0:00" || horarioInicio == horarioFim) {
            $("#msgValidacao").text("Informe a hora fim e verifique se ela está diferente da hora Início.");
            submit = false;
        }

        //Submetendo se estiver validado
        if (submit) {

            $(this).button('loading');



            $("#modalAgendamento").modal("hide");

            $(this).button('reset');
            modalWait.showPleaseWait();
            setTimeout(function () {
                $.ajax({
                    url: '/PlanejamentoEscolar/CadastrarAgendamento',
                    dataType: 'json',
                    async: false,
                    data: {
                        dtInicio: $("#dtInicio").val(),
                        dtFim: $("#dtFim").val(),
                        idDocente: Docente,
                        idComponente: Componente,
                        idTurma: Turma,
                        idAmbiente: Ambiente,
                        dias: diasSemana,
                        horaInicio: horarioInicio,
                        horaFinal: horarioFim,
                        versao: versaourl

                    },
                    success: function (events) {

                        successCallback(events);
                    }
                });

            }, 100);
        } else {
            e.preventDefault();


            //$("#msgValidDiasSemana").text("Selecione pelo menos um dia da semana.");

            //modalWait.hidePleaseWait();
            //modalNotSubmited.show();
            //setTimeout(function () {
            //    modalNotSubmited.hide();
            //}, 2500);
        }
        //location.reload();
    });

    $('#tabb a').click(function (e) {
        e.preventDefault()
        $(this).tab('show')
    });

    $('#tabc a').click(function (e) {
        e.preventDefault()
        $(this).tab('show')
    });

    var date = new Date($("#ItemData").val());

    $('#tabd a').click(function (e) {
        e.preventDefault()
        $(this).tab('show')
    });

    $(".information").click(function (event) {

        diasSemana = "";
        $("#dtInicio").val("");
        $("#dtFim").val("");

        var tipoClick = $(this).find("h6").attr("tipo");
        var itemidClick = $(this).find("h6").attr("itemid");


        $("#modalAgendamento .modal-body").css("overflow-y", "visible");
        var name = $(this).find("h6").text();

        //console.log($(this).find("h6").text());
        var itemidContext = $("#ItemId").val();
        var TipoItemContext = $("#ItemTipo").val();
        var itemNome = $("#NomeItem").val();
        var that = $(this);

        //modalWait.showPleaseWait();
        $("#modalAgendamento").on("show.bs.modal", function () {
            $("#myModalLabel").text(name);
        });

        //TODO: Porque o modal está abrindo com tempo fixo
        setTimeout(function () {
            $("#modalAgendamento").modal();
            modalWait.hidePleaseWait();

            //carregarCalendario();
        }, 2000);


        switch (tipoClick) {


            case "1":

                CarregarOptions('/PlanejamentoEscolar/_OptionsDocente', itemidClick, itemidContext, TipoItemContext, that);
                break;

            case "2":

                CarregarOptions('/PlanejamentoEscolar/_OptionsTurma', itemidClick, itemidContext, TipoItemContext, that);
                break;

            case "3":

                CarregarOptions('/PlanejamentoEscolar/_OptionsComponente', itemidClick, itemidContext, TipoItemContext, that);
                break;

            case "4":

                CarregarOptions('/PlanejamentoEscolar/_OptionsAmbiente', itemidClick, itemidContext, TipoItemContext, that);
                break;
        }

        event.stopPropagation();

    });


    carregarCalendario();
});

function deletarModal() {
    $("#modalExcluir").modal('hide');
    $("#modalExcluir").on("hide", function (e) {
        e.stopPropagation();
    });
}

function deletarAgendamento(id) {

    $.ajax({
        type: "POST",
        url: "/Agendamento/ExcluirAgendamento",
        data: { "id": id },
        success: function (data) {
            if (data) {
                carregarCalendario();

            }

            location.reload();
            console.log('dbg: Retorno false.');
        },
        error: function (a, b) {
            console.log('dbg: Erro ao tentar acessar o link');
        }
    });
}

function setDatePickerMonth(m, y) {
    var inicioMes = new Date(y + "/" + (m + 1) + "/1");
    var fimMes = new Date((new Date(y + "/" + (m + 2) + "/1")).setDate(0));

    $("#dtInicio").datepicker("option", "defaultDate", inicioMes);
    //$("#dtInicio").datepicker("option", "minDate", inicioMes);
    //$("#dtInicio").datepicker("option", "maxDate", fimMes);

    $("#dtFim").datepicker("option", "defaultDate", fimMes);
    //$("#dtFim").datepicker("option", "minDate", inicioMes);
    //$("#dtFim").datepicker("option", "maxDate", fimMes);
}

function carregarCalendario() {

    var date = new Date($("#ItemData").val());
    var d = date.getDate();
    var m = date.getMonth();
    var y = date.getFullYear();

    setDatePickerMonth(m, y);

    $('#planejamento').html("");
    $('#planejamento').fullCalendar({
        year: y,
        month: m,
        header: {
            left: 'prev,next', //, today',
            center: 'title',
            right: 'month,basicWeek,basicDay'
        },
        events: function (start, end, callback) {

            var fullCalendarDate = $("#planejamento").fullCalendar('getDate');
            var m = fullCalendarDate.getMonth();
            var y = fullCalendarDate.getFullYear();

            setDatePickerMonth(m, y);

            $.ajax({
                url: '/PlanejamentoEscolar/ListarAgendamentos',
                dataType: 'json',
                cache: false,
                data: {
                    start: Math.round(start.getTime() / 1000),
                    end: Math.round(end.getTime() / 1000),

                    itemId: $("#ItemId").val(),
                    itemTipo: $("#ItemTipo").val(),
                    itemVersao: $("#ItemVersao").val(),
                    itemData: $("#ItemData").val(),
                },
                success: function (events) {
                    callback(events);

                    $(".pop").each(function () {
                        $event = $(this).parent();
                        $event.attr("data-toggle", 'tooltip');
                        $event.attr("data-placement", 'right');
                        $event.attr("data-original-title", $(this).html());
                        $event.attr("alt", 'tootip');
                    })
                    $("div[alt='tootip']").popover({ trigger: "hover", html: true });

                    $(".fc-button").click(function () {
                        $(".pop").each(function () {
                            $event = $(this).parent();
                            $event.attr("data-toggle", 'tooltip');
                            $event.attr("data-placement", 'right');
                            $event.attr("data-original-title", $(this).html());
                            $event.attr("alt", 'tootip');
                        });
                        $("div[alt='tootip']").popover({ trigger: "hover", html: true });
                    })

                }
            });

        },

        monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
        monthNamesShort: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
        dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado'],
        dayNamesShort: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado'],
        buttonText: {
            month: "mês",
            week: "semana",
            day: "dia"
        },
        //editable: true,
        viewDisplay: function (view) {

            $(".fc-day").each(function () {
                $(this).addClass("day-" + $(this).find(".fc-day-number").html());
            });

            dateArr = [];
            var today = $('#planejamento').fullCalendar('getDate');
            var viewData = $('#planejamento').fullCalendar('getView');

            cMonth = today.getMonth();
            cYear = today.getFullYear();
            $('.fc-day-number').each(function () {

                lDay = parseInt($(this).text());
                lYear = parseInt(cYear);
                //check if it is another month date
                if ($(this).parents('td').hasClass('fc-other-month')) {

                    //if it is belong to the previous month
                    /*if(lDay>15)
					{
						lMonth = parseInt(cMonth) - 1;
						lDate = new Date(lYear,lMonth,lDay);
						dateArr.push(lDate);
					}
					else //belong to the next month
					{
						lMonth = parseInt(cMonth) + 1;
						lDate = new Date(lYear,lMonth,lDay);
						dateArr.push(lDate);
					}*/
                }
                else {
                    lMonth = parseInt(cMonth);
                    lDate = new Date(lYear, lMonth, lDay);
                    dateArr.push(lDate);
                }

            });
        },
        eventRender: function (event, element) {
            //console.log(event);

            if (event.tipo == "CalendarioCivil" || event.tipo == "AgendaDocente") {

                element.html("<span class='eventTitle'> " + event.title + "</span>" +
					//"<span class='eventDescription'> " + event.description + "</span>" +
				   "<input type='hidden' class = 'idAgendamento' value='" + event.id + "'/>" +
				   "<div class='pop' style='display:none;'>" +
						"<b>" + event.title + "</b><br/>" +
						event.description +
					"</div>");
            }
            else {
                element.html(
					"<span style='float:right;' class='deleteComponente'><i class='icon-remove-circle'></i></span>" +
					"<br>" +
					"<span class='eventTitle'> " + event.componente + "</span>" +
					"<br />" +
					"<span class='eventDescription'> " + event.docente + "</span>" +
					"<br />" +
					"<span class='eventDescription'> " + event.ambiente + "</span>" +
					"<input type='hidden' class = 'idAgendamento' value='" + event.id + "'/>" +
					"<div class='pop' style='display:none;'>" +
						'<b>Componente: </b>' + event.componente +
						'<br><b>Docente: </b>' + event.docente +
						'<br><b>Ambiente:</b>' + event.ambiente +
						'<br><b>Turma:</b>' + event.turma +
						'<br><b>Início:</b>' + event.horaInicio +
						'<br><b>Fim:</b>' + event.horaFim +
					"</div>"
					);
            }

            for (var i in dateArr) {
                if (dateArr[i].getTime() == event.start.getTime()) {
                    var date = new Date(dateArr[i].getTime());
                    $('td.day-' + date.getDate()).not(".fc-other-month").addClass("dayWithEvent");
                }
            }
        }
        //eventMouseover: function (event, element) {

        //    //alert('Coordinates: ' + event.title + ',' + event.description);
        //}
    });

}

function confirmarAgendamento(pularConflitos) {

    var Docente = $("#slt1").val(),
		Ambiente = $("#slt3").val(),
		Componente = $("#slt2").val(),
		Turma = $("#slt4").val(),
		versaourl = window.location.valueOf().search.split("&")[2].substr(7),
		horarioInicio = $("#horaInicio").val()
    horarioFim = $("#horaFim").val(), diasSemana = "";
    dtini = $("#dtInicio").val(),
	dtfim = $("#dtFim").val();

    //$("#msgValidDiasSemana").text("");

    $("input[name='diasemana']:checked").each(function () {
        diasSemana += $(this).val();
    });
    //var submit = Boolean(Docente && Ambiente && Componente && Turma && dataInicio && DataFim && diasSemana);

    //Não precisa de validação, pois a confirmação acontece depois que o formulário é fechado
    //if (submit) {

    
    $("#modalConflito").modal("hide");
    $("#pleaseWaitDialog").modal("show");

    $.ajax({
        url: '/PlanejamentoEscolar/CadastrarAgendamento',
        dataType: 'json',
        cache: false,
        data: {
            dtInicio: dtini,
            dtFim: dtfim,
            idDocente: Docente,
            idComponente: Componente,
            idTurma: Turma,
            idAmbiente: Ambiente,
            dias: diasSemana,
            horaInicio: horarioInicio,
            horaFinal: horarioFim,
            pularValidacao: true,
            versao: versaourl,
            pularConflito: pularConflitos
        },
        success: function (events) {

            successCallback(events);

            //$("#pleaseWaitDialog").hide();
            //modalSalvo.show();
            //setTimeout(function () {
            //    modalSalvo.hide();
            //}, 3000);

            ////location.reload();
            //carregarCalendario();

        }
    });

    //}
}

function successCallback(events) {
    $("#pleaseWaitDialog").modal("hide");

    //setTimeout(function () {
    $(this).button('reset');

    if (events.status == false) {

        $("#modalConflito").remove();

        $("body").append("<div id='modalConflito' class='modal hide fade' tabindex='-1' role='dialog' aria-labelledby='Informação' aria-hidden='true'>" +
            "<div class='modal-header'>" +
            " <button type='button' class='close' data-dismiss='modal' aria-hidden='true'>×</button>" +
            ((!events.temOpcao) ? "<h3 id='myModalLabel'>Atenção</h3>" : "<h3 id='myModalLabel'>Conflito no Agendamento</h3>") +
            "</div>" +
            " <div class='modal-body'>" +
            " <p>" + events.msg + "</p>" +
            ((events.temOpcao) ? "<b>Como deseja proceder?</b>" : "") +
            " </div><div class='modal-footer'>" +
             " <button class='btn btn-primary' data-dismiss='modal' aria-hidden='true' onclick='$(\"#modalAgendamento\").modal(\"show\");'>Ajustar informações</button>" +

            ((events.temOpcao) ? "<button class='btn' data-dismiss='modal' aria-hidden='true' onclick='confirmarAgendamento(true)'>Agendar nos dias que não houveram conflitos</button>" : "") +
            //"<button class='btn btn-primary' data-dismiss='modal' aria-hidden='true'  onclick='confirmarAgendamento()'>Agendar mesmo que tenha conflito</button>" +

            " </div>" +
            " </div>");

        //$("#modalExcluir").ready(function () {
        $("#modalConflito").modal({ backdrop: 'static', keyboard: true });

        //});

    } else {
        modalSalvo.show();
        setTimeout(function () {
            modalSalvo.hide();
        }, 3000);

        //location.reload();
        carregarCalendario();
    }
    //}, 100);
}

function CarregarOptions(url, itemidClick, itemidContext, TipoItemContext, that) {
    $("input[name=diasemana]").attr("checked", false);
    $("#horaInicio").val("0:00");
    $("#horaFim").val("0:00");

    $.ajax({
        url: url,
        dataType: 'json',
        cache: false,
        data: {

            itemId: itemidClick,
            itemIdContext: itemidContext,
            tipoItemContext: TipoItemContext


        },
        success: function (events) {
            console.log(events);
            var i, myOptionComponente = {}, myOptionAmbiente = {},
				myOptionTurma = {}, myOptionDocente = {}, sltDocente = $('#slt1'),
				sltComponente = $('#slt2'), sltAmbiente = $('#slt3'),
				sltTurma = $('#slt4');

            //limpando select
            sltAmbiente.find("option").remove();
            sltComponente.find("option").remove();
            sltTurma.find("option").remove();
            sltDocente.find("option").remove();
            //limpando inputs



            //componente

            // verificando se veio null
            if (!events.listaComponente.length) {
                submit = false;
                sltComponente.append(
									   $('<option selected></option>').val("")
									   .text("Não há componentes relacionados")
								   );
            } else {
                submit = true;
                for (i = 0; i < events.listaComponente.length; i++) {

                    if (i === 0) {
                        sltComponente.append(
							$('<option selected></option>').val(events.listaComponente[i].IdComponente)
							.html(events.listaComponente[i].Nome)
						);
                    } else {
                        sltComponente.append(
							$('<option></option>').val(events.listaComponente[i].IdComponente)
							.html(events.listaComponente[i].Nome)
						);
                    }

                }
            }


            //Ambiente

            if (events.listaAmbientes.length) {

                for (i = 0; i < events.listaAmbientes.length; i++) {

                    if (i === 0) {
                        sltAmbiente.append(
							$('<option selected></option>').val(events.listaAmbientes[i].IdAmbiente)
							.html(events.listaAmbientes[i].Nome)
						);
                    } else {
                        sltAmbiente.append(
							$('<option></option>').val(events.listaAmbientes[i].IdAmbiente)
							.html(events.listaAmbientes[i].Nome)
						);
                    }


                }
            } else {

                sltAmbiente.append(
							$('<option selected></option>').text("Não há ambientes relacionados").val("")
						);
            }

            //Turma
            if (events.listaTurma.length) {

                for (i = 0; i < events.listaTurma.length; i++) {
                    console.log("entrou")
                    if (i === 0) {
                        sltTurma.append(
							$('<option selected></option>').val(events.listaTurma[i].IdTurma)
							.html(padLeft(events.listaTurma[i].IdTurma, "00000") + " - " + events.listaTurma[i].Nome)
						);
                    } else {
                        sltTurma.append(
							$('<option></option>').val(events.listaTurma[i].IdTurma)
							.html(padLeft(events.listaTurma[i].IdTurma, "00000") + " - " + events.listaTurma[i].Nome)
						);
                    }


                }
            } else {

                sltTurma.append(
						   $('<option selected></option>').text("Não há turmas relacionadas").val("")
						   );
            }

            //Docente
            if (events.listaDocentes.length) {

                for (i = 0; i < events.listaDocentes.length; i++) {

                    if (i === 0) {
                        sltDocente.append(
							$('<option selected></option>').val(events.listaDocentes[i].IdDocente)
							.html(events.listaDocentes[i].Nome)
						);
                    } else {
                        sltDocente.append(
							$('<option></option>').val(events.listaDocentes[i].IdDocente)
							.html(events.listaDocentes[i].Nome)
						);
                    }


                }
            } else {

                sltDocente.append(
							$('<option selected></option>').text("Não há docentes relacionados").val("")
						);
            }



            //Lista componente
            //$.each(myOptionsSlt1, function (val, text) {
            //    sltComponente.append(
            //        $('<option></option>').val(val).html(text)
            //    );
            //});
            //lista ambiente
            //$.each(myOptionsSlt1, function (val, text) {
            //    sltAmbiente.append(
            //        $('<option></option>').val(val).html(text)
            //    );
            //});



            //callback(events);

        }
    });

}
var modalWait = (function () {
    var pleaseWaitDiv = $('<div class="modal hide" id="pleaseWaitDialog" data-backdrop="static" data-keyboard="false"><div class="modal-header"><h1>Processando...</h1></div><div class="modal-body"><div class="progress progress-striped active"><div class="bar" style="width: 100%;"></div></div></div></div>');
    return {
        showPleaseWait: function () {
            pleaseWaitDiv.modal();
        },
        hidePleaseWait: function () {
            pleaseWaitDiv.modal('hide');

        },

    };
})();

var modalSalvo = (function () {
    var pleaseWaitDiv = $('<div class="modal hide" id="pleaseWaitDialog" data-backdrop="static" data-keyboard="false"><div class="modal-header"><h1>Agendamento salvo com sucesso.</h1></div><div class="modal-body"></div></div>');

    return {
        show: function () {
            pleaseWaitDiv.modal();
        },
        hide: function () {
            pleaseWaitDiv.modal('hide');
            //location.reload();
        },

    };
})();

var modalNotSubmited = (function () {
    var pleaseWaitDiv = $('<div class="modal hide" id="pleaseWaitDialog" data-backdrop="static" data-keyboard="false"><div class="modal-header"><h6>Não foi possível efetuar o agendamento, pois, faltam itens.</h6></div><div class="modal-body"></div></div>');
    return {
        show: function () {
            pleaseWaitDiv.modal();
        },
        hide: function () {
            pleaseWaitDiv.modal('hide');
            //location.reload();
        },

    };
})();

function padLeft(value, padFormat) {
    var str = "" + value;
    var pad = padFormat || "00000";
    return pad.substring(0, pad.length - str.length) + str
}