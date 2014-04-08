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
			// ���������� '�������' �� �������� ������ ��� ������������������.
			this.CheckReentrancy();

			// ������� ������ ���� ������������� �� ��������, ��� ��� ������ ������������� �������� ���� ������.
			foreach (int index in indexes.OrderByDescending(_ => _))
			{
				this.Items.RemoveAt(index);
			}

			this.OnPropertyChanged(new PropertyChangedEventArgs("Count"));
			this.OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
			this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}
	}
}