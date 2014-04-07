using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PlayFieldModelTests
{
    [TestClass]
    public class BubblesExtensionsShould
    {
        [TestMethod]
        public void DestroyElementsProperly()
        {
            // Arrange
            var factory = new BubbleFactory();
            Bubble[] bubbles = factory.Create(9u);
            int[] indexes = {0, 3, 8};

            // Act
            bubbles.DestroyAt(indexes);

            // Assert
            Assert.IsTrue(bubbles.Where((_, index) => indexes.Contains(index)).All(elem => elem == null));
        }

        [TestMethod]
        public void RefillSuccessfully()
        {
            // Arrange
            var factory = new BubbleFactory();
            Bubble[] bubbles = factory.Create(9u);
            int[] indexes = { 0, 3, 8 };

            // Act
            bubbles.DestroyAt(indexes);
            bubbles.Refill(); 

            // Assert
            Assert.IsTrue(bubbles.Where((_, index) => indexes.Contains(index)).All(elem => elem != null));
        }
    }
}
