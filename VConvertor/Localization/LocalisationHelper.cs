using System;
using System.ComponentModel;
using System.Linq;

namespace VConvertor.Localization
{
    /// <summary>
    /// Class which hhelps with localization
    /// </summary>
    public class LocalisationHelper : INotifyPropertyChanged
    {
        /// <summary>
        /// ctor.
        /// </summary>
        public LocalisationHelper()
        {
            if (!DesignHelpers.IsInDesignMode)
            {
                // Refresh all bindings when changing locales
                ResourceManagerService.LocaleChanged += (s, e) => { RaisePropertyChanged(string.Empty); };
            }
        }

        /// <summary>
        /// Getting string from resource using ResourceManager
        /// {Binding Source={StaticResource localisation}, Path=.[MainScreenResources.IntroTextLine1]}
        /// </summary>
        /// <param name="key">The key to extract from the resources in the format [ManagerName].[ResourceKey]</param>
        /// <returns>Item</returns>
        public string this[string key]
        {
            get
            {
                if (!ValidateKey(key))
                {
                    throw new ArgumentException(@"Wrong string format. [ManagerName].[ResourceKey]");
                }

                if (DesignHelpers.IsInDesignMode)
                {
                    return $"[{GetValueName(key)}]";
                }

                return ResourceManagerService.GetResourceString(GetManagerKey(key), GetResourceKey(key));
            }
        }

        #region Private Key Methods

        private bool ValidateKey(string input)
        {
            return input.Contains(".");
        }

        private string GetManagerKey(string input)
        {
            return input.Split('.')[0];
        }

        private string GetValueName(string input)
        {
            return input.Split('.').Last();
        }

        private string GetResourceKey(string input)
        {
            return input.Substring(input.IndexOf('.') + 1);
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            var evt = PropertyChanged;

            evt?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}