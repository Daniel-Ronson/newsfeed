let LOGIN_FORM_SELECTOR = '#login-form';
let SIGNUP_FORM_SELECTOR = '#signup-form';
let REGISTER_RESPONSE_SELECTOR = '#response';
let LOGIN_BUTTON_SELECTOR = '#login-btn';
let SIGNUP_BUTTON_SELECTOR = '#signup-btn';
let LOGOUT_BUTTON_SELECTOR = '#logout-btn';

let $response = $(REGISTER_RESPONSE_SELECTOR);
let userLoggedIn = false;

/**
 * Registers user in DB
 * @param {object} data Data with fields: [email, password, passwordCheck]
 */
function registerUser(data) {
    $response.text("");
    $.ajax({
        type: 'POST',
        url: '/Form/HandleRegister',
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
    //
    // if (data.password !== data.RegisterPasswordCheck) {
    //     $response.text("Passwords don't match!");
    // } else if (userDS.emailMap[data.email]) {
    //     $response.text("Email already in use");
    // } else {
    //     delete data.passwordCheck;
    //     userDS.add(data.email, data);
    //     $(REGISTER_MODAL_SELECTOR).modal('hide');
    //     toggleLogin(data.email);
    // }
}

/**
 * Authenticates a user and changes UI accordingly
 * @param {object} data Data with fields: [email, password]
 */
function authUser(data) {
    $response.text("");
    $.ajax({
        type: 'POST',
        url: '/Form/HandleLogin',
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
    
}

function logout() {
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
    } else {
        $(LOGIN_BUTTON_SELECTOR).slideUp();
        $(SIGNUP_BUTTON_SELECTOR).slideUp(() => {
            $(LOGOUT_BUTTON_SELECTOR).slideDown();
        });
        $(FAVOURITES_SELECTOR).parent().fadeIn();
    }
    userLoggedIn = !userLoggedIn;
}
