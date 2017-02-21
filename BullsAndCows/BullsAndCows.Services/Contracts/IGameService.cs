namespace BullsAndCows.Services.Contracts
{
    using System;

    using BullsAndCows.Core.Results;

    interface IGameService
    {
        void StartGame();
        
        GuessResult Guess(Guid userId, string guess);
    }
}
