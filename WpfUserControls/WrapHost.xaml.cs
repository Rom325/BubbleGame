using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace WpfUserControls
{
	/// <summary>
    /// Interaction logic for WrapHost.xaml
    /// </summary>
    public partial class WrapHost : UserControl
    {
        public const int MinChainLength = 3;

		// Our model here
        public GameField Model { get; set; }

        public ICommand Move { get; set; }

        public WrapHost()
        {
            InitializeComponent();
            Model = new GameField(8, 8, new BubbleFactory());
            Move = new RelayCommand(container =>
	            {
                    var contentPresenter = container as ContentPresenter;
	                if (contentPresenter == null)
	                {
	                    return;
	                }

                    int currentPosition = this.GameField.ItemContainerGenerator.IndexFromContainer(contentPresenter);
                    List<int> coloredChain = Model.GetColoredChain(currentPosition);
                    if (coloredChain.Count < MinChainLength)
                    {
                        return;
                    }

                    Model.Bubbles.Destroy(coloredChain);
	            });

            DataContext = this;
        }
    }
}
