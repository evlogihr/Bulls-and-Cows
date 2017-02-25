import toastr from 'toastr'
import data from '../utils/data.js'
import templates from '../utils/templateLoader.js'

export default {
    loadEvents: ($container) => {
        loadEvents($container);
    },
    loadLoginRegForm: ($container) => {
        return templates.load('form-login-register')
            .then((resp) => $container.append(resp))
    },
    login: ($target) => {
        let form = $target.parents('form'),
            email = form.find('.username').val(),
            pass = form.find('.password').val(); // TODO: encrypt password

        // TODO: add input validation
        return data.users.login(email, pass)
            .then((resp) => {
                toastr.success(`Welcome ${resp.user}`)
                location.reload();
            })
            .catch((err) => toastr.error(JSON.parse(err.responseText).error_description));
    },
    register: ($target) => {
        let form = $target.parents('form'),
            email = form.find('.username').val(),
            pass = form.find('.password').val(), // TODO: encrypt password
            confirmPass = form.find('.confirm-password').val();

        // TODO: add input validation
        return data.users.register(email, pass, confirmPass)
            .then((resp) => {
                toastr.success(`Successfully registered`);
                location.reload();
            })
            .catch((err) => toastr.error(JSON.parse(err.responseText).modelState['model.ConfirmPassword'][0]));
    },
    logout: () => {
        return data.users.logout()
            .then(() => location.reload());
    },
    isUserLoggedIn: () => {
        return data.users.loggedUser();
    },
    loadUserUI: ($container) => {
        return templates.load('panel-user')
            .then((resp) => $container.append(resp))
    }
}