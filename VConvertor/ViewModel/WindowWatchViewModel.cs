using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using VConvertor.Annotations;
using VConvertor.Localization;
using VConvertor.UserControls.PlayerControl;
using VConvertor.View;

namespace VConvertor.ViewModel
{
    /// <summary>
    /// Viev model for window <see cref="WindowWatch"/>
    /// </summary>
    public class WindowWatchViewModel : ViewModelBase
    {
        //fields
        private string _mainTitle;
        private readonly string _movieName;

        public WindowWatchViewModel(IPlayerControlViewModel viewModel, string movieName)
        {
            _movieName = movieName;
            UcViewModel = viewModel;
            UcViewModel.PositionChanged += ViewModel_PositionChanged;
        }

        /// <summary>
        /// User control view model <see cref="IPlayerControlViewModel"/>
        /// </summary>
        public IPlayerControlViewModel UcViewModel { get; }

        /// <summary>
        /// Main window title
        /// </summary>
        public string MainTitle
        {
            get
            {
                if (Application.Current.Resources["Localisation"] is LocalisationHelper resourseLang)
                    return string.Format(resourseLang["LanguageRes.MainTitleWatchWindow"], _movieName);
                return _movieName;
            }
            set
            {
                if (_mainTitle == value) return;
                _mainTitle = value;
                OnPropertyChanged(nameof(MainTitle));
            }
        }

        /// <summary>
        /// Event when position change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewModel_PositionChanged(object sender, System.EventArgs e)
        {
            if (e is PlayerControlEventArgs posArgs)
                UcViewModel.Position = posArgs.Position;
        }
    }
}
