$(document).keypress(function (e) {
    if (e.which == 13 && $("#txtBusca:focus").length == 1) {
        $("#btnBusca").click();
        return false;
    }
});

$(document).ready(function () {
    $("#hidden").hide();

    AddComponente();

    enableDragNDrop();

    ValidaCarregarComponentes();

    $("#DataFimView").datepicker();


    $("#aprovarMatriz").change(function () {
        if ($(this).val() == "0" || $(this).val() == "1") {
            $("#hidden").show();
        } else {
            $("#hidden").hide();
            $("#hidden [name='descricao']").val("");
        }
    });

    $(".addModulo").click(function () {
        event.preventDefault();

        var order = 1;

        if ($("#modulos .modulo-item").length > 0) {
            order = parseInt($("#modulos .modulo-item").last().attr("order")) + 1;
        }

        $("#moduloModel .modulo-item").attr("order", order);
        var nameModulo = "Modulo " + (parseInt($("#modulos .modulo-item").length) + 1);
        $("#moduloModel .modelTitle").attr("value", nameModulo);
        var html = $("#moduloModel").html();
        $("#modulos").append(html);

        enableDragNDrop();
        ValidarNomes();
        //  SalvarMatriz();

    });

    SalvarMatriz();

    onChangeAreaAtuacao();
});


function onChangeAreaAtuacao() {

    $("#IdAreaAtuacao").change(function (eventChange) {
      
        var currentValue = $(this).val(),
            isModuloNotEmpty = $(".modulo-item .ul1").eq(1).find("li").length > 0;

        if (currentValue && isModuloNotEmpty) {

            var previousValue = $(this).data("previous-value");

            IncluirModalConfirm("Atenção", "Mudar a área de atuação vai retirar os componentes que estão nos modulos. Deseja continuar mesmo assim?",
                function () {

                    limparComponentesTodosModulos();

                    ListarComponentes();

                }, function () {

                    $("#IdAreaAtuacao").val(previousValue);
                });
        }
        else
        {
            ListarComponentes();
        }

        // update previous value
        $(this).data("previous-value", $(this).val());
    });
}

function ValidarModalModulo() {

    var validate = true;


    $("#modulos input").each(function () {

        var $this = $(this);

        $("#modulos input").each(function (i) {


            var isNameEquals = RemoverAcentos($(this).val().trim()) == RemoverAcentos($this.val().trim())

            if ($(this).parent().parent().attr("order") != $this.parent().parent().attr("order") && isNameEquals) {

                //Alert 5
                $("body").append("<div id='ModalInfo5' class='modal hide fade' tabindex='-1' role='dialog' aria-labelledby='Informação' aria-hidden='true'>" +
                    "<div class='modal-header'>" +
                            " <button type='button' class='close' data-dismiss='modal' aria-hidden='true'>×</button>" +
                            "  <h3 id='myModalLabel'>Informação</h3>" +
                    "</div>" +
                    " <div class='modal-body'>" +
                            " <p>Há módulos com o mesmo nome. Por favor, informe um novo nome.</p>" +
                    "</div>" +
                    " <div class='modal-footer'>" +
                            " <button class='btn' data-dismiss='modal' aria-hidden='true'>Fechar</button>" +
                    " </div>" +
" </div>");
                $("#ModalInfo5").modal('show');

                $this.focus();
                validate = false;
            }

        });

    });

    return validate;
}

