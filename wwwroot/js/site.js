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
