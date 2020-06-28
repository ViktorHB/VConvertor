using System.Windows;
using VConvertor.ViewModel;

namespace VConvertor.View
{
    /// <summary>
    /// Interaction logic for WindowWatch.xaml
    /// </summary>
    public partial class WindowWatch : Window
    {
        /// <inheritdoc />
        public WindowWatch(WindowWatchViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            //userControlPlayer.DataContext = viewModel.UcViewModel;
        }
    }
}
