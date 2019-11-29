let CARD_CONTAINER_SELECTOR = '#articles';
let GENRE_CONTAINER_SELECTOR = '#genres';
let WEBSITE_CONTAINER_SELECTOR = ".website-container";
let ARTICLE_SELECTOR = '.card, .mb-3';
let FOCUS_SWITCH_SELECTOR = "#focus-switch";
let FOCUS_CONTAINER_SELECTOR = '#focus-container';

let App = window.App;
let FormHandler = App.FormHandler;
let LOGIN_FORM = new FormHandler(LOGIN_FORM_SELECTOR);
let SIGNUP_FORM = new FormHandler(SIGNUP_FORM_SELECTOR);

let loginModal = $('#login-modal');

$('#settings_dropdown').click(function (e) {
   // e.preventDefault();
    e.stopPropagation();
});

$(function () {
    $(FAVOURITES_TOGGLE_SELECTOR).click(function () {
        toggleFavourites();
    });
  
    LOGIN_FORM.addSubmitHandler(authUser);
    SIGNUP_FORM.addSubmitHandler(registerUser);

    $(FOCUS_SWITCH_SELECTOR).bootstrapSwitch();
    $(FOCUS_SWITCH_SELECTOR).bootstrapSwitch('onSwitchChange', function (e, data) {
        toggleFocusMode(data);
    });
    
});

function toggleFocusMode(data) {
    if (data) {
        $(FOCUS_CONTAINER_SELECTOR).fadeOut();
    } else {
        $(FOCUS_CONTAINER_SELECTOR).fadeIn();
    }
}

/**
 * Handles clicks on genres. Adds visual cue of selected state and filters genres.
 * @param element {Element} The clicked genre element
 */
var genreClick = function (element) {
    var genre = $(element).children()[0].id;
    var shouldRemove = $(element).hasClass("selected");

    if (shouldRemove) {
        $(element).removeClass("selected");
    } else {
        $(element).addClass("selected");
    }

    filterByGenre(genre);
    $("#article-search-field").val("");
};

/**
 * Refreshes articles and genres with new data from clicked website.
 * @param element {Element} The clicked website
 */
function websiteClick(element) {
    let target = $(element);
    var id = target.attr('value');

    $(WEBSITE_CONTAINER_SELECTOR).removeClass("selected");
    target.addClass("selected");

    $.ajax({
        url: 'Filter/GetFilteredArticles',
        data: {websiteId: id},
        success: (function (data) {
            refreshCards(data);
            markFavourites();
        })
    });
    $.ajax({
        url: 'Filter/GetFilteredGenres',
        data: {websiteId: id},
        success: (data => refreshGenres(data))
    });
    $("#article-search-field").val("");
}

/**
 * Resets articles with new data
 * @param data {string} HTML formatted article data
 */
function refreshCards(data) {
    let cardview = $(CARD_CONTAINER_SELECTOR);
    cardview.empty();
    cardview.append(data);
}

/**
 * Resets genres with new data
 * @param data {string} HTML formatted genre data
 */
function refreshGenres(data) {
    let genreView = $(GENRE_CONTAINER_SELECTOR);
    genreView.empty();
    genreView.append(data);
    genres = []; // Reset genre filter
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
