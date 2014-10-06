/// <reference path="../jquery-1.8.2.js" />
/// <reference path="../xdate.js" />


$(document).ready(function () {

    $("#HoraIniView,#HoraFimView").mask("99:99");
    $("#HoraAmbienteInicial").mask("99:99");
    $("#HoraAmbienteFinal").mask("99:99");

    $("#HoraDocenteInicial").mask("99:99");
    $("#HoraDocenteFinal").mask("99:99");


    $("#HoraIniView").unbind("change").change(function () {

        if (ValidateTime(this)) {
            if (ValidaHorariosInicioFim()) {
                CalcularHoras();
                $("#HorarioDocente").append('<option value="" selected="selected">' + $("#HoraIniView").val() + " às " + $("#HoraFimView").val() + '</option>');
                $("#HorarioAmbiente").append('<option value="" selected="selected">' + $("#HoraIniView").val() + " às " + $("#HoraFimView").val() + '</option>');
            }
        }
    });

    $("#DataIniView").datepicker({
        format: 'dd/mm/yyyy',
        onSelect: CalcularHoras
    })

    $("#HoraFimView").change(function () {
        if (ValidateTime(this)) {
            if (ValidaHorariosInicioFim()) {
                CalcularHoras();
                $("#HorarioDocente").append('<option value="" selected="selected">' + $("#HoraIniView").val() + " às " + $("#HoraFimView").val() + '</option>');
                $("#HorarioAmbiente").append('<option value="" selected="selected">' + $("#HoraIniView").val() + " às " + $("#HoraFimView").val() + '</option>');
            }
        }
    });


    $(".addAmbiente").click(function () {
        var diasSelecionados = "";
        var isReturn = false;
        var horaInicial = $('#HoraAmbienteInicial').val();
        var horaFinal = $('#HoraAmbienteFinal').val();
        var numeroDiasEscolhido = $("input:checkbox[name='DiasAmbiente']:checked").not(":disabled").length;
        var CHComponente = $("#Componente_CH").val();
        
        

        var turnoInicial = Date.parse("1-1-2000 " + $('#hidTurnoInicio').val());
        var turnoFinal = Date.parse("1-1-2000 " + $('#hidTurnoFim').val());

        if (horaInicial != '' && horaFinal != '') {
            $("input:checkbox[name='DiasAmbiente']:checked").not(":disabled").each(function () {

                if (diasSelecionados != "") {
                    diasSelecionados += "/" + " " + $(this).val();
                } else {
                    diasSelecionados += $(this).val() + " ";
                }
                $(this).removeAttr("checked");
                isReturn = true;
            });

            if (isReturn) {

                var validacaoFormatoInicial = ValidateTime(horaInicial);
                var validacaoFormatoFinal = ValidateTime(horaFinal);
                var validacaoTurno = true;
                var validacaoHorario = true;
                var validacaoHorarioIgual = true;
                var validacaoQtd = true;
                var validacaoLimiteDias = true;


                if (parseInt($("#hidVagasQtd").val()) > parseInt($(this).attr("Capacidade"))) {
                    validacaoQtd = false;
                }

                if (horaInicial == horaFinal) {
                    validacaoHorarioIgual = false;
                }
                var horaInicialEscolhidaS = new XDate("1-1-2000 " + horaInicial);
                var horaFimEscolhidaS = new XDate("1-1-2000 " + horaFinal);
                var difHoras = horaInicialEscolhidaS.diffHours(horaFimEscolhidaS);

                if (parseFloat(CHComponente / numeroDiasEscolhido) < parseFloat(difHoras)) {
                    validacaoLimiteDias = false;
                }
                var totalCargaHoraria = CHComponente-(difHoras*numeroDiasEscolhido);
                alert("totalCargaHoraria sobrou: "+ totalCargaHoraria);
                if ((horaInicialEscolhidaS > turnoFinal) || (horaInicialEscolhidaS < turnoInicial) || (horaFimEscolhidaS > turnoFinal) || (horaFimEscolhidaS < turnoInicial)) {
                    validacaoTurno = false;
                }
                $("#tableAmbienteEscolhido").find("icon").each(function () {
                    
                    var contemDia = Contem(diasSelecionados, $(this).attr("diasSemana"));
                    var qtdDiasSelecionados=$(this).attr("diasSemana").split("/").length;
                    alert(qtdDiasSelecionados);
                    var horaInicialExistente = new XDate("1-1-2000 " + $(this).attr("horainicial"));
                    var horaFimExistente = new XDate("1-1-2000 " + $(this).attr("horafinal"));
                    var horaInicialEscolhida = new XDate("1-1-2000 " + horaInicial);
                    var horaFimEscolhida = new XDate("1-1-2000 " + horaFinal);
                    var difHorariosExistentes = horaInicialExistente.diffHours(horaFimExistente);
                    alert("dif horario existe : "+difHorariosExistentes);
                    totalCargaHoraria = totalCargaHoraria - (difHorariosExistentes*qtdDiasSelecionados);
                    alert("total carga:"+totalCargaHoraria);
                    if (contemDia) {
                        
                        if (((horaInicialEscolhida < horaFimExistente) && (horaInicialEscolhida >= horaInicialExistente)
                           || (horaFimEscolhida <= horaFimExistente) && (horaFimEscolhida > horaInicialExistente)
                            || (horaFimEscolhida == horaFimExistente) && (horaInicialEscolhida == horaInicialExistente))) {
                            validacaoHorario = false;
                        }

                    }
                });
                if(totalCargaHoraria < 0){
                    validacaoLimiteDias = false;

                }
                //alert(totalCargaHoraria);
                //verifica se horario passou em todas as  validações 
                if (validacaoHorario && validacaoHorarioIgual && validacaoTurno && validacaoFormatoInicial && validacaoFormatoFinal && validacaoQtd && validacaoLimiteDias) {
                    //inclui dentro da tabela dos escolhidos a linha com as informações do horario selecionado
                    $("#tableAmbienteEscolhido tbody").append($(this).parent().parent().clone().append('<td>' + diasSelecionados + '</td>' + '<td>' + horaInicial + '</td>' + '<td>' + horaFinal + '</td>')).find("icon:last").attr({
                        diasSemana: diasSelecionados,
                        HoraInicial: horaInicial,
                        HoraFinal: horaFinal,

                        class: "icon-remove",
                        onclick: "RemoverItemAmbiente(this)"
                    });
                }
                // abaixo segue todas as verificações das validações, caso o horario selecionado não tenha passado em alguma validação, é incluido um modal 
                // a respectiva mensagem
                if (!validacaoQtd) {
                    //Validação de Vagas disponiveis no ambiente
                    IncluirModal("O ambiente em questão não comporta o número de vagas para essa turma.");
                } else {
                    if (!validacaoLimiteDias) {
                        //Validação de soma total dos dias escolhidos excede o numero total de carga horaria destinada para esse componente
                        IncluirModal("A soma das horas ultrapassa a carga horária total do componente.");
                    }
                    if (!validacaoFormatoInicial) {
                        //validação de horário inicial    
                        IncluirModal("O horário inicial escolhido não é um horário  válido.");
                    } else {
                        if (!validacaoFormatoFinal) {
                            //validação de horário Final
                            IncluirModal("O horário final escolhido não é um horário  válido.");
                        }
                        else {
                            if (!validacaoTurno) {
                                //validação de horário fora do turno
                                IncluirModal("O horário escolhido não é compatível com o turno escolhido para turma.");
                            } else {
                                if (!validacaoHorarioIgual) {
                                    //validação para caso o usuário coloque o mesmo horario tanto no inicial quanto no final ex:(9:00/9:00)
                                    IncluirModal("É Necessário uma diferença mínima entre os horários de início e fim.");
                                }
                                else {
                                    if (!validacaoHorario) {
                                        IncluirModal("Já existe um ambiente agendado no horário e dia da semana informado.");
                                    }
                                }
                            }
                        }
                    }
                }
            } else {
                IncluirModal("É Necessário preencher os dias da semana em que será agendado.");
            }
        } else {
            IncluirModal("É Necessário preencher o horário inicial e o horário final.");
        }

    });

    $(".addDocente").click(function () {
        var diasSelecionados = "";
        var isReturn = false;
        var horaInicial = $('#HoraDocenteInicial').val();
        var horaFinal = $('#HoraDocenteFinal').val();

        var turnoInicial = Date.parse("1-1-2000 " + $('#hidTurnoInicio').val());
        var turnoFinal = Date.parse("1-1-2000 " + $('#hidTurnoFim').val());;

        if (horaInicial != '' && horaFinal != '') {
            $("input:checkbox[name='DiasDocente']:checked").not(":disabled").each(function () {

                if (diasSelecionados != "") {
                    diasSelecionados += "/" + " " + $(this).val();
                } else {
                    diasSelecionados += $(this).val() + " ";
                }
                $(this).removeAttr("checked");
                isReturn = true;
            });
            if (isReturn) {
                var validacaoTurno = true;
                var validacaoHorario = true;
                var validacaoHorarioIgual = true;
                var validacaoFormatoInicial = ValidateTime(horaInicial);
                var validacaoFormatoFinal = ValidateTime(horaFinal);

                if (horaInicial == horaFinal) {
                    validacaoHorarioIgual = false;
                }
                var horaInicialEscolhidaS = Date.parse("1-1-2000 " + horaInicial);
                var horaFimEscolhidaS = Date.parse("1-1-2000 " + horaFinal);

                if ((horaInicialEscolhidaS > turnoFinal) || (horaInicialEscolhidaS < turnoInicial) || (horaFimEscolhidaS > turnoFinal) || (horaFimEscolhidaS < turnoInicial)) {
                    validacaoTurno = false;
                }
                $("#tableDocenteEscolhidos").find("icon").each(function () {

                    var contemDia = Contem(diasSelecionados, $(this).attr("diasSemana"));
                    if (contemDia) {
                        var horaInicialExistente = Date.parse("1-1-2000 " + $(this).attr("horainicial"));
                        var horaFimExistente = Date.parse("1-1-2000 " + $(this).attr("horafinal"));
                        var horaInicialEscolhida = Date.parse("1-1-2000 " + horaInicial);
                        var horaFimEscolhida = Date.parse("1-1-2000 " + horaFinal);

                        if (((horaInicialEscolhida < horaFimExistente) && (horaInicialEscolhida >= horaInicialExistente)
                           || (horaFimEscolhida <= horaFimExistente) && (horaFimEscolhida > horaInicialExistente)
                             )) {
                            validacaoHorario = false;
                        }

                    }
                });

                if (validacaoHorario && validacaoHorarioIgual && validacaoTurno && validacaoFormatoInicial && validacaoFormatoFinal) {
                    $("#tableDocenteEscolhidos tbody").append($(this).parent().parent().clone().append('<td>' + diasSelecionados + '</td>' + '<td>' + horaInicial + '</td>' + '<td>' + horaFinal + '</td>')).find("icon:last").attr({
                        diasSemana: diasSelecionados,
                        HoraInicial: horaInicial,
                        HoraFinal: horaFinal,
                        class: "icon-remove",
                        onclick: "RemoverItemDocente(this)"
                    });
                } if (!validacaoFormatoInicial) {
                    IncluirModal("O horário inicial escolhido não é um horário  válido.");

                } else {
                    if (!validacaoFormatoFinal) {
                        IncluirModal("O horário final escolhido não é um horário  válido.");
                    }
                    else {
                        if (!validacaoTurno) {
                            IncluirModal("O horário escolhido não é compatível com o turno escolhido para turma.");
                        } else {
                            if (!validacaoHorarioIgual) {

                                IncluirModal("É Necessário uma diferença mínima entre os horários de início e fim.");
                            }
                            else {

                                if (!validacaoHorario) {
                                    IncluirModal("Já existe um docente agendado no horário e dia da semana informado.");
                                }
                            }

                        }
                    }
                }
            }
            else {
                IncluirModal("É Necessário preencher os dias da semana em que será agendado.");
            }
        } else {
            IncluirModal("É Necessário preencher o horário inicial e o horário final.");
        }
    });




    $(":checkbox[name='DiasSemanaComponente']:checked").each(function () {
        var Dia = $(this).val();
        $("#hidDiasSemanaComponente").val($("#hidDiasSemanaComponente").val() + $(this).val() + ",");
        $(":checkbox[name='DiasAmbiente']").each(function () {
            if ($(this).val() == Dia) {
                $(this).removeAttr("Disabled");
            }
        });
        $(":checkbox[name='DiasDocente']").each(function () {
            if ($(this).val() == Dia) {
                $(this).removeAttr("Disabled");
            }
        });
    });



    $(":checkbox[name='DiasSemanaAmbiente']:checked").each(function () {
        $("#hidDiasSemanaAmbiente").val($("#hidDiasSemanaAmbiente").val() + $(this).val() + ",");
    });


    $(":checkbox[name='DiasSemanaDocente']:checked").each(function () {
        $("#hidDiasSemanaDocente").val($("#hidDiasSemanaDocente").val() + $(this).val() + ",");
    });



});


