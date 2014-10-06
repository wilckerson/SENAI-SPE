/// <reference path="../jquery-1.8.2.js" />

$(document).ready(function () {
    //SetCrDropDown();

    if ($("input[name='TipoOferta']").eq(0).is(":checked")) {
        var preco = $("#PrecoView");
        preco.val("0,00");
        preco.maskMoney('destroy');
        preco.attr('readonly', true);
   
    }

    $("#IdMatriz").change(function () {
     
     var number=   $("#IdMatriz option:selected").text().replace(/[^0-9]/gi, '');
     $("#CHView").val(number);

    });

    $(".btnsalvar").click(function () {
        event.preventDefault();
        var btn = $(this);
      
        if ($("input[name='TipoOferta']:checked").attr("value") != "0") {

            if (parseFloat($("#PrecoView").val().replace(",", ".")) > 0) {
                $("form").submit();
                if ($('form').validate().form()) {
                    btn.button('loading');
                }
            } else {

                ModalALert("Informação", "O tipo da oferta está como paga, porém o preço está como 0, por favor coloque um valor " +
                    "ou coloque o tipo de oferta como gratuita");
            }
        } else {

            if ($('#ReprovacaoTurma_Observacao').val() == "" && $('#ReprovacaoTurma_observacao:visible').val() == "") {
                $(".field-validation-error").show();
            } else {
                if ($('form').validate().form()) {
                    btn.button('loading');
                }
                $("form").submit();
            }
            
        }

    });
    $("input[name='TipoOferta']").change(function (e) {
        var preco = $("#PrecoView"); 
        if ($(this).attr("readonly") !== "readonly") {
            if ($(this).attr("value") == "0") {
                preco.attr("readonly", "readonly");
                preco.val("0,00");
                preco.maskMoney('destroy');
            } else {
                preco.removeAttr("readonly");
                preco.maskMoney({ showSymbol: true, symbol: "R$", decimal: ",", thousands: "." });

            }
        } else {
            e.preventDefault();
        }
    });

    $("#aprovacaoTurma").change(function () {
        console.log("mudou!");
     
        var val = $(this).val();
        var $elm = $("#Observacao");

        if (val  == "0")
        {
            // data-val="true" data-val-required="Campo Obrigatório."
            $elm.attr("data-val", "true");
            $elm.attr("data-val-required", "Campo Obrigatório.");
        }
        else
        {
            console.log("NÂO obrigatório!");
            $elm.removeAttr("data-val");
            $elm.removeAttr("data-val-required");
        }
    });

});



//function SetCrDropDown() {
//    var qs = GetQueryStrings();
//    var myParam = qs["idCr"];
//    console.log(myParam);
//    if (myParam != undefined && myParam != null && myParam != "") {
//        $("#idCR").val(myParam)
//    }   
//}

function GetQueryStrings() {
    var assoc = {};
    var decode = function (s) { return decodeURIComponent(s.replace(/\+/g, " ")); };
    var queryString = location.search.substring(1);
    var keyValues = queryString.split('&');

    for (var i in keyValues) {
        var key = keyValues[i].split('=');
        if (key.length > 1) {
            assoc[decode(key[0])] = decode(key[1]);
        }
    }

    return assoc;
}

function RetornarTurmas() {

    var id = $("#idCR").val();
    window.location = "/Turma/Index?idCr=" + id;    
}

