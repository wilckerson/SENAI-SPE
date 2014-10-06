$(document).ready(function () {
    $('input[type=submit]').click(function () {
        if ($('form').validate().form()) {
            $(this).button('loading');

        }
    });

});