function ValidaHorariosInicioFim() {

    var $ini = $("#HoraIniView");
    var $fim = $("#HoraFimView");

    if ($ini.val() != "" && $fim.val() != "") {
        var dataIni = parseInt($ini.val().replace(':', ''));
        var dataFim = parseInt($fim.val().replace(':', ''));

        if (dataIni >= dataFim) {
            $fim.next().html("O campo Hora Final deve ser maior que o Hora Inicial");
            return false;
        } else {
            $fim.next().html("");
        }
    }
    return true;
}


function ValidateTime(_currentInput) {

    var $this = _currentInput;

    if ($this != "") {
        var value = parseInt($this.replace(':', ''));

        if (value > 2359) {
            validation = false;

            return false;
        } else {
            return true;
        }
    }
}

function validarCampoData() {

    if ($("#DataIniView").val() != "") {
        $("#DataIniView").next("").hide();

    } else {

        $("#DataIniView").next("span").html("Campo Obrigatório").show();

    }
}

function VerificarPossibilidadeRemocao(e) {
    var vali = true;
    $("#BoxDocente").find("icon").each(function () {
        if ($(this).attr("diasSemana").indexOf("" + $(e).val() + "") >= 0) {
            $(e).attr("checked", "checked");

            IncluirModal("Há Docentes e/ou Ambientes agendados para este dia. Por Favor remova-os antes de remover os dias.");
            vali = false;
        }
    });

    $("#tableAmbienteEscolhido").find("td").each(function () {
        if ($(this).html().indexOf("" + $(e).val() + "") >= 0) {
            $(e).attr("checked", "checked");

            IncluirModal("Há Docentes e/ou Ambientes agendados para este dia. Por Favor remova-os antes de remover os dias.");

            vali = false;
        }
    });
    return vali;
}



