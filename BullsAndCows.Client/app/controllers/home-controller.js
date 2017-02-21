import 'jquery'
import data from '../utils/data.js'
import templates from '../utils/templateLoader.js'

export default {
    loadEvents: (container) => {
        let $container = $(container);

        $container.on('click', '#btn-login', (ev) => {
            ev.stopPropagation();
            ev.preventDefault();

            let form = $(ev.target).parents('form'),
                email = form.find('.email').val(),
                pass = form.find('.password').val(); // TODO: encrypt password

            data.users.login(email, pass)
                .then((resp) => {
                    sessionStorage.setItem('userName', resp.userName);
                    sessionStorage.setItem('accessToken', resp.access_token);

                    // TODO: user notification
                });
        })

        $container.on('click', '#btn-register', (ev) => {
            ev.stopPropagation();
            ev.preventDefault();

            let form = $(ev.target).parents('form'),
                email = form.find('.email').val(),
                pass = form.find('.password').val(), // TODO: encrypt password
                confirmPass = form.find('.confirm-password').val();

            data.users.register(email, pass, confirmPass)
                .then((resp) => {
                    // TODO: user notification
                });
        })

    },
    loadNavigation: (container) => {
        let $container = $(container);
        templates.load('navigation')
            .then((resp) => $container.html(resp))
    },
    home: (container) => {
        let $container = $(container);
        templates.load('login-register')
            .then((resp) => $container.append(resp))
    }
};