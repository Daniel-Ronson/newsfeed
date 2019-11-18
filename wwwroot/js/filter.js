let genres = [];

function searchArticles(element) {
    let articles = $(ARTICLE_SELECTOR);

    let queryText = $(element).val().toLowerCase();
    
    articles.each(function () {
        let title = $(this).children(".row").children().children('a').children().text();
        let summary = $(this).children(".row").children().children('.card-text').text();
        let searchableText = title + " " + summary;
        searchableText = searchableText.toLowerCase();

        toggleElement.call(this, searchableText.includes(queryText));
    });
}

function searchFavourites(element) {
    
}

function toggleElement(shouldDisplay) {
    if (shouldDisplay) {
        if ($(this).hasClass("d-none")) {
            $(this).fadeIn();
            $(this).removeClass("d-none");
        }
    } else {
        $(this).fadeOut();
        $(this).addClass("d-none");
    }
}

/**
 * Filters articles by a genre. If no genres are given, then all articles are displayed
 * @param genre {string} Genre to filter by
 */
function filterByGenre(genre) {
    let articles = $(ARTICLE_SELECTOR);
    
    // Add or remove genre from array
    if (genres.includes(genre)) {
        genres = genres.filter(function(value, index, arr) {
            return value !== genre;
        })
    } else {
        genres.push(genre);
    }

    // Display all articles if no genres are given
    if (genres.length === 0) {
        articles.each(function () {
            $(this).fadeIn();
            $(this).removeClass("d-none");
        });
        return;
    }
    
    // Filter articles
    articles.each(function () {
        let articleGenres = [];
        
        $(this).children(".row").children().children(".badge").each(function () {
            articleGenres.push($(this).text().slice(1, -1));
        });
        
        let genreExists = articleGenres.some(genre => genres.includes(genre));

        toggleElement.call(this, genreExists);
    });
}