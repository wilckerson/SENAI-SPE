$(document).ready(function () {
    $('input[type=submit]').click(function () {
        if ($('form').validate().form()) {
            $(this).button('loading');

        }
    });
});

function SelecionarUsuario() {
    $("#drpUsuario").change(function () {

        //Obtendo o valor do dropDown do usuário selecionado
        var jsonValue = $("#drpUsuario option:selected").val();

        if (jsonValue) {
            //Json gerado pera partial _ListaUsuarioLogin.cshtml
            var value = JSON.parse(jsonValue);

            //Atribuindo o valor nos campos
            $('#Nome').val(value.userName);
            $('#Email').val(value.email);
        }
    });
}

function BuscaUsuarios() {
    var nome = $("#txtNomeUsuario").val();
    $("#drpUsuario").append("<option class='temp' selected='selected'>Carregando...<option>");
    event.preventDefault();
    $.ajax({
        url: '/usuario/BuscarUsuario/',
        data: { nome: nome },
        type: "post",
        success: function (data) {
            $('#divUsuario').html(data);
            SelecionarUsuario();
            $(".temp").remove();
        }
    });
}