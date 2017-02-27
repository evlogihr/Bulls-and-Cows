import data from '../utils/data.js'

class Digit {
    constructor(digit) {
        this.val = digit;
        this.pos = [0, 1, 2, 3];
        this.bulls = [];
        this.isPresent = null;
    }
    is() {
        this.isPresent = true;
        if (this.pos.length === 1 && this.bulls.indexOf[this.pos[0]] === -1) {
            this.bulls.push(this.pos[0]);
        }
    }
    not() {
        this.isPresent = false;
    }
}

class AI {
    constructor(gameId) {
        this.gameId = gameId;
        this.nums = new Array(10).fill(1).map((v, i) => new Digit(i));
        this.hasFinalGuess = false;
        this.finalGuess = ['-', '-', '-', '-'];
        this.guesses = new Array(10).fill(1).map((v, i) => this.genGuessNum(i));
        this.possibilities = [];
    }
    genGuessNum(n) {
        return `${(n + 1)%10}${(n + 2)%10}${(n + 3)%10}${(n + 4)%10}`;
    }
    getFinalGuess() {
        return this.possibilities;
    }
    total(guess) {
        return guess.cows + guess.bulls;
    }
    findCombinations(numArr, position) {
        if (position < 0 || 3 < position) {
            this.possibilities.push(numArr.join(''));
            return;
        }

        let possible = this.nums
            .filter(v => v.isPresent !== false &&
                numArr.indexOf(v.val) === -1 &&
                v.pos.indexOf(position) !== -1 &&
                (v.bulls.length === 0 || v.bulls.indexOf(position) !== -1))
            .map(v => v.val)

        for (let i = 0, len = possible.length; i < len; i += 1) {
            numArr[position] = possible[i];
            this.findCombinations(numArr, this.finalGuess.indexOf('-', ++position));
            --position;
            numArr[position] = '-';
        }
    }
    processResult(guess, prevGuess, nums) {
        let currentGuessDigits = guess.number.split('').map(d => nums[d]), // nums.filter(v => guess.number.indexOf(v.val) !== -1),
            presentDigits = currentGuessDigits.filter(v => v.isPresent === true),
            notPresentDigits = currentGuessDigits.filter(v => v.isPresent === false),
            unknownDigits = currentGuessDigits.filter(v => v.isPresent === null),
            onlyCows = guess.bulls === 0 && guess.cows > 0,
            onlyBulls = guess.bulls > 0 && guess.cows === 0,
            nonGuessed = this.total(guess) === 0;

        if (onlyCows) { // if no bulls, mark that all are not in their places
            currentGuessDigits.forEach((d, j) => d.pos.splice(d.pos.indexOf(j), 1));
        } else if (onlyBulls) { // if all are bulls, mark that they at at thier position
            currentGuessDigits.forEach((d, j) => d.isPresent === true || d.isPresent === null ? d.bulls.push(j) : '');
        } else if (nonGuessed) { // if no digits were guessed mark them as such
            currentGuessDigits.forEach((d, j) => d.not());
        }

        if (this.total(guess) !== 0 && this.total(guess) === presentDigits.length) {
            unknownDigits.forEach(v => v.not());
        }

        // compare with previous guess
        if (prevGuess && this.total(guess) < this.total(prevGuess)) {
            nums[prevGuess.number[0]].is();
            nums[guess.number[3]].not();
        } else if (prevGuess && this.total(guess) > this.total(prevGuess)) {
            nums[prevGuess.number[0]].not();
            nums[guess.number[3]].is();
        }
    }
    processResults(guess, nums) {
        for (let i = 0, len = guess.length; i < len; i += 1) {
            this.processResult(guess[i], guess[i - 1], nums);
        }

        nums.filter(v => v.isPresent !== false)
            .forEach(v => {
                if (v.pos.length === 0) {
                    v.not();
                } else if (v.pos.length === 1 && v.bulls.indexOf(v.pos[0]) === -1) {
                    v.bulls.push(v.pos[0]);
                }

                if (v.isPresent === true && v.bulls.length === 1) {
                    this.finalGuess[v.bulls[0]] = v.val;
                }
            })

        // nums.filter(v => v.isPresent !== false)
        //     .forEach(v => {
        //         console.log(`${v.val} - ${v.isPresent} - pos [${v.pos}] & bulls ${v.bulls}`)
        //     })

        this.findCombinations(this.finalGuess.slice(0), this.finalGuess.indexOf('-'));
        return this.getFinalGuess();
    }
    guessNumber() {
        let allGuesses = [];
        return Promise.all(new Array(10).fill(1).map((v, i) => data.games.makeGuess(this.gameId, this.guesses[i])))
            .then((resp) => {
                allGuesses = resp;
                return this.processResults(resp, this.nums)
            })
            .then((final) => {
                return Promise.all(new Array(final.length).fill(1).map((v, i) => data.games.makeGuess(this.gameId, final[i])))
            })
            .then((resp) => {
                return allGuesses.concat(resp);
            })
    }
}

export default {
    guessNumber: (gameId) => new AI(gameId).guessNumber()
}