/// <reference path="../base.SPE.js" />
/// <reference path="../jquery-1.8.2.min.js" />


$(document).ready(function () {
    //RemoveComponente();
    EnableDrag();
    RemoveCompetencia();
    AddComponente();
    removeAreaAtuacao();
    addAreaAtuacao();
    ListarComponente();
    addAreaAtuacaoComponente();
    removeAreaAtuacaoComponente();
    

    $("#btnSalvar").click(function (e) {
        var btn = $(this);
        if ($(".modulo-item li").length > 0 && $("#tableAreaAtuacaoEscolhidos").find(".icon").length > 0) {

            var idComponentes = new Array();
            var idAreaAtuacao = new Array();

            $(".modulo-item li").each(function () {
                $this = $(this);
                idComponentes.push($this.attr("componentId"));
            });

            $("#idComponentes").val(idComponentes.join(';'));

            $("#tableAreaAtuacaoEscolhidos").find(".icon").each(function () {
                $this = $(this);
                idAreaAtuacao.push($this.attr("id"));
            });

            $("#idsAreaAtuacao").val(idAreaAtuacao.join(';'));

            if ($('form').validate().form()) {
                btn.button('loading');
            }

            $("form").submit();

        } else {

            IncluirModal("Por favor. Cadastre pelo menos uma competência e uma área de atuação.");
            e.preventDefault();
        }
    });

});

function RemoveCompetencia() {


    $(".modulo-item a.removeModulo").unbind("click").click(function (e) {

        e.preventDefault();

        $(".ul1.modulos li").each(function () {

            $thisLi = $(this);

            $("#componentesDocente ul").append($('<li>').append($(this).clone()).html());

            $thisLi.remove();
        });
        $('#cargaTotal').html("0");
    });
}

function RemoveComponente() {

    $("#modulos a.remove").unbind("click").click(function (e) {
        e.preventDefault();

        var $this = $(this).parent().parent().parent();

        //Transfere o componente a ser excluido do módulo para a lista da componentes
        $thisLi = $(this).parent();

        $("#componentesDocente ul").append($('<li>').append($thisLi.clone()).html());


        $thisLi.remove();

    });
}

function EnableDrag() {

    $("#componentesDocente ul").sortable({
        connectWith: "ul",
        start: function (event, ui) {
            var componente = RemoverAcentos(ui.item.text().trim());
            var validate = ValidaComponenteCompetencia(componente);

            if (!validate) {

                $("body").append("<div id='ModalInfo1' class='modal hide fade' tabindex='-1' role='dialog' aria-labelledby='Informação' aria-hidden='true'>" +
"<div class='modal-header'>" +
" <button type='button' class='close' data-dismiss='modal' aria-hidden='true'>×</button>" +
"  <h3 id='myModalLabel'>Informação</h3>" +
"</div>" +
" <div class='modal-body'>" +
" <p>Já existe um componente com o mesmo nome nessa competência.</p>" +
"</div>" +
" <div class='modal-footer'>" +
" <button class='btn' data-dismiss='modal' aria-hidden='true'>Fechar</button>" +

" </div>" +
" </div>");
                $("#ModalInfo1").modal('show');

                EnableDrag();
                $("#componentesDocente ul li").attr("style", "position: relative; cursor:pointer; cursor:hand -webkit-touch-callout: none;" +
"-webkit-user-select: none;" +
            " -khtml-user-select: none;" +
             "-moz-user-select: moz-none;" +
             "-ms-user-select: none;" +
             "user-select: none;");

            }

            //EnableDrag();


        },

        revert: "invalid"
    });

    $(".modulos").sortable({
        items: 'li:not(.titulo)',
        revert: true,
        connectWith: "ul",
        update: function (e, ui) {
            RemoveComponente();
        }
    });
}


function ValidaComponenteCompetencia(componente) {

    var validate = true;
    $("#modulos li").each(function () {
        $this = $(this);
        var componenteCompetencia = RemoverAcentos($this.attr("nome").trim());
        if (componenteCompetencia == componente) {
            $("#componentesDocente ul").sortable("cancel");
            validate = false;
        }
    });

    return validate;
}

