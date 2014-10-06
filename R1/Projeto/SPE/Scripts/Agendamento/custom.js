
var DatasCache = [];
function CarregarCalendario(datas, dataAtual) {
    DatasCache = datas;

    if (!dataAtual) {
        var anoAtual = new Date().getFullYear(),
            dataAtual = new Date(anoAtual + "/01/01");
        JCalendario.setDate(dataAtual);
    }

    $('#calendario').jCal({
        day: dataAtual,
        days: 0,
        showMonths: 12,
        monthSelect: false,
        dow: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb'],		// days of week - change this to reflect your dayOffset
        ml: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
        ms: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
        customDay: function (day) {

            for (var i in datas) {
                if (day.getDate() == datas[i].start.getDate() && day.getMonth() == datas[i].start.getMonth() && day.getYear() == datas[i].start.getYear()) {
                    //return "<strong><a href='/Calendario/DeletarFeriado?id=" + datas[i].id + "' onclick='return confirm(\"Deseja excluir esta data?\");'>" + day.getDate() + "</a></strong>";
                    var Brocador = "";

                    if ($('#dataN' + datas[i].IdAgendamento + '').html) {


                        var variavel = "";

                        if (datas[i].conflito) {
                            //Tooltip do Conflito
                            variavel += "<div class='customTooltip' id='dataN" + datas[i].IdAgendamento + "'><h4>Conflito!</h4></div>";

                            //Colocando icone quando possuir conflito
                            variavel += "<div style=\"background: url('/Images/icoWarning.png');" +
                                "background-size: 100%;  " +
                                "background-repeat: no-repeat;  " +
                                "width: 15px;" +
                                "height: 15px;" +
                                "position: absolute;" +
                                "margin-left: 18px;" +
                                "margin-top: -8px;" +
                                "z-index: 999;\"" +
                                "></div>";

                            //variavel += "<img src='/Images/icoWarning.png' style='" +                               
                            //   "width: 15px;" +
                            //   "height: 15px;" +
                            //   "position: absolute;" +
                            //   "margin-left: 18px;" +
                            //   "margin-top: -8px;" +
                            //   "z-index: 999;\"" +
                            //   "/>";

                        }
                        else {
                            if (!datas[i].tipo) {
                                variavel = "<div class='customTooltip' id='dataN" + datas[i].IdAgendamento + "'>" +
                                        "<input type='hidden' value='" + datas[i].originalStart + "' class='start' />" +
                                        "<input type='hidden' value='" + datas[i].originalEnd + "' class='end' />" +
                                        "<input type='hidden' value='" + datas[i].description + "' class='description' />" +
                                        "<div class=\"label label-info\" data-toggle=\"tooltip\" data-placement=\"top\">" +
                                        "Componente:" + datas[i].componente +
                                    "</br>Docente:" + datas[i].docente +
                                    "</br>Ambiente:" + datas[i].ambiente +
                                    "</br>Turma:" + datas[i].turma +
                                    "</br>Inicio:" + datas[i].horaInicio +

                                    "</br>Fim:" + datas[i].horaFim +
                                       // "</div>" + "<div class=\"btn\" data-toggle=\"tooltip\" data-placement=\"top\" onclick='editarEvento(" + datas[i].id + "); return false;' title=\"Editar\" data-original-title=\"Editar\" alt=\"Editar\" href=\"\">" +
                                       //     "<i class=\"icon-edit\" alt=\"editar\"></i>" +
                                       // "</div>" +
                                       // "<div class=\"btn\" data-toggle=\"tooltip\" data-placement=\"top\" onclick='excluirAgendamento(" + datas[i].IdAgendamento + "); return false;' title=\"Excluir\" data-original-title=\"Excluir\" alt=\"Excluir\">" +
                                       //"<i class=\"icon-ban-circle\"></i>" +
                                        "</div>" +
                                        "</div>";
                            } else {
                                variavel = "<div class='customTooltip' id='dataN" + datas[i].IdAgendamento + "'>" +
                                      datas[i].description +
                                      "</div>";

                            }
                        }

                        variavel += "<strong>" + (day.getDate()) + "</strong>";
                    }

                    return variavel;
                    continue;
                }
            }

            return false;
        },
        customStyle: function (day) {
            for (var i in datas) {
                if (day.getDate() == datas[i].start.getDate() && day.getMonth() == datas[i].start.getMonth() && day.getYear() == datas[i].start.getYear()) {
                    return "position:relative; background:" + datas[i].color;
                }
            }
            return false;
        },
        dCheck: function (day) {
            for (var i in datas) {
                if (day.getDate() == datas[i].start.getDate() && day.getMonth() == datas[i].start.getMonth() && day.getYear() == datas[i].start.getYear()) {
                    return 'day dayHover';
                }
            }
            return 'day';
        }
        //callback: function (day, days) {
        //    return true;
        //}
    });

    $(".dayHover").hover(function () {
        $(this).find('.customTooltip').show();
    }, function () {
        $(this).find('.customTooltip').hide();
    });

    $("#Cancelar").click(function () {
        $("#editarFeriado").hide();

        $("#idFeriado").val("");
        $("#edit_datainicio").val("");
        $("#edit_datafim").val("");
        $("#edit_IdLegenda").val("");
        $("#edit_dataDesc").val("");
    });
}