function enableDragNDrop() {
    $('.modulo-item').tsort({ attr: 'order' });
    $("#componentes ul").sortable({
        connectWith: "ul",

        start: function (event, ui) {

            var Componente = ui.item.text().split("(")[0];
            var validacao = ValidarDragnDrop(Componente);

            if (!validacao) {

                //Alert 1
                $("body").append("<div id='ModalInfo1' class='modal hide fade' tabindex='-1' role='dialog' aria-labelledby='Informação' aria-hidden='true'>" +
"<div class='modal-header'>" +
" <button type='button' class='close' data-dismiss='modal' aria-hidden='true'>×</button>" +
"  <h3 id='myModalLabel'>Informação</h3>" +
"</div>" +
" <div class='modal-body'>" +
" <p>Já existe um componente com o mesmo nome em um dos módulos dessa matriz.</p>" +
"</div>" +
" <div class='modal-footer'>" +
" <button class='btn' data-dismiss='modal' aria-hidden='true'>Fechar</button>" +

" </div>" +
" </div>");
                $("#ModalInfo1").modal('show');



                enableDragNDrop();
                $("#componentes ul li").attr("style", "position: relative; cursor:pointer; cursor:hand -webkit-touch-callout: none;" +
"-webkit-user-select: none;" +
               " -khtml-user-select: none;" +
                "-moz-user-select: moz-none;" +
                "-ms-user-select: none;" +
                "user-select: none;");

            }
        },
        //helper: "clone",
        revert: "invalid"
    });

    $(".modulos").sortable({
        items: 'li:not(.titulo)',
        revert: true,
        connectWith: "ul",
        update: function (e, ui) {
            var somaCarga = 0;
            $($(this).parent().find("li")).each(function () {
                if ($(this).attr("carga") != 0) {
                    somaCarga = somaCarga + parseInt($(this).attr("carga"));
                }
            });
            $(this).parent().find('#cargaTotal').html(somaCarga);
            var totalModulos = 0;
            $(this).parent().parent().find('#cargaTotal').each(function () {
                totalModulos = totalModulos + parseInt($(this).html());
            });
            $(this).parent().parent().parent().parent().find('#CH').val(totalModulos);
            removerComponente();
        }
    });

    $("a.up").click(function () {
        event.preventDefault();
        order = parseInt($(this).parent().attr("order"));
        if (order != 1) {
            $(".modulo-item").each(function () {
                if (parseInt($(this).attr("order")) == order - 1) {
                    $(this).attr("order", order);
                }
            });
            $(this).parent().attr("order", order - 1);
        }
        $('.modulo-item').tsort({ attr: 'order' });
    });

    $("a.down").unbind("click").click(function () {
        event.preventDefault();
        order = parseInt($(this).parent().attr("order"));
        last = $(".modulo-item").last().attr("order");
        if (order != last) {
            $(".modulo-item").each(function () {
                if (parseInt($(this).attr("order")) == order + 1) {
                    $(this).attr("order", order);
                }
            });
            $(this).parent().attr("order", order + 1);
        }
        $('.modulo-item').tsort({ attr: 'order' });
    });


    $(".modulo-item a.toggle").unbind("click").click(function () {
        event.preventDefault();
        $(this).parent().parent().parent().find("ul").slideToggle();
    });


    MoverComponentes();
    removerComponente();

    $("ul, li").disableSelection();

}

function limparComponentesTodosModulos() {

    $(".modulo-item").find("li").remove();
}

function MoverComponentes() {

    $(".modulo-item a.removeModulo").unbind("click").click(function () {

        event.preventDefault();
        var $this = $(this);

        $this.parent().parent().find("li").remove();

        $(this).parent().parent().remove();

        calcularCarga();

        ListarComponentes();
    });
}


function ValidarDragnDrop(Componente) {

    var validacao = true;

    $("#modulos li").each(function () {

        var componenteModulo = RemoverAcentos($(this).text().split("(")[0].trim());
        var componente = RemoverAcentos(Componente.trim());

        if (componenteModulo == componente) {

            $("#componentes ul").sortable("cancel");
            validacao = false;
        }
    });
    return validacao;
}






function ValidarNomeModulos() {

    var validacao = true;

    $(".modelTitle").each(function () {

        if ($(this).val() == "" || $(this).val() == null) {

            validacao = false;

            //Alert 3
            $("body").append("<div id='ModalInfo3' class='modal hide fade' tabindex='-1' role='dialog' aria-labelledby='Informação' aria-hidden='true'>" +
"<div class='modal-header'>" +
" <button type='button' class='close' data-dismiss='modal' aria-hidden='true'>×</button>" +
"  <h3 id='myModalLabel'>Informação</h3>" +
"</div>" +
" <div class='modal-body'>" +
" <p>Há Módulos sem nome. Por favor inclua um nome ao seu módulo ou remova-o.</p>" +
"</div>" +
" <div class='modal-footer'>" +
" <button class='btn' data-dismiss='modal' aria-hidden='true'>Fechar</button>" +

" </div>" +
" </div>");
            $("#ModalInfo3").modal('show');
        }
    });

    return validacao;
}

