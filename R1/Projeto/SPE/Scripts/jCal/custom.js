$(document).ready(function () {

    $("#datainicio").datepicker({
        onSelect: function (date) {
            var date2 = $('#datainicio').datepicker('getDate');
            date2.setDate(date2.getDate());
            $('#datafim').datepicker('setDate', date2);
            //sets minDate to dt1 date + 1
            $('#datafim').datepicker('option', 'minDate', date2);
        }
    });

    $("#edit_datainicio").datepicker({
        onSelect: function (date) {
            var date2 = $('#edit_datainicio').datepicker('getDate');
            date2.setDate(date2.getDate());
            $('#edit_datafim').datepicker('setDate', date2);
            //sets minDate to dt1 date + 1
            $('#edit_datafim').datepicker('option', 'minDate', date2);
        }
    });

    $("#datafim").datepicker();//{
    //    onClose: function () {
    //        var dt1 = $('#datainicio').datepicker('getDate');
    //        //console.log(dt1);
    //        var dt2 = $('#datafim').datepicker('getDate');
    //        if (dt2 <= dt1) {
    //            var minDate = $('#datafim').datepicker('option', 'minDate');
    //            $('#datafim').datepicker('setDate', minDate);
    //        }
    //    }
    //});

    $("#edit_datafim").datepicker();//{
    //    onClose: function () {
    //        var dt1 = $('#edit_datainicio').datepicker('getDate');
    //        //console.log(dt1);
    //        var dt2 = $('#edit_datafim').datepicker('getDate');
    //        if (dt2 <= dt1) {
    //            var minDate = $('#edit_datafim').datepicker('option', 'minDate');
    //            $('#edit_datafim').datepicker('setDate', minDate);
    //        }
    //    }
    //});

    $("#Cadastrar").click(function () {

        var botao = $(this);
        botao.button('loading');

        var dataIni = "", dataFim = "",
            data = $("#datainicio").val(),
            data2 = $("#datafim").val(),
            isFieldsValid = true,
            fields = [
                 $("#datainicio"),
                 $("#datafim"),
                 $("#dataDesc")
            ];

        var retornoFalse = true;
        $(fields).each(function () {
            if (!$(this).val()) {
                IncluirModal("Campo " + $(this).attr("title") + " não pode ser vazio");
                isFieldsValid = false;

                retornoFalse = false;
                return false;
            }
        });
        if (!retornoFalse) {
            botao.button('reset');
            return retornoFalse;

        }

        var dataSplit = data.split("/");
        var data2Split = data2.split("/");

        dataIni = new Date(dataSplit[2], parseInt(dataSplit[1]) - 1, dataSplit[0]);
        dataFim = new Date(data2Split[2], parseInt(data2Split[1]) - 1, data2Split[0]);

        if (dataIni > dataFim) {

            ModalALert("Erro de data", "A Data de Fim deve ser maior que a Data Início");
            //botao.removeAttr("disabled");
            botao.button('reset');


        } else if (isFieldsValid) {

            $.post("/Calendario/CadastrarFeriado",
                {
                    DataInicial: data,
                    DataFim: data2,
                    IdLegenda: $("#IdLegenda").val(),
                    Descricao: $("#dataDesc").val()
                },
                function () {

                    //Limpando os campos
                    $(fields).each(function () {
                        $(this).val("");
                    });

                    CarregarDadosCalendario();
                    IncluirModal("Salvo com sucesso");
                    botao.button('reset');
                    //botao.removeAttr("disabled");
                });
        }
    });

    $(".deletarData").click(function () {
        $.post("/Calendario/DeletarFeriado",
            {
                id: 1
            },
            function () {
                $.ajax({
                    url: "/Calendario/GetFeriados",
                    type: "GET",
                    cache: false,
                    success: function (data) {
                        for (var i in data) {
                            data[i].start = new Date(data[i].start.replace(/-/g, "/"));
                            // data[i].start.setDate(data[i].start.getDate() + 1);
                        }
                        CarregarCalendario(data);
                    }
                });

            });
    });

    $("#Cancelar").click(function () {

        $("#formAdicionar").show();
        $("#formAdicionar").effect("highlight", undefined, 500);

        $("#editarFeriado").hide();

        $("#idFeriado").val("");
        $("#edit_datainicio").val("");
        $("#edit_datafim").val("");
        $("#edit_IdLegenda").val("");
        $("#edit_dataDesc").val("");

    });

    $("#btnEditarFeriado").click(function (e) {
        e.preventDefault();
        var botao = $(this);

        var fields = [
                 $("#datainicio"),
                 $("#datafim"),
                 $("#dataDesc")
        ];
       
        botao.button('loading');

        if (!verifyEmptyFields(true)) {
            //e.preventDefault();
            botaoSalvar.button('reset');
            return false
        }
        else
        {
            $.post("/Calendario/EditarFeriado",
                {
                    DataInicial: $("#edit_datainicio").val(),
                    DataFim: $("#edit_datafim").val(),
                    IdLegenda: $("#IdLegenda").val(),
                    Descricao: $("#edit_dataDesc").val(),
                    IdCalendarioCivil: $("#idFeriado").val()
                },
                function () {

                    //Limpando os campos
                    $(fields).each(function () {
                        $(this).val("");
                    });

                    CarregarDadosCalendario();
                    IncluirModal("Salvo com sucesso");
                    botao.button('reset');
                    botao.removeAttr("disabled");

                    hideFormEdit();
                });
        }

    });


    CarregarDadosCalendario();
});

