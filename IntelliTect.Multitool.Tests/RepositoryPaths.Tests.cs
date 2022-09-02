using Xunit;

namespace IntelliTect.Multitool.Tests;

public class RepositoryPathsTests
{
    [Fact]
    public void DefaultRepoRoot_ReturnsRepoRootDirectory()
    {
        // Makes the assumption that the repository directory for this solution is named the same as the solution
        Assert.EndsWith(nameof(Multitool), RepositoryPaths.DefaultRepoRoot);
    }
}