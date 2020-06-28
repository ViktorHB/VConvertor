using System;

namespace VConvertor
{
    /// <summary>
    /// Interface for Converter that convet video files
    /// </summary>
    public interface IConverter
    {
        /// <summary>
        /// Checks if converting started
        /// </summary>
        bool IsStarted { get;  }

        /// <summary>
        /// Start converting
        /// </summary>
        void StartConvert();

        /// <summary>
        /// Stops convertation
        /// </summary>
        void Stop();

        /// <summary>
        /// Gets progress status
        /// </summary>
        /// <returns>Status</returns>
        TimeSpan GetProgress();

        /// <summary>
        /// Gets full video dutation
        /// </summary>
        /// <returns>Full video dutation</returns>
        TimeSpan GetTotalDuration();

        /// <summary>
        /// Gets file name without extentions
        /// </summary>
        /// <returns>Movie name</returns>
        string GetMovieName();
    }
}
