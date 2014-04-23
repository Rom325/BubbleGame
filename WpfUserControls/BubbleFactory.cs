namespace WpfUserControls
{
	using System;
	using System.Linq;

	public class BubbleFactory : IBubbleFactory
	{
		public const byte DifferentColorsCount = 5;

		private readonly Random _random;

		public BubbleFactory()
		{
			_random = new Random();
		}

		public Bubble NewRandomBubble()
		{
			var color = _random.Next(0, DifferentColorsCount);
			return new Bubble(color);
		}

		public Bubble[] Create(uint collectionLength)
		{
			return Enumerable.Range(0, (int)collectionLength).Select(_ => NewRandomBubble()).ToArray();
		}
	}
}