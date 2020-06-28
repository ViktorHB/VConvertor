using System;
using VConvertor.ViewModel;

namespace VConvertor.UserControls.PlayerControl
{
    /// <summary>
    /// ViewModel that contains logic for playing movie
    /// </summary>
    public interface IPlayerControlViewModel 
    {
        /// <summary>
        /// Movie name
        /// </summary>
        string MovieName { get; set; }

        /// <summary>
        /// Movie source
        /// </summary>
        string MovieSource { get; set; }

        /// <summary>
        /// Movie possition
        /// </summary>
        long Position { get; set; }

        /// <summary>
        /// Max value (duration)
        /// </summary>
        long MaxPosition { get; set; }

        /// <summary>
        /// Min value (duration)
        /// </summary>
        long MinPosition { get; set; }

        /// <summary>
        /// Position changed
        /// </summary>
        event EventHandler PositionChanged;
    }
}
