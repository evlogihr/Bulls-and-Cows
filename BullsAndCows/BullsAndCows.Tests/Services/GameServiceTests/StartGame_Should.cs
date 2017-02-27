namespace BullsAndCows.Tests.Services.GameServiceTests
{
    using System;
    using System.Data.Entity;

    using NUnit.Framework;
    using Moq;

    using BullsAndCows.Services;
    using BullsAndCows.Data.Models;
    using Data.Contracts;

    [TestFixture]
    public class StartGame_Should
    {
        [Test]
        public void AddGameToGameRepository()
        {
            // Arrange
            var dbContextMock = new Mock<DbContext>();
            var usersRepoMock = new Mock<IRepository<ApplicationUser>>();
            var gamesRepoMock = new Mock<IRepository<Game>>();
            var guessRepoMock = new Mock<IRepository<Guess>>();
            var gameMock = new Mock<Game>();

            gamesRepoMock.Setup(x => x.Add(gameMock.Object));
            gamesRepoMock.Setup(x => x.SaveChanges());

            var service = new GameService(dbContextMock.Object, usersRepoMock.Object, gamesRepoMock.Object, guessRepoMock.Object);

            // Act
            var result = service.StartGame("invalid userId string");

            // Assert
            gamesRepoMock.Verify(x => x.Add(It.IsAny<Game>()), Times.Once);
        }

        [Test]
        public void SaveChangesToGameRepository()
        {
            // Arrange
            var dbContextMock = new Mock<DbContext>();
            var usersRepoMock = new Mock<IRepository<ApplicationUser>>();
            var gamesRepoMock = new Mock<IRepository<Game>>();
            var guessRepoMock = new Mock<IRepository<Guess>>();
            var gameMock = new Mock<Game>();

            gamesRepoMock.Setup(x => x.Add(gameMock.Object));
            gamesRepoMock.Setup(x => x.SaveChanges());

            var service = new GameService(dbContextMock.Object, usersRepoMock.Object, gamesRepoMock.Object, guessRepoMock.Object);

            // Act
            var result = service.StartGame("invalid userId string");

            // Assert
            gamesRepoMock.Verify(x => x.Add(It.IsAny<Game>()), Times.Once);
            gamesRepoMock.Verify(x => x.SaveChanges(), Times.Once);
            Assert.AreEqual(36, result.ToString().Length);
        }
    }
}
