using DiceRollGame.Game;
using Moq;
using NUnit.Framework;

namespace _32_DiceRollGameTests
{
    [TestFixture]
    public class GuessingGameTests
    {
        [Test]
        public void Play_ShallReturnVictory_IfTheUserGuessesTheNumberOnTheFirstTry()
        {
            var diceMock = new Mock<IDice>();
            const int NumberOndie = 3;
            diceMock.Setup(mock => mock.Roll()).Returns(NumberOndie);
            var cut = new GuessingGame(diceMock.Object);
            
            var gameResult = cut.Play();
        }
    }
}