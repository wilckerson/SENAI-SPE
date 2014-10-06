/// <reference path="../base.SPE.js" />
/// <reference path="../jquery-1.8.2.min.js" />


$(document).ready(function () {
    var btnSalvar = $("#salvarPerfil");

    RemoveItem();
    EnableDrag();
    LimparTudo();
    AddComponente();
    //limparFuncionalidades();
    //addAreaAtuacao();
    //ListarComponente();
    //addAreaAtuacaoComponente();
    //removeAreaAtuacaoComponente();

    btnSalvar.click(function (e) {

        var funcionalidade = $("#modulos ul li").length;

        if (funcionalidade) {
            SalvarPerfil();

            if ($('form').validate().form()) {
                $(this).button('loading');

            }

        } else {
            ModalALert("Error ao cadastrar", "Não é possível cadastrar perfil sem uma funcionalidade");
            e.preventDefault();
        }
    });

});

function LimparTudo() {

    $(".limparTudo").unbind("click").click(function (e) {

        e.preventDefault();

        $("#modulos ul li").each(function () {

            $thisLi = $(this);
            $("#componentesDocente ul:first").append($thisLi);
        });
    });
}

function RemoveItem() {

    $("#modulos").delegate(".remove","click",function () {
               
        //Transfere o componente a ser excluido do módulo para a lista da componentes
        $thisLi = $(this).parents("li");
        $("#componentesDocente ul:first").append($thisLi);
        
    });
}

function EnableDrag() {

    $("#componentesDocente ul").sortable({
        connectWith: ".ul1",
//        start: function (event, ui) {
//            var componente = RemoverAcentos(ui.item.text().trim());
//            var validate = ValidaComponenteCompetencia(componente);

//            if (!validate) {

//                $("body").append("<div id='ModalInfo1' class='modal hide fade' tabindex='-1' role='dialog' aria-labelledby='Informação' aria-hidden='true'>" +
//"<div class='modal-header'>" +
//" <button type='button' class='close' data-dismiss='modal' aria-hidden='true'>×</button>" +
//"  <h3 id='myModalLabel'>Informação</h3>" +
//"</div>" +
//" <div class='modal-body'>" +
//" <p>Já existe uma funcionalidade com o mesmo nome.</p>" +
//"</div>" +
//" <div class='modal-footer'>" +
//" <button class='btn' data-dismiss='modal' aria-hidden='true'>Fechar</button>" +

//" </div>" +
//" </div>");
//                $("#ModalInfo1").modal('show');

//                EnableDrag();
//                $("#componentesDocente ul li").attr("style", "position: relative; cursor:pointer; cursor:hand -webkit-touch-callout: none;" +
//"-webkit-user-select: none;" +
//            " -khtml-user-select: none;" +
//             "-moz-user-select: moz-none;" +
//             "-ms-user-select: none;" +
//             "user-select: none;");

//            }

//            //EnableDrag();


//        },

        revert: "invalid"
    }).disableSelection();

    $("#modulos ul").sortable({
        items: 'li:not(.titulo)',
        revert: true,
        connectWith: "ul1",
        //update: function (e, ui) {
        //    RemoveComponente();
        //}
    });
}


//function ValidaComponenteCompetencia(componente) {

//    var validate = true;
//    $("#modulos li").each(function () {
//        $this = $(this);
//        var componenteCompetencia = RemoverAcentos($this.attr("nome").trim());
//        if (componenteCompetencia == componente) {
//            $("#componentesDocente ul").sortable("cancel");
//            validate = false;
//        }
//    });

//    return validate;
//}


//function ValidaCarregarComponentes() {

//    $("#componentesDocente ul li").each(function () {

//        $thisli = $(this);

//        $("#modulos ul li").each(function (i) {

//            $thisCompetencia = $(this);

//            if (RemoverAcentos($thisCompetencia.attr("nome")) == RemoverAcentos($thisli.attr("nome"))) {
//                $thisli.remove();
//            }
//        });
//    });
//}


function SalvarPerfil() {

    var idFuncionalidades = new Array();

    $(".modulo-item li").each(function () {
        $this = $(this);
        idFuncionalidades.push($this.attr("funcionalidadeId"));
    });

    $("#idFuncionalidades").val(idFuncionalidades.join(';'));
}

//function limparFuncionalidades() {

//    $("#lnkLimparFunc").click(function () {

//        $(".ul1.modulos").html("");
//    });
//}