var verifyEmptyFields = function () {
    var datainicio = $("#dataini"), datafim = $("#datafim"), desc = $("#dataDesc"), isSubmittable = true, docente = $("#IdDocente").val();

    if (!datainicio.val()) {
        ModalALert("Data vazia", "Compo data inicio não pode ser vazio");
        isSubmittable = false;
    } else if (!datafim.val()) {
        ModalALert("Data vazia", "Compo data fim não pode ser vazio");
        isSubmittable = false;
    } else if (!desc) {
        ModalALert("Descrição vazia", "Compo descrição não pode ser vazio");
        isSubmittable = false;
    } else if (docente === "0") {
        ModalALert("Docente", "Selecione um docente");
        isSubmittable = false;
    }
    return isSubmittable;
};

var buscouAgendamentos = false;
$(document).ready(function () {

    CarregarCalendario();

    $(".jCalMo > .jCal").delegate("div", "click", function () {
        //setup variables

        if (!buscouAgendamentos) {
            ModalALert("Atenção", "Primeiramente você deve selecionar os campos e clicar em buscar.")
            console.info()
        }

    });

    //$.get("/AgendaEvento/GetEventos?idDocente=" + idDocente, function (data) {
    //    for (var i in data) {
    //        data[i].start = new Date(data[i].start);
    //        data[i].start.setDate(data[i].start.getDate() + 1);
    //    }
    //    CarregarCalendario(data);
    //});

});

function excluirFeriado(id) {
    if (confirm("Deseja excluir esta data?")) {
        window.location = "/AgendaEvento/DeletarEvento?id=" + id;
    }

    return false;
}

function TipoSelecionado() {
    var tipo = $("#drpTipo :selected").val();
    $('#drpItem').attr('disabled', 'disabled');

    $.post("/Agendamento/GetListItem",
            {
                idNumero: tipo
            },
            function (data) {
                switch (tipo) {
                    case '1':

                        $('#drpItem').html("");
                        $('#drpItem').append(new Option("Selecione", 0, true, false));
                        for (var i = 0; i < data.length; i++) {
                            $('#drpItem').append(new Option(data[i].Nome, data[i].IdAmbiente, true, false));
                        }
                        $('#drpItem').removeAttr('disabled');
                        ItemSelecionado()

                        break;
                    case '2':

                        $('#drpItem').html("");
                        $('#drpItem').append(new Option("Selecione", 0, true, false));
                        for (var i = 0; i < data.length; i++) {
                            if (data[i]) {
                                $('#drpItem').append(new Option(data[i].Nome, data[i].IdComponente, true, false));
                            }
                        }
                        $('#drpItem').removeAttr('disabled');
                        ItemSelecionado()

                        break;
                    case '3':

                        $('#drpItem').html("");
                        $('#drpItem').append(new Option("Selecione", 0, true, false));
                        for (var i = 0; i < data.length; i++) {
                            $('#drpItem').append(new Option(data[i].Nome, data[i].IdDocente, true, false));
                        }
                        $('#drpItem').removeAttr('disabled');
                        ItemSelecionado()

                        break;
                    case '4':

                        $('#drpItem').html("");
                        $('#drpItem').append(new Option("Selecione", 0, true, false));
                        for (var i = 0; i < data.length; i++) {
                            $('#drpItem').append(new Option('Turma ' + data[i].IdTurmaZero + ' - ' + data[i].NomeMatriz, data[i].IdTurma, true, false));
                        }
                        $('#drpItem').removeAttr('disabled');
                        ItemSelecionado()

                        break;
                    default:
                }
            });

}

function ItemSelecionado() {
    var e = $('#drpItem');

    if (e.val() && e.val() != 0 && e.val() != "0" && e.val() != "Selecione") {
        $('#btnBuscarAgendamento').removeAttr('disabled');
    } else {
        $('#btnBuscarAgendamento').attr('disabled', 'disabled');
    }
}

