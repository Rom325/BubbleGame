using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
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
    using RC2 = WeakReference<ObservableCollection<Poco>> ;
    /// <summary>
    /// Interaction logic for WrapHost.xaml
    /// </summary>
    public partial class WrapHost : UserControl
    {
        public ObservableCollection<Poco> Pocos { get; set; }

        public ICommand Move { get; set; }
        public WrapHost()
        {
            InitializeComponent();
            Pocos = new ObservableCollection<Poco>(Enumerable.Range(0, 64).Select(i => new Poco { Color = i % 3, Id = i}));
            Move = new MoveCommand( new RC2 (this.Pocos));
            DataContext = this;
        }

        protected internal class MoveCommand : ICommand
        {
            private readonly RC2 _collection;

            public MoveCommand(RC2 collection)
            {
                _collection = collection;
            }

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                int id = (int)parameter;
                ObservableCollection<Poco> target;
                if (!_collection.TryGetTarget(out target))
                {
                    return;
                }          
      
                target.Move(id, target.Count - (id + 1));

            }

            public event EventHandler CanExecuteChanged;
        }
    }

    public class Poco
    {
        public int Color { get; set; }
        public int Id { get; set; }
    }
}
