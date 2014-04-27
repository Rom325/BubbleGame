using Moq;

namespace PlayFieldModelTests
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
	using ViewModelClassLibrary;

	[TestClass]
    public class GameFieldShould
    {
        public const string MoveLeftFailure = "Move left failed";
        public const string MoveRightFailure = "Move right failed";
        public const string MoveUpFailure = "Move up failed";
        public const string MoveDownFailure = "Move down failed";

        [TestMethod]
        public void InitFieldSizeProperly()
        {
            const byte cols = 8;
            const byte rows = 8;
            
            var game = new GameField(rows, cols, new BubbleFactory());

            Assert.AreEqual(cols * rows, game.Bubbles.Count);
            Assert.IsTrue(game.Bubbles.All(pos => pos != null));
        }

	    [TestMethod]
	    public void Find3InARowCorrectly()
	    {
            // Arrange
	        const int currentPosition = 4;
	        int[] expected = {3, currentPosition, 5};
	        var fakeFactory = new Mock<IBubbleFactory>();
	        fakeFactory
                .Setup(ff => ff.Create(It.IsAny<uint>()))
                .Returns(Enumerable.Range(0, 9).Select(i => (expected.Contains(i)) ? new Bubble(3) : new Bubble(i%3)).ToArray());

	        var gameField = new GameField(3, 3, fakeFactory.Object);
            
            // Act
	        List<int> result = gameField.GetColoredChain(currentPosition);
            result.Sort();

            // Assert
            Assert.IsTrue(result.SequenceEqual(expected));
	    }

	    [TestMethod]
	    public void Find3InAColSuccessfully()
	    {
            // Arrange
            const int currentPosition = 5;
            int[] expected = { 2, currentPosition, 8 };
            var fakeFactory = new Mock<IBubbleFactory>();
            fakeFactory
                .Setup(ff => ff.Create(It.IsAny<uint>()))
                .Returns(Enumerable.Range(0, 9).Select(i => (expected.Contains(i)) ? new Bubble(3) : new Bubble(i % 3)).ToArray());

            var gameField = new GameField(3, 3, fakeFactory.Object);

            // Act
            List<int> result = gameField.GetColoredChain(currentPosition);
            result.Sort();

            // Assert
            Assert.IsTrue(result.SequenceEqual(expected));   
	    }

	    [TestMethod]
	    public void Find3NotInLineSuccessfully()
	    {
            const int currentPosition = 1;
            int[] expected = { currentPosition, 4, 5 };
            var fakeFactory = new Mock<IBubbleFactory>();
            fakeFactory
                .Setup(ff => ff.Create(It.IsAny<uint>()))
                .Returns(Enumerable.Range(0, 9).Select(i => (expected.Contains(i)) ? new Bubble(3) : new Bubble(i % 3)).ToArray());

            var gameField = new GameField(3, 3, fakeFactory.Object);

            // Act
            List<int> result = gameField.GetColoredChain(currentPosition);
            result.Sort();

            // Assert
            Assert.IsTrue(result.SequenceEqual(expected));   
	    }

	    [TestMethod]
	    public void FindManyNotInLineSuccessfully()
	    {
	        const int sideLength = 20;
	        const int currentPosition = 1;
	        int[] expected = { currentPosition, 21, 22, 41, 42, 61};
            var fakeFactory = new Mock<IBubbleFactory>();
            fakeFactory
                .Setup(ff => ff.Create(It.IsAny<uint>()))
                .Returns(Enumerable.Range(0, sideLength * sideLength).Select(i => (expected.Contains(i)) ? new Bubble(3) : new Bubble(i % 3)).ToArray());

            var gameField = new GameField(sideLength, sideLength, fakeFactory.Object);

            // Act
            List<int> result = gameField.GetColoredChain(currentPosition);
            result.Sort();

            // Assert
            Assert.IsTrue(result.SequenceEqual(expected));   
	    }
    }
}
