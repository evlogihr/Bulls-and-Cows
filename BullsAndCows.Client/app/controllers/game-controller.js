import ai from '../utils/ai.js'
import data from '../utils/data.js'
import templates from '../utils/templateLoader.js'

let $container;

function startNewGame(ev) {
    ev.stopPropagation();
    ev.preventDefault();

    $('#prev-nums').find('tbody').html('');
    data.games.startNewSingle()
        .then((resp) => console.log(resp))
}

function makeGuess(ev) {
    ev.stopPropagation();
    ev.preventDefault();

    let form = $(ev.target).parents('form'),
        guess = form.find('input').val();

    Promise.all([data.games.makeGuess(guess), templates.load('guess-result')])
        .then(([resp, template]) => {
            let cont = $('#prev-nums').find('tbody').append(template(resp));
            form.find('input').val('');
            console.log(resp)
        })
}

export default {
    loadSingleGame: (container) => {
        $container = $(container);

        templates.load('single-game')
            .then((resp) => {
                $container.html(resp);

                $container.on('click', '#btn-start-single-game', startNewGame);
                $container.on('click', '#btn-make-guess', makeGuess);
            })
    },
    startBotGame: () => {
        ai.guessNumber();
    },
    startGame: () => {
        data.games.startNewSingle()
            .then(data);
    },
    guessNumber: (guess) => {

    }
}