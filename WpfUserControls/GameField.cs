namespace WpfUserControls
{
	using System;

	public enum Direction
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
		public bool TryMove(int position, Direction direction)
		{
			if (!CanMove(position, direction))
			{
				return false;
			}

			Move(position, direction);
			return true;
		}

		public bool CanMove(int position, Direction direction)
		{
			switch (direction)
			{
				case Direction.Right:
					return !IsInLastColumn(position);
				case Direction.Down:
					return !IsOnLastRow(position);
				case Direction.Left:
					return !IsInFirstColumn(position);
				case Direction.Up:
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

		private void Move(int position, Direction direction)
		{
			switch (direction)
			{
				case Direction.Right:
					Bubbles.Move(position, position + 1);
					break;
				case Direction.Down:
					Bubbles.Move(position, position + ColsCount);
					break;
				case Direction.Left:
					Bubbles.Move(position, position - 1);
					break;
				case Direction.Up:
					Bubbles.Move(position, position - ColsCount);
					break;
				default:
					throw new ArgumentOutOfRangeException("direction");
			}
		}

	}
}