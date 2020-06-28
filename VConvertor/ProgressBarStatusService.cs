namespace VConvertor
{
    /// <summary>
    /// Service which gets file lenghth to use in progress bar
    /// </summary>
    public class ProgressBarStatusService : IProgressBarStatusService
    {
        //fields
        private readonly string _fileInput;
        private readonly string _fileOutput;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="fileInput">File path input file</param>
        /// <param name="fileOutput">File path output file</param>
        public ProgressBarStatusService(string fileInput, string fileOutput)
        {
            _fileInput = fileInput;
            _fileOutput = fileOutput;
        }

        /// <inheritdoc />
        public long GetMaxValue() => GetFileSize(_fileInput);

        /// <inheritdoc />
        public long GetCurrentValue() => GetFileSize(_fileOutput);

        /// <summary>
        /// Gets file size
        /// </summary>
        /// <param name="filePath">File path</param>
        /// <returns>File size</returns>
        private static long GetFileSize(string filePath) => new System.IO.FileInfo(filePath).Length;
    }
}