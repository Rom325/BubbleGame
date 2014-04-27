using System.CodeDom;
using System.Diagnostics;

namespace WpfUserControls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using ViewModelClassLibrary;

	/// <summary>
    /// Interaction logic for WrapHost.xaml
    /// </summary>
    public partial class WrapHost : UserControl
    {
        public const int MinChainLength = 3;

	    public static readonly RoutedEvent BubbleClickedEvent =
	        EventManager.RegisterRoutedEvent(
	            "BubbleClicked",
	            RoutingStrategy.Bubble,
	            typeof (RoutedEventHandler),
	            typeof (WrapHost));

        public event RoutedEventHandler BubbleClicked
        {
            add { AddHandler(BubbleClickedEvent, value); }
            remove { RemoveHandler(BubbleClickedEvent, value);}
        }

	    public WrapHost()
        {
            InitializeComponent();
        }

	    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
	    {
	        var btn = sender as Button;
	        if (btn != null)
	        {
	            var parent = VisualTreeHelper.GetParent(btn);

                var contentPresenter = parent as ContentPresenter;
                if (contentPresenter == null)
                {
                    return;
                }

                int currentPosition = this.GameField.ItemContainerGenerator.IndexFromContainer(contentPresenter);
                RaiseEvent(new BubbleClickedEventArgs(currentPosition, BubbleClickedEvent, sender));

                GameFieldViewModel gameField = this.DataContext as GameFieldViewModel;
	            if (gameField != null)
	            {
	                gameField.HandleClick(currentPosition);
	            }
	        }
	    }
    }
}
