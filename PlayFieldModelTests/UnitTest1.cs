using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PlayFieldModelTests
{
    [TestClass]
    public class UnitTest1
    {
        private const uint StandartLength = 64u;

        [TestMethod]
        public void UseAllColorsSuccessfully()
        {
            var bubbleCollection = new BubbleFactory();
            Bubble[] arr = bubbleCollection.Create(StandartLength);

            int colorsCount = arr.GroupBy(bubble => bubble.Color).Count();

            Assert.AreEqual(BubbleFactory.DifferentColorsCount, colorsCount);
        }

        [TestMethod]
        public void TestMethod()
        {
            // Arrange
            var bubbleCollection = new BubbleFactory();
            Bubble[] bubbles = bubbleCollection.Create(StandartLength);
            var collection = new BubbleCollection(bubbles);
            
            // Act
            collection.RemoveAt(0);

            // Assert
            Assert.IsNull(collection[0]);
        }
    }

    public class BubbleCollection : ObservableCollection<Bubble>
    {
        public BubbleCollection(IEnumerable<Bubble> bubbles) : base(bubbles)
        {
           
        }

        public void Destroy(params int[] indexes)
        {
            foreach (int index in indexes)
            {
                this.RemoveItem(index);
            }

            base.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, Items.Where((b, index) => indexes.Contains(index))));
        }

        protected override void RemoveItem(int index)
        {
            this.SetItem(index, null);
        }
    }

    public class BubbleFactory
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
