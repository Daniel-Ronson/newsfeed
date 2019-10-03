// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var modal = $('#login-modal');
(function (window) {
    "use strict";
    var LOGIN_FORM_SELECTOR = "[id=\"login-form\"]";
    var SIGNUP_FORM_SELECTOR = "[id=\"signup-form\"]";

    var App = window.App;
    var FormHandler = App.FormHandler;
    //var login_form = new FormHandler(LOGIN_FORM_SELECTOR);
    //var signup_form = new FormHandler(SIGNUP_FORM_SELECTOR);

    //login_form.addSubmitHandler(function (data) {
    //    console.log("Login received data: " + data.userEmail);
    //});

    //signup_form.addSubmitHandler(function (data) {
    //    console.log("Signup received data: " + data.email);
    //});


})(window);

function LoadData() {
    //$("#tblStudent tbody tr").remove();
    $.ajax({
        type: 'POST',
        url: 'article/getArticles',
        dataType: 'json',
        data: { id: '' },
        success: function (data) {
            var items = '';
            $.each(data, function (i, item) {
                var card = "<div class='card mb-3'>" +
                    "<div class='row no-gutters flex-wrap-reverse'>" +
                        "<div class='col-md-8'>" +
                            " <div class='card-body'> " +
                                "<h5 class='card-title'>" + item.title + "</h5> " +
                                "<p class='card-subtitle mb-2 text-muted'>" + item.websiteName + "</p> " +
                                "<p class='card-text'> Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum  Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum</p> " +
                                "<div class='badge badge-primary'>#Technology</div> " +
                                "<div class='badge badge-primary'>#World</div>" +
                                "<div class='badge badge-primary'>#Trending</div>" +
                            "</div>" +
                        "</div>" +
                        "<div class='col-md-4 d-flex align-items-center'>" +
                            "<img src='images/wireframe.png' class='card-img pr-1' />" +
                        "</div>" +
                    "</div >" +
                    "<div class='card-footer'>" +
                    "<small class='text-muted'>Published : " + item.date.substr(0, 9) + " </small>" +
                    "</div>" +
                "</div >"


                var rows = "<tr>"
                    + "<td class='prtoducttd'>" + item.id + "</td>"
                    + "<td class='prtoducttd'>" + item.url + "</td>"
                    //+ "<td class='prtoducttd'>" + item. + "</td>"
                    + "</tr>";
                $('#cardView').append(card);
            });
        },
        error: function (ex) {
            var r = jQuery.parseJSON(response.responseText);
            alert("Message: " + r.Message);
            alert("StackTrace: " + r.StackTrace);
            alert("ExceptionType: " + r.ExceptionType);
        }
    });
    return false;
}

LoadData();

$('#login-modal').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget);
    var recipient = button.data('type');

    
    modal.find('.modal-title').text(recipient);

    if (recipient === "Signup") {
        modal.find('[id=login-form]').hide();
        modal.find('[id=signup-form]').show();   
    } else {
        modal.find('[id=signup-form]').hide();
        modal.find('[id=login-form]').show();
    }
});
