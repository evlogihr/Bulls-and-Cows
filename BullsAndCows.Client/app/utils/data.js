import requester from './http-requester.js'
import constants from './constants.js'

export default {
    games: {
        startNewSingle: () => {
            let accesstoken = sessionStorage.getItem('accessToken'),
                authHeaders = {};

            if (accesstoken) {
                authHeaders.Authorization = `Bearer ${accesstoken}`;
            }

            return requester.postJSON(`${constants.serverUrl}/api/game`, null, authHeaders);
        },
        makeGuess: (guess) => {
            let accesstoken = sessionStorage.getItem('accessToken'),
                authHeaders = {};

            if (accesstoken) {
                authHeaders.Authorization = `Bearer ${accesstoken}`;
            }

            return requester.get(`${constants.serverUrl}/api/game?guess=${guess}`, authHeaders);
        }
    },
    users: {
        login: (user, pass) => {
            let data = `grant_type=password&username=${user}&password=${pass}`;
            return requester.post(`${constants.serverUrl}/Token`, data)
        },
        register: (user, pass, confirmPass) => {
            let data = {
                Email: user,
                Password: pass,
                ConfirmPassword: confirmPass
            };
            return requester.postJSON(`${constants.serverUrl}/api/account/register`, data)
        },
        logout: () => {

        }
    }
}