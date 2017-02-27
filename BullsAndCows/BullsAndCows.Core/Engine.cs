namespace BullsAndCows.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using BullsAndCows.Core.Results;
    using Data.Models;

    public class Engine
    {
        private static Random rand = new Random();

        public static string GenerateNumber()
        {
            StringBuilder sb = new StringBuilder();
            List<int> allNums = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            int randIndex;

            for (int i = 0; i < 4; i++)
            {
                randIndex = rand.Next(0, Math.Min(allNums.Count, 9));
                sb.Append(allNums[randIndex]);
                allNums.Remove(allNums[randIndex]);
            }

            return sb.ToString();
        }

        public static GuessResult EvaluateGuess(string number, string guess)
        {
            if (string.IsNullOrEmpty(guess) || string.IsNullOrEmpty(number))
            {
                throw new ArgumentException();
            }

            if (guess.Length != number.Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            var result = new GuessResult(guess);
            HashSet<char> numSet = new HashSet<char>();
            foreach (var ch in number)
            {
                numSet.Add(ch);
            }

            for (int i = 0; i < guess.Length; i++)
            {
                if (numSet.Contains(guess[i]))
                {
                    result.Cows++;
                }
            }

            for (int i = 0; i < guess.Length; i++)
            {
                if (guess[i] == number[i])
                {
                    result.Bulls++;
                    result.Cows--;
                }
            }

            return result;
        }

        public static GameResult EvaluateGame(Game game)
        {
            var gameResult = GameResult.FromModel(game);
            gameResult.Guesses = gameResult.Guesses
                .Select(g => EvaluateGuess(game.Number, g.Number));
            return gameResult;
        }
    }
}
