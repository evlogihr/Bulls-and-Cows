import toastr from 'toastr'
import ai from '../utils/ai.js'
import data from '../utils/data.js'
import templates from '../utils/templateLoader.js'

export default {
    loadGame: ($container) => {
        templates.load('panel-game-results')
            .then((resp) => $container.append(resp))
    },
    startBotGame: ($resultsContainer) => {
        return Promise.all([ai.guessNumber(), templates.load('guess-result')])
            .then(([resp, template]) => {
                resp.forEach((v) => {
                    $resultsContainer.append(template(v));
                });

                let result = resp.filter((v) => v.Bulls === 4)[0];
                result.count = resp.length;
                return result;
            });
    },
    startGame: () => {
        return data.games.startNewSingle()
            .then(data);
    },
    guessNumber: ($target, $resultsContainer) => {
        let $form = $target.parents('form'),
            guess = $form.find('input').val();

        return Promise.all([data.games.makeGuess(guess), templates.load('guess-result')])
            .then(([resp, template]) => {
                $resultsContainer.append(template(resp));
                $form.find('input').val('');
                return resp;
            })
    }
}