function onClickCalendarDay(jCalHeader)
{
    var month = jCalHeader.find(".monthNumber").text();
    var year = jCalHeader.find(".monthYear").text();

    var type = $("#drpTipo").val(), itemVal = $("#drpItem").val(), version = $("#drpVersao").val();

    if (version !== "Selecione") {
        console.log("Enviando post");

        window.location = "/PlanejamentoEscolar/Index?tipo=" + type + "&item=" + itemVal + "&versao=" + version + "&mes=" + month + "&ano=" + year;

        //$.post("/PlanejamentoEscolar/Index", { item: itemVal, versao: version, tipo: type });
    } else {
        ModalALert("Versão", "Selecione uma versão")
        console.info()
    }
}

function BuscarAgendamento() {
    var botaoBuscar = $('#btnBuscarAgendamento');
    var id = $("#drpItem :selected").val();
    var tipo = $("#drpTipo :selected").val();
    var versao = $("#drpVersao").val();

    botaoBuscar.button('loading');
    modalWait.showPleaseWait();

    $.post("/Agendamento/GetEventos",
                {
                    tipo: tipo,
                    item: id,
                    versao: versao

                },
                function (data) {
                    buscouAgendamentos = true;

                    for (var i in data) {
                        data[i].start = new Date(data[i].start.replace(/-/g, "/"));
                        //data[i].start.setDate(data[i].start.getDate() + 1);
                    }

                    CarregarCalendario(data);
                    var novaDataAno = new Date().getFullYear();
                    JCalendario.setDate(new Date(novaDataAno + '/01/01'));


                    $(".jCalMo").delegate(".day", "click", function () {

                        var jCalHeader = $(this).parent(".jCalMo").find(".jCal");

                        onClickCalendarDay(jCalHeader);
                    });

                    $(".jCalMo > .jCal").delegate("div", "click", function () {
                        //setup variables
                        var calendar = $(this);
                        onClickCalendarDay(jCalHeader);

                    });
                }).done(function () {
                    modalWait.hidePleaseWait();
                    botaoBuscar.button('reset');
                });

}

function editarEvento(id) {
    var start = transformDate($("#dataN" + id).find(".start").val());
    var end = transformDate($("#dataN" + id).find(".end").val());
    var idDocente = $("#dataN" + id).find("#IdDocente").val();
    var description = $("#dataN" + id).find(".description").val();
    $("#editarEvento").show();

    $("#idEvento").val(id);
    $("#edit_datainicio").val(start);
    $("#edit_datafim").val(end);
    $("#edit_IdEvento").val(idLegenda);
    $("#edit_dataDesc").val(description);

}

function transformDate(date) {
    if (date != "") {
        date = date.split("-");
        date = date[2] + "/" + date[1] + "/" + date[0];
    }
    return date;
}

var JCalendario = (function () {

    var currentDate = new Date(new Date().getFullYear() + "/01/01");

    function updateCalendar(modif) {

        var id = $("#drpItem :selected").val();
        var tipo = $("#drpTipo :selected").val();
        var versao = $("#drpVersao").val()

        if (id && tipo && versao && id != "Selecione" && tipo != "Selecione") {

            var year = currentDate.getFullYear() + modif;
            currentDate.setFullYear(year);

            $.post("/Agendamento/GetEventos",
                        {
                            tipo: tipo,
                            item: id,
                            versao: versao,
                            ano: year

                        },
                        function (data) {

                            for (var i in data) {
                                data[i].start = new Date(data[i].start.replace(/-/g, "/"));
                                //data[i].start.setDate(data[i].start.getDate() + 1);
                            }

                            CarregarCalendario(data, currentDate);

                            $(".jCalMo").delegate(".day", "click", function () {

                                var jCalHeader = $(this).parent(".jCalMo").find(".jCal");

                                onClickCalendarDay(jCalHeader);
                            });

                            $(".jCalMo > .jCal").delegate("div", "click", function () {
                                //setup variables
                                var calendar = $(this);
                                onClickCalendarDay(jCalHeader);

                            });
                        });

        }
    }

    return {
        backYear: function () {

            updateCalendar(-1);
        },
        addYear: function () {

            updateCalendar(1);
        },
        setDate: function (date) {
            currentDate = date;
        }
    }
})()

var modalWait = (function () {
    var pleaseWaitDiv = $('<div class="modal hide" id="pleaseWaitDialog" data-backdrop="static" data-keyboard="false"><div class="modal-header"><h1>Processando...</h1></div><div class="modal-body"><div class="progress progress-striped active"><div class="bar" style="width: 100%;"></div></div></div></div>');
    return {
        showPleaseWait: function () {
            pleaseWaitDiv.modal();
        },
        hidePleaseWait: function () {
            pleaseWaitDiv.modal('hide');

        }

    };
})();
