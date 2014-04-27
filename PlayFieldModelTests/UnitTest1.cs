namespace PlayFieldModelTests
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ViewModelClassLibrary;

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
    }
}
