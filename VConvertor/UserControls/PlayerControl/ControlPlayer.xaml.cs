using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace VConvertor.UserControls.PlayerControl
{
    /// <summary>
    /// Interaction logic for ControlPlayer.xaml
    /// </summary>
    public partial class ControlPlayer : UserControl
    {
        //fields
        private bool _isPlayed;

        /// <summary>
        /// Creates an instance of <see cref="ControlPlayer"/>
        /// </summary>
        public ControlPlayer()
        {
            InitializeComponent();
            mePlayer.Play();
            _isPlayed = true;
        }

        /// <summary>
        /// Creates an instance of <see cref="ControlPlayer"/>
        /// </summary>
        /// <param name="viewModel">View model</param>
        internal ControlPlayer(PlayerControlViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        /// <summary>
        /// Plays video
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">EventArgs <see cref="RoutedEventArgs"/></param>
        private void BtnPlay_OnClick(object sender, RoutedEventArgs e)
        {
            mePlayer.Play();
            _isPlayed = true;
        }

        /// <summary>
        /// Pauses video
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">EventArgs <see cref="RoutedEventArgs"/></param>
        private void BtnPause_OnClick(object sender, RoutedEventArgs e)
        {
            mePlayer.Pause();
            _isPlayed = false;
        }

        /// <summary>
        /// Stops video
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">EventArgs <see cref="RoutedEventArgs"/></param>
        private void BtnStop_OnClick(object sender, RoutedEventArgs e)
        {
            mePlayer.Stop();
            _isPlayed = false;
        }

        /// <summary>
        /// Video rewinding
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">EventArgs <see cref="RoutedEventArgs"/></param>
        private void SProgress_OnValueChanged(object sender, RoutedEventArgs e)
        {
            var slider = (Slider)sender;
            mePlayer.Position = new TimeSpan((long)slider.Value);
        }

        /// <summary>
        /// Pause on mouse click
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">EventArgs <see cref="MouseButtonEventArgs"/></param>
        private void MePlayer_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_isPlayed)
            {
                mePlayer.Pause();
                _isPlayed = false;
            }
            else
            {
                mePlayer.Play();
                _isPlayed = true;
            }
        }
    }
}
