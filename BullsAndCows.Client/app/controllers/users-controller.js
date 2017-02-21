import data from '../utils/data.js'
import templates from '../utils/templateLoader.js'

export default {
    login: (user, pass) => {
        var loginData = {
            username: user,
            password: pass
        };

        return data.users.login(loginData)
            .then(data);
    },
    register: (user, pass) => {

    },
    logout: (token) => {

    }
}