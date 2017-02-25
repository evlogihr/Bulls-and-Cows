import 'jquery'
import toastr from 'toastr'
import userController from './controllers/users-controller.js'
import gamesController from './controllers/games-controller.js'

let $container = $('#main div');

userController
    .isUserLoggedIn()
    .then((resp) => {
        loadEvents($container);
        if (!resp) {
            userController.loadLoginRegForm($container);
        } else {
            userController
                .loadUserUI($container, resp)
                .then(() => gamesController.loadGame($container));
        }
    });


function loadEvents($container) {
    $container.on('click', '#panel-users ul.nav li a', (ev) => {
        ev.stopPropagation();
        ev.preventDefault();
        let $target = $(ev.target),
            formToShow = $target.parents('li').attr('data-form');

        $target.parents('ul').find('.active').removeClass('active');
        $target.parents('li').addClass('active');

        $target.parents('#panel-users').find(`.form-container`).hide();
        $target.parents('#panel-users').find(`.form-${formToShow}`).show();
    })

    $container.on('click', '#btn-login', (ev) => {
        ev.stopPropagation();
        ev.preventDefault();
        userController.login($(ev.target));
    })

    $container.on('click', '#btn-register', (ev) => {
        ev.stopPropagation();
        ev.preventDefault();
        userController.register($(ev.target));
    })

    $container.on('click', '#btn-logout', (ev) => {
        ev.stopPropagation();
        ev.preventDefault();
        userController.logout();
    })

    $container.on('click', '#btn-play', (ev) => {
        ev.stopPropagation();
        ev.preventDefault();
        gamesController.guessNumber($(ev.target), $('#panel-results').find('tbody'))
            .then((resp) => toastr.success(`You got ${resp.Bulls} bulls and ${resp.Cows} cows`))
            .catch((err) => toastr.error(JSON.parse(err.responseText)));
    })

    $container.on('click', '#btn-bot', (ev) => {
        ev.stopPropagation();
        ev.preventDefault();

        gamesController
            .startBotGame($('#panel-results').find('tbody'))
            .then((resp) => toastr.success(`The bot guessed the number ${resp.Number} in ${resp.count} guesses`))
            .catch((err) => toastr.error(err));
    })

    $container.on('click', '#btn-new', (ev) => {
        ev.stopPropagation();
        ev.preventDefault();

        $('#panel-results').find('tbody').html('');
        gamesController.startGame()
            .then((resp) => toastr.success('Successfullu started a new game'))
            .catch((err) => toastr.error(JSON.parse(err.responseText)));

    })
};