function OnSelectDia(e) {


    if (VerificarPossibilidadeRemocao(e)) {
        if ($(e).attr("checked") == "checked") {
            $('input[value="' + $(e).val() + '"]').not(":checkbox[name='DiasSemanaComponente']").each(function () {
                if ($(this).attr("disabled") != null) {
                    $(this).removeAttr("disabled");
                } else {
                    $(this).attr("disabled", "disabled");
                }


                if ($("#hidDiasSemanaComponente").val().indexOf("" + $(this).val() + "") >= 0) {

                } else {
                    $("#hidDiasSemanaComponente").val($("#hidDiasSemanaComponente").val() + $(this).val() + ",");

                }
            });
        } else {
            $('input[value="' + $(e).val() + '"]').not(":checkbox[name='DiasSemanaComponente']").each(function () {

                if ($(this).attr("disabled") != null) {
                    $(this).removeAttr("disabled");
                } else {
                    $(this).attr("disabled", "disabled");
                }


            });
            var teste = "";
            $("input:checkbox[name='DiasSemanaComponente']:checked").each(function () {
                teste += $(this).val() + ",";

            });
            ($("#hidDiasSemanaComponente").attr('value', teste));


        }
    }
    CalcularHoras();


}


function CalcularHoras() {

    if ($("#DataIniView").val() != "" && $("#HoraIniView").val() != "" && $("#HoraFimView").val() != "") {
        var dia = $("#DataIniView").val().split("/")[0];
        var mes = $("#DataIniView").val().split("/")[1];
        var ano = $("#DataIniView").val().split("/")[2];

        var horaInicial = $("#HoraIniView").val().split(":")[0];
        var minutosInicial = $("#HoraIniView").val().split(":")[1];

        var dateInicial = new XDate(ano, mes, dia, horaInicial, minutosInicial, 0, 0);

        var horaFinal = $("#HoraFimView").val().split(":")[0];
        var minutosFinal = $("#HoraFimView").val().split(":")[1];
        var dateFim = new XDate(ano, mes, dia, horaFinal, minutosFinal, 0, 0);

        var result = dateInicial.diffHours(dateFim);

        var count = $(":checkbox[name='DiasSemanaComponente']:checked").length;
        if (result != "0" && count != "0") {

            var difWeek = Math.ceil(($("#Componente_CH").val() / result) / count);

            var dataFinal = dateInicial.addDays(difWeek * 7.0);
            if (dataFinal != "")
                $("#DataFimView").val(dataFinal.toString("dd/MM/yyyy"));

        } else {
            $("#DataFimView").val("");
        }

    } else {
        $("#DataFimView").val("");
    }

}





