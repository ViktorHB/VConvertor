using System.ComponentModel;
using System.Runtime.CompilerServices;
using VConvertor.Annotations;

namespace VConvertor.ViewModel
{
    /// <summary>
    /// Base class for view models which implements INotifyPropertyChanged
    /// </summary>
    public class ViewModelBase : IViewModel
    {
        #region MVVM PropertyChanged part
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion MVVM PropertyChanged part
    }
}
