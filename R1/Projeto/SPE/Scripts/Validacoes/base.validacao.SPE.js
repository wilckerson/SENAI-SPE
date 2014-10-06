/// <reference path="../jquery-1.8.2.min.js" />


$(document).ready(function () {

    $("input:submit").click(function () {
		var cpf = $("form :input:text[name='CPF']");
		var consulta = true;

		if ($("form :input:text[name='CPF']").length > 0)
		    return ValidaCpf($("#CPF"));
		else
            return true
		
	});

	$("#CPF").focusout(function () { // Evento que dispara a validação de CPF 
		var cpf = $("#CPF").val();
		var resulta = ValidaCpf($(this));
	});


	$("input:submit").click(function () {
	    var cnpj = $("form :input:text[name='CNPJ']");
	    var consulta = true;
	    
	    if ($("form :input:text[name='CNPJ']").length > 0)
	        return ValidaCnpj($("#CNPJ"));
	    else
	        return true
	});

	$("#CNPJ").focusout(function () { // Evento que dispara a validação de CNPJ 
	    var cnpj = $("#CNPJ").val();
	    var resulta = ValidaCnpj($(this));
	});

});


function ValidaCpf(value) {

	var str = value.val();

	str = str.replace('.', '');
	str = str.replace('.', '');
	str = str.replace('-', '');

	cpf = str;
	var numeros, digitos, soma, i, resultado, digitos_iguais;
	digitos_iguais = 1;
	if (cpf.length < 11)
		return false;
	for (i = 0; i < cpf.length - 1; i++)
		if (cpf.charAt(i) != cpf.charAt(i + 1)) {
			digitos_iguais = 0;
			break;
		}
	if (!digitos_iguais) {
		numeros = cpf.substring(0, 9);
		digitos = cpf.substring(9);
		soma = 0;
		for (i = 10; i > 1; i--)
			soma += numeros.charAt(10 - i) * i;
		resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
		if (resultado != digitos.charAt(0)) {
			value.css("background-color", "#ff7373");
			return false;
		}
		numeros = cpf.substring(0, 10);
		soma = 0;
		for (i = 11; i > 1; i--)
			soma += numeros.charAt(11 - i) * i;
		resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
		if (resultado != digitos.charAt(1)) {
			value.css("background-color", "#ff7373");
			return false;
		} else {
			value.css("background-color", "#ffffff");
			return true;
		}
	}
	else {
		value.css("background-color", "#ff7373");
		return false;
	}
}

function ValidaCnpj(value) {
    
    var str = value.val();
    str = str.replace(/[^\d]+/g, '');

    if (str == '') {
        value.css("background-color", "#ff7373");
        return false;
    }

    if (str.length != 14) {
        value.css("background-color", "#ff7373");
        return false;
    }

    // Elimina CNPJs invalidos conhecidos
    if (str == "00000000000000" ||
        str == "11111111111111" ||
        str == "22222222222222" ||
        str == "33333333333333" ||
        str == "44444444444444" ||
        str == "55555555555555" ||
        str == "66666666666666" ||
        str == "77777777777777" ||
        str == "88888888888888" ||
        str == "99999999999999") {
        value.css("background-color", "#ff7373");
        return false;
    }

    // Valida DVs
    tamanho = str.length - 2
    numeros = str.substring(0, tamanho);
    digitos = str.substring(tamanho);
    soma = 0;
    pos = tamanho - 7;
    for (i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2)
            pos = 9;
    }
    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    if (resultado != digitos.charAt(0)) {
        value.css("background-color", "#ff7373");
        return false;
    }

    tamanho = tamanho + 1;
    numeros = str.substring(0, tamanho);
    soma = 0;
    pos = tamanho - 7;
    for (i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2)
            pos = 9;
    }
    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    if (resultado != digitos.charAt(1)) {
        value.css("background-color", "#ff7373");
        return false;
    }

    value.css("background-color", "#ffffff");
    return true;

}