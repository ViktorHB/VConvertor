using System;
using System.Windows;

namespace VConvertor
{
    /// <summary>
    /// Class which changes application resources <see cref="ResourceDictionary"/>
    /// </summary>
    internal static class ResourcesChanger
    {
        /// <summary>
        /// Change resource 
        /// </summary>
        /// <param name="stylePath">Style file path</param>
        internal static void ChangeResource(string stylePath)
        {
            var resourceDict =
                Application.LoadComponent(new Uri(stylePath, UriKind.Relative)) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
        }
    }
}