using System;
using System.Collections.Generic;
using System.Linq;

namespace ViewModelClassLibrary
{
    public class GameField
	{
        internal class Movement
        {
            protected int Addition { get; private set; }
            protected Func<int, bool> CanMoveFunc { get; private set; }

            public Movement(int addition, Func<int, bool> canMoveFunc)
            {
                Addition = addition;
                CanMoveFunc = canMoveFunc;
            }

            public int? Apply(int position)
            {
                return CanApply(position) ? (int?)position + Addition : null;
            }

            public bool CanApply(int positon)
            {
                return CanMoveFunc(positon);
            }
        }

	    private Movement[] _movements;
        private IBubbleFactory _bubbleFactory;

        public GameField(byte rowsCount, byte colsCount, IBubbleFactory bubbleFactory)
		{
            _bubbleFactory = bubbleFactory;
            RowsCount = rowsCount;
			ColsCount = colsCount;

	        var size = (uint)(rowsCount * colsCount);
	        Bubbles = new BubbleCollection(size, bubbleFactory);

	        var moveRight = new Movement(+1             , i => !IsInLastColumn(i));
	        var moveLeft  = new Movement(-1             , i => !IsInFirstColumn(i));
	        var moveDown  = new Movement(+this.ColsCount, i => !IsOnLastRow(i));
	        var moveUp    = new Movement(-this.ColsCount, i => !IsOnFirstRow(i));
	        _movements = new []{moveDown, moveLeft, moveRight, moveUp};
		}

		public byte RowsCount { get; private set; }

		public byte ColsCount { get; private set; }

		public BubbleCollection Bubbles { get; private set; }

	    public List<int> GetColoredChain(int currentPosition)
	    {
	        var visited = new List<int>();
	        var toVisit = new List<int>{currentPosition};

	        while (toVisit.Count > 0)
	        {
	            var item = toVisit[0];
	            visited.Add(item);

	            var tmpList = GetNeighbors(item, visited);
	            toVisit.AddRange(tmpList);
	            toVisit.RemoveAt(0);
	        }

	        return visited.Distinct().ToList();
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

	    private IEnumerable<int> GetNeighbors(int currentPosition, List<int> visited)
	    {
            int currentColor = Bubbles[currentPosition].Color;
	        return
	            _movements
	                .Select(m => m.Apply(currentPosition))
	                .Where(pos => pos.HasValue && !visited.Contains(pos.Value) && pos.Value < Bubbles.Count && Bubbles[pos.Value].Color == currentColor)
                    .Cast<int>()
	                .ToList();
	    }

        public void StartNew(byte cols, byte rows)
        {
            RowsCount = rows;
            ColsCount = cols;

            var size = (uint)(rows * cols);
            Bubbles = new BubbleCollection(size, _bubbleFactory);

            var moveRight = new Movement(+1, i => !IsInLastColumn(i));
            var moveLeft = new Movement(-1, i => !IsInFirstColumn(i));
            var moveDown = new Movement(+this.ColsCount, i => !IsOnLastRow(i));
            var moveUp = new Movement(-this.ColsCount, i => !IsOnFirstRow(i));
            _movements = new[] { moveDown, moveLeft, moveRight, moveUp };
        }
	}
}