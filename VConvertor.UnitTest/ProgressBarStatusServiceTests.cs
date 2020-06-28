using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VConvertor.UnitTest
{
    /// <summary>
    /// Tests for <see cref="ProgressBarStatusService"/>
    /// </summary>
    [TestClass]
    public class ProgressBarStatusServiceTests
    {
        private const string FilePath = @"..\..\TestData\TestMovie.mp4";
        private IProgressBarStatusService _progressBarStatusService;

        /// <summary>
        /// Test initializator
        /// </summary>
        [TestInitialize]
        public void InitializeTest()
        {
            _progressBarStatusService = new ProgressBarStatusService(FilePath, FilePath);
        }

        /// <summary>
        /// Checks if  GetMaxValue gets file size
        /// </summary>
        [TestMethod]
        public void GetMaxValueTest_ReturnsFileSize()
        {
            //Arrange
            const int exprctedValue = 2074718;

            //Act
            var resultValue = _progressBarStatusService.GetMaxValue();

            //Assert
            Assert.AreEqual(exprctedValue, resultValue);
        }

        /// <summary>
        /// Checks if  GetCurrentValue gets current file size
        /// </summary>
        [TestMethod]
        public void GetCurrentValueTest_ReturnsFileSize()
        {
            //Arrange
            const int exprctedValue = 2074718;

            //Act
            var resultValue = _progressBarStatusService.GetCurrentValue();

            //Assert
            Assert.AreEqual(exprctedValue, resultValue);
        }
    }
}
