namespace BullsAndCows.Tests.Core.EngineTests
{
    using System;

    using NUnit.Framework;

    using BullsAndCows.Core;
    using BullsAndCows.Core.Results;

    [TestFixture]
    public class EvaluateGuess_Should
    {
        private const string VALID_GUESS = "2345";
        private const string VALID_NUM = "1234";

        [Test]
        public void ThrowArgumentException_WhenNumberIsNull()
        {
            // Act && Assert
            Assert.Throws<ArgumentException>(() => Engine.EvaluateGuess(null, VALID_GUESS));
        }

        [Test]
        public void ThrowArgumentException_WhenNumberIsEmpty()
        {
            // Act && Assert
            Assert.Throws<ArgumentException>(() => Engine.EvaluateGuess(string.Empty, VALID_GUESS));
        }

        [Test]
        public void ThrowArgumentException_WhenGuessIsNull()
        {
            // Act && Assert
            Assert.Throws<ArgumentException>(() => Engine.EvaluateGuess(VALID_NUM, null));
        }

        [Test]
        public void ThrowArgumentException_WhenGuessIsEmpty()
        {
            // Act && Assert
            Assert.Throws<ArgumentException>(() => Engine.EvaluateGuess(VALID_NUM, string.Empty));
        }

        [Test]
        public void ThrowArgumentOutOfRangeException_WhenNumberAndGuessAreWithDifferentLength()
        {
            // Arrange
            var number = "123";
            var guess = "23456";

            // Act && Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => Engine.EvaluateGuess(number, guess));
        }

        [Test]
        public void ReturnGameReult_WhenPassedValidInput()
        {
            // Arrange && Act
            var result = Engine.EvaluateGuess(VALID_NUM, VALID_GUESS);

            // Assert
            Assert.IsInstanceOf<GuessResult>(result);
        }

        [Test]
        public void Return4Bulls_WhenGuessAndNumberAreEqual()
        {
            // Arrange && Act
            var result = Engine.EvaluateGuess(VALID_NUM, VALID_NUM);

            // Assert
            Assert.AreEqual(4, result.Bulls);
        }

        [Test]
        public void Return1Bull_WhenGuessAndNumberAreEqual()
        {
            // Arrange
            var number = "1234";
            var guess = "1567";

            // Act
            var result = Engine.EvaluateGuess(number, guess);

            // Assert
            Assert.AreEqual(1, result.Bulls);
        }

        [Test]
        public void Return1Cow_WhenGuessAndNumberAreEqual()
        {
            // Arrange
            var number = "1234";
            var guess = "2567";

            // Act
            var result = Engine.EvaluateGuess(number, guess);

            // Assert
            Assert.AreEqual(1, result.Cows);
        }


        [Test]
        public void Return4Cows_WhenDigitsInGuessMatchDigitsInNumberButNotPosition()
        {
            // Arrange
            var number = "1234";
            var guess = "2341";

            // Act
            var result = Engine.EvaluateGuess(number, guess);

            // Assert
            Assert.AreEqual(4, result.Cows);
        }
    }
}
