using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace ViewModelClassLibrary
{
    public class BubbleCollection : ObservableCollection<Bubble>
	{
	    private readonly IBubbleFactory _bubbleFactory;

	    public BubbleCollection(uint length, IBubbleFactory bubbleFactory) : base(bubbleFactory.Create(length))
		{
		    _bubbleFactory = bubbleFactory;
		}

	    public void Destroy(IEnumerable<int> indexes)
		{
			// Используем 'монитор' из базового класса для потокобезопасности.
			this.CheckReentrancy();

	        var arr = indexes as int[] ?? indexes.ToArray();
	        
            // Индексы должны быть отсортированы по убыванию, так как список автоматически изменяет свой размер.
	        foreach (int index in arr.OrderByDescending(_ => _))
			{
				this.Items.RemoveAt(index);
			}

			this.OnPropertyChanged(new PropertyChangedEventArgs("Count"));
			this.OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
			this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

	        this.Refill(arr.Length);
		}

	    private void Refill(int length)
	    {
	        for (int i = 0; i < length; i++)
	        {
	            this.Items.Add(_bubbleFactory.NewRandomBubble());
	        }

            this.OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            this.OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
	    }
	}
}