using System;
using System.Diagnostics;
using System.Threading;
using Xunit;

namespace IntelliTect.Diagnostics.Tests
{
    public class ConsoleProcessTests
    {
        [Fact]

        public void IsProcessIsProcessAlreadyRunning_NotRunning_False()

        {
            Assert.False(ConsoleProcess.IsProcessAlreadyRunning("notavalidprocess.exe"));
        }

        [Fact]
        public void IsProcessIsProcessAlreadyRunning_Xunit_False()
        {
            Assert.True(ConsoleProcess.IsProcessAlreadyRunning(Process.GetCurrentProcess().ProcessName));
        }

        [Fact]
        public void StartConsoleProcess_ValidProcessName_Success()
        {
            var process = ConsoleProcess.StartConsoleProcess("xcopy.exe",
                "This may be overridden with /-Y on the command line.", "/?");


            process.WaitForOutput();
            process.WaitForExit();

            Assert.Equal(0, process.ExitCode);
        }

        [Fact]
        public void StartConsoleProcess_WaitsUntilCancellation()
        {
            var process = ConsoleProcess.StartConsoleProcess("xcopy.exe", "higgledy piggledy", "/?");

            using (var cts = new CancellationTokenSource())
            {
                cts.CancelAfter(TimeSpan.FromMilliseconds(100));

                Assert.False(process.WaitForOutput(TimeSpan.FromMilliseconds(-1), cts.Token));
            }
        }

        [Fact]
        public void StartConsoleProcess_WaitsUntilTimeout()
        {
            var process = ConsoleProcess.StartConsoleProcess("xcopy.exe", "higgledy piggledy", "/?");

            Assert.False(process.WaitForOutput(1));
        }
    }
}