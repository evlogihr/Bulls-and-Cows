import toastr from 'toastr'
import ai from '../utils/ai.js'
import data from '../utils/data.js'
import templates from '../utils/templateLoader.js'

export default {
    loadGame: ($container, gameId) => {
        return Promise.all([data.games.getGame(gameId), templates.load('panel-game-results')])
            .then(([resp, template]) => $container.append(template(resp)));
    },
    startBotGame: ($resultsContainer) => {
        let gameId = $('#panel-results').attr('data-id');
        if (gameId) {
            return Promise.all([ai.guessNumber(gameId), templates.load('guess-result')])
                .then(([resp, template]) => {
                    resp.forEach((v) => $resultsContainer.append(template(v)));

                    let result = resp.filter((v) => v.bulls === 4)[0];
                    result.count = resp.length;
                    return result;
                });
        } else {
            return Promise.reject('You should have a selected active game');
        }
    },
    startGame: ($container) => {
        return Promise.all([data.games.startNewSingle(), templates.load('active-game')])
            .then(([resp, template]) => $container.append(template(resp)));
    },
    guessNumber: ($target, $resultsContainer) => {
        let $form = $target.parents('form'),
            guess = $form.find('input').val(),
            gameId = $('#panel-results').attr('data-id');

        if (gameId) {
            return Promise.all([data.games.makeGuess(gameId, guess), templates.load('guess-result')])
                .then(([resp, template]) => {
                    $resultsContainer.append(template(resp));
                    if (resp.bulls === 4) {
                        $(`button[data-id=${gameId}]`).addClass('btn-success');
                    }

                    $form.find('input').val('');
                    return resp;
                })
        } else {
            return Promise.reject('You should have a selected active game');
        }
    }
}