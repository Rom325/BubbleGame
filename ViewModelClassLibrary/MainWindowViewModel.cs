using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ViewModelClassLibrary
{
    public class MainWindowViewModel : ViewModelBase
    {
        private int _cols;
        private int _rows;

        public MainWindowViewModel(ScoreViewModel scoreViewModel, GameFieldViewModel gameFieldViewModel)
        {
            NumsList = Enumerable.Range(8, 13).ToList();
            Cols = Rows = 8;
            
            GameFieldViewModel = gameFieldViewModel;
            gameFieldViewModel.BubblesDestroyed += GameFieldViewModelOnBubblesDestroyed;

            ScoreViewModel = scoreViewModel;
            NewGameCommand = new RelayCommand(p => StartNewGame());
        }

        private void GameFieldViewModelOnBubblesDestroyed(object sender, BubblesDestroyedEventArgs e)
        {
            ScoreViewModel.Score += e.Count + (e.Count/4) + (e.Count/5);
        }

        public ScoreViewModel ScoreViewModel { get; private set; }
        public GameFieldViewModel GameFieldViewModel { get; private set; }
        public ICommand NewGameCommand { get; private set; }

        public List<int> NumsList { get; private set; }

        public int Cols
        {
            get { return _cols; }
            set 
            {
                if (_cols == value)
                {
                    return;
                }
                _cols = value;
                OnPropertyChanged();
            }
        }

        public int Rows
        {
            get { return _rows; }
            set
            {
                if (_rows == value)
                {
                    return;
                }
                _rows = value;
                OnPropertyChanged();
            }
        }

        private void StartNewGame()
        {
            ScoreViewModel.ResetScore();
            GameFieldViewModel.NewGame(Convert.ToByte(Cols), Convert.ToByte(Rows));
        }
    }
}