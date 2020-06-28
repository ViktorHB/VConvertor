using System;
using VConvertor.ViewModel;

namespace VConvertor.UserControls.PlayerControl
{
    /// <summary>
    /// Base view model class for <see cref="ControlPlayer"/>
    /// </summary>
    public abstract class PlayerControlViewModelBase : ViewModelBase, IPlayerControlViewModel
    {
        /// <summary>
        /// Movie name
        /// </summary>
        public abstract string MovieName { get; set; }

        /// <summary>
        /// Movie source
        /// </summary>
        public abstract string MovieSource { get; set; }

        /// <summary>
        /// Current slider position
        /// </summary>
        public abstract long Position { get; set; }

        /// <summary>
        /// Max slider position
        /// </summary>
        public abstract long MaxPosition { get; set; }

        /// <summary>
        /// Min slider position
        /// </summary>
        public abstract long MinPosition { get; set; }

        /// <summary>
        /// Position change event
        /// </summary>
        public event EventHandler PositionChanged;

        /// <summary>
        /// PositionChanged invoker
        /// </summary>
        /// <param name="position">Position</param>
        protected virtual void OnPositionChanged(long position)
        {
            PositionChanged?.Invoke(this, new PlayerControlEventArgs{Position = position});
        }
    }
}
