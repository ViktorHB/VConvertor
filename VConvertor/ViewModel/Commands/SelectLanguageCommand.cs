using System;
using System.Windows.Input;
using VConvertor.Localization;

namespace VConvertor.ViewModel.Commands
{
    /// <summary>
    /// Command which provides change language
    /// </summary>
    internal class SelectLanguageCommand : ICommand
    {
        public bool CanExecute(object parameter) => parameter != null;

        public void Execute(object parameter)
        {
            var locName = parameter as string;

            if (!string.IsNullOrEmpty(locName))
                ChangeLocalization(locName);
        }

        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Change localization
        /// </summary>
        /// <param name="regionName">Style name</param>
        private void ChangeLocalization(string regionName)
        {
            switch (regionName)
            {
                case "Русский":
                    ResourceManagerService.ChangeLocale("ru-Ru");
                    break;
                case "English":
                    ResourceManagerService.ChangeLocale("en-US");
                    break;
                default:
                    ResourceManagerService.ChangeLocale("en-US");
                    break;
            }
        }
    }
}
