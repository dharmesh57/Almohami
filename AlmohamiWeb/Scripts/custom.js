
// Menu navigation active

//$(function () {

//    $('ul li').click(function () {
//        $('li').removeClass("active");

//        $(this).addClass("active");
//    });
//});


// Dynamic errors from model
function GetErrorsFromModel(data) {
    $.each(data.Errors, function (key, value) {
        if (value != null) {
            $("#Err_" + key).html(value[value.length - 1].ErrorMessage);
        }
    });
}

function showloader() {
    $('.customloader').loader('show');
}

function hideloader() {
    $('.customloader').loader('hide');
}

function GetValidationMsg(xhr) {
    for (var i = 0; i < xhr.responseJSON.length; i++) {
        $('span[data-valmsg-for="' + xhr.responseJSON[i].key + '"]').text(xhr.responseJSON[i].errors);
    }
}