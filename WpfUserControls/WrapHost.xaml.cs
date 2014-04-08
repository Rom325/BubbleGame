using System.Windows.Controls;
using System.Windows.Input;

namespace WpfUserControls
{
	/// <summary>
    /// Interaction logic for WrapHost.xaml
    /// </summary>
    public partial class WrapHost : UserControl
    {
		// Our model here
        public GameField Model { get; set; }

        public ICommand Move { get; set; }

        public WrapHost()
        {
            InitializeComponent();
            Model = new GameField(8, 8);
            Move = new RelayCommand(container =>
	            {
					var contentPresenter = container as ContentPresenter;
					int position = this.GameField.ItemContainerGenerator.IndexFromContainer(contentPresenter);
					int[] indexes = {position, position - 1, position + 1};

					Model.Bubbles.Destroy(indexes);
	            });

            DataContext = this;
        }
    }
}
