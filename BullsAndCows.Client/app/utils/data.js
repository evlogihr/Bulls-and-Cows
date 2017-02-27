import req from './http-requester.js'
import constants from './constants.js'

function getAccessTokenHeaders() {
    let accesstoken = localStorage.getItem('accessToken'),
        authHeaders = {};

    if (accesstoken) {
        authHeaders.Authorization = `Bearer ${accesstoken}`;
    }

    return authHeaders;
}

export default {
    games: {
        startNewSingle: () => {
            return req.postJSON(`${constants.serverUrl}/api/game`, null, getAccessTokenHeaders());
        },
        makeGuess: (gameId, guess) => {
            return req.get(`${constants.serverUrl}/api/game?gameId=${gameId}&guess=${guess}`, getAccessTokenHeaders());
        },
        getActiveGames: () => {
            return req.get(`${constants.serverUrl}/api/game/active`, getAccessTokenHeaders());
        },
        getGame: (gameId) => {
            return req.get(`${constants.serverUrl}/api/game/active?gameId=${gameId}`, getAccessTokenHeaders());
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