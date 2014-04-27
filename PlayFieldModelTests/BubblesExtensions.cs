namespace PlayFieldModelTests
{
    using ViewModelClassLibrary;
	public static class BubblesExtensions
    {
        public static void DestroyAt(this Bubble[] bubbleCollection, params int[] indexes)
        {
            foreach (int index in indexes)
            {
                bubbleCollection.SetValue(null, index); 
            }
        }

        public static void Refill(this Bubble[] bubbleCollection)
        {
            var factory = new BubbleFactory();
            for (int i = 0; i < bubbleCollection.Length; i++)
            {
                bubbleCollection[i] = bubbleCollection[i] ?? factory.NewRandomBubble();
            }          
        }
    }
}