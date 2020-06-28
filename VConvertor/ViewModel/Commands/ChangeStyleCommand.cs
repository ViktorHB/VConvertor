using System;
using System.Windows.Input;
using VConvertor.Model;

namespace VConvertor.ViewModel.Commands
{
    /// <summary>
    /// Command which provides change styles
    /// </summary>
    internal class ChangeStyleCommand : ICommand
    {
        //consts
        private const string DefaultStyle = "Resources/Styles/StyleDefault.xaml";
        private const string DarkStyle = "Resources/Styles/StyleDark.xaml";
        private const string AnimatedStyle = "Resources/Styles/StyleAnimated.xaml";
        public bool CanExecute(object parameter) => parameter != null;

        public void Execute(object parameter)
        {
            var styleName = (StyleNames)parameter;
            ChangeStyle(styleName);
        }

        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Change style
        /// </summary>
        /// <param name="styleName">Style name</param>
        private void ChangeStyle(StyleNames styleName)
        {
            switch (styleName)
            {
                case StyleNames.Default:
                    ResourcesChanger.ChangeResource(DefaultStyle);
                    break;
                case StyleNames.Dark:
                    ResourcesChanger.ChangeResource(DarkStyle);
                    break;
                case StyleNames.Animated:
                    ResourcesChanger.ChangeResource(AnimatedStyle);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(styleName), styleName, null);
            }
        }
    }
}

