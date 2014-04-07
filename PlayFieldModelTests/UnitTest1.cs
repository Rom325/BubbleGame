using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PlayFieldModelTests
{
	using WpfUserControls;

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
}
