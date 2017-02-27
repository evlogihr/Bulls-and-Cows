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
    public class GetUserActiveGameIds_Should
    {
        [Test]
        public void ReturnLIstOfGuids()
        {
            // Arrange
            var dbContextMock = new Mock<DbContext>();
            var usersRepoMock = new Mock<IRepository<ApplicationUser>>();
            var gamesRepoMock = new Mock<IRepository<Game>>();
            var guessRepoMock = new Mock<IRepository<Guess>>();
            var gameMock = new Mock<Game>();
            
        }
    }
}
