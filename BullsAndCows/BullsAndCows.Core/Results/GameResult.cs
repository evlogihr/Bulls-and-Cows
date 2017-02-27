namespace BullsAndCows.Core.Results
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BullsAndCows.Data.Models;

    public class GameResult
    {
        public Guid Id { get; set; }

        public IEnumerable<GuessResult> Guesses { get; set; }

        public static GameResult FromModel(Game model)
        {
            return new GameResult()
            {
                Id = model.Id,
                Guesses = model.Guesses
                    .OrderBy(g => g.Date)
                    .Select(g => new GuessResult(g.Number))
            };
        }
    }
}
