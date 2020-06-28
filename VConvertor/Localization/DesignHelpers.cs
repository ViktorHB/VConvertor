using System.ComponentModel;
using System.Windows;

namespace VConvertor.Localization
{
    /// <summary>
    /// Class which checs design mode
    /// </summary>
    public static class DesignHelpers
    {
        /// <summary>
        /// Checking whether applications are in design mode
        /// </summary>
        public static bool IsInDesignMode =>
            (bool)(DesignerProperties.IsInDesignModeProperty
                .GetMetadata(typeof(DependencyObject))
                .DefaultValue);
    }
}
