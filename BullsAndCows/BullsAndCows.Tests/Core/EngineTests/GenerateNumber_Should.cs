namespace BullsAndCows.Tests.Core.EngineTests
{
    using NUnit.Framework;

    using BullsAndCows.Core;

    [TestFixture]
    public class GenerateNumber_Should
    {
        [Test]
        public void Return4DigitStringNumber()
        {
            // Arrange
            var num = Engine.GenerateNumber();

            // Assert
            Assert.AreEqual(4, num.Length);
        }

        [Test]
        public void ReturnNonRepeatableDigitsInTheGeneratedNumber()
        {
            // Arrange
            var num = Engine.GenerateNumber();

            // Act
            var dig1 = num[0];
            var dig2 = num[1];
            var dig3 = num[2];
            var dig4 = num[3];

            // Assert
            Assert.AreNotEqual(dig1, dig2);
            Assert.AreNotEqual(dig1, dig3);
            Assert.AreNotEqual(dig1, dig4);
            Assert.AreNotEqual(dig2, dig3);
            Assert.AreNotEqual(dig2, dig4);
            Assert.AreNotEqual(dig3, dig4);
        }

        [Test]
        public void ReturnDifferentNumbersEveryTime()
        {
            // Arrange
            var num1 = Engine.GenerateNumber();
            var num2 = Engine.GenerateNumber();
            
            // Assert
            Assert.AreNotEqual(num1, num2);
        }
    }
}
