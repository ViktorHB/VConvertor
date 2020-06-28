using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using System;
using VConvertor.Model;

namespace VConvertor.UnitTest
{
    /// <summary>
    /// Tests for <see cref="FileDialogWrapper"/>
    /// </summary>
    [TestClass]
    public class FileDialogWrapperTests
    {
        /// <summary>
        /// Checks if GetFileFialogTests creates necessary file dialogs
        /// </summary>
        /// <param name="dialogType">File dialog type</param>
        /// <param name="expectedRes">Expected dialog</param>
        [TestMethod]
        [DataRow(FileDialogsType.Open, typeof(OpenFileDialog))]
        [DataRow(FileDialogsType.Save, typeof(SaveFileDialog))]
        public void GetFileFialogTests(FileDialogsType dialogType, Type expectedRes )
        {
            //Act
            var result = FileDialogWrapper.GetFileFialog(dialogType);

            //Assert
            Assert.AreEqual(expectedRes, result.GetType());
        }
    }
}
