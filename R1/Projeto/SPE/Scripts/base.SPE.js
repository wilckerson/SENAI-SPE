/// <reference path="jquery-1.8.2.min.js" />

$(document).ready(function () {
    $.datepicker.regional['br'] = {
        showButtonPanel: true,
        closeText: 'OK',
        prevText: 'Anterior',
        nextText: 'Próximo',
        currentText: 'Hoje',
        monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho',
        'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
        monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun',
        'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
        dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado'],
        dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sab'],
        dayNamesMin: ['Do', 'Se', 'Te', 'Qua', 'Qui', 'Se', 'Sa'],
        weekHeader: 'Semana',
        dateFormat: 'dd/mm/yy',
        firstDay: 1,
        isRTL: false,
        showMonthAfterYear: false,
        yearSuffix: ''
    };
    $.datepicker.setDefaults($.datepicker.regional['br']);
    $("#CPF").mask("999.999.999-99");

    $("#Preco").maskMoney({ showSymbol: true, symbol: "R$", decimal: ",", thousands: "." });
    $("div[alt='tootip']").tooltip();
    $('.datepicker').not("[readonly='readonly']").datepicker({
        format: 'dd/mm/yyyy',
    });
    function setCampos() {
        $("input[mascara]").each(function () {
            $(this).mask($(this).attr("mascara"));
        });

        $("input[tipo]").each(function () {
            var tipo = $(this).attr("tipo");
            if (tipo == "data") {
                //$(this).datepicker();
                geraCalendario($(this).attr("id"));
            }
            if (tipo == "moeda")
                mascaraMoeda($(this).attr("id"));
            if (tipo == "telefone")
                $(this).setMask("(99) 9999-99999").ready(function(event) {
                    var target, phone, element;
                    target = (event.currentTarget) ? event.currentTarget : event.srcElement;
                    phone = target.value.replace(/\D/g, '');
                    element = $(target);
                    element.unsetMask();
                    if(phone.length > 10) {
                        element.setMask("(99) 99999-9999");
                    } else {
                        element.setMask("(99) 9999-99999");
                    }
                });

            if (tipo == "email")
                $(this).blur(campoEmail($(this).attr("id")));
            if (tipo == "numero")
                $(this).keypress(verificaNumero);
        });

        function verificaNumero(tecla) {
            if (tecla.which != 8 && tecla.which != 0 && (tecla.which < 48 || tecla.which > 57)) {
                return false;
            }
        }

        $("input[obrigatorio]").blur(function () {
            if ($(this).val() != "") {
                $(this).removeClass("erro");
                $(this).attr("title", "");
            } else {
                $(this).addClass("erro");
                $(this).attr("title", "Este campo deverá ser preenchido");
            }
        });

        $("input[type='text']").each(function () {

            if ($(this).attr("mascara") == undefined) {
                if ($(this).attr("tipo") == undefined) {
                    $(this).keypress(function () {
                        retirarAcento($(this));
                    });
                    $(this).blur(function () {
                        retirarAcento($(this));
                    });
                    $(this).keyup(function () {
                        retirarAcento($(this));
                    });
                }
            }
        });
    }

    function campoEmail(id) {

        $("#" + id).blur(function () {
            if ($(this).val() != "") {
                var vl = $.validateEmail($(this).val());
                if (vl) {
                    $(this).attr("class", "text");
                    $(this).attr("title", "");
                } else {
                    $(this).attr("class", "textErro");
                    $(this).attr("title", "O e-mail informado é inválido.");
                }
            } else {
                $(this).attr("class", "text");
                $(this).attr("title", "");
            }
        });
    }
    $('.user').dropdown();
    //$('#sair').click(function () {
    //    window.location = "/login/logout";
    //});
    //$("#editar").click(function () {
    //    var idUsuario = $("#userid").val();
    //    window.location = "/usuario/editar/" + idUsuario;
    //});

});

function IncluirModal(mensagem) {

    var random = Math.floor((Math.random() * 1000) + 1);

    $("body").append("<div id='ModalInfo" + random + "' class='modal hide fade' tabindex='-1' role='dialog' aria-labelledby='Informação' aria-hidden='true'>" +
   "<div class='modal-header'>" +
    " <button type='button' class='close' data-dismiss='modal' aria-hidden='true'>×</button>" +
   "  <h3 id='myModalLabel'>Informação</h3>" +
   "</div>" +
  " <div class='modal-body'>" +
    " <p>" + mensagem + "</p>" +
   "</div>" +
  " <div class='modal-footer'>" +
    " <button class='btn' data-dismiss='modal' aria-hidden='true'>Fechar</button>" +

  " </div>" +
" </div>");
    $("#ModalInfo" + random + "").modal('show');
}

