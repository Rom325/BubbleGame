using System.Dynamic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PlayFieldModelTests
{
	using WpfUserControls;

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
            
            var game = new GameField(rows, cols);

            Assert.AreEqual(cols * rows, game.Bubbles.Count);
            Assert.IsTrue(game.Bubbles.All(pos => pos != null));
        }

        [TestMethod]
        public void SwapElementsCorrectly()
        {
            int fromIndex = 0;
            var game = new GameField(8, 8);
            Bubble first = game.Bubbles[fromIndex];

            game.TryMove(fromIndex, Direction.Right);
            fromIndex++;
            Assert.AreSame(first, game.Bubbles[fromIndex], MoveRightFailure);

            game.TryMove(fromIndex , Direction.Down);
            fromIndex += 8;
            Assert.AreSame(first, game.Bubbles[fromIndex], MoveDownFailure);

            game.TryMove(fromIndex, Direction.Left);
            fromIndex--;
            Assert.AreSame(first, game.Bubbles[fromIndex], MoveLeftFailure);

            game.TryMove(fromIndex, Direction.Up);
            fromIndex -= 8;
            Assert.AreSame(first, game.Bubbles[fromIndex], MoveUpFailure);
        }

        [TestMethod]
        public void HandleEdgeSwappingCasesSuccessfully()
        {
            int rightMost = 34;
            int leftMost = 0;

            var game = new GameField(5, 7);

            Assert.IsFalse(game.TryMove(rightMost, Direction.Right));
            Assert.IsFalse(game.TryMove(rightMost, Direction.Down));
            Assert.IsFalse(game.TryMove(leftMost, Direction.Left));
            Assert.IsFalse(game.TryMove(leftMost, Direction.Up));

        }
    }
}
