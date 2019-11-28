let LOGIN_FORM_SELECTOR = '#login-form';
let SIGNUP_FORM_SELECTOR = '#signup-form';
let REGISTER_RESPONSE_SELECTOR = '#response';
let LOGIN_BUTTON_SELECTOR = '#login-btn';
let SIGNUP_BUTTON_SELECTOR = '#signup-btn';
let LOGOUT_BUTTON_SELECTOR = '#logout-btn';

let $response = $(REGISTER_RESPONSE_SELECTOR);
let userLoggedIn = false;
let userId = -1;

/**
 * Registers user in DB
 * @param {object} data Data with fields: [email, password, passwordCheck]
 */
function registerUser(data) {
    $response.text("");
    let email = data.RegisterEmail;
    $.ajax({
        type: 'POST',
        url: 'Auth/HandleRegister',
        data: { model: data },
        success: function(data) {
            $response.text(data);
            if (data === "") {
                toggleLogin();
                getUserId(email);
            }
        },
        error:function(){
            $response.text('An error occured')
        }
    });
}

function getUserId(email, fn=null) {
    $.ajax({
        type: 'POST',
        url: 'User/GetUserId',
        data: { email: email },
        success: function(data) {
            if (data !== -1) {
                userId = data;
                if (fn) {
                    fn(userId);
                }
            }
        },
        error:function(){
            alert('An error occured');
        }
    });
}

/**
 * Authenticates a user and changes UI accordingly
 * @param {object} data Data with fields: [email, password]
 */
function authUser(data) {
    $response.text("");
    $.ajax({
        type: 'POST',
        url: 'Auth/HandleLogin',
        data: { model: data },
        success:function(data) {
            $response.text(data);
            if (data === "") {
                toggleLogin();
            }
        },
        error:function(){
            $response.text('An error occured')
        }
    });
    getUserId(data.LoginUserEmail, getUserFavourites);
}

function logout() {
    $(FAVOURITES_SELECTOR + " ul").empty();
    favourites = [];
    toggleLogin();
}

/**
 * Toggles login state dependant on current state
 * @param {string} email User email
 */
function toggleLogin() {
    loginModal.modal('hide');
    if (userLoggedIn) {
        $(LOGOUT_BUTTON_SELECTOR).slideUp(() => {
            $(LOGIN_BUTTON_SELECTOR).slideDown();
            $(SIGNUP_BUTTON_SELECTOR).slideDown();
        });
        $(FAVOURITES_SELECTOR).parent().fadeOut();
        userId = -1;
    } else {
        $(LOGIN_BUTTON_SELECTOR).slideUp();
        $(SIGNUP_BUTTON_SELECTOR).slideUp(() => {
            $(LOGOUT_BUTTON_SELECTOR).slideDown();
        });
        $(FAVOURITES_SELECTOR).parent().fadeIn();
    }
    userLoggedIn = !userLoggedIn;
}
