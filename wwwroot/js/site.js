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

<<<<<<< Updated upstream
    //login_form.addSubmitHandler(function (data) {
    //    console.log("Login received data: " + data.userEmail);
    //});

    //signup_form.addSubmitHandler(function (data) {
    //    console.log("Signup received data: " + data.email);
    //});
=======
$(document).ready(function () {
    var genres = $('label');
    genres.click(function (event) {
        checkboxClick(event);
    });
})

var checkboxClick = function (event) {
    var target = event.currentTarget
    var genre = target.children[0].id;

    var shouldRemove = target.className.endsWith("active");
    var cardview = $('#cardView');
    cardview.empty();

    if (shouldRemove) {
        $.ajax({
            url: 'filter/removeGenre',
            data: { genre: genre },
            success: function (data) {
                cardview.empty();
                cardview.append(data);
            }
        });
    } else {
        $.ajax({
            url: 'filter/addGenre',
            data: { genre: genre },
            success: function (data) {
                cardview.empty();
                cardview.append(data);
            }
        });
    }
}
>>>>>>> Stashed changes

$('img').bind('click', function () {
    if ($(this).attr('id') == 'CNN') {
        var id = 1;
        $.ajax({
            url: '/Home/Index',
            data: { data: id },
            success: function (data) {
                alert('success CNN');
            }
        });
    }
    if ($(this).attr('id') == 'BBC') {
        var id = 3;
        $.ajax({
            url: '/Home/Index',
            data: { id : id },
            success: function (data) {
                 alert('success BBC');
            }
        });
    }
});

})(window);

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