var DatasCache = undefined;

var JCalendario = (function () {

    var currentDate = new Date(new Date().getFullYear() + "/01/01");

    function updateCalendar(modif) {

        var year = currentDate.getFullYear() + modif;
        currentDate.setFullYear(year);

        CarregarDadosCalendario(year);
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

function CarregarDadosCalendario(ano) {

    var url = "/Calendario/GetFeriados";
    if (ano) {
        url = "/Calendario/GetFeriados?ano=" + ano
    }

    $.ajax({
        url: url,
        type: "GET",
        cache :false,
        success: function (data) {
            for (var i in data) {
                data[i].start = new Date(data[i].start.replace(/-/g, "/"));
                //data[i].start.setDate(data[i].start.getDate() + 1);
            }

            var novaData = ano ? new Date(ano + '/01/01') : null;
            CarregarCalendario(data, novaData);
            CarregarFeriados(data, novaData);
        }
    });
}

function CarregarFeriados(data) {
    template = "<p><i>Legenda:</i></p>";
    var jafoi = new Array();

    for (var i = 0; i < data.length; i++) {
        var item = data[i];

        var exist = false;

        for (var j = 0; j < jafoi.length; j++) {
            if (jafoi[j] == item.id) {
                exist = true;
                break;
            }
        }

        if (!exist) {

            template += '<div class="itemData">' +
                '<div class=""><strong>' + item.description + '</strong></div>' +
                '<div class="">' + formatDate(item.originalStart) + '</div>' +
                '<div class="">' + formatDate(item.originalEnd) + '</div>' +
                '</div>';
            jafoi.push(item.id);
        }
    }

    $('#datasFeriadosSPE').html(template);
}

function formatDate(strDate) {
    var formated = "";
    if (strDate) {
        var splited = strDate.split('-');
        formated = splited[2] + "/" + splited[1] + "/" + splited[0];
    }
    return formated;
}

function CarregarCalendario(datas, dataAtual) {
    DatasCache = datas;
    //dataCalendario = new Date();
    //if (!dataAtual) { dataAtual = dataCalendario.getFullYear() + "-02-01" }
    if (!dataAtual) {
        var anoAtual = new Date().getFullYear(),
            dataAtual = new Date(anoAtual + "/01/01");
        JCalendario.setDate(dataAtual);
    }
    console.log(new Date(dataAtual));

    $('#calendario').jCal({
        day: new Date(dataAtual),//$(".actualDate").click(function () {return new Date($(this).val()) }),
        days: 0,
        showMonths: 12,
        monthSelect: true,
        dow: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb'],		// days of week - change this to reflect your dayOffset
        ml: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
        ms: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
        customDay: function (day) {
            for (var i = 0; i < datas.length; i++) {

                if (day.getDate() == datas[i].start.getDate() && day.getMonth() == datas[i].start.getMonth() && day.getYear() == datas[i].start.getYear()) {
                    //return "<strong><a href='/Calendario/DeletarFeriado?id=" + datas[i].id + "' onclick='return confirm(\"Deseja excluir esta data?\");'>" + day.getDate() + "</a></strong>";
                    return (
                    "<div class='customTooltip' id='dataN" + datas[i].id + "'>" +
                        "<input type='hidden' value='" + datas[i].originalStart + "' class='start' />" +
                        "<input type='hidden' value='" + datas[i].originalEnd + "' class='end' />" +
                        "<input type='hidden' value='" + datas[i].description + "' class='description' />" +
                        "<input type='hidden' value='" + datas[i].idLegenda + "' class='idLegenda' />" +

                        "<div class='label label-info' data-toggle='tooltip' data-placement='top'>" + datas[i].description + "</div><br>" +
                        "<div class=\"btn\" data-toggle=\"tooltip\" data-placement=\"top\" onclick='editarFeriado(" + datas[i].id + "); return false;' title=\"Editar\" data-original-title=\"Editar\" alt=\"Editar\" href=\"\">" +
                            "<i class=\"icon-edit\" alt=\"editar\"></i>" +
                        "</div>" +
                        "<div class=\"btn\" data-toggle=\"tooltip\" data-placement=\"top\" onclick='excluirFeriado(" + datas[i].id + "); return false;' title=\"Excluir\" data-original-title=\"Excluir\" alt=\"Excluir\">" +
                            "<i class=\"icon-ban-circle\"></i>" +
                        "</div>" +
                    "</div>" +
                    "<strong>" + (day.getDate()) + "</strong>");
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

function excluirFeriado(id) {

    IncluirModalConfirm("Excluir data", "Deseja excluir esta data?", function () {

        $.ajax({
            url: "/Calendario/DeletarFeriado?id=" + id,
            type: "GET",
            cache: false,
            success: function () {

                hideFormEdit();
                CarregarDadosCalendario();
                ModalALert("Excluido com sucesso", "Evento excluido com sucesso");
            }
        });
    });

};

function editarFeriado(id) {

    var start = transformDate($("#dataN" + id).find(".start").val());
    var end = transformDate($("#dataN" + id).find(".end").val());
    if (!end) { end = start }

    var idLegenda = $("#dataN" + id).find(".idLegenda").val();
    var description = $("#dataN" + id).find(".description").val();

    $("#formAdicionar").hide();

    $("#editarFeriado").show();
    $("#editarFeriado").effect("highlight", undefined, 1000);

    $("#idFeriado").val(id);
    $("#edit_datainicio").val(start);
    $("#edit_datafim").val(end);

    var date2 = $('#edit_datainicio').datepicker('getDate');   
    $("#edit_datafim").datepicker('option', 'minDate', date2);

    $("#edit_IdLegenda").val(idLegenda);
    $("#edit_dataDesc").val(description);
}

function hideFormEdit() {
    $("#formAdicionar").show();
    $("#editarFeriado").effect("highlight", undefined, 1000);

    $("#editarFeriado").hide();
    $("#idFeriado").val("");
    $("#edit_datainicio").val("");
    $("#edit_datafim").val("");
    $("#edit_IdLegenda").val("");
    $("#edit_dataDesc").val("");
}

function transformDate(date) {
    if (date != "") {
        date = date.split("-");
        date = date[2] + "/" + date[1] + "/" + date[0];
    }
    return date;
}

var verifyEmptyFields = function (isEdit) {
    if (!isEdit) {
        var datainicio = $("#dataini"), datafim = $("#datafim"),
        desc = $("#dataDesc"), isSubmittable = true,
        docente = $("#IdDocente").val();

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
        }
    }
    if (isEdit) {
        var datainicio = $("#edit_datainicio"), datafim = $("#edit_datafim"),
        desc = $("#edit_dataDesc"), isSubmittable = true,
        tipoLegenda = $("#edit_IdLegenda").val();

        if (!datainicio.val()) {
            ModalALert("Data vazia", "Campo data inicio não pode ser vazio");
            isSubmittable = false;
        } else if (!datafim.val()) {
            ModalALert("Data vazia", "Campo data fim não pode ser vazio");
            isSubmittable = false;
        } else if (!desc.val()) {
            ModalALert("Descrição vazia", "Campo descrição não pode ser vazio");
            isSubmittable = false;
        } else if (tipoLegenda == "0") {
            ModalALert("Tipo", "Selecione um tipo");
            isSubmittable = false;
        }
    }


    return isSubmittable;
};
