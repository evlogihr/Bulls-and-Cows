namespace BullsAndCows.Tests.Services.GameServiceTests
{
    using System;
    using System.Data.Entity;

    using NUnit.Framework;
    using Moq;

    using BullsAndCows.Services;
    using BullsAndCows.Data.Models;

    [TestFixture]
    public class StartGame_Should
    {
        [Test]
        public void CreateNewGameInDB()
        {
            // Arrange
            var dbContextMock = new Mock<DbContext>();
            dbContextMock.Setup(x => x.SaveChanges());

            var service = new GameService(dbContextMock.Object);

            // Act
            service.StartGame("invalid userId string");

            // Assert
            dbContextMock.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
