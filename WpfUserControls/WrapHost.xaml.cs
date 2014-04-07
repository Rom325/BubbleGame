using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfUserControls
{
	using System.Windows;

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
            Move = new RelayCommand(item =>
	            {
					var listBoxItem = item as ContentPresenter;
					int id = this.GameField.ItemContainerGenerator.IndexFromContainer(listBoxItem);
					if (!this.Model.TryMove(id, Direction.Left))
					{
						this.Model.TryMove(id, Direction.Right);
					}

	            });

            DataContext = this;
        }
    }
}
