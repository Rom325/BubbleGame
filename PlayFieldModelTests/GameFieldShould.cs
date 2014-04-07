using System;
using System.Dynamic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PlayFieldModelTests
{
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

            game.TryMove(fromIndex, Directions.Right);
            fromIndex++;
            Assert.AreSame(first, game.Bubbles[fromIndex], MoveRightFailure);

            game.TryMove(fromIndex , Directions.Down);
            fromIndex += 8;
            Assert.AreSame(first, game.Bubbles[fromIndex], MoveDownFailure);

            game.TryMove(fromIndex, Directions.Left);
            fromIndex--;
            Assert.AreSame(first, game.Bubbles[fromIndex], MoveLeftFailure);

            game.TryMove(fromIndex, Directions.Up);
            fromIndex -= 8;
            Assert.AreSame(first, game.Bubbles[fromIndex], MoveUpFailure);
        }

        [TestMethod]
        public void HandleEdgeSwappingCasesSuccessfully()
        {
            int rightMost = 34;
            int leftMost = 0;

            var game = new GameField(5, 7);

            Assert.IsFalse(game.TryMove(rightMost, Directions.Right));
            Assert.IsFalse(game.TryMove(rightMost, Directions.Down));
            Assert.IsFalse(game.TryMove(leftMost, Directions.Left));
            Assert.IsFalse(game.TryMove(leftMost, Directions.Up));

        }
    }

    public enum Directions
    {
        Right,
        Down,
        Left,
        Up
    }

    public class GameField
    {
        public GameField(byte rowsCount, byte colsCount)
        {
            RowsCount = rowsCount;
            ColsCount = colsCount;

            // Потом можно произвести DI
            var factory = new BubbleFactory();
            var size = (uint)(rowsCount * colsCount);
            Bubbles = new BubbleCollection(factory.Create(size));
        }

        public byte RowsCount { get; private set; }

        public byte ColsCount { get; private set; }

        public BubbleCollection Bubbles { get; private set; }


        /// <summary>
        /// Передвигает элемент на 1 клетку по указанному направлению.
        /// </summary>
        /// <param name="position">Номер элемента в массиве</param>
        /// <param name="direction">Направление сдвига</param>
        /// <returns>Возвращает true, в случае успешного передвижения, false если передвинуть нельзя</returns>
        public bool TryMove(int position, Directions direction)
        {
            if (!CanMove(position, direction))
            {
                return false;
            }

            Move(position, direction);
            return true;
        }

        public bool CanMove(int position, Directions direction)
        {
            switch (direction)
            {
                case Directions.Right:
                    return !IsInLastColumn(position);
                case Directions.Down:
                    return !IsOnLastRow(position);
                case Directions.Left:
                    return !IsInFirstColumn(position);
                case Directions.Up:
                    return !IsOnFirstRow(position);
                default:
                    throw new ArgumentOutOfRangeException("direction");
            }
        }

        protected bool IsOnFirstRow(int position)
        {
            return position < this.ColsCount;
        }

        protected bool IsInFirstColumn(int position)
        {
            return position % this.ColsCount == 0;
        }

        protected bool IsOnLastRow(int position)
        {
            return position >= (this.ColsCount*(this.RowsCount - 1));
        }

        protected bool IsInLastColumn(int position)
        {
            return position % this.ColsCount == this.ColsCount - 1;
        }

        private void Move(int position, Directions direction)
        {
            switch (direction)
            {
                case Directions.Right:
                    Bubbles.Move(position, position + 1);
                    break;
                case Directions.Down:
                    Bubbles.Move(position, position + ColsCount);
                    break;
                case Directions.Left:
                    Bubbles.Move(position, position - 1);
                    break;
                case Directions.Up:
                    Bubbles.Move(position, position - ColsCount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("direction");
            }
        }

    }
}
