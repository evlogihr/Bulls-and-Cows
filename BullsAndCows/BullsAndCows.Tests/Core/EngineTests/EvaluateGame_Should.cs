namespace BullsAndCows.Tests.Core.EngineTests
{
    using System;
    using System.Linq;
    using NUnit.Framework;

    using BullsAndCows.Core;
    using BullsAndCows.Data.Models;

    [TestFixture]
    public class EvaluateGame_Should
    {
        [Test]
        public void ReturnCorrectResults_WhenPassedValidData()
        {
            // Arrange
            var game = new Game()
            {
                Number = "1234",
                Date = DateTime.Now,
                Guesses =
                {
                    new Guess() { Number = "0129" },
                    new Guess() { Number = "1027" },
                    new Guess() { Number = "1234" }
                }
            };

            // Act
            var result = Engine.EvaluateGame(game);

            // Assert
            Assert.AreEqual(2, result.Guesses.First().Cows);
            Assert.AreEqual(1, result.Guesses.Skip(1).First().Cows);
            Assert.AreEqual(1, result.Guesses.Skip(1).First().Bulls);
            Assert.AreEqual(4, result.Guesses.Last().Bulls);
        }

        [Test]
        public void NotThrowException_WhenGuessesListIsEmpty()
        {
            // Arrange
            var game = new Game()
            {
                Number = "1234",
                Date = DateTime.Now
            };

            // Act && Assert
            Assert.DoesNotThrow(() => Engine.EvaluateGame(game));
        }
    }
}
