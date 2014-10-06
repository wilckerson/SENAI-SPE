$(document).ready(function () {
    $.fn.dataTableExt.oApi.fnGetColumnData = function (oSettings, iColumn, bUnique, bFiltered, bIgnoreEmpty) {
        // check that we have a column id
        if (typeof iColumn == "undefined") return new Array();

        // by default we only want unique data
        if (typeof bUnique == "undefined") bUnique = true;

        // by default we do want to only look at filtered data
        if (typeof bFiltered == "undefined") bFiltered = true;

        // by default we do not want to include empty values
        if (typeof bIgnoreEmpty == "undefined") bIgnoreEmpty = true;

        // list of rows which we're going to loop through
        var aiRows;

        // use only filtered rows
        if (bFiltered == true) aiRows = oSettings.aiDisplay;
            // use all rows
        else aiRows = oSettings.aiDisplayMaster; // all row numbers

        // set up data array   
        var asResultData = new Array();

        for (var i = 0, c = aiRows.length; i < c; i++) {
            iRow = aiRows[i];
            var aData = this.fnGetData(iRow);
            var sValue = aData[iColumn];

            // ignore empty values?
            if (bIgnoreEmpty == true && sValue.length == 0) continue;

                // ignore unique values?
            else if (bUnique == true && jQuery.inArray(sValue, asResultData) > -1) continue;

                // else push the value onto the result data array
            else asResultData.push(sValue);
        }

        return asResultData;
    }

}(jQuery));


function ResetaFiltrosNoBraco(dataTable) {
    var oSettings = dataTable.fnSettings();

    $(".camposFiltro tr td").each(function (i) {
        if ($(this).attr("column") != undefined) {
            var column = $(this).attr("column");
            var filterString = $(this).find("input, select").val();
            oSettings.aoPreSearchCols[column].sSearch = filterString;
        }
    });

    dataTable.fnDraw();
}


function fnCreateSelect(aData) {


    var r = '<select><option value=""></option>';

    var data = aData + "";

    if (data.indexOf("<li>") != -1) {

        data = data.replace(/<ul>|<\/ul>|,|<\/li>/g, "").split("<li>");
        var arrDistinct = {}
        for (var ii = 0; ii < data.length; ii++) {
            data[ii] = data[ii].trim();

            //Cria a opção se possui valor ou se nao se repetiu
            if (data[ii] != "" && !arrDistinct.hasOwnProperty(data[ii])) {
                r += '<option value="' + data[ii] + '">' + data[ii] + '</option>';
                arrDistinct[data[ii]] = data[ii];
            }
        }
    }
    else
    {
        aData.sort(function (a, b) { return a - b; });

        var arrDistinct = {}
        for (var i = 0 ; i < aData.length ; i++) {

            if (aData[i] != "" && !arrDistinct.hasOwnProperty(aData[i])) {
                r += '<option value="' + aData[i] + '">' + aData[i] + '</option>';
                arrDistinct[aData[i]] = aData[i];
            }
        }
    }
    return r + '</select>';
}

var data;

$(document).ready(function () {
    data = $("#dataTable").dataTable({
        "bPaginate": true,
        "sPaginationType": "full_numbers",
        "bLengthChange": false,
        "bFilter": true,
        "bSort": true,
        "bInfo": false,
        "bAutoWidth": false,
        "oLanguage": {
            "sProcessing": "A processar...",
            "sLengthMenu": "Mostrar _MENU_ registos",
            "sZeroRecords": "Não foram encontrados resultados",
            "sInfo": "Mostrando de _START_ até _END_ de _TOTAL_ registos",
            "sInfoEmpty": "Mostrando de 0 até 0 de 0 registos",
            "sInfoFiltered": "(filtrado de _MAX_ registos no total)",
            "sInfoPostFix": "",
            "sSearch": "Procurar:",
            "sUrl": "",
            "oPaginate": {
                "sFirst": "Primeiro",
                "sPrevious": "Anterior",
                "sNext": "Seguinte",
                "sLast": "Último"
            }
        }
    });


    $("#dataTable thead th").each(function (i) {
        if ($(this).attr("filtered") != undefined) {
            $(".camposFiltro tr").append("<td>" + this.innerHTML + "</td>");
            $(".camposFiltro tr").append("<td column='" + i + "'>" + fnCreateSelect(data.fnGetColumnData(i)) + "</td>");
        }
        if ($(this).attr("search") != undefined) {
            $(".camposFiltro tr").append("<td>" + this.innerHTML + "</td>");
            $(".camposFiltro tr").append("<td column='" + i + "'><input value='' /></td>");
        }
    });

    $('.camposFiltro tr td select').change(function () {
        ResetaFiltrosNoBraco(data);
        //data.fnFilter($(this).val(), $(this).attr("column"), false, true);
    });
    $('.camposFiltro tr td input').bind("keyup", function () {
        ResetaFiltrosNoBraco(data);
        //data.fnFilter($(this).val(), $(this).attr("column"),false,true);
    });
});