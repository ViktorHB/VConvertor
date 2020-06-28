using System;
using System.Globalization;

namespace VConvertor.Localization
{
    public delegate void LocaleChangedEventHander(object sender, LocaleChangedEventArgs e);

    /// <summary>
    /// Locale changed event args
    /// </summary>
    public class LocaleChangedEventArgs : EventArgs
    {
        /// <summary>
        /// New local
        /// </summary>
        public CultureInfo NewLocale { get; set; }

        /// <summary>
        /// Change event args
        /// </summary>
        /// <param name="newLocale">New local <see cref="CultureInfo"/></param>
        public LocaleChangedEventArgs(CultureInfo newLocale)
        {
            NewLocale = newLocale;
        }
    }
}
