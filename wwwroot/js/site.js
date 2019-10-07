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
            $.each(data, function (i, item) {
                var $card = $("<div></div>", {
                    "class": "card mb-3"
                })

                var $row = $('<div></div>', {
                    "class": "row no-gutters flex-wrap-reverse"
                })

                var $col8 = $('<div></div>', {
                    "class": "col-md-8"
                })

                var $cardBody = $('<div></div>', {
                    "class": "card-body"
                })

                $cardBody.append("<h5 class='card-title'>" + item.title + "</h5> ");
                $cardBody.append("<p class='card-subtitle mb-2 text-muted'>" + item.websiteName + "</p>");
                $cardBody.append("<p class='card-text'> Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum  Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum</p> ");

                item.genres.forEach(function (genre) {
                    $cardBody.append("<div class='badge badge-primary'>#" + genre + "</div> ")
                });

                var $col4 = $('<div></div>', {
                    "class": "col-md-4 d-flex align-items-center"
                })

                var $img = $('<img></img>', {
                    "class": "card-img pr-1",
                    "src": 'images/wireframe.png'
                })

                var $footer = $("<div></div>", {
                    "class": "card-footer"
                })

                $footer.append("<small class='text-muted'>Published : " + item.date.substr(0, 9) + " </small>")

                $col8.append($cardBody);
                $col4.append($img);
                $row.append($col8);
                $row.append($col4);
                $card.append($row);
                $card.append($footer);
                $('#cardView').append($card);
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
