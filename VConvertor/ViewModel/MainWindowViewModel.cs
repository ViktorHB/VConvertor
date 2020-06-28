using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Timers;
using System.Windows.Input;
using VConvertor.Localization;
using VConvertor.Model;
using VConvertor.Resources.Language;
using VConvertor.UserControls.PlayerControl;
using VConvertor.View;
using VConvertor.ViewModel.Commands;

namespace VConvertor.ViewModel
{
    /// <inheritdoc />
    /// <summary>
    /// Viww model class for MainWindow <see cref="T:VConvertor.MainWindow" />
    /// </summary>
    internal class MainWindowViewModel : ViewModelBase
    {
        #region Fields 
        private IConverter _converter;
        private string _title;
        private string _inputFile;
        private string _outputFile;
        private string _selectedFormat;
        private ICommand _startConvertCommand;
        private ICommand _selectFileCommand;
        private ICommand _saveFileCommand;
        private ICommand _abortCommand;
        private ICommand _exitCommand;
        private ICommand _watchCommand;
        private bool _btnWatchIsEnable;
        private long _pbMax;
        private long _pbMin;
        private long _pbValue;
        private System.Timers.Timer _timer;
        private int _selectedIndexFormat;
        #endregion Fields 

        #region ctor
        /// <summary>
        /// ctor creates instance <see cref="MainWindowViewModel"/>
        /// </summary>
        public MainWindowViewModel()
        {
            ResourceManagerService.RegisterManager("LanguageRes", LanguageRes.ResourceManager, true);
        }

        #endregion

        #region Properties

        public int SelectedIndexFormat
        {
            get => _selectedIndexFormat;
            set
            {
                if (_selectedIndexFormat == value)
                    return;
                _selectedIndexFormat = value;
                OnPropertyChanged(nameof(SelectedIndexFormat));
            }
        }

        /// <summary>
        /// Is watch button enable
        /// </summary>
        public bool BtnWatchIsEnable
        {
            get => _btnWatchIsEnable;
            set
            {
                if (_btnWatchIsEnable == value)
                    return;
                _btnWatchIsEnable = value;
                OnPropertyChanged(nameof(BtnWatchIsEnable));
            }
        }

        /// <summary>
        /// Progress bar max value
        /// </summary>
        public long PbMax
        {
            get => _pbMax;
            set
            {
                if (_pbMax == value)
                    return;
                _pbMax = value;
                OnPropertyChanged(nameof(PbMax));
            }
        }

        /// <summary>
        /// Progress bar min value
        /// </summary>
        public long PbMin
        {
            get => _pbMin;
            set
            {
                if (_pbMin == value)
                    return;
                _pbMin = value;
                OnPropertyChanged(nameof(PbMin));
            }
        }

        /// <summary>
        /// Progress bar current value
        /// </summary>
        public long PbValue
        {
            get => _pbValue;
            set
            {
                if (_pbValue == value)
                    return;
                _pbValue = value;
                OnPropertyChanged(nameof(PbValue));
            }
        }

