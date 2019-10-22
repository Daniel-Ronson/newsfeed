var modal = $('#login-modal');
(function (window) {
    "use strict";

    var App = window.App;
})(window);

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
