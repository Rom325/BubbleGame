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
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public readonly LinearGradientBrush Brush0;
        public readonly LinearGradientBrush Brush1;
        public readonly LinearGradientBrush Brush2;
        public readonly LinearGradientBrush Brush3;
        public UserControl1()
        {
            InitializeComponent();
            Brush0 = CreateLinearGradient(Colors.BlueViolet);
            Brush1 = CreateLinearGradient(Colors.HotPink);
            Brush2 = CreateLinearGradient(Colors.Aqua);
            Brush3 = CreateLinearGradient(Colors.LightGreen);
            AddEllipses(64);
            
        }

        private static LinearGradientBrush CreateLinearGradient(Color color)
        {
            return new LinearGradientBrush(new GradientStopCollection
            {
                new GradientStop {Color = color, Offset = 0.0},
                new GradientStop {Color = Colors.White, Offset = 1.0}
            });

        }

        private void AddEllipses(int ellipseCount)
        {
            for (int i = 0; i < ellipseCount; i++)
            {
                this.ContainerPanel.Children.Add(new Ellipse(){ Height = 40.0, Width = 40.0, Fill = GetRandomBrush()});
            }
        }

        private Brush GetRandomBrush()
        {
            var random = new Random(Guid.NewGuid().ToByteArray().First());
            switch (random.Next() % 4)
            {
                case 0:
                    return this.Brush0;
                case 1:
                    return this.Brush1;
                case 2:
                    return this.Brush2;
                default:
                    return this.Brush3;
            }

        }
    }
}