function ValidarCampos() {

    var validacao = ValidarNomeModulos();

    if (validacao) {

        $(".modulo-item .ul1:not(:first)").each(function () {
            if ($.trim($(this).html()) == "" || $.trim($(this).html()) == null) {
                validacao = false;
            }
        });

        if ($(".modulo-item:not(:first)").length == 0) {
            validacao = false;
        }

        if (!validacao) {
            //Alert 4
            $("body").append("<div id='ModalInfo4' class='modal hide fade' tabindex='-1' role='dialog' aria-labelledby='Informação' aria-hidden='true'>" +
    "<div class='modal-header'>" +
     " <button type='button' class='close' data-dismiss='modal' aria-hidden='true'>×</button>" +
    "  <h3 id='myModalLabel'>Informação</h3>" +
    "</div>" +
    " <div class='modal-body'>" +
     " <p>Há Módulos sem componentes. Por favor inclua componentes ou remova-o.</p>" +
    "</div>" +
    " <div class='modal-footer'>" +
     " <button class='btn' data-dismiss='modal' aria-hidden='true'>Fechar</button>" +

    " </div>" +
    " </div>");
            $("#ModalInfo4").modal('show');
        }
    }

    return validacao;
}

function ListarComponentes() {

    var id = $("#idMatrizBusca").val();
    var busca = $("#txtBusca").val();
    var idAreaAtuacao = $("#IdAreaAtuacao").val();

    $.ajax({
        url: '/Matriz/BuscarComponentes/',
        data: { filtro: busca, idAreaAtuacao: idAreaAtuacao },
        type: "get",
        success: function (data) {

            $("#componentes").html(data);

            ValidaCarregarComponentes();
            enableDragNDrop();
            MoverComponentes()
        },
        error: function (data) {

        }
    });
    enableDragNDrop();
}

function ValidarModulos() {

    $("#modulos input").each(function (i) {
        var valor = $(this).val();
        $this = $(this);

        $("#modulos input").each(function (ii) {
            var reg = /^Modulo [0-9]+$/;
            if (reg.test($(this).val())) {
                $(this).val("Modulo " + (ii + 1));
            }
            if ($(this).parent().attr("order") != $this.parent().attr("order") && $(this).val() == $this.val()) {
                $(this).val("Modulo " + (ii + 1));
            }
        });
    });

}

function ValidarNomes() {

    $("#modulos input").each(function (i) {
        var valor = $(this).val();
        $this = $(this);

        $("#modulos input").each(function (ii) {
            var reg = /^Modulo [0-9]+$/;
            if (reg.test($(this).val())) {
                $(this).val("Modulo " + (ii + 1));
            }

            if ($(this).parent().parent().attr("order") != $this.parent().parent().attr("order") && $(this).val() == $this.val()) {
                $(this).val("Modulo " + (ii + 1));
            }
        });
    });



}

function RetirarVinculoComponente(idComponente) {
    var idMatriz = $("#IdMatriz").val();

    $.ajax({
        url: '/Matriz/RemoverVinculo/',
        data: { IdComponente: idComponente, IdMatriz: idMatriz },
        type: "get",
        datatype: "json",
        success: function (data) {
            $("#alertBox").html("<p> A Remoção foi efetuada.</p>");
            $("#alertBox").show();
        },
        error: function (data) {


        }
    });
    $.ajaxSetup({ chace: true, async: true })
}

function Submit() {
    var validate = true;
    if ($("input[name='cbComponente']:checked").length == 0) {
        validate = false;
        alert("Por favor selecione pelo menos um componente.");
    }
    return validate;
}

function removerComponente() {

    $("#modulos a.remove").unbind("click").click(function () {
        var $this = $(this).parent().parent().parent();
        var soma = 0;

        //Transfere o componente a ser excluido do módulo para a lista da componentes
        $thisLi = $(this).parent();
        $("#componentes ul").append($('<li>').append($thisLi.clone()).html());

        $thisLi.remove();

        $this.find("li").each(function (i, item) {
            if ($(item).attr("carga") != 0) {
                soma = soma + parseInt($(item).attr("carga"));
            }
        });
        $this.find('#cargaTotal').html(soma);

        calcularCarga();
    });
};

