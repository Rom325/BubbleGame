using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
	/// <summary>
	/// Interaction logic for WrapHost.xaml
	/// </summary>
	public partial class UserControl1 : UserControl
	{
		// Our model here
		public GameField Model { get; set; }

		public ICommand Move { get; set; }

		public UserControl1()
		{
			InitializeComponent();
			Model = new GameField(8, 8);
			Move = new RelayCommand(container =>
			{
				var contentPresenter = container as ContentPresenter;
				int position = this.GameField.ItemContainerGenerator.IndexFromContainer(contentPresenter);
				int[] indexes = { position, position - 1, position + 1 };

				Model.Bubbles.Destroy(indexes);
			});

			DataContext = this;
		}
	}
}
