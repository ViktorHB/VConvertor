using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VConvertor.UnitTest
{
    /// <summary>
    /// Tests for <see cref="Converter"/>
    /// </summary>
    [TestClass]
    public class ConverterTests
    {
        private const string FilePath = @"..\..\TestData\TestMovie.mp4";
        private IConverter _converter;

        /// <summary>
        /// Test initializator
        /// </summary>
        [TestInitialize]
        public void InitializeTest()
        {
            _converter = new Converter(FilePath, FilePath, Formats.Avi);
        }

        /// <summary>
        /// Test initializator
        /// </summary>
        [TestCleanup]
        public void CleanUp()
        {
            _converter?.Stop();
        }

        /// <summary>
        /// Checks if  Stop interrupt converting 
        /// </summary>
        [TestMethod]
        public void StopTest_StartConverting()
        {
            //Arrange
            _converter.StartConvert();

            //Act
            _converter.Stop();

            //Assert
            Assert.IsFalse(_converter.IsStarted);
        }

        /// <summary>
        /// Checks if   StartConvert starts convetring
        /// </summary>
        [TestMethod]
        public void StartConvertTest_StartConverting()
        {
            //Arrange
            //Act
            _converter.StartConvert();

            //Assert
            Assert.IsTrue(_converter.IsStarted);
        }

        /// <summary>
        /// Checks if  GetMovieName returns filename without extensions 
        /// </summary>
        [TestMethod]
        public void IsStarted_ReturnFalse()
        {
            //Arrange
            //Act
            //Assert
            Assert.IsFalse(_converter.IsStarted);
        }

        /// <summary>
        /// Checks if  GetMovieName returns filename without extensions 
        /// </summary>
        [TestMethod]
        public void GetMovieName_ReturnsFilename()
        {
            //Arrange
            const string expectedName = "TestMovie";

            //Act
            var resName = _converter.GetMovieName();

            //Assert
            Assert.AreEqual(expectedName,resName);
        }
    }
}
