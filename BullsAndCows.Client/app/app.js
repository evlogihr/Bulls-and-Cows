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
            userController
                .loadLoginRegForm($container);
        } else {
            userController
                .loadUserUI($container, resp)
                .then((userId) => userController.loadActiveGames($container));
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

        gamesController
            .guessNumber($(ev.target), $('#panel-results').find('tbody'))
            .then((resp) => toastr.success(`You got ${resp.bulls} bulls and ${resp.cows} cows`))
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
        gamesController
            .startGame($('#panel-active-games').find('#list-active-games'))
            .then((resp) => {
                $container.find('#panel-results').remove();
                return gamesController.loadGame($container, resp);
            })
            .then((resp) => toastr.success('Successfulluy started a new game'))
            .catch((err) => toastr.error(JSON.parse(err.responseText)));

    })

    $container.on('click', '#panel-active-games button', (ev) => {
        ev.stopPropagation();
        ev.preventDefault();
        let $target = $(ev.target),
            gameId = $target.attr('data-id');

        $container.find('.panel-container').remove();
        gamesController
            .loadGame($container, gameId)
            .then((resp) => toastr.success('Successfulluy loaded the game game'))
            .catch((err) => toastr.error(JSON.parse(err.responseText)));
    })
};