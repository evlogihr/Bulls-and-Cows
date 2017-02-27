namespace BullsAndCows.Services.Contracts
{
    using System;
    using System.Collections.Generic;

    using BullsAndCows.Core.Results;
    using BullsAndCows.Data.Models;

    public interface IGameService
    {
        Guid StartGame(string userId);

        IEnumerable<Guid> GetUserActiveGameIds(string userId);

        GameResult GetGame(Guid gameId);
        
        GuessResult Guess(string user, Guid gameId, string guess);
    }
}
