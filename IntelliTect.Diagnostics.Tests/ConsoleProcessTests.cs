using System.Diagnostics;
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
            Assert.True(ConsoleProcess.IsProcessAlreadyRunning("vstest.console.exe"));
        }

               [Fact]
        public void StartConsoleApplicationProcess_ValidProcessName_Success()
        {

            ConsoleProcess process = ConsoleProcess.StartConsoleProcess("xcopy.exe", "/?");


            process.WaitForOutput("This may be overridden with /-Y on the command line.");
            process.WaitForExit();

            Assert.Equal(0, process.ExitCode);
        }
    }
}