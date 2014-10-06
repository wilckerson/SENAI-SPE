function ResetaFiltrosNoBraco(dataTable, id) {
    var oSettings = dataTable.fnSettings();

    $(".camposFiltro"+id+" tr td").each(function (i) {
        if ($(this).attr("column") != undefined) {
            var column = $(this).attr("column");
            var filterString = $(this).find("input, select").val();
            oSettings.aoPreSearchCols[column].sSearch = filterString;
        }
    });

    dataTable.fnDraw();
}

var data;

$(document).ready(function () {
    data1 = $("#dataTable1").dataTable({
        "bPaginate": false,
        "bLengthChange": false,
        "bFilter": true,
        "bSort": true,
        "bInfo": false,
        "bAutoWidth": false
    });


    $("#dataTable1 thead th").each(function (i) {
        if ($(this).attr("search") != undefined) {
            $(".camposFiltro1 tr").append("<td>" + this.innerHTML + "</td>");
            $(".camposFiltro1 tr").append("<td column='" + i + "'><input value='' /></td>");
        }
    });

    $('.camposFiltro1 tr td input').bind("keyup", function () {
        ResetaFiltrosNoBraco(data1, 1);
        //data.fnFilter($(this).val(), $(this).attr("column"),false,true);
    });

    data2 = $("#dataTable2").dataTable({
        "bPaginate": false,
        "bLengthChange": false,
        "bFilter": true,
        "bSort": true,
        "bInfo": false,
        "bAutoWidth": false
    });


    $("#dataTable2 thead th").each(function (i) {
        if ($(this).attr("search") != undefined) {
            $(".camposFiltro2 tr").append("<td>" + this.innerHTML + "</td>");
            $(".camposFiltro2 tr").append("<td column='" + i + "'><input value='' /></td>");
        }
    });

    $('.camposFiltro2 tr td input').bind("keyup", function () {
        ResetaFiltrosNoBraco(data2, 2);
        //data.fnFilter($(this).val(), $(this).attr("column"),false,true);
    });

    data3 = $("#dataTable3").dataTable({
        "bPaginate": false,
        "bLengthChange": false,
        "bFilter": true,
        "bSort": true,
        "bInfo": false,
        "bAutoWidth": false
    });


    $("#dataTable3 thead th").each(function (i) {
        if ($(this).attr("search") != undefined) {
            $(".camposFiltro3 tr").append("<td>" + this.innerHTML + "</td>");
            $(".camposFiltro3 tr").append("<td column='" + i + "'><input value='' /></td>");
        }
    });

    $('.camposFiltro3 tr td input').bind("keyup", function () {
        ResetaFiltrosNoBraco(data3, 3);
        //data.fnFilter($(this).val(), $(this).attr("column"),false,true);
    });
});