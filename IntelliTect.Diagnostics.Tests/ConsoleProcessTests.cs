using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using Xunit;
using static IntelliTect.Diagnostics.ConsoleProcess;

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
            using (var process = ConsoleProcess.StartConsoleProcess("xcopy.exe", "/?",
                "This may be overridden with /-Y on the command line."))
            {
                process.WaitForOutput();
                process.WaitForExit();

                Assert.Equal(0, process.ExitCode);
            }
        }

        [Fact]
        public void StartConsoleProcess_IsRunningDoNotStart_FailsToStart()
        {
            Assert.Throws<ArgumentException>(()=>
                ConsoleProcess.StartConsoleProcess(Process.GetCurrentProcess().ProcessName, options: ConsoleApplicationStartOptions.DoNotStartIfAlreadyRunning)
                );
        }

        [Fact]
        public void StartConsoleProcess_ReadStandardOut_Fails()
        {
            // TODO: You cannot configure process to monitor events and to still read StandadOutput.  We 
            // Need to switch to use Standard Output.
            ConsoleProcess process = ConsoleProcess.StartConsoleProcess("cmd.exe", "/?");
            string data = process.StandardOutput.ReadToEnd();
            Assert.Contains("The special characters that require quotes are", data);
        }

        [Fact]
        public void StartConsoleProcess_SpecifyDifferentDirectory_Success()
        {
            string expected = new DirectoryInfo(@"..\").FullName.TrimEnd(Path.DirectorySeparatorChar);
            using (var process = ConsoleProcess.StartConsoleProcess("cmd.exe", "/C cd", expected, @"..\"))
            {
                Assert.True(process.WaitForOutput(2000));

                process.WaitForExit();
                
                Assert.Equal(0, process.ExitCode);
            }
        }

        [Fact]
        public void StartConsoleProcess_CaptureOutput_Success()
        {
                /// NOTES:
                /// 1. If the caller reads StdOutput or StdError an exception will be thrown but these streams are available on ConsoleProcess
                /// 2. We could expect the caller to subscribe to OutputDataReceived and ErrorDataReceived
                /// 3. We could save the output (into a stream or a string?) and provide that to the caller instead
                /// 4. Should we wrap process or derive from it to avoid 1. (and likely others)
        }

        [Fact]
        public void StartConsoleProess_TryCaptureImmediteOutput_Success()
        {
            using (ConsoleProcess process = ConsoleProcess.StartConsoleProcess(
                "cmd.exe", "/?", "Starts a new instance of the Windows command interpreter"))
            {
                Assert.False(process.WaitForOutput(5000),
                    "The output should have appeared but it didn't because there is an instant (currently simulated with Task.Delay()) between the process starting and the call to BeginOutputReadLine");
            }
        }


        [Fact]
        public void StartConsoleProcess_WaitsUntilCancellation()
        {
            using (var process = ConsoleProcess.StartConsoleProcess("xcopy.exe", "/?", "higgledy piggledy"))
            {
                using (var cts = new CancellationTokenSource())
                {
                    cts.CancelAfter(TimeSpan.FromMilliseconds(100));

                    Assert.False(process.WaitForOutput(TimeSpan.FromMilliseconds(-1), cts.Token));
                }
            }
        }

        [Fact]
        public void StartConsoleProcess_WaitsUntilTimeout()
        {
            using (var process = ConsoleProcess.StartConsoleProcess("xcopy.exe", "/?", "higgledy piggledy"))
            {
                Assert.False(process.WaitForOutput(1));
            }
        }
    }
}