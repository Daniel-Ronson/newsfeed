let FAVOURITES_SELECTOR = "#favourites";

let favourites = [];

function getUserFavourites(userId) {
    
}

function markFavourites() {
    favourites.forEach(function (id) {
        toggleIconSelected(id);
    });
}

function toggleIconSelected(id) {
    let icon = $('#' + id).children('.row').children().children('.fav-icon');
    if (icon.hasClass('icon-selected')) {
        icon.removeClass('icon-selected');
    } else {
        icon.addClass('icon-selected');
    }
}

function addFavourite(id) {
    if (!favourites.includes(id)) {
        favourites.push(id);
    } else {
        return;
    }
    
    let body = $('#' + id).children('.row').children();
    let title = body.children('a').children().text();
    let website = body.children('.card-subtitle').text();
    let url = body.children('a').attr('href');

    toggleIconSelected(id);
    
    let li = $('<li></li>', {
        'class': 'todo-done',
        'data-id': id
    });

    let icon = $('<div></div>', {
        'class': 'todo-icon fui-heart',
        'onclick': 'removeFavourite(' + id + ')'
    });

    let content = $('<a></a>', {
        'class': 'todo-content',
        'href': url
    });

    let name = $('<h4></h4>',
        {'class': 'todo-name text-ellipsis'}
    );
    name.append(title);
    content.append(name);
    content.append(website);
    li.append(icon);
    li.append(content);
    
    li.hide();
    li.fadeIn(150);
    $(FAVOURITES_SELECTOR + ' ul').append(li);
}

function removeFavourite(id) {
    if (favourites.includes(id)) {
        favourites = favourites.filter(function (value, index, arr) {
            return value !== id;
        })
    }
    
    toggleIconSelected(id);
    
    let item = $(`[data-id="${id}"]`);
    item.fadeOut(250, function () {
        this.remove();
    });
}   