function IncluirModalConfirm(titulo, mensagem, callbackOk, callbackCancel) {

    var random = Math.floor((Math.random() * 1000) + 1);

    $("body").append("<div id='ModalInfo" + random + "' class='modal hide fade' tabindex='-1' role='dialog' aria-labelledby='Informação' aria-hidden='true'>" +
   '<div class="modal-header">' +
       ' <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>' +
        '<h3 id="myModalLabel">' + titulo + '</h3>' +
    '</div>' +
    '<div class="modal-body">' +
        '<p>' + mensagem + '</p>' +
    '</div>' +
    '<div class="modal-footer">' +
        '<button id="btnConfirm' + random + '" class="btn btn-primary" data-dismiss="modal" aria-hidden="true">Confirmar</button>' +
        '<button id="btnCancel' + random + '" class="btn" data-dismiss="modal" aria-hidden="true" onclick="callbackCancel()">Fechar</button>' +
    '</div>');

    $('#btnConfirm' + random).click(function () {
        if (callbackOk) { callbackOk(); }
        deleteModal(random);
    });

    $('#btnCancel' + random).click(function () {
        if (callbackCancel) { callbackCancel(); }
        deleteModal(random);
    });

    function deleteModal(_random) {
        $("#ModalInfo" + _random).remove();
        $(".modal-backdrop:first").remove();
    }

    $("#ModalInfo" + random + "").modal('show');
}

function Excluir(item) { // Função que abre o modal de exclusão
    var url = $("#hrefModal").attr("href");
    url += item;
    $("#hrefModal").attr("href", url);
    $("#modalExcluir").click();
}

function Editar(url) {
    window.location = url;
}

function Contem(contem, contido) {

    if (contem.indexOf(contido) > -1 || contido.indexOf(contem) > -1) {
        return true;

    } else {

        return false;
    }
}

function AllowOnlyString(key) {

    var datakey = (key.which) ? key.which : key.keyCode; //capturing pressed key

    if (datakey == 46)//allowing delete key
        return true;
    else if (datakey == 8)//allowing backspace ke
        return true;
    else if (datakey == 44)
        return false;
    else if (!(datakey < 48 || datakey > 57))//avoiding charecters other than digits

        return false;

}

function AllowOnlyNumbers(key) {

    var datakey = (key.which) ? key.which : key.keyCode; //capturing pressed key

    if (datakey == 46)//allowing delete key
        return true;
    else if (datakey == 8)//allowing backspace ke
        return true;
    else if (datakey == 44)
        return false;
    else if ((datakey < 48 || datakey > 57))//avoiding charecters other than digits

        return false;

}