function RemoverItemAmbiente(e) {
    var dia = $(e).parent().parent().find("td:last").html();
    $(":checkbox[name='DiasAmbiente']").each(function () {
        if (Contem(dia, $(this).val())) {
            $(this).removeAttr("Disabled");

            $(this).prop('checked', false);
        } else {

        }
    });

    var Ambiente = $(e).parent().parent().remove();



}

function RemoverItemDocente(e) {
    var dia = $(e).attr("diasSemana");

    $(":checkbox[name='DiasDocente']").each(function () {
        if (Contem(dia, $(this).val())) {
            $(this).removeAttr("Disabled");

            $(this).prop('checked', false);
        } else {

        }
    });
    var Docente = $(e).parent().parent().remove();



    $(".addDocente").click(function () {
        var diasSelecionados = "";
        var isReturn = false;
        var horaInicial = $('#HoraDocenteInicial').val();
        var horaFinal = $('#HoraDocenteFinal').val();

        $("input:checkbox[name='DiasDocente']:checked").not(":disabled").each(function () {

            if (diasSelecionados != "") {
                diasSelecionados += "/" + " " + $(this).val();
            } else {
                diasSelecionados += $(this).val() + " ";
            }
            $(this).removeAttr("checked");
            isReturn = true;
        });
        if (isReturn) {

            var validacaoHorario = true;
            $("#tableDocente").find("icon").each(function () {

                var contemDia = Contem(diasSelecionados, $(this).attr("diasSemana"));
                if (contemDia) {
                    var horaInicialExistente = Date.parse("1-1-2000 " + $(this).attr("horainicial"));
                    var horaFimExistente = Date.parse("1-1-2000 " + $(this).attr("horafinal"));
                    var horaInicialEscolhida = Date.parse("1-1-2000 " + horaInicial);
                    var horaFimEscolhida = Date.parse("1-1-2000 " + horaFinal);

                    if (((horaInicialEscolhida <= horaFimExistente) && (horaInicialEscolhida >= horaInicialExistente)
                       || (horaFimEscolhida <= horaFimExistente) && (horaFimEscolhida >= horaInicialExistente)
                        )) {
                        validacaoHorario = false;
                    }

                }
            });

            if (validacaoHorario) {
                "<div class='docente'>" + $(this).parent().append("<div name='divSemana'>" + diasSelecionados + "</span><span>" + horaInicial + " / " + horaFinal + "</span>").parent().clone().append("</div>").insertAfter($("#aRef")).find("icon").attr({
                    diasSemana: diasSelecionados,
                    class: "icon-remove",
                    HoraInicial: horaInicial,
                    HoraFinal: horaFinal,
                    onclick: "RemoverItemDocente(this)"
                });
                $(this).parent().find("div[name='divSemana']").remove();
            }
        }
    });

}


