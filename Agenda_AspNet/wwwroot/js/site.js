// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


$(function () {
    // Alerta de Error e Sucesso
    function displayMessage(message, msgType) {
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "newestOnTop": false,
            "progressBar": true,
            "positionClass": "toast-bottom-right",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "10000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
        switch (msgType) {
            case "error":
                toastr.error(message)
                break
            case "success":
                toastr.success(message)
                break
            case "info":
                toastr.info(message)
                break
            case "warning":
                toastr.warning(message)
                break
        }
    }

    if ($("#success").text()) {
        displayMessage($("#success").text(), "success")
    }
    if ($("#error").text()) {
        displayMessage($("#error").text(), "error")
    }
    if ($("#warning").text()) {
        displayMessage($("#warning").text(), "warning")
    }
    if ($("#info").text()) {
        displayMessage($("#info").text(), "info")
    }
    // Fim do script do Alert de Error e Successo
})
