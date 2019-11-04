let CARD_CONTAINER_SELECTOR = '#cardContainer';
let GENRE_CONTAINER_SELECTOR = '#genreContainer';

var modal = $('#login-modal');
(function (window) {
    "use strict";

    var App = window.App;
})(window);

$(document).ready(function () {
    addGenreClick();

})

function addGenreClick() {
    var genres = $('label');
    genres.click(function (event) {
        genreClick(event);
    });
}


var genreClick = function (event) {
    var target = event.currentTarget
    var genre = target.children[0].id;

    var shouldRemove = target.className.endsWith("active");
    var cardview = $('#cardView');
    cardview.empty();

    if (shouldRemove) {
        $.ajax({
            url: 'filter/removeGenre',
            data: { genre: genre },
            success: (data => refreshCards(data))
        });
    } else {
        $.ajax({
            url: 'filter/addGenre',
            data: { genre: genre },
            success: (data => refreshCards(data))
        });
    }
}

$('img').bind('click', function () {
    var id = $(this).attr('id');
    $.ajax({
        url: 'filter/getFilteredArticles',
        data: { websiteId: id },
        success: (data => refreshCards(data))
     });
    $.ajax({
        url: 'filter/getFilteredGenres',
        data: { websiteId: id },
        success: (data => refreshGenres(data))
     });
 });

function refreshCards(data) {
    var cardview = $(CARD_CONTAINER_SELECTOR);
    cardview.empty();
    cardview.append(data);
}

function refreshGenres(data) {
    var genreView = $(GENRE_CONTAINER_SELECTOR);
    genreView.empty();
    genreView.append(data);
    addGenreClick();
}

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