function Validar() {

    var isValid = true;

    if ($("#DataIniView").val() == "" || $("#DataIniView").val() == null || $("#DataIniView").val() == undefined) {
        $("#DataIniView").next().html("Campo Obrigatório.").addClass("error");
        $("#DataIniView").focus();

        isValid = false;
    }

    if ($("#HoraFimView").val() == "") {
        $("#HoraFimView").next().html("Campo Obrigatório.").addClass("error");
        isValid = false;
        $("#HoraFimView").fous();
    }

    if ($("#HoraIniView").val() == "") {
        $("#HoraIniView").next().html("Campo Obrigatório.").show();
        $("#HoraIniView").focus();
        isValid = false;
    }

    if ($("#DataFimView").val() == "") {
        $("#DataFimView").next().html("Campo Obrigatório.").addClass("error");
        $("#DataFimView").focus();
        isValid = false;
    }
    return isValid;
}



function OnClickSalvarAgendaComponente() {

    event.preventDefault();

    if (ValidaHorariosInicioFim()) {
        var isValid = Validar();

        var JsonModulo = new Object();

        JsonModulo.IdModulo = $("#IdModulo").val();
        JsonModulo.IdTurma = $("#IdTurma").val();
        JsonModulo.IdComponente = $("#IdComponente").val();
        JsonModulo.DataIniView = $("#DataIniView").val();
        JsonModulo.DataFimView = $("#DataFimView").val();
        JsonModulo.DiasSemanaComponente = $("#hidDiasSemanaComponente").val();
        JsonModulo.HoraIniView = $("#HoraIniView").val();
        JsonModulo.HoraFimView = $("#HoraFimView").val();
        JsonModulo.AgendaAmbientes = new Array();
        JsonModulo.AgendaDocentes = new Array();

        $("#tableAmbienteEscolhido tbody tr").each(function () {
            $(this).find("td icon").each(function () {

                if ($(this).attr("id") != undefined) {
                    JsonModulo.AgendaAmbientes.push({
                        IdAmbiente: $(this).attr("id"),
                        IdModulo: $("#IdModulo").val(),
                        IdTurma: $("#IdTurma").val(),
                        DiasSemanaAmbiente: $(this).attr("diasSemana"),
                        IdComponente: $("#IdComponente").val(),
                        DataIni: $("#DataIniView").val(),
                        DataFim: $("#DataFimView").val(),
                        HoraIni: $(this).attr("horainicial"),
                        HoraFim: $(this).attr("horafinal")
                    });
                }
            });
        });

        $("#tableDocenteEscolhidos tbody tr").each(function () {
            $(this).find("icon").each(function () {

                JsonModulo.AgendaDocentes.push({
                    IdModulo: $("#IdModulo").val(),
                    IdDocente: $(this).attr("id"),
                    IdComponente: $("#IdComponente").val(),
                    IdTurma: $("#IdTurma").val(),
                    DiasSemanaDocente: $(this).attr("diasSemana"),
                    DataIni: $("#DataIniView").val(),
                    DataFim: $("#DataFimView").val(),
                    HoraIni: $(this).attr("horainicial"),
                    HoraFim: $(this).attr("horafinal")
                });
            });
        });

        if (isValid) {
            $.ajax({
                url: '/agendamento/AgendamentoCompleto/',
                data: JSON.stringify(JsonModulo),
                type: "post",
                datatype: "json",
                async: false,
                'contentType': 'application/json',
                success: function (data) {

                    $("#alertBox").html("<p> O Agendamento foi efetuado com sucesso.</p>");
                    $("#alertBox").show();
                    window.location = "/Turma/EditarTurma/" + $("#IdTurma").val();
                },
                error: function (data) {

                    window.location = "/Turma/EditarTurma/" + $("#IdTurma").val();

                }

            });
        }
    }
}
