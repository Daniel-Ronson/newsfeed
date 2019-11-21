let CARD_CONTAINER_SELECTOR = '#cardContainer';
let GENRE_CONTAINER_SELECTOR = '#genres';
let WEBSITE_CONTAINER_SELECTOR = ".website-container";

let loginModal = $('#login-modal');
$('#settings_dropdown').click(function (e) {
   // e.preventDefault();
    e.stopPropagation();
});
(function (window) {
    "use strict";

    const App = window.App;
})(window);

$(document).ready(function () {
    addGenreClick();
    addWebsiteClick();
});

function addGenreClick() {
    var genres = $('#genres label');
    genres.click(function (event) {
        genreClick(event);
    });
}

var genreClick = function (event) {
    var target = event.currentTarget;
    var genre = target.children[0].id;

    var shouldRemove = target.className.endsWith("selected");
    var cardview = $(CARD_CONTAINER_SELECTOR);
    cardview.empty();

    if (shouldRemove) {
        $(target).removeClass("selected");

        $.ajax({
            url: 'filter/removeGenre',
            data: { genre: genre },
            success: (data => refreshCards(data))
        });
    } else {
        $(target).addClass("selected");

        $.ajax({
            url: 'filter/addGenre',
            data: { genre: genre },
            success: (data => refreshCards(data))
        });
    }
};

function addWebsiteClick() {
    $(WEBSITE_CONTAINER_SELECTOR).bind('click', function () {
        let target = $(this);
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
    });
}

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
