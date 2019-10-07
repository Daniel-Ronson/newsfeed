// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var modal = $('#login-modal');
(function (window) {
    "use strict";

    var App = window.App;
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
