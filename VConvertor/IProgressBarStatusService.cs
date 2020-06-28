namespace VConvertor
{
    /// <summary>
    /// Provides ProgressBar show status string
    /// </summary>
    public interface IProgressBarStatusService
    {
        /// <summary>
        /// Get max value from the file
        /// </summary>
        /// <returns>Max file size</returns>
        long GetMaxValue();

        /// <summary>
        /// Get current position
        /// </summary>
        /// <returns>Current possition</returns>
        long GetCurrentValue();
    }
}