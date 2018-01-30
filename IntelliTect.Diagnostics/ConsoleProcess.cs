using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace IntelliTect.Diagnostics
{
    public class ConsoleProcess : Process
    {
        [Flags]
        public enum ConsoleApplicationStartOptions
        {
            DoNotStartIfAlreadyRunning = 0x1
        }

        protected ConsoleProcess()
        {
            
        }

        /*public static string GetCommandLine(this Process process)
        {
            var commandLine = new StringBuilder(process.MainModule.FileName);

            commandLine.Append(" ");
            using (var searcher =
                new ManagementObjectSearcher("SELECT CommandLine FROM Win32_Process WHERE ProcessId = " + process.Id))
            {
                foreach (var @object in searcher.Get())
                {
                    commandLine.Append(@object["CommandLine"]);
                    commandLine.Append(" ");
                }
            }

            return commandLine.ToString();
        }*/

        public static bool IsProcessAlreadyRunning(string fileName)
        {
            return Process.GetProcesses().Any(
                item => string.Equals(item.ProcessName, Path.GetFileNameWithoutExtension(fileName),
                    StringComparison.OrdinalIgnoreCase));
        }

        public static ConsoleProcess StartConsoleProcess( string fileName, string arguments = null, ConsoleApplicationStartOptions options = default,
            string workingDirectory = null)
        {
            var cliResetEvent = new ManualResetEventSlim();
            if (options.HasFlag(ConsoleApplicationStartOptions.DoNotStartIfAlreadyRunning))
                if (!Process.GetProcesses().Any(
                    item => string.Equals(item.ProcessName, fileName, StringComparison.OrdinalIgnoreCase)))
                    throw new ArgumentException(
                        $"The process '{fileName}' with arguments '{arguments}' is already running.");
            // The process isn't already running.
            var process = new ConsoleProcess
            {
                StartInfo = new ProcessStartInfo(fileName, arguments) {UseShellExecute = false}
            };
            if (string.IsNullOrWhiteSpace(workingDirectory) == false)
            {
                process.StartInfo.WorkingDirectory = workingDirectory;
            }
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;

            process.OutputDataReceived += OnStandardOutput;
            process.ErrorDataReceived += OnStandardError;


            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            cliResetEvent.Wait(10000);

            return process;
        }

        public void WaitForOutput(string text)
        {

        }

        private  void OnStandardError(object sender, DataReceivedEventArgs data)
        {
        }

        private  void OnStandardOutput(object sender, DataReceivedEventArgs data)
        {
        }
    }
}