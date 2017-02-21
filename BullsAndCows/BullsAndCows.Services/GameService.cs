namespace BullsAndCows.Services
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using BullsAndCows.Core;
    using BullsAndCows.Core.Results;
    using BullsAndCows.Data.Contracts;
    using BullsAndCows.Data.Models;
    using BullsAndCows.Data.Repositories;

    public class GameService
    {
        private IRepository<ApplicationUser> usersRepo;
        private IRepository<Game> gamesRepo;

        public GameService(DbContext context)
        {
            this.usersRepo = new EFRepository<ApplicationUser>(context);
            this.gamesRepo = new EFRepository<Game>(context);
        }

        public string StartGame(string userId)
        {
            var number = Engine.GenerateNumber();
            var newGame = new Game()
            {
                Id = Guid.NewGuid(),
                Number = number,
                UserId = userId,
                Date = DateTime.Now
            };

            this.gamesRepo.Add(newGame);
            this.gamesRepo.SaveChanges();

            return number;
        }

        public GuessResult Guess(string userId, string guess)
        {
            Game game;
            if (userId != null)
            {
                game = this.gamesRepo.All()
                    .OrderByDescending(g => g.Date)
                    .First(g => g.UserId == userId && !g.Solved);
            }
            else
            {
                game = this.gamesRepo.All()
                    .OrderByDescending(g => g.Date)
                    .First(g => !g.Solved);
            }

            var result = Engine.EvaluateGuess(game.Number, guess);

            return result;
        }
    }
}
