namespace ViewModelClassLibrary
{
    public class ScoreViewModel : ViewModelBase
    {
        private int _score;

        public int Score
        {
            get { return _score; }
            set
            {
                if (value == _score) return;
                _score = value;
                OnPropertyChanged();
            }
        }

        public void ResetScore()
        {
            this.Score = 0;
        }
    }
}
