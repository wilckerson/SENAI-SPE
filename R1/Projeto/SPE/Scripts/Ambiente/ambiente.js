/// <reference path="../jquery-1.8.2.js" />
$(document).ready(function () {
    ckbClick();
    RemoverRecurso();
    AddRecurso();

    $(".boxMarcado").click(function () {
        ckbClick();
    });
    $('.tooltiper').tooltip('hide');
    $('.tooltiper').click(function (event) {
        event.preventDefault();
        return false;
    });

    $('input[type=submit]').click(function () {
        if ($('form').validate().form()) {
            $(this).button('loading');

        }
    });

});

function ckbClick() {
    var isEditarAmbiente = $(document).find("#isEditarAmbiente").val();
    var lista = "";

    $(document).find('#tableRecursoEscolhido tbody tr').each(function () {
        //id-status;id-status;id-status;id-status
        if (isEditarAmbiente == "True") {
            //Devo ver se o input está marcado e colocar o valor
            var checked = $(this).find('.boxMarcado').prop('checked');
            $this = $(this).find('.icon-remove');

            if (checked) {
                lista = lista + $this.prop('id') + '-1' + ";"
            } else {
                lista = lista + $this.prop('id') + '-0' + ";"
            }
            
        } else {
            $this = $(this).find('.icon-remove');
            lista = lista + $this.prop('id') + '-1' + ";"
        }

    });

    $('#HiddenListaRecurso').val(lista);

}

function tooltip(option) {
    $('input[class=boxMarcado]').each(function () {
        $this = $(this);
        if ($this.is(':disabled'))
            $this.tooltip(option);
    });
}

function RemoverRecurso() {

    $(document).delegate(".removeRecurso", "click", function () {
        var linha = $(this).parents("tr");
        var isEditarAmbiente = $(document).find("#isEditarAmbiente").val();

        if (isEditarAmbiente == "True") {
            linha.find("input").parent().remove();
        }

        //alert(linha.html())

        $("#dataTable tbody").append(linha).find(".icon").attr({
            "class": "icon icon-plus-sign addRecurso"
        });

        //AddRecurso();
        ckbClick();
    });    
}

function AddRecurso() {
    $(document).delegate(".addRecurso", "click", function () {
        var linha = $(this).parents("tr");
        var isEditarAmbiente = $(document).find("#isEditarAmbiente").val();

        if (isEditarAmbiente == "True") {
            var id = $(this).attr('id');
           
            if (linha.find("input").html() != "") {
                linha.append('<td><input name="' + id + '" type="checkbox" value="true" class="boxMarcado" checked></td>');
            }
        }

        $("#tableRecursoEscolhido tbody").append(linha).find(".icon").attr({
            "class": "icon icon-remove removeRecurso"
        });

        //RemoverRecurso();
        ckbClick();
    });
}