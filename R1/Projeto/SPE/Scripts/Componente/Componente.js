/// <reference path="jquery-1.8.2.min.js" />

$(document).ready(function () {
    $("#CH").focusout(function () {
        if ($("#CH").val() == "" || $("#CH").val() == 0 || $("#CH").val() == null || $("#CH").val() == undefined) {
            $("#CH").focusin();

        }
    });

    $(".addAreaAtuacao").click(function () {
        var item = '';
        var idAreaAtuacao = '#' + $(this).attr("id");
        if ($('#tableAreaAtuacaoEscolhidos tbody').find(idAreaAtuacao).length) {
            IncluirModal("O Componente já possui esta Área de Atuação");
        }
        else {
            $('#tableAreaAtuacaoEscolhidos tbody').append($(this).parent().parent().clone()).find("icon:last").attr({
                class: "icon-remove"
            });
            removeAreaAtuacao();
        }

    });


    $('input[type=submit]').click(function () {
        var areaAtuacao = $("#tableAreaAtuacaoEscolhidos td"),
            tipoAmbiente = $("input[type=checkbox]");
        var isValid = areaAtuacao.length && tipoAmbiente.is(":checked");
        if ($('form').validate().form() && isValid) {
            $(this).button('loading');

        }
    });



});


function removeAreaAtuacao() {

    $(".icon-remove").click(function () {
        $(this).parent().parent().remove();
    });
}


function SalvarComponente() {

    var validate = true;

    if ($("#tableAreaAtuacaoEscolhidos").find("icon").length > 0) {

        var idAreaAtuacao = new Array();

        $("#tableAreaAtuacaoEscolhidos").find("icon").each(function () {
            $this = $(this);
            idAreaAtuacao.push($this.attr("id"));
        });

        $("#idsAreaAtuacao").val(idAreaAtuacao.join(';'));  
    } else {
        isisValidForm = false;
        validate = false;
        IncluirModal("Por favor. Cadastre pelo menos uma área de atuação.");
    }


    if (!$("input[name=tipoAmbiente]:checked").length) {
        validate = false;
        IncluirModal("Por favor. Cadastre pelo menos um tipo de ambiente.");
    }

    return validate;
}


