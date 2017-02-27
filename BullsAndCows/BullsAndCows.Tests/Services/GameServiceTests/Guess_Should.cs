namespace BullsAndCows.Tests.Services.GameServiceTests
{
    using System;
    using System.Data.Entity;

    using NUnit.Framework;
    using Moq;

    using BullsAndCows.Services;
    using BullsAndCows.Data.Models;
    using Data.Contracts;
    using BullsAndCows.Core.Results;

    [TestFixture]
    public class Guess_Should
    {
        [Test]
        public void GetGameByIdFromGameRepository()
        {
            // Arrange
            var dbContextMock = new Mock<DbContext>();
            var usersRepoMock = new Mock<IRepository<ApplicationUser>>();
            var gamesRepoMock = new Mock<IRepository<Game>>();
            var guessRepoMock = new Mock<IRepository<Guess>>();
            var guessMock = new Mock<Guess>();
            var gameMock = new Mock<Game>();
            var guid = Guid.NewGuid();
            
            gameMock.SetupSet(x => x.Number = "1234");
            gamesRepoMock.Setup(x => x.GetById(guid)).Returns(gameMock.Object);

            var service = new GameService(dbContextMock.Object, usersRepoMock.Object, gamesRepoMock.Object, guessRepoMock.Object);

            // Act
            var result = service.Guess("invalid userId string", guid, "1234");

            // Assert
            gamesRepoMock.Verify(x => x.GetById(guid), Times.Once);
        }

        [Test]
        public void ReturnValidGuessResult_WhenPassedValidData()
        {
            // Arrange
            var dbContextMock = new Mock<DbContext>();
            var usersRepoMock = new Mock<IRepository<ApplicationUser>>();
            var gamesRepoMock = new Mock<IRepository<Game>>();
            var guessRepoMock = new Mock<IRepository<Guess>>();
            var guessMock = new Mock<Guess>();
            var gameMock = new Mock<Game>();
            var guid = Guid.NewGuid();

            guessRepoMock.Setup(x => x.Add(guessMock.Object));
            guessRepoMock.Setup(x => x.SaveChanges());

            gameMock.SetupSet(x => x.Number = "1234");
            gamesRepoMock.Setup(x => x.GetById(guid)).Returns(gameMock.Object);

            var service = new GameService(dbContextMock.Object, usersRepoMock.Object, gamesRepoMock.Object, guessRepoMock.Object);

            // Act
            var result = service.Guess("invalid userId string", guid, "1234");

            // Assert
            Assert.IsInstanceOf<GuessResult>(result);
            Assert.AreEqual(4, result.Bulls);
        }

        [Test]
        public void AddGuessToGuessRepository()
        {
            // Arrange
            var dbContextMock = new Mock<DbContext>();
            var usersRepoMock = new Mock<IRepository<ApplicationUser>>();
            var gamesRepoMock = new Mock<IRepository<Game>>();
            var guessRepoMock = new Mock<IRepository<Guess>>();
            var guessMock = new Mock<Guess>();
            var gameMock = new Mock<Game>();
            var guid = Guid.NewGuid();

            guessRepoMock.Setup(x => x.Add(guessMock.Object));
            guessRepoMock.Setup(x => x.SaveChanges());

            gameMock.SetupSet(x => x.Number = "1234");
            gamesRepoMock.Setup(x => x.GetById(guid)).Returns(gameMock.Object);

            var service = new GameService(dbContextMock.Object, usersRepoMock.Object, gamesRepoMock.Object, guessRepoMock.Object);

            // Act
            var result = service.Guess("invalid userId string", guid, "1234");

            // Assert
            guessRepoMock.Verify(x => x.Add(It.IsAny<Guess>()), Times.Once);
        }

        [Test]
        public void SaveChangesToGuessRepository()
        {
            // Arrange
            var dbContextMock = new Mock<DbContext>();
            var usersRepoMock = new Mock<IRepository<ApplicationUser>>();
            var gamesRepoMock = new Mock<IRepository<Game>>();
            var guessRepoMock = new Mock<IRepository<Guess>>();
            var guessMock = new Mock<Guess>();
            var gameMock = new Mock<Game>();
            var guid = Guid.NewGuid();

            guessRepoMock.Setup(x => x.Add(guessMock.Object));
            guessRepoMock.Setup(x => x.SaveChanges());

            gameMock.SetupSet(x => x.Number = "1234");
            gamesRepoMock.Setup(x => x.GetById(guid)).Returns(gameMock.Object);

            var service = new GameService(dbContextMock.Object, usersRepoMock.Object, gamesRepoMock.Object, guessRepoMock.Object);

            // Act
            var result = service.Guess("invalid userId string", guid, "1234");

            // Assert
            guessRepoMock.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
