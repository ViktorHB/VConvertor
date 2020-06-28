using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace VConvertor.Localization
{
    /// <summary>
    /// Class which manage resources 
    /// </summary>
    public static class ResourceManagerService
    {
        private static readonly Dictionary<string, ResourceManager> Managers = new Dictionary<string, ResourceManager>();

        /// <summary>
        /// Local change event
        /// </summary>
        public static event LocaleChangedEventHander LocaleChanged;

        /// <summary>
        /// LocaleChanged event invoke
        /// </summary>
        /// <param name="newLocale">New local <see cref="CultureInfo"/></param>
        private static void RaiseLocaleChanged(CultureInfo newLocale)
        {
            var evt = LocaleChanged;

            evt?.Invoke(null, new LocaleChangedEventArgs(newLocale));
        }

        /// <summary>
        /// Current application culture
        /// </summary>
        public static CultureInfo CurrentLocale { get; private set; }

        /// <summary>
        /// Static ctor
        /// </summary>
        static ResourceManagerService()
        {
            ChangeLocale(CultureInfo.CurrentCulture.Name);
        }

        /// <summary>
        /// Getting a string resource by the specified key from the specified ResourceManager
        /// </summary>
        /// <param name="managerName">ResourceManager name</param>
        /// <param name="resourceKey">Resource name to search</param>
        /// <returns>Resource string</returns>
        public static string GetResourceString(string managerName, string resourceKey)
        {
            var resource = string.Empty;

            if (Managers.TryGetValue(managerName, out var manager))
            {
                resource = manager.GetString(resourceKey);
            }
            return resource;
        }

        /// <summary>
        /// Change current culture
        /// </summary>
        /// <param name="newLocaleName">Culture name</param>
        public static void ChangeLocale(string newLocaleName)
        {
            var newCultureInfo = new CultureInfo(newLocaleName);
            Thread.CurrentThread.CurrentCulture = newCultureInfo;
            Thread.CurrentThread.CurrentUICulture = newCultureInfo;

            CurrentLocale = newCultureInfo;

            RaiseLocaleChanged(newCultureInfo);
        }

        /// <summary>
        /// Fires the LocaleChanged event to update bindings
        /// </summary>
        public static void Refresh()
        {
            ChangeLocale(CultureInfo.CurrentCulture.IetfLanguageTag);
        }

        /// <summary>
        /// Register a resource manager without updating the interface.
        /// </summary>
        public static void RegisterManager(string managerName, ResourceManager manager)
        {
            RegisterManager(managerName, manager, false);
        }

        /// <summary>
        /// Register a new resource manager and update the interface.
        /// </summary>
        public static void RegisterManager(string managerName, ResourceManager manager, bool refresh)
        {
            Managers.TryGetValue(managerName, out var _manager);

            if (_manager == null)
            {
                Managers.Add(managerName, manager);
            }

            if (refresh)
            {
                Refresh();
            }
        }

        /// <summary>
        /// Remove resource from manager.
        /// </summary>
        public static void UnregisterManager(string name)
        {
            Managers.TryGetValue(name, out var manager);

            if (manager != null)
            {
                Managers.Remove(name);
            }
        }
    }
}
