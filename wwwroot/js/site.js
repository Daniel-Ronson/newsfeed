let CARD_CONTAINER_SELECTOR = '#cardContainer';
let GENRE_CONTAINER_SELECTOR = '#genres';
let WEBSITE_CONTAINER_SELECTOR = ".website-container";
let ARTICLE_SELECTOR = '.card, .mb-3';
let loginModal = $('#login-modal');



// TODO: remove
(function (window) {
    "use strict";

    const App = window.App;
})(window);

$(document).ready(function () {
});

var genreClick = function (event) {
    var genre = $(event).children()[0].id;
    var shouldRemove = $(event).hasClass("selected");

    if (shouldRemove) {
        $(event).removeClass("selected");
    } else {
        $(event).addClass("selected");
    }

    filterByGenre(genre);
};

function websiteClick(e) {
    let target = $(e);
    var id = target.attr('value');

    $(WEBSITE_CONTAINER_SELECTOR).removeClass("selected");
    target.addClass("selected");

    $.ajax({
        url: 'filter/getFilteredArticles',
        data: {websiteId: id},
        success: (data => refreshCards(data))
    });
    $.ajax({
        url: 'filter/getFilteredGenres',
        data: {websiteId: id},
        success: (data => refreshGenres(data))
    });
}

function refreshCards(data) {
    let cardview = $(CARD_CONTAINER_SELECTOR);
    cardview.empty();
    cardview.append(data);
}

function refreshGenres(data) {  
    let genreView = $(GENRE_CONTAINER_SELECTOR);
    genreView.empty();
    genreView.append(data);
    genres.empty(); // Reset genre filter
}

loginModal.on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget);
    var recipient = button.data('type');

    loginModal.find('.modal-title').text(recipient);

    if (recipient === "Signup") {
        loginModal.find('[id=login-form]').hide();
        loginModal.find('[id=signup-form]').show();
    } else {
        loginModal.find('[id=signup-form]').hide();
        loginModal.find('[id=login-form]').show();
    }
});
