using System;
using System.Security.Claims;
using System.Security.Principal;
using Xunit;

namespace IntelliTect.Multitool.Tests
{
    public class IPathsTests
    {
        [Fact]
        public void GetDefaultRepoRoot_ReturnsRepoRootDirectory()
        {
            // Makes the assumption that the repository directory for this solution is named the same as the solution
            string RepoRoot = $"{nameof(IntelliTect)}.{nameof(Multitool)}.{nameof(Tests)}";
            Assert.EndsWith($"{nameof(IntelliTect)}.{nameof(Multitool)}", IPaths.GetDefaultRepoRoot($"{nameof(IntelliTect)}.{nameof(Multitool)}.{nameof(Tests)}"));
        }
    }
}