function ValidaCarregarComponentes() {

    $("#componentes ul li").each(function () {

        $thisli = $(this);

        $("#modulos .modulo-item").each(function (i) {

            $thisModulo = $(this);

            $thisModulo.find("li").each(function (ii) {

                if ($(this).attr("componentid") == $thisli.attr("componentid")) {
                    $thisli.remove();
                }

            });
        });
    });

}

function IncluirVinculo(value) {

    var newRowContent = $(value).html();
    var idMatriz = $("#IdMatriz").val();
    var idComponente = $(value).attr("id");

    $.ajax({
        url: '/Matriz/VincularComponente/',
        data: { IdComponente: idComponente, IdMatriz: idMatriz },
        type: "get",
        datatype: "json",
        success: function (data) {
            $("#tbComponentes tbody").append("<tr><td>" + newRowContent + " <button type='button' class='close' data-dismiss='alert' onclick='RetirarVinculoComponente(" + idComponente + ")'>×</button></td></tr>");

        },
        error: function (data) {


        }
    });
    $.ajaxSetup({ chace: true, async: true })


}

function remoId(value) {
    alert($(value).parent().attr("componenteid"));
}

//Esse metodo é chamado toda vez que é necessario calcular ou recalcular os dias de duração de um componente
//Nele
function calcularCarga() {
    var somaCarga = 0;

    $($(".modulos").parent().find("li")).each(function () {

        if ($(this).attr("carga") != 0) {
            somaCarga = somaCarga + parseInt($(this).attr("carga"));
        }
    });

    $(this).parent().find('#cargaTotal').html(somaCarga);

    var totalModulos = 0;
    $(".modulos").parent().parent().find('#cargaTotal').each(function () {
        totalModulos = totalModulos + parseInt($(this).html());
    });
    $('#CH').val(totalModulos);

    removerComponente();
}

$(".addModulo").click(function () {
    event.preventDefault();

    var order = 1;

    if ($("#modulos .modulo-item").length > 0) {
        order = parseInt($("#modulos .modulo-item").last().attr("order")) + 1;
    }

    $("#moduloModel .modulo-item").attr("order", order);
    var html = $("#moduloModel").html();

    $("#modulos").append(html);
    enableDragNDrop();
});

function SalvarMatriz() {

    $(".json").unbind("click").click(function (event) {
       
        var btn = $(this);

        event.preventDefault();
        var validacaoModalModulo = ValidarModalModulo();

        if (validacaoModalModulo)
            var validacao = ValidarCampos();

        if (validacao) {
            var lista = new Array();
            $("#modulos .modulo-item").each(function (i) {
                $this = $(this);

                var JsonModulo = new Object();

                JsonModulo.IdMatriz = $("#IdMatriz").val();
                JsonModulo.Nome = $(this).find(".modelTitle").val();

                JsonModulo.Componentes = new Array();

                $this.find("li").each(function (ii) {
                    JsonModulo.Componentes.push({ IdComponente: $(this).attr("componentId") });
                });

                lista.push(JsonModulo);
            });
            if ($('#ReprovacaoMatriz_observacao').val() == "" && $('#ReprovacaoMatriz_observacao:visible').val() == "") {
                if ($("#aprovarMatriz").val() != 1) {
                    $(".field-validation-error").show();
                    $('#ReprovacaoMatriz_observacao').focus();
                    return false;
                }
            }

            $.ajax({
                url: '/Matriz/ModuloTeste?i=' + $("#idMatrizBusca").val() + "&Nome=" + $("#Nome").val() + "&CH=" + $("#CH").val(),
                data: JSON.stringify(lista),
                type: "post",
                datatype: "json",
                'contentType': 'application/json',
                success: function (data) {
                    $("#frmEditarMatriz").submit();

                    if ($('form').validate().form()) {
                        btn.button('loading');
                    }
           
                },
                error: function (data) {


                }
            });

        }



    });

}