(function ($) {
    m = {
        '\b': '\\b',
        '\t': '\\t',
        '\n': '\\n',
        '\f': '\\f',
        '\r': '\\r',
        '"': '\\"',
        '\\': '\\\\'
    },
    $.toJSON = function (value, whitelist) {
        var a,          // The array holding the partial texts.
            i,          // The loop counter.
            k,          // The member key.
            l,          // Length.
            r = /["\\\x00-\x1f\x7f-\x9f]/g,
            v;          // The member value.

        switch (typeof value) {
            case 'string':
                return r.test(value) ?
                '"' + value.replace(r, function (a) {
                    var c = m[a];
                    if (c) {
                        return c;
                    }
                    c = a.charCodeAt();
                    return '\\u00' + Math.floor(c / 16).toString(16) + (c % 16).toString(16);
                }) + '"' :
                '"' + value + '"';

            case 'number':
                var numberReturn = 'null';
                if (isFinite(value)) {
                    if (value.toString().indexOf('.') > 0) {
                        numberReturn = String(value);
                    }
                    else {
                        numberReturn = '"' + String(value) + '"';
                    }
                }
                return numberReturn;

            case 'boolean':
            case 'null':
                return String(value);

            case 'object':
                if (!value) {
                    return 'null';
                }
                if (typeof value.toJSON === 'function') {
                    return $.toJSON(value.toJSON());
                }
                a = [];
                if (typeof value.length === 'number' &&
                    !(value.propertyIsEnumerable('length'))) {
                    l = value.length;
                    for (i = 0; i < l; i += 1) {
                        a.push($.toJSON(value[i], whitelist) || 'null');
                    }
                    return '[' + a.join(',') + ']';
                }
                if (whitelist) {
                    l = whitelist.length;
                    for (i = 0; i < l; i += 1) {
                        k = whitelist[i];
                        if (typeof k === 'string') {
                            v = $.toJSON(value[k], whitelist);
                            if (v) {
                                a.push($.toJSON(k) + ':' + v);
                            }
                        }
                    }
                } else {
                    for (k in value) {
                        if (typeof k === 'string') {
                            v = $.toJSON(value[k], whitelist);
                            if (v) {
                                a.push($.toJSON(k) + ':' + v);
                            }
                        }
                    }
                }
                return '{' + a.join(',') + '}';
        }
    };
})(jQuery);

// Esse método ativa a modal alert e recebe como parâmetros o Título e Descrição do a ser Mostrada.
function ModalALert(Titulo, Descricao) {

    var model = new Object();
    model.Titulo = Titulo;
    model.Descricao = Descricao;

    $("#ModalInfo2").remove();

    $("body").append("<div id='ModalInfo2' class='modal hide fade' tabindex='-1' role='dialog' aria-labelledby='Informação' aria-hidden='true'>" +
"<div class='modal-header'>" +
" <button type='button' class='close' data-dismiss='modal' aria-hidden='true'>×</button>" +
"  <h3 id='myModalLabel'>" + Titulo + " </h3>" +
"</div>" +
" <div class='modal-body'>" +
" <p>" + Descricao + "</p>" +
"</div>" +
" <div class='modal-footer'>" +
" <button class='btn' data-dismiss='modal' aria-hidden='true'>Fechar</button>" +

" </div>" +
" </div>");

    $("#ModalInfo2:last").modal('show');
}

function addAreaAtuacaoComponente() {
    $(".addAreaAtuacaoComponente").click(function () {
        var item = '';
        var idAreaAtuacao = '#' + $(this).attr("id");

        if ($('#tableAreaAtuacaoComponenteEscolhidos tbody').find(idAreaAtuacao).length) {
            IncluirModal("O Docente já possui esta Área de Atuação");
        }
        else {
            $('#tableAreaAtuacaoComponenteEscolhidos tbody').append($(this).parent().parent().clone()).find("icon:last").attr(
                {
                    class: "icon-remove"
                });

            $("#spanAreaAtuacao").html("");
            removeAreaAtuacaoComponente();
        }
        ListarComponente();
    });

}

function removeAreaAtuacaoComponente() {
    $(".icon-remove").click(function () {
        $(this).parent().parent().remove();
    });
}

function AddComponente() {
    $(".addComponenteMatriz").click(function () {
        $.ajax({
            url: '/Componente/ModalComponente/',
            type: "get",
            datatype: "json",

            success: function (data) {

                $("#ModalComponente").modal();
                $("#ModalComponente").html(data);
                $("#ModalComponente").show();
                addAreaAtuacaoComponente();
                removeAreaAtuacaoComponente()

            },
            error: function (data) {
            }
        });
    });
}

function ValidarFormComponente() {
    var validate = true;

    $("#frmComponente :input[type=text]").each(function () {
        var $this = $(this);
        var $span = $this.next();

        if ($this.val() == "") {
            $span.html("Campo Obrigatório");
            $this.unbind("keyup").keyup(function () {
                $span.html("");
            });
            validate = false;
        }
    });

    var $ch = $("#iptCH")
    var CH = parseInt($ch.val()) == 0 ? true : false;

    if (CH) {
        validate = false;
        $ch.next().html("A CH deve ser maior que zero");
        $ch.unbind("keyup").keyup(function () {
            $ch.next().html("");
        });
        validate = false;
    }

    if ($("#frmComponente :input[type=checkbox]:checked").length == 0) {
        validate = false;
        $("#spanTipoAmbiente").html("Campo Obrigatório");

        $("#frmComponente :input[type=checkbox]").click(function () {
            $("#spanTipoAmbiente").html("");
        });
    }

    if ($("#tableAreaAtuacaoComponenteEscolhidos").find("icon").length == 0) {
        validate = false;
        $("#spanAreaAtuacao").html("Campo Obrigatório");

    }

    return validate;
}

function SalvarComponenteMatriz() {
    if (ValidarFormComponente()) {
        var Componente = new Object();

        Componente.IdComponente = 0;
        Componente.Nome = $.trim($("#frmComponente :input[type=text]")[0].value);
        Componente.CH = $.trim($("#frmComponente :input[type=text]")[1].value);

        var arrayIdTipoAmbiente = new Array();
        $("#frmComponente :input[type=checkbox]:checked").each(function () {
            var $this = $(this);
            arrayIdTipoAmbiente.push($this.val());
        });

        var arrayIdAreaAtuacao = new Array();
        $("#tableAreaAtuacaoComponenteEscolhidos").find("icon").each(function () {
            var $this = $(this);
            arrayIdAreaAtuacao.push($this.attr('id'));
        });

        Componente.AreaAtuacaoId = arrayIdAreaAtuacao.join(',');
        Componente.TipoAmbienteId = arrayIdTipoAmbiente.join(',');

        $.ajax({
            url: '/Componente/ModalCadastrarComponente/',
            data: JSON.stringify(Componente),
            type: "post",
            datatype: "json",
            'contentType': 'application/json',
            success: function (data) {

                if (data != "false") {
                    $(".componentes .ul1").append("<li componentid='" + data.ComponenteId + "' carga='" + data.CH + "' class='ui-draggable' style='position: relative; cursor:pointer; cursor:hand -webkit-touch-callout: none;" +
                         "-webkit-user-select: none;" +
                         "-khtml-user-select: none;" +
                         "-moz-user-select: moz-none;" +
                         "-ms-user-select: none;" +
                         "user-select: none;'>" + data.Nome + " <a href='' onclick='return false;' class='remove' style='margin-left:4px;'> <icon class='icon-remove'></icon></a><span id='cargaTotal' style='font-weight:bold; float:right'>(" + data.CH + " h)  </span></li>");

                    if ($("#componentesDocente").length > 0) {

                        $("#componentesDocente li").each(function () {
                            $this = $(this)

                            if ($this.find("#cargaTotal") != undefined && $this.find("#cargaTotal").length > 0) {
                                $this.find("#cargaTotal").html("");
                            }
                        });

                    }

                    $("#ModalComponente").modal('hide');
                } else {
                    var $nome = $("#iptNome");
                    var $span = $nome.next().html("Componente já existente.");
                    $nome.unbind("keyup").keyup(function () {
                        $span.html("");
                    });

                }
            },
            error: function (data) {

            }
        });
        return false;
    }

}


function RemoverAcentos(value) {

    value = value.replace(new RegExp('[ÁÀÂÃ]', 'gi'), 'A');
    value = value.replace(new RegExp('[ÉÈÊ]', 'gi'), 'E');
    value = value.replace(new RegExp('[ÍÌÎ]', 'gi'), 'I');
    value = value.replace(new RegExp('[ÓÒÔÕ]', 'gi'), 'O');
    value = value.replace(new RegExp('[ÚÙÛ]', 'gi'), 'U');
    value = value.replace(new RegExp('[Ç]', 'gi'), 'C');

    return value.toLowerCase();
}

function prepararIntervaloDatas ($DataInicio, $DataFim) {

    $DataInicio.change(function () {

        var dataInicio = $DataInicio.datepicker("getDate");

        if (dataInicio) {
            //RG10
            $DataFim.datepicker("setDate", dataInicio);

            $DataFim.datepicker('option', "minDate", dataInicio);
        }
    });

    //if ($DataInicio.val() == "01/01/0001" || !$DataInicio.val()) {
    //    $DataInicio.datepicker("setDate", new Date());           
    //}
    //$DataInicio.change();
}

String.prototype.ReplaceAll = function (text, replacetext) {

    var reg = new RegExp(text, "g");
    return this.replace(reg, replacetext);
}

function validarCNPJ(cnpj) {

    cnpj = cnpj.replace(/[^\d]+/g, '');

    if (cnpj == '') return false;

    if (cnpj.length != 14)
        return false;

    // Elimina CNPJs invalidos conhecidos
    if (cnpj == "00000000000000" ||
        cnpj == "11111111111111" ||
        cnpj == "22222222222222" ||
        cnpj == "33333333333333" ||
        cnpj == "44444444444444" ||
        cnpj == "55555555555555" ||
        cnpj == "66666666666666" ||
        cnpj == "77777777777777" ||
        cnpj == "88888888888888" ||
        cnpj == "99999999999999")
        return false;

    // Valida DVs
    tamanho = cnpj.length - 2
    numeros = cnpj.substring(0, tamanho);
    digitos = cnpj.substring(tamanho);
    soma = 0;
    pos = tamanho - 7;
    for (i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2)
            pos = 9;
    }
    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    if (resultado != digitos.charAt(0))
        return false;

    tamanho = tamanho + 1;
    numeros = cnpj.substring(0, tamanho);
    soma = 0;
    pos = tamanho - 7;
    for (i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2)
            pos = 9;
    }
    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    if (resultado != digitos.charAt(1))
        return false;

    return true;

}