function BuscaComponentes() {


    var id = $("#idMatrizBusca").val();
    var busca = $("#txtBusca").val();

    var $componentes = $("#componentesDocente ul li");

    $componentes.each(function () {

        var $elm = $(this);
        $elm.show();

        var nome = $elm.attr("nome");

        //Se não contem o termo buscado
        if (nome.toUpperCase().indexOf(busca.toUpperCase()) < 0) {
            $elm.hide();
        }
    });

    //$.ajax({
    //    url: '/Docente/BuscarComponentes/',
    //    data: { filtro: busca },
    //    type: "get",
    //    success: function (data) {
    //        $("#componentesDocente").html(data);
    //        ValidaCarregarComponentes();
    //        EnableDrag();
    //    },
    //    error: function (data) {

    //    }
    //});

}

function ValidaCarregarComponentes() {

    $("#componentesDocente ul li").each(function () {

        $thisli = $(this);

        $("#modulos ul li").each(function (i) {

            $thisCompetencia = $(this);

            if (RemoverAcentos($thisCompetencia.attr("nome")) == RemoverAcentos($thisli.attr("nome"))) {
                $thisli.remove();
            }
        });
    });
}

function removeAreaAtuacao() {

    $("#tableAreaAtuacaoEscolhidos .icon-remove").click(function () {
        var idAreaAtuacaoRemover = $(this).attr('id');
        var listaArea = "";

        $("#tableAreaAtuacaoEscolhidos tbody").find(".icon").each(function () {

            if (listaArea == "") {
                listaArea = $(this).attr("id");
            } else {
                listaArea += "," + $(this).attr("id");
            }
        });


        if (podeRemover(idAreaAtuacaoRemover, listaArea)) {
            $(this).parent().parent().remove();

            ListarComponente();

        } else {
            IncluirModal("Para remover a área de atuação, tire as competencias antesPor favor. Cadastre pelo menos uma competência e uma área de atuação.");
            e.preventDefault();
        }



    });
}

function ListarComponente() {
    var listaArea = "";
    $("#tableAreaAtuacaoEscolhidos tbody").find(".icon").each(function () {
        if (listaArea == "") {
            listaArea = $(this).attr("id");
        } else {
            listaArea += "," + $(this).attr("id");
        }
    });

    var IdsModulosFiltrar = "";
    $(".modulos li").each(function () {
        if (IdsModulosFiltrar == "") {
            IdsModulosFiltrar = $(this).attr("componentid");
        } else {
            IdsModulosFiltrar += ";" + $(this).attr("componentid");
        }
    });

    if (listaArea != "") {
        $.ajax({
            url: '/Componente/GetListaComponenteAreaAtuacao/',
            data: {
                IdsAreaAtuacao: listaArea,
                IdsModulosFiltrar: IdsModulosFiltrar
            },
            type: "post",

            success: function (data) {
                $("#componentesDocente").find(".ul1").html(data);

            }
        });

    } else {
        $.ajax({
            url: '/Componente/GetListaComponenteAreaAtuacao/',
            data: { IdsAreaAtuacao: 0 },
            type: "post",
            success: function (data) {
                $("#componentesDocente").find(".ul1").html(data);

            }
        });

    }


}

function addAreaAtuacao() {

    $(".addAreaAtuacao").click(function () {
        var item = '';
        var idAreaAtuacao = '#' + $(this).attr("id");
        if ($('#tableAreaAtuacaoEscolhidos tbody').find(idAreaAtuacao).length) {
            IncluirModal("O Docente já possui esta Área de Atuação");
        }
        else {
            $('#tableAreaAtuacaoEscolhidos tbody').append(
                $(this).parents("tr").clone()).find("span:last").attr({
                class: "icon-remove"
            });

            removeAreaAtuacao();
        }

        ListarComponente();
    });

}

function podeRemover(idAreaAtuacao, listaArea) {
    var podeRemover = true;
    var areasArray = listaArea.split(',');
    var index = areasArray.indexOf(idAreaAtuacao);

    if (index != -1) {
        areasArray.splice(index, 1);
    }

    $(".modulos li").each(function () {

        var idArea = $(this).attr('idareaatuacao');
        
        var listaAreasAtuacaoDoComponente = (idArea) ? idArea.split(';') : "";

        if (listaAreasAtuacaoDoComponente.indexOf(idAreaAtuacao) != -1) {
            podeRemover = false;

            for (var i = 0; i < areasArray.length; i++) {
                for (var j = 0; j < listaAreasAtuacaoDoComponente.length; j++) {
                    if (areasArray[i] == listaAreasAtuacaoDoComponente[j]) {
                        podeRemover = true;
                        break;
                    }
                }

                if (podeRemover) {
                    break;
                }
            }
        }

    });

    return podeRemover;
}