using System;
using Microsoft.Win32;
using VConvertor.Model;

namespace VConvertor
{
    /// <summary>
    /// Wrapper which wrap instance <see cref="FileDialog"/> to generate necessary file dialog
    /// </summary>
    public static class FileDialogWrapper
    {
        /// <summary>
        /// Formats to convert
        /// </summary>
        private const string Formats = "Video files (*.avi;*.mp4;*.mov)|*.avi;*.mp4;*.mov";

        /// <summary>
        /// Gets Open/Save file dialog, depends of type
        /// </summary>
        /// <param name="typeDialog">File dialog type<see cref="FileDialogsType"/></param>
        /// <returns>Dile Dialog<see cref="FileDialog"/></returns>
        public static FileDialog GetFileFialog(FileDialogsType typeDialog)
        {
            switch (typeDialog)
            {
                case FileDialogsType.Open:
                   return new OpenFileDialog
                   {
                       Filter = Formats,
                       Multiselect = false
                   };
                case FileDialogsType.Save:
                   return new SaveFileDialog
                   {
                       Filter = Formats
                   };
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
