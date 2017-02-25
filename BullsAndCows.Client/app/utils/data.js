import req from './http-requester.js'
import constants from './constants.js'

export default {
    games: {
        startNewSingle: () => {
            let accesstoken = localStorage.getItem('accessToken'),
                authHeaders = {};

            if (accesstoken) {
                authHeaders.Authorization = `Bearer ${accesstoken}`;
            }

            return req.postJSON(`${constants.serverUrl}/api/game`, null, authHeaders);
        },
        makeGuess: (guess) => {
            let accesstoken = localStorage.getItem('accessToken'),
                authHeaders = {};

            if (accesstoken) {
                authHeaders.Authorization = `Bearer ${accesstoken}`;
            }

            return req.get(`${constants.serverUrl}/api/game?guess=${guess}`, authHeaders);
        }
    },
    users: {
        login: (user, pass) => {
            let data = `grant_type=password&username=${user}&password=${pass}`;
            return req.post(`${constants.serverUrl}/Token`, data)
                .then((resp) => {
                    localStorage.setItem(`userName`, resp.userName);
                    localStorage.setItem(`accessToken`, resp.access_token);
                    return { user: resp.userName };
                }, (err) => err)
        },
        register: (user, pass, confirmPass) => {
            let data = {
                Email: user,
                Password: pass,
                ConfirmPassword: confirmPass
            };
            return req.postJSON(`${constants.serverUrl}/api/account/register`, data)
        },
        logout: () => {
            localStorage.removeItem(`userName`);
            localStorage.removeItem(`accessToken`);
            return Promise.resolve();
        },
        loggedUser: () => {
            return Promise.resolve(localStorage.getItem(`userName`));
        }
    }
}