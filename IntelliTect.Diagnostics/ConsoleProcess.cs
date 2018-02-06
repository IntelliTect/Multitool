using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace IntelliTect.Diagnostics
{
    public sealed class ConsoleProcess : Process
    {
        [Flags]
        public enum ConsoleApplicationStartOptions
        {
            DoNotStartIfAlreadyRunning = 0x1
        }


        private ConsoleProcess()
        {
        }

        private ManualResetEventSlim OutputResetEvent { get; } = new ManualResetEventSlim(false);

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing) OutputResetEvent?.Dispose();
        }


        public static bool IsProcessAlreadyRunning(string fileName)
        {
            return GetProcesses().Any(
                item => string.Equals(item.ProcessName, Path.GetFileNameWithoutExtension(fileName),
                    StringComparison.OrdinalIgnoreCase));
        }

        public static ConsoleProcess StartConsoleProcess(string fileName, string expectedOutput,
            string arguments = null,
            ConsoleApplicationStartOptions options = default,
            string workingDirectory = null)
        {
            if (options.HasFlag(ConsoleApplicationStartOptions.DoNotStartIfAlreadyRunning))
                if (!GetProcesses().Any(
                    item => string.Equals(item.ProcessName, fileName, StringComparison.OrdinalIgnoreCase)))
                    throw new ArgumentException(
                        $"The process '{fileName}' with arguments '{arguments}' is already running.");

            // The process isn't already running.
            var process = new ConsoleProcess
            {
                StartInfo = new ProcessStartInfo(fileName, arguments) {UseShellExecute = false}
            };
            if (string.IsNullOrWhiteSpace(workingDirectory) == false)
                process.StartInfo.WorkingDirectory = workingDirectory;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.OutputDataReceived += OnDataReceived;
            process.ErrorDataReceived += OnDataReceived;

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            return process;

            void OnDataReceived(object sender, DataReceivedEventArgs args)
            {
                if (string.IsNullOrWhiteSpace(args.Data)) return;

                if (args.Data.IndexOf(expectedOutput, StringComparison.OrdinalIgnoreCase) >= 0)
                    process.OutputResetEvent.Set();
            }
        }

        /// <summary>
        ///     Blocks and waits for the expected output until a timeout is reached or cancellation is requested.
        /// </summary>
        /// <param name="timeOut"></param>
        /// <param name="token"></param>
        /// <returns>True if the output is found.</returns>
        /// <remarks>Will not re-throw a <see cref="OperationCanceledException" />.</remarks>
        public bool WaitForOutput(TimeSpan timeOut, CancellationToken token)
        {
            try
            {
                return OutputResetEvent.Wait(timeOut, token);
            }
            catch (OperationCanceledException)
            {
                return false;
            }
        }

        /// <summary>
        ///     Blocks and waits for the expected output until a timeout is reached.
        /// </summary>
        /// <param name="timeOut"></param>
        /// <returns>True if the output is found.</returns>
        public bool WaitForOutput(TimeSpan timeOut)
        {
            return OutputResetEvent.Wait(timeOut);
        }


        public bool WaitForOutput(double millisecondsTimeout)
        {
            return WaitForOutput(TimeSpan.FromMilliseconds(millisecondsTimeout));
        }

        public void WaitForOutput()
        {
            WaitForOutput(-1);
        }
    }
}