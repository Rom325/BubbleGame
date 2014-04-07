namespace WpfUserControls
{
	using System.Collections;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Collections.Specialized;
	using System.ComponentModel;
	using System.Linq;

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
		}
	}
}