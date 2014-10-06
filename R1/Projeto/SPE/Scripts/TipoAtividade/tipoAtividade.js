/// <reference path="jquery-1.8.2.js" />
/// <reference path="jquery-ui-1.8.24.js" />
/// <reference path="jquery.validate.js" />


$(document).ready(function () {
    $('input[type=submit]').click(function () {
        if ($('form').validate().form()) {
            $(this).button('loading');

        }
    });

});