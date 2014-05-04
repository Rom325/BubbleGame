namespace ViewModelClassLibrary
{
    public interface IBubbleFactory
    {
        Bubble NewRandomBubble();
        Bubble[] Create(uint collectionLength);
    }
}