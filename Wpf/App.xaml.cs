using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ViewModelClassLibrary;

namespace Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            #region Вместо этого куска кода по идее должен работать Inversion of Control контейнер

            var scoreViewModel = new ScoreViewModel();
            var gameFieldViewModel = new GameFieldViewModel();
            var mainVm = new MainWindowViewModel(scoreViewModel, gameFieldViewModel);
            var mainWindow = new MainWindow {DataContext = mainVm};
            
            #endregion

            mainWindow.Show();
        }
    }
}
