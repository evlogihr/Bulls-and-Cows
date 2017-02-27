namespace BullsAndCows.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using BullsAndCows.Core;
    using BullsAndCows.Core.Results;
    using BullsAndCows.Data.Contracts;
    using BullsAndCows.Data.Models;
    using BullsAndCows.Data.Repositories;
    using BullsAndCows.Services.Contracts;

    public class GameService: IGameService
    {
        private IRepository<ApplicationUser> usersRepo;
        private IRepository<Game> gamesRepo;
        private IRepository<Guess> guessRepo;

        public GameService(DbContext context)
        {
            this.usersRepo = new EFRepository<ApplicationUser>(context);
            this.gamesRepo = new EFRepository<Game>(context);
            this.guessRepo = new EFRepository<Guess>(context);
        }

        public GameService(DbContext context, IRepository<ApplicationUser> usersRepo, IRepository<Game> gamesRepo, IRepository<Guess> guessRepo)
        {
            this.usersRepo = usersRepo;
            this.gamesRepo = gamesRepo;
            this.guessRepo = guessRepo;
        }

        public Guid StartGame(string userId)
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

            return newGame.Id;
        }

        public GuessResult Guess(string userId, Guid gameId, string guess)
        {
            var game = this.gamesRepo.GetById(gameId);
            var newGuess = new Guess() {
                Id = Guid.NewGuid(),
                Number = guess,
                UserId = userId,
                Date = DateTime.Now,
                Game = game
            };

            var result = Engine.EvaluateGuess(game.Number, guess);

            if (result.Bulls == 4)
            {
                game.Solved = true;
            }

            this.guessRepo.Add(newGuess);
            this.guessRepo.SaveChanges();

            return result;
        }

        public IEnumerable<Guid> GetUserActiveGameIds(string userId)
        {
            var result = this.gamesRepo.All()
                .Where(g => g.UserId == userId && !g.Solved)
                .Select(g => g.Id).ToList();

            return result;
        }

        public GameResult GetGame(Guid gameId)
        {
            var game = this.gamesRepo.GetById(gameId);
            var result = Engine.EvaluateGame(game);

            return result;
        }
    }
}
