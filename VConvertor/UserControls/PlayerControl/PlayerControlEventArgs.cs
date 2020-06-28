using System.Windows;

namespace VConvertor.UserControls.PlayerControl
{
    /// <summary>
    /// Class which helps to get / set video position
    /// </summary>
    internal class PlayerControlEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// Video position
        /// </summary>
        public long Position { get; set; }
    }
}
