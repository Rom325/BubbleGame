using System;
using System.Windows;

namespace WpfUserControls
{
    public class BubbleClickedEventArgs : RoutedEventArgs
    {
        private readonly int _index;

        public BubbleClickedEventArgs(int index, RoutedEvent routedEvent, object source) : base(routedEvent, source)
        {
            _index = index;
        }

        public int Index
        {
            get { return _index; }
        }
    }
}