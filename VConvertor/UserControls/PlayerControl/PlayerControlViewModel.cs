namespace VConvertor.UserControls.PlayerControl
{
    /// <summary>
    /// View model for <see cref="ControlPlayer"/>
    /// </summary>
    public class PlayerControlViewModel : PlayerControlViewModelBase
    {
        //Fields
        private string _movieName;
        private string _movieSource;
        private long _position;
        private long _maxPosition;
        private long _minPosition;

        /// <summary>
        /// Creates instanse <see cref="PlayerControlViewModel"/>
        /// </summary>
        public PlayerControlViewModel()
        {
        }


        /// <summary>
        /// Max slider position
        /// </summary>
        public override long MaxPosition
        {
            get => _maxPosition;
            set
            {
                if (_maxPosition == value) return;
                _maxPosition = value;
                OnPropertyChanged(nameof(MaxPosition));
            }
        }

        /// <summary>
        /// Min slider position
        /// </summary>
        public override long MinPosition
        {
            get => _minPosition;
            set
            {
                if (_minPosition == value) return;
                _minPosition = value;
                OnPropertyChanged(nameof(MinPosition));
            }
        }

        /// <summary>
        /// Movie name
        /// </summary>
        public override string MovieName
        {
            get => _movieName;
            set
            {
                if (_movieName == value) return;
                _movieName = value;
                OnPropertyChanged(nameof(MovieName));
            }
        }

        /// <summary>
        /// Movie sourse 
        /// </summary>
        public override string MovieSource
        {
            get => _movieSource;
            set
            {
                if (_movieSource == value) return;
                _movieSource = value;
                OnPropertyChanged(nameof(MovieSource));
            }
        }

        /// <summary>
        /// Current position
        /// </summary>
        public override long Position
        {
            get => _position;
            set
            {
                if (_position == value) return;
                _position = value;
                OnPropertyChanged(nameof(Position));
                OnPositionChanged(Position);
            }
        }
    }
}
