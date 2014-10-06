/// <reference path="../base.SPE.js" />
function setDocente() {
    var id = $("#IdDocente").val();

    $("#msg").html("Carregando dados do calendário...");

    $.ajax({
        url: "/AgendaEvento/GetEventos?idDocente=" + id,
        type: "GET",
        cache: false,
        success: function (data) {
            for (var i in data) {
                data[i].start = new Date(data[i].start.replace(/-/g, "/"));
                //data[i].start.setDate(data[i].start.getDate() + 1);
            }
            CarregarCalendario(data);
            $("#msg").html("");

        }, error: function () {
            $("#msg").html("");
        }
    });
}


function CarregarCalendario(datas, dataAtual) {
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

                    var htmlItem = "<div class='customTooltip' id='dataN" + datas[i].id + "'>" +
                            "<input type='hidden' value='" + datas[i].originalStart + "' class='start' />" +
                            "<input type='hidden' value='" + datas[i].originalEnd + "' class='end' />" +
                            "<input type='hidden' value='" + datas[i].description + "' class='description' />" +
                            "<input type='hidden' value='" + datas[i].idLegenda + "' class='idTipo' />" +
                            "<div class=\"label label-info\" data-toggle=\"tooltip\" data-placement=\"top\">" + datas[i].description + "</div>";

                    if (!datas[i].tipo) {

                        htmlItem += "<div class=\"btn\" data-toggle=\"tooltip\" data-placement=\"top\" onclick='editarEvento(" + datas[i].id + "); return false;' title=\"Editar\" data-original-title=\"Editar\" alt=\"Editar\" href=\"\">" +
                                    "<i class=\"icon-edit\" alt=\"editar\"></i>" +
                                "</div>" +
                                "<div class=\"btn\" data-toggle=\"tooltip\" data-placement=\"top\" onclick='excluirEvento(" + datas[i].id + "); return false;' title=\"Excluir\" data-original-title=\"Excluir\" alt=\"Excluir\">" +
                                    "<i class=\"icon-ban-circle\"></i>" +
                                "</div>";
                    }


                    htmlItem += "</div>" +
                            "<strong>" + (day.getDate()) + "</strong>";

                    return htmlItem;
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


}

var verifyEmptyFields = function (isEdit) {
    if (!isEdit) {
        var datainicio = $("#dataini"), datafim = $("#datafim"),
        desc = $("#dataDesc"), isSubmittable = true,
        docente = $("#IdDocente").val(),
        tipo = $("#TipoEvento").val();

        if (!datainicio.val()) {
            ModalALert("Data vazia", "Campo data inicío não pode ser vazio");
            isSubmittable = false;
        } else if (!datafim.val()) {
            ModalALert("Data vazia", "Campo data fim não pode ser vazio");
            isSubmittable = false;
        } else if (!desc.val()) {
            ModalALert("Descrição vazia", "Campo descrição não pode ser vazio");
            isSubmittable = false;
        } else if (docente == "0") {
            ModalALert("Docente", "Selecione um docente");
            isSubmittable = false;
        } else if (!tipo) {
            ModalALert("Docente", "Selecione um tipo");
            isSubmittable = false;
        }

    } else {
        var datainicio = $("#editDataIni"), datafim = $("#editDataFim"),
        desc = $("#edit_dataDesc"), isSubmittable = true,
        docente = $("#IdDocente").val(),
        tipo = $("#TipoEventoEditar").val();

        if (!datainicio.val()) {
            ModalALert("Data vazia", "Campo data inicio não pode ser vazio");
            isSubmittable = false;
        } else if (!datafim.val()) {
            ModalALert("Data vazia", "Campo data fim não pode ser vazio");
            isSubmittable = false;
        } else if (!desc.val()) {
            ModalALert("Descrição vazia", "Campo descrição não pode ser vazio");
            isSubmittable = false;
        } else if (docente == "0") {
            ModalALert("Docente", "Selecione um docente");
            isSubmittable = false;
        } else if (!tipo) {
            ModalALert("Docente", "Selecione um tipo");
            isSubmittable = false;
        }
    }


    return isSubmittable;
};

$(document).ready(function () {

    //"#dataini, #datafim, #editDataIni, #editDataFim").datepicker();
    $("#dataini").datepicker({
        onSelect: function (date) {
            var date2 = $('#dataini').datepicker('getDate');
            //date2.setDate(date2.getDate());
            $('#datafim').datepicker('setDate', date2);
            //sets minDate to dt1 date + 1
            $('#datafim').datepicker('option', 'minDate', date2);
        }
    });
    $("#datafim").datepicker();//{
    //    onClose: function () {
    //        var dt1 = $('#dataini').datepicker('getDate');
    //        //console.log(dt1);
    //        var dt2 = $('#datafim').datepicker('getDate');
    //        if (dt2 <= dt1) {
    //            var minDate = $('#datafim').datepicker('option', 'minDate');
    //            $('#datafim').datepicker('setDate', minDate);
    //        }
    //    }
    //});
    $("#editDataIni").datepicker({
        onSelect: function (date) {
            var date2 = $('#editDataIni').datepicker('getDate');
            //date2.setDate(date2.getDate());
            $('#editDataFim').datepicker('setDate', date2);
            //sets minDate to dt1 date + 1
            $('#editDataFim').datepicker('option', 'minDate', date2);
        }
    });
    $("#editDataFim").datepicker();//{
    //    onClose: function () {
    //        var dt1 = $('#editDataFim').datepicker('getDate');
    //        //console.log(dt1);
    //        var dt2 = $('#editDataFim').datepicker('getDate');
    //        if (dt2 <= dt1) {
    //            var minDate = $('#editDataFim').datepicker('option', 'minDate');
    //            $('#editDataFim').datepicker('setDate', minDate);
    //        }
    //    }
    //});
    //var idDocente = $("#IdDocente").val();
    //setDocente();

    $("#Cadastrar").click(function (e) {

        var botao = $("#Cadastrar");
        //botao.attr("disabled", "disabled");
        botao.button('loading');

        if (!verifyEmptyFields()) {

            //botao.removeAttr("disabled");
            botao.button('reset');
            e.preventDefault();
            return false;
        }

        var dataIni = "", dataFim = "",
            data = $("#dataini").val(),
            data2 = $("#datafim").val(),
            idChange = $("#IdDocente").val(),
            tipoEvento = $("#TipoEvento").val();

        var dataSplit = data.split("/");
        var data2Split = data2.split("/");

        dataIni = new Date(dataSplit[2], parseInt(dataSplit[1]) - 1, dataSplit[0]);
        dataFim = new Date(data2Split[2], parseInt(data2Split[1]) - 1, data2Split[0]);

        if (dataIni > dataFim) {

            ModalALert("Erro de data", "A Data de Fim deve ser maior que a Data Início");
            //botao.removeAttr("disabled");
            botao.button('reset');
        } else {
            $.post("/AgendaEvento/CadastrarEvento",
                {
                    DataIni: data,
                    DataFim: data2,
                    Descricao: $("#dataDesc").val(),
                    TipoEvento: tipoEvento,
                    IdDocente: $("#IdDocente").val()
                },
                function (returned) {
                    //console.log(returned);
                    if (returned) {
                        ModalALert("Colisão de datas", returned);
                        //botao.removeAttr("disabled");
                        botao.button('reset');

                        return false;
                    }
                    else {

                        //Limpar campos
                        $("#horaInicio").val("");
                        $("#horaFim").val("");
                        $("#msgValidacao").val("");

                        $("#dataini").val("");
                        $("#datafim").val("");
                        $("dataDesc").val("");
                        $("#TipoEvento").val(0);

                        IncluirModal("Data incluida com Sucesso");
                        botao.button('reset');

                        setDocente();
                    }

                }).always(function () {
                    botao.button('reset');
                    //botao.removeAttr("disabled");
                });
        }
    });

    $("#btnEditar").click(function (e) {

        e.preventDefault();

        var botao = $(this);
        botao.button('loading');



        if (verifyEmptyFields(true)) {

            var data = $("#editDataIni").val();
            var data2 = $("#editDataFim").val();

            //var dataSplit = data.split("/");
            //var data2Split = data2.split("/");

            //dataIni = new Date(dataSplit[2], parseInt(dataSplit[1]) - 1, dataSplit[0]);
            //dataFim = new Date(data2Split[2], parseInt(data2Split[1]) - 1, data2Split[0]);

            var tipoEvento = $("#TipoEventoEditar").val();

            $.post("/AgendaEvento/EditarEvento",
               {
                   DataIni: data,
                   DataFim: data2,
                   Descricao: $("#edit_dataDesc").val(),
                   TipoEvento: tipoEvento,
                   IdDocente: $("#IdDocente").val(),
                   IdAgendaEvento: $("#idAgendaEvento").val()
               },
               function (returned) {
                   //console.log(returned);


                   //Limpar campos
                   $("#horaInicio").val("");
                   $("#horaFim").val("");
                   $("#msgValidacao").val("");
                   $("#editDataIni").val("");
                   $("#editDataFim").val("");
                   $("edit_dataDesc").val("");
                   $("#TipoEventoEdit").val(0);

                   IncluirModal("Data incluida com Sucesso");
                   botao.button('reset');

                   hideFormEdit();
                   setDocente();

               }).always(function () {
                   botao.button('reset');
                   botao.removeAttr("disabled");
               });
        }
        else {
            botao.button('reset');
            botao.removeAttr("disabled");
        }
    });

    $("#CancelarEdit").click(function () {
        hideFormEdit();
    });

    //setDocente();
    CarregarCalendario();
});



function hideFormEdit() {
    $("#editarEvento").hide();

    $("#idAgendaEvento").val("");
    $("#idDocenteEdit").val("");
    $("#edit_dataDesc").val("");
    $("#editDataIni").val("");
    $("#editDataFim").val("");

    $("#idFeriado").val("");
    $("#edit_dataDesc").val("");

    $("#divAdicionar").show();
    $("#divAdicionar").effect("highlight", undefined, 1000);
}

//#refatorar Está replicado no arquivo custom de jCal
var JCalendario = (function () {

    var currentDate = new Date(new Date().getFullYear() + "/01/01");

    function updateCalendar(modif) {

        var year = currentDate.getFullYear() + modif;
        currentDate.setFullYear(year);

        var id = $("#IdDocente").val();
        $.ajax({
            url: "/AgendaEvento/GetEventos?idDocente=" + id + "&ano=" + year,
            type: "GET",
            cache: false,
            success: function (data) {
                for (var i in data) {
                    data[i].start = new Date(data[i].start.replace(/-/g, "/"));
                    //data[i].start.setDate(data[i].start.getDate() + 1);
                }
                CarregarCalendario(data, currentDate);
            }
        });
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
})();

function excluirEvento(id) {

    IncluirModalConfirm("Excluir data", "Deseja excluir esta data?", function () {

        $.ajax({
            url: "/AgendaEvento/DeletarEvento?id=" + id,
            type: "GET",
            cache: false,
            success: function () {

                hideFormEdit();
                setDocente();
                ModalALert("Excluido com sucesso", "Evento excluido com sucesso");
            }
        });
    });

    return false;
}

function editarEvento(id) {
    var start = transformDate($("#dataN" + id).find(".start").val()),
        end = transformDate($("#dataN" + id).find(".end").val()),
        idDocente = $("#IdDocente").val(),
        description = $("#dataN" + id).find(".description").val(), tipo = $("#dataN" + id).find(".idTipo").val();

    if (!end) { end = start }

    $("#editarEvento").show();
    $("#idAgendaEvento").val(id);
    $("input[name='DataIni']").val(start);
    $("input[name='DataFim']").val(end);

    var date2 = $("input[name='DataIni']").datepicker('getDate');
    $("input[name='DataFim']").datepicker('option', 'minDate', date2);

    $("#idDocenteEdit").val(idDocente);
    $("#edit_dataDesc").val(description);
    $("#TipoEventoEditar").find("option[value=" + tipo + "]").attr("selected", '');

    $("#divAdicionar").hide();

    $("#editarEvento").effect("highlight", undefined, 1000);
}

function transformDate(date) {
    if (date != "") {
        date = date.split("-");
        date = date[2] + "/" + date[1] + "/" + date[0];
    }
    return date;
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
