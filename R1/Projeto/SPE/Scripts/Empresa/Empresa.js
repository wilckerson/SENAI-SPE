$(document).ready(function () {
    $('input[type=submit]').click(function (e) {

        var cnpj = $("#CNPJ").val();
        var cnpjValMsg = $("span[data-valmsg-for=CNPJ]");

        var valCNPJ = validarCNPJ(cnpj);
        if (!valCNPJ)
        {           
            cnpjValMsg.addClass("field-validation-error");
            cnpjValMsg.html("CNPJ Inválido!");

            e.preventDefault();
            return;
        }
        else
        {
            cnpjValMsg.removeClass("field-validation-error");
            cnpjValMsg.html("");
        }

        if ($('form').validate().form()) {

            $(this).button('loading');
        }
    });
});