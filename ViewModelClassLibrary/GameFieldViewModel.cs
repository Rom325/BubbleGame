namespace ViewModelClassLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;

    public class GameFieldViewModel : ViewModelBase
    {
        private double _calculatedWidth;
        public const int MinChainLength = 3;

        // Hack
        public const double EllipseSize = 30.0;

        // Основная логика в модели
        public GameField Model { get; private set; }

        public double CalculatedWidth
        {
            get { return _calculatedWidth; }
            set {
                if (_calculatedWidth == value)
                {
                    return;
                }
                _calculatedWidth = value;
                OnPropertyChanged();
            }
        }

        public ICommand Move { get; set; }

	    public event EventHandler<BubblesDestroyedEventArgs> BubblesDestroyed;

	    protected virtual void OnBubblesDestroyed(BubblesDestroyedEventArgs e)
	    {
	        var handler = BubblesDestroyed;
	        if (handler != null) handler(this, e);
	    }

        public GameFieldViewModel()
        {
            CalculatedWidth = (8*EllipseSize) + 10.0;
	        Model = new GameField(8, 8, new BubbleFactory());
        }

        public void NewGame(byte cols, byte rows)
        {
            Model.StartNew(cols, rows);
            CalculatedWidth = (cols*EllipseSize) + 10.0;
            OnPropertyChanged("Model");
        }

        public void HandleClick(int currentPosition)
        {
            List<int> coloredChain = Model.GetColoredChain(currentPosition);
            if (coloredChain.Count < MinChainLength)
            {
                return;
            }

            Model.Bubbles.Destroy(coloredChain);
            this.OnBubblesDestroyed(new BubblesDestroyedEventArgs(coloredChain.Count));
        }
    }

    public class BubblesDestroyedEventArgs : EventArgs
    {
        public int Count { get; private set; }

        public BubblesDestroyedEventArgs(int count)
        {
            Count = count;
        }
    }
}