        /// <summary>
        /// Title of file
        /// </summary>
        public string Title
        {
            get => _title;
            set
            {
                if (_title == value)
                    return;
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        /// <summary>
        /// Select file path
        /// </summary>
        public string InputFile
        {
            get => _inputFile;
            set
            {
                if (_inputFile == value)
                    return;
                _inputFile = value;
                OnPropertyChanged(nameof(InputFile));
            }
        }

        /// <summary>
        /// Saves file path
        /// </summary>
        public string OutputFile
        {
            get => _outputFile;
            set
            {
                if (_outputFile == value)
                    return;
                _outputFile = value;
                OnPropertyChanged(nameof(OutputFile));
            }
        }

        /// <summary>
        /// Saves file path
        /// </summary>
        public string SelectedFormat
        {
            get => _selectedFormat;
            set
            {
                if (_selectedFormat == value)
                    return;
                _selectedFormat = value;
                AddExtentionToSavedFilePath();
                OnPropertyChanged(nameof(SelectedFormat));
            }
        }

        /// <summary>
        /// Video formats
        /// </summary>
        public ObservableCollection<string> Formats { get; } = new ObservableCollection<string>(new List<string> { "avi", "mp4", "mov" });

    
        #endregion Properties

        #region Commands

        /// <summary>
        /// The command used for change language
        /// </summary>
        public ICommand SelectLanguageCommand { get; set; } = new SelectLanguageCommand();

        /// <summary>
        /// The command used for change language
        /// </summary>
        public ICommand ChangeStyleCommand { get; set; } = new ChangeStyleCommand();

        /// <summary>
        /// The command used select file to convert.
        /// </summary>
        public ICommand SelectFileCommand
        {
            get => _selectFileCommand ?? (_selectFileCommand = new RelayCommand(SelectFileCommandExecute));
            set => _selectFileCommand = value;
        }

        /// <summary>
        /// The command used to start converting.
        /// </summary>
        public ICommand StartConvertCommand
        {
            get => _startConvertCommand ?? (_startConvertCommand = new RelayCommand(StartConvertCommandExecute));
            set => _startConvertCommand = value;
        }

        /// <summary>
        /// The command used select file path to save file.
        /// </summary>
        public ICommand SaveFileCommand
        {
            get => _saveFileCommand ?? (_saveFileCommand = new RelayCommand(SaveFileCommandExecute));
            set => _saveFileCommand = value;
        }

        /// <summary>
        /// The command used to cancel convertation.
        /// </summary>
        public ICommand AbortCommand
        {
            get => _abortCommand ?? (_abortCommand = new RelayCommand(AbortCommandExecute));
            set => _abortCommand = value;
        }

        /// <summary>
        /// The command used to exit application.
        /// </summary>
        public ICommand ExitCommand
        {
            get => _exitCommand ?? (_exitCommand = new RelayCommand(CommandExitExecute));
            set => _exitCommand = value;
        }

        /// <summary>
        /// Open wach window
        /// </summary>
        public ICommand WatchCommand
        {
            get => _watchCommand ?? (_watchCommand = new RelayCommand(WatchCommandExecute));
            set => _watchCommand = value;
        }

        #endregion Commands

        #region Private methods

        /// <summary>
        /// Open watch film window <see cref="ControlPlayer"/>
        /// </summary>
        /// <param name="obj"></param>
        private void WatchCommandExecute(object obj)
        {
            if (!_converter.IsStarted) return;

            RunWatchWindow();
        }

        /// <summary>
        /// Cancel convertation
        /// </summary>
        /// <param name="obj"></param>
        private void AbortCommandExecute(object obj)
        {
            if (_converter == null) return;

            _converter?.Stop();
            _timer?.Stop();
            BtnWatchIsEnable = _converter.IsStarted;
            PbMin = 0;
            PbValue = 0;
        }

        /// <summary>
        /// Select file path to save converted file
        /// </summary>
        /// <param name="obj"></param>
        private void SaveFileCommandExecute(object obj)
        {
            var saveFileDialog = FileDialogWrapper.GetFileFialog(FileDialogsType.Save);

            if (saveFileDialog.ShowDialog() == true)
            {
                OutputFile = saveFileDialog.FileName;
            }
        }

        /// <summary>
        /// Select file to converting
        /// </summary>
        /// <param name="obj"></param>

        private void SelectFileCommandExecute(object obj)
        {
            var openFileDialog = FileDialogWrapper.GetFileFialog(FileDialogsType.Open);

            if (openFileDialog.ShowDialog() == true)
            {
                Title = Path.ChangeExtension(Path.GetFileName(openFileDialog.FileName), string.Empty)
                    .TrimEnd('.');
                InputFile = openFileDialog.FileName;
            }
        }

        /// <summary>
        /// Start converting. Converts file to another format
        /// </summary>
        /// <param name="obj"></param>
        private void StartConvertCommandExecute(object obj)
        {
            StartConverting();
        }

        /// <summary>
        /// Exit from the application
        /// </summary>
        /// <param name="obj"></param>
        private void CommandExitExecute(object obj)
        {
            Exit();
        }

        /// <summary>
        /// Adds extention to the saveed file path
        /// </summary>
        private void AddExtentionToSavedFilePath()
        {
            OutputFile = Path.ChangeExtension(OutputFile, SelectedFormat);
        }

        /// <summary>
        /// Start converting 
        /// </summary>
        private void StartConverting()
        {
            _converter = new Converter(InputFile, OutputFile, VConvertor.Formats.Avi);
            _converter?.StartConvert();
            Thread.Sleep(1000);

            BtnWatchIsEnable = _converter.IsStarted;
            PbMax = _converter.GetTotalDuration().Ticks;

            RunTimer();
        }

        /// <summary>
        /// Exit from app
        /// </summary>
        private void Exit()
        {
            _converter?.Stop();
            Environment.Exit(-1);
        }

        /// <summary>
        /// Sets up and run timer
        /// </summary>
        private void RunTimer()
        {
            _timer = new System.Timers.Timer
            {
                Interval = ConverterConstants.TimerIntreval
            };
            _timer.Elapsed += OnTimedEvent;
            _timer.Enabled = true;
            _timer.Start();
        }

        /// <summary>
        /// Timer click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            PbValue = _converter.GetProgress().Ticks;

            if (PbValue == PbMax)
                _timer.Stop();
        }

        /// <summary>
        /// Sets up and run watch window
        /// </summary>
        private void RunWatchWindow()
        {
            var playerViewModel = new PlayerControlViewModel
            {
                MovieName = _converter.GetMovieName(),
                MovieSource = InputFile,
                Position = 0,
                MaxPosition = _converter.GetTotalDuration().Ticks,
                MinPosition = 0,
            };

            var watchWindowViewModel = new WindowWatchViewModel(playerViewModel, _converter.GetMovieName())
            {
                MainTitle = _converter.GetMovieName()
            };

            new WindowWatch(watchWindowViewModel).Show();
        }

        #endregion Private methods

    }
}
