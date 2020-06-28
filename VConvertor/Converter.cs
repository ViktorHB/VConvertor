using NReco.VideoConverter;
using System;
using System.IO;
using System.Threading.Tasks;

namespace VConvertor
{
    /// <inheritdoc />
    /// <summary>
    /// Class which converts files(video)
    /// </summary>
    public class Converter : IConverter
    {
        //fields
        private readonly string _convertInputFilePath;
        private readonly string _convertOutputFilePath;
        private readonly Formats _format;
        private readonly FFMpegConverter _convertor;
        private TimeSpan _progress;
        private TimeSpan _totalDutarion;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="convertInputFilePath">File path convert from</param>
        /// <param name="convertOutputFilePath">File path convert to</param>
        /// <param name="format">Format <see cref="Formats"/></param>
        public Converter(string convertInputFilePath, string convertOutputFilePath, Formats format)
        {
            _convertInputFilePath = convertInputFilePath;
            _convertOutputFilePath = convertOutputFilePath;
            _format = format;
            _convertor = new FFMpegConverter();
            _convertor.ConvertProgress += UpdateProgress;
        }

        /// <inheritdoc />
        public TimeSpan GetProgress() => _progress;

        /// <inheritdoc />
        public TimeSpan GetTotalDuration() => _totalDutarion;

        /// <inheritdoc />
        public string GetMovieName() => 
            Path.ChangeExtension(Path.GetFileName(_convertInputFilePath), string.Empty)
            .TrimEnd('.');

        /// <inheritdoc />
        public bool IsStarted { get; private set; } 

        /// <inheritdoc />
        public void StartConvert()
        {
            IsStarted = true;
            Task.Factory.StartNew(() =>
            {
                _convertor?.ConvertMedia(_convertInputFilePath, _convertOutputFilePath, GetFormat());
                IsStarted = false;
            });
        }

        /// <inheritdoc />
        public void Stop()
        {
            _convertor.Stop();
            IsStarted = false;
        }

        /// <summary>
        /// Update progress event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">EventArgs<see cref="ConvertProgressEventArgs"/></param>
        private void UpdateProgress(object sender, ConvertProgressEventArgs e)
        {
            if (_totalDutarion == TimeSpan.Zero)
                _totalDutarion = e.TotalDuration;

            _progress = e.Processed;
        }

        /// <summary>
        /// Get necessary format
        /// </summary>
        /// <returns>Format type<see cref="Formats"/></returns>
        private string GetFormat()
        {
            switch (_format)
            {
                case Formats.Mov:
                    return Format.mov;
                case Formats.Mp4:
                    return Format.mp4;
                case Formats.Avi:
                    return Format